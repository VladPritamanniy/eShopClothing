using System.Text;
using System.Text.Json;
using Application.DTO;
using Application.Interfaces;
using Core.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Application.Services
{
    public sealed class EmailNotificationService : BackgroundService, IEmailNotificationService
    {
        private readonly AuthMessageSenderOptions _authOptions;
        private readonly RabbitMqOptions _mqOptions;
        private readonly ILogger<EmailNotificationService> _logger;
        private IConnection? _rabbitMqConnection;
        private IModel? _consumerChannel;
        private readonly ConnectionFactory _connectionFactory;

        public EmailNotificationService(IOptions<AuthMessageSenderOptions> authOptions, IOptions<RabbitMqOptions> mqOptions, ILogger<EmailNotificationService> logger)
        {
            _authOptions = authOptions.Value;
            _mqOptions = mqOptions.Value;
            _logger = logger;
            _connectionFactory = new ConnectionFactory
            {
                HostName = _mqOptions.HostName,
                UserName = _mqOptions.UserName,
                Password = _mqOptions.Password,
                VirtualHost = _mqOptions.VirtualHost,
                DispatchConsumersAsync = true
            };
        }

        public Task PublishAsync(List<EmailMessageDto> message)
        {
            try
            {
                if (_rabbitMqConnection == null || !_rabbitMqConnection.IsOpen)
                {
                    throw new InvalidOperationException("RabbitMQ connection is not open");
                }

                using var channel = _rabbitMqConnection.CreateModel();

                channel.ExchangeDeclare(exchange: _mqOptions.ExchangeName, type: ExchangeType.Direct);
                var body = JsonSerializer.SerializeToUtf8Bytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: _mqOptions.ExchangeName,
                    routingKey: _mqOptions.RoutingKeyName,
                    basicProperties: properties,
                    body: body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing message");
            }
            
            return Task.CompletedTask;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ = Task.Factory.StartNew(() =>
            {
                try
                {
                    stoppingToken.ThrowIfCancellationRequested();

                    _rabbitMqConnection = _connectionFactory.CreateConnection();

                    if (!_rabbitMqConnection.IsOpen)
                    {
                        throw new InvalidOperationException("Failed to open RabbitMQ connection");
                    }

                    _consumerChannel = _rabbitMqConnection.CreateModel();
                    _consumerChannel.CallbackException += (sender, eventArgs) =>
                    {
                        _logger.LogWarning(eventArgs.Exception, "Error with RabbitMQ consumer channel");
                    };

                    _consumerChannel.ExchangeDeclare(exchange: _mqOptions.ExchangeName, type: ExchangeType.Direct);

                    _consumerChannel.QueueDeclare(queue: _mqOptions.QueueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    _consumerChannel.QueueBind(queue: _mqOptions.QueueName,
                        exchange: _mqOptions.ExchangeName,
                        routingKey: _mqOptions.RoutingKeyName);

                    var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
                    consumer.Received += OnMessageReceived;

                    _consumerChannel.BasicConsume(queue: _mqOptions.QueueName,
                        autoAck: false,
                        consumer: consumer);

                    stoppingToken.Register(() =>
                    {
                        _consumerChannel?.Close();
                        _rabbitMqConnection?.Close();
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error starting RabbitMQ connection");
                }
            },
            TaskCreationOptions.LongRunning);

            return Task.CompletedTask;
        }

        private async Task OnMessageReceived(object? sender, BasicDeliverEventArgs eventArgs)
        {
            using var client = new SmtpClient();
            try
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var emailMessageList = JsonSerializer.Deserialize<IReadOnlyList<EmailMessageDto>>(message);

                await client.ConnectAsync(_authOptions.SmtpHost, _authOptions.SmtpPort, SecureSocketOptions.StartTls);
                if (!_authOptions.UseDefaultCredentials)
                {
                    await client.AuthenticateAsync(_authOptions.FromEmail, _authOptions.FromEmailPassword);
                }

                using var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(_authOptions.FromDisplayName, _authOptions.FromEmail));
                mimeMessage.Subject = emailMessageList!.First().Subject;
                mimeMessage.Body = new TextPart("html") { Text = emailMessageList!.First().Body };

                _logger.LogInformation("Start sending email.");


                foreach (var emailMessage in emailMessageList!)
                {
                    mimeMessage.To.Add(MailboxAddress.Parse(emailMessage.Email));
                    await client.SendAsync(mimeMessage).ConfigureAwait(false);
                    mimeMessage.To.Clear();
                }

                _logger.LogInformation("Send letters is succeed");

                _consumerChannel?.BasicAck(eventArgs.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing received message");
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }

        public override void Dispose()
        {
            _consumerChannel?.Dispose();
            _rabbitMqConnection?.Dispose();
            base.Dispose();
        }
    }
}
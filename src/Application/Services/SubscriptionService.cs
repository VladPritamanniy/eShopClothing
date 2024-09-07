using Application.DTO;
using Application.Interfaces;
using Core.Entities;
using Core.Exceptions.Subscription;
using Core.Repositories.Base;
using Core.Specifications;
using System.Text.Encodings.Web;

namespace Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IRepository<Subscription> _subscriptionRepository;
        private IEmailNotificationService _notificationService;

        public SubscriptionService(IRepository<Subscription> subscriptionRepository, IEmailNotificationService notificationService)
        {
            _subscriptionRepository = subscriptionRepository;
            _notificationService = notificationService;
        }

        public async Task Subscribe(string? userId, string sellerId)
        {
            if (userId == null)
                throw new SubscriptionNotAuthorizedExeption();

            var entity = new Subscription
            {
                UserId = userId,
                SellerId = sellerId,
                SubscribedDate = DateTime.UtcNow
            };
            await _subscriptionRepository.AddAsync(entity);
        }

        public async Task<bool> CheckSubscriptionIfExist(string? userId, string sellerId)
        {
            if (userId == null)
                return false;

            var specification = new SubscriptionSpecification(userId, sellerId);
            var result = await _subscriptionRepository.AnyAsync(specification);
            return result;
        }

        public async Task NotifySubscribers(ClothingDto product)
        {
            if (product == null || product.Id <= 0)
            {
                throw new ArgumentNullException();
            }

            var subsEmail = await _subscriptionRepository.GetAllUserSubscribersByUserId(product.ApplicationUserId);

            if (!subsEmail.Any())
            {
                return;
            }

            var messageList = new List<EmailMessageDto>();

            foreach (var subEmail in subsEmail)
            {
                messageList.Add(new EmailMessageDto
                {
                    Email = subEmail,
                    Subject = "The seller you are subscribed to added a new product",
                    Body = $"You can <a href='{HtmlEncoder.Default.Encode("https://localhost:7188/product/" + product.Id)}'>check new product</a>."
                });
            }

            await _notificationService.PublishAsync(messageList).ConfigureAwait(false);
        }
    }
}

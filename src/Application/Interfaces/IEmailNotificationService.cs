using Application.DTO;

namespace Application.Interfaces
{
    public interface IEmailNotificationService
    {
        Task PublishAsync(List<EmailMessageDto> message);
    }
}

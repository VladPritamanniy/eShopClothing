using Application.DTO;

namespace Application.Interfaces
{
    public interface ISubscriptionService
    {
        Task Subscribe(string? userId, string sellerId);
        Task<bool> CheckSubscriptionIfExist(string? userId, string sellerId);
        Task NotifySubscribers(ClothingDto product);
    }
}

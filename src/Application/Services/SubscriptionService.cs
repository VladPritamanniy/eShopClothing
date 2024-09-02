using Application.Interfaces;
using Core.Entities;
using Core.Exceptions.Subscription;
using Core.Repositories.Base;
using Core.Specifications;

namespace Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IRepository<Subscription> _subscriptionRepository;

        public SubscriptionService(IRepository<Subscription> subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
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
    }
}

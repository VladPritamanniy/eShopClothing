namespace Application.Interfaces
{
    public interface IPermissionService
    {
        Task IsProductOwner(int productId, string userId);
        Task<int> IsBasketItemOwner(int basketItemId, string userName);
    }
}

namespace Web.Interfaces
{
    public interface IChangeProductPricePageService
    {
        Task<decimal?> GetProductPrice(int productId, string userId);
        Task ChangePrice(int productId, decimal productPrice, string userId);
    }
}

namespace Web.Interfaces
{
    public interface IChangeProductPricePageService
    {
        Task<int?> GetProductPrice(int productId, string userId);
        Task ChangePrice(int productId, int productPrice, string userId);
    }
}

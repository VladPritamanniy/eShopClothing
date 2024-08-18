namespace Application.Interfaces
{
    public interface IPermissionService
    {
        Task IsProductOwner(int productId, string userId);
    }
}

using Application.DTO;

namespace Application.Interfaces
{
    public interface IElasticService
    {
        Task CreateIndex();
        Task IndexDocument(ClothingDto clothingDto);
        Task<string> LiveSearch(string searchQuery);
        Task<string> SearchByQuery(string query);
    }
}

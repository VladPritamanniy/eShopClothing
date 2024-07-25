using Application.DTO;

namespace Application.Interfaces
{
    public interface ITypeService
    {
        Task<IEnumerable<TypeDto>> GetAllTypesClothing();
    }
}

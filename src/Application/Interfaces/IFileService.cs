using Application.DTO;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IFileService
    {
        Task<ClothingCreateDto> UploadFiles(ClothingCreateDto clothingDto, List<IFormFile> files);
    }
}

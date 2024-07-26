using Application.DTO;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IFileService
    {
        Task<ClothingDto> UploadFiles(ClothingDto clothingDto, List<IFormFile> files);
    }
}

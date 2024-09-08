using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IClamAVService
    {
        Task AVCheck(List<IFormFile> files);
    }
}

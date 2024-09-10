using CloudinaryDotNet.Actions;

namespace Application.Interfaces
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadAsync(Stream photoStream);
    }
}

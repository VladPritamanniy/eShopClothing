using Application.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Options;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinaryOptions cloudinaryOptions;

        public CloudinaryService(IOptions<CloudinaryOptions> cloudinaryOptions)
        {
            this.cloudinaryOptions = cloudinaryOptions.Value;
            var account = new Account(this.cloudinaryOptions.CloudName, this.cloudinaryOptions.ApiKey, this.cloudinaryOptions.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> UploadAsync(Stream photoStream)
        {
            var fileName = Guid.NewGuid().ToString();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, photoStream),
                PublicId = fileName,
                Overwrite = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult;
        }
    }
}

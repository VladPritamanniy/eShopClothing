using Application.DTO;
using Application.Interfaces;
using Core.Exceptions.File;
using Core.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class FileService : IFileService
    {
        private readonly ImageOptions imageOptions;
        private readonly IClamAVService _clamAVService;
        private readonly ICloudinaryService _cloudinaryService;

        public FileService(IOptions<ImageOptions> imageOptions, IClamAVService clamAVService, ICloudinaryService cloudinaryService)
        {
            this.imageOptions = imageOptions.Value;
            _clamAVService = clamAVService;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ClothingCreateDto> UploadFiles(ClothingCreateDto clothingDto, List<IFormFile> files)
        {
            await _clamAVService.AVCheck(files);

            foreach (var file in files)
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);

                if (memoryStream.Length > imageOptions.MaxImageSizeInBytes)
                {
                    throw new FileSizeException($"File size {file.FileName} is more then {imageOptions.MaxImageSizeInBytes} bytes.");
                }

                memoryStream.Position = 0;
                var imgUploadResult = await _cloudinaryService.UploadAsync(memoryStream);
                var image = new ImageDto()
                {
                    PublicId = imgUploadResult.PublicId
                };
                clothingDto.Images.Add(image);
            }

            return clothingDto;
        }
    }
}

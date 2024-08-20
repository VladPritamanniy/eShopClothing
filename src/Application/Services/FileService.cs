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
        private static readonly Dictionary<string, List<byte[]>> _fileSignature =
            new()
            {
                { ".jpg", new List<byte[]>
                    {
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xEE }
                    }
                },
                { ".jpeg", new List<byte[]>
                    {
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xEE }
                    }
                },
                { ".png", new List<byte[]>
                    {
                        new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
                    }
                },
            };

        public ImageOptions Options { get; set; }

        public FileService(IOptions<ImageOptions> options)
        {
            Options = options.Value;
        }

        public async Task<ClothingCreateDto> UploadFiles(ClothingCreateDto clothingDto, List<IFormFile> files)
        {
            foreach (var file in files)
            {
                using var memoryStream = new MemoryStream();

                await file.CopyToAsync(memoryStream);
                if (memoryStream.Length > Options.MaxImageSizeInBytes)
                {
                    throw new FileSizeException($"File size {file.FileName} is more then {Options.MaxImageSizeInBytes} bytes.");
                }

                CheckingFileFormatAndSignature(file.FileName, memoryStream);
                var image = new ImageDto()
                {
                    Value = memoryStream.ToArray()
                };
                clothingDto.Images.Add(image);
            }
            return clothingDto;
        }

        private void CheckingFileFormatAndSignature(string fileName, Stream data)
        {
            if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0)
            {
                throw new ArgumentNullException("CheckingImagesSignatures -> file name is null.");
            }

            var ext = Path.GetExtension(fileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext))
            {
                throw new FileFormatException($"An error while getting format this file - {ext}.");
            }

            data.Position = 0;

            using var reader = new BinaryReader(data);

            if (!_fileSignature.ContainsKey(ext))
            {
                throw new FileFormatException($"Unsupported file format - {ext}.");
            }

            var signatures = _fileSignature[ext];
            var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

            bool isCorrectSignatures = signatures.Any(signature =>
                headerBytes.Take(signature.Length).SequenceEqual(signature));
            if (!isCorrectSignatures)
            {
                throw new FileSignatureException($"File has incorrect signature - {fileName}.");
            }
        }
    }
}

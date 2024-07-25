using Application.Interfaces;
using AutoMapper;
using Core.Exceptions.File;
using Core.Options;
using Microsoft.Extensions.Options;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class ProductPageService : IProductPageService
    {
        private IMapper _mapper;
        private readonly ISizeService _sizeService;
        private readonly ITypeService _typeService;
        private readonly IClothingService _clothingService;
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

        public ProductPageService(IMapper mapper, ISizeService sizeService, ITypeService typeService, IOptions<ImageOptions> options, IClothingService clothingService)
        {
            _mapper = mapper;
            _sizeService = sizeService;
            _typeService = typeService;
            Options = options.Value;
            _clothingService = clothingService;
        }

        public ImageOptions Options { get; set; }

        public async Task CreateProduct(CreateClothingViewModel product)
        {
            var productWithImages = await UploadFiles(product);
        }

        public async Task<IEnumerable<SizeViewModel>> GetAllSizesClothing()
        {
            var sizeList = await _sizeService.GetAllSizesClothing();
            var mapped = _mapper.Map<IEnumerable<SizeViewModel>>(sizeList);
            return mapped;
        }

        public async Task<IEnumerable<TypeViewModel>> GetAllTypesClothing()
        {
            var typeList = await _typeService.GetAllTypesClothing();
            var mapped = _mapper.Map<IEnumerable<TypeViewModel>>(typeList);
            return mapped;
        }

        private async Task<CreateClothingViewModel> UploadFiles(CreateClothingViewModel product)
        {
            foreach (var file in product.FormFiles)
            {
                using var memoryStream = new MemoryStream();

                await file.CopyToAsync(memoryStream);
                if (memoryStream.Length > Options.MaxImageSizeInBytes)
                {
                    throw new FileSizeException($"File size {file.FileName} is more then {Options.MaxImageSizeInBytes} bytes.");
                }

                await CheckingFilesSignatures(file.FileName, memoryStream);
                var image = new ImageViewModel
                {
                    Value = memoryStream.ToArray()
                };
                product.Images.Add(image);
            }
            return product;
        }

        private async Task CheckingFilesSignatures(string fileName, Stream data)
        {
            if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0)
            {
                throw new ArgumentNullException("CheckingImagesSignatures -> fileName is null.");
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

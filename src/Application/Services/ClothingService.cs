using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Core.Repositories;
using Core.Entities;

namespace Application.Services
{
    public class ClothingService : IClothingService
    {
        private readonly IClothingRepository _productRepository;
        private readonly IMapper _mapper;

        public ClothingService(IClothingRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClothingDto>> GetAllUserProductByUserId(string id)
        {
            var list = await _productRepository.GetAllUserProductByUserId(id);
            var mapped = _mapper.Map<IEnumerable<ClothingDto>>(list);
            return mapped;
        }

        public async Task CreateClothing(ClothingDto clothing)
        {
            var mapped = _mapper.Map<Clothing>(clothing);
            await _productRepository.Add(mapped);
        }
    }
}

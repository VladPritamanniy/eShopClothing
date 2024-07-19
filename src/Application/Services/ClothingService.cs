using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Core.Repositories;
using Core.Specification;

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

        public async Task<IEnumerable<ClothingDto>> GetAllUserProductByUserIdListAsync(string id)
        {
            var spec = new GetClothingListByUserIdSpecification(id);
            var list = await _productRepository.GetById(id, spec);
            var mapped = _mapper.Map<IEnumerable<ClothingDto>>(list);
            return mapped;
        }
    }
}

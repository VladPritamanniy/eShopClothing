using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Repositories.Base;
using Core.Specifications;

namespace Application.Services
{
    public class ClothingService : IClothingService
    {
        private readonly IRepository<Clothing> _clothingRepository;
        private readonly IMapper _mapper;

        public ClothingService(IRepository<Clothing> clothingRepository, IMapper mapper)
        {
            _clothingRepository = clothingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClothingDto>> GetAllUserProductByUserId(string id)
        {
            var specification = new ClothingIncludeByUserIdSpecification(id);
            var entities = await _clothingRepository.Get(specification);
            var mapped = _mapper.Map<IEnumerable<ClothingDto>>(entities);
            return mapped;
        }

        public async Task CreateClothing(ClothingDto clothing)
        {
            var mapped = _mapper.Map<Clothing>(clothing);
            await _clothingRepository.AddAsync(mapped);
        }

        public async Task<int?> GetClothingPriceById(int id)
        {
            var specification = new ClothingByIdSpecification(id);
            var entity = await _clothingRepository.FirstOrDefaultAsync(specification);
            if (entity == null)
            {
                throw new ArgumentNullException($"{entity}", $"Cannon get entity by id = {id}.");
            }
            return entity.OldPrice;
        }

        public async Task ChangePriceById(int id, int price)
        {
            var specification = new ClothingByIdSpecification(id);
            var entity = await _clothingRepository.FirstOrDefaultAsync(specification);
            if (entity == null)
            {
                throw new ArgumentNullException($"{entity}", $"Cannon get entity by id = {id}.");
            }

            entity.ValidPrice = price;
            await _clothingRepository.SaveChangesAsync();
        }
    }
}

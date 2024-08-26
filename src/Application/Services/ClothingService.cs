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

        public async Task<IEnumerable<ClothingAccountDto>> GetAllUserProductByUserId(string id)
        {
            var specification = new ClothingIncludeByUserIdSpecification(id);
            var entities = await _clothingRepository.ToListAsync(specification);
            var mapped = _mapper.Map<IEnumerable<ClothingAccountDto>>(entities);
            return mapped;
        }

        public async Task CreateClothing(ClothingCreateDto clothing)
        {
            var mapped = _mapper.Map<Clothing>(clothing);
            await _clothingRepository.AddAsync(mapped);
        }

        public async Task<ClothingDto> GetById(int id)
        {
            var specification = new ClothingByIdSpecification(id);
            var entity = await _clothingRepository.FirstOrDefaultAsync(specification);

            if (entity == null)
                throw new ArgumentNullException($"{entity}", $"Cannon get entity by id = {id}.");

            var mapped = _mapper.Map<ClothingDto>(entity);
            return mapped;
        }

        public async Task ChangePriceById(int id, decimal price)
        {
            var specification = new ClothingByIdSpecification(id);
            var entity = await _clothingRepository.FirstOrDefaultAsync(specification);

            if (entity == null)
                throw new ArgumentNullException($"{entity}", $"Cannon get entity by id = {id}.");
            
            entity.ValidPrice = price;
            await _clothingRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClothingItemPageDto>> GetAllWithPagination(ClothingPaginationFilterDto paginationFilterDto)
        {
            var specification = new ClothingFilterPaginatedSpecification(
                paginationFilterDto.PageNum,
                paginationFilterDto.PageSize,
                paginationFilterDto.TypeId,
                paginationFilterDto.SizeId);

            var entities = await _clothingRepository.ToListAsync(specification);
            var mapped = _mapper.Map<IEnumerable<ClothingItemPageDto>>(entities);
            return mapped;
        }

        public async Task<int> GetCountFilteredProducts(int? typeId, int? sizeId)
        {
            var specification = new ClothingFilterSpecification(typeId, sizeId);
            return await _clothingRepository.CountAsync(specification);
        }
    }
}

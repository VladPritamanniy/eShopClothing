using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Core.Repositories.Base;
using Type = Core.Entities.Type;

namespace Application.Services
{
    public class TypeService : ITypeService
    {
        private readonly IReadRepository<Type> _typeRepository;
        private readonly IMapper _mapper;

        public TypeService(IReadRepository<Type> typeRepository, IMapper mapper)
        {
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesClothing()
        {
            var entities = await _typeRepository.GetAll();
            var mapped = _mapper.Map<IEnumerable<TypeDto>>(entities);
            return mapped;
        }
    }
}

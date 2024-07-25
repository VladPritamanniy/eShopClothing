using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Core.Repositories;

namespace Application.Services
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepository _typeRepository;
        private readonly IMapper _mapper;

        public TypeService(ITypeRepository typeRepository, IMapper mapper)
        {
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesClothing()
        {
            var list = await _typeRepository.GetAll();
            var mapped = _mapper.Map<IEnumerable<TypeDto>>(list);
            return mapped;
        }
    }
}

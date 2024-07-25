using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Core.Repositories;

namespace Application.Services
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;
        public SizeService(ISizeRepository sizeRepository, IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SizeDto>> GetAllSizesClothing()
        {
            var list = await _sizeRepository.GetAll();
            var mapped = _mapper.Map<IEnumerable<SizeDto>>(list);
            return mapped;
        }
    }
}

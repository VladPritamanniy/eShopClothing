using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Core.Repositories.Base;

namespace Application.Services
{
    public class SizeService : ISizeService
    {
        private readonly IRepository<Size> _sizeRepository;
        private readonly IMapper _mapper;
        public SizeService(IRepository<Size> sizeRepository, IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SizeDto>> GetAllSizesClothing()
        {
            var entities = await _sizeRepository.GetAll();
            var mapped = _mapper.Map<IEnumerable<SizeDto>>(entities);
            return mapped;
        }
    }
}

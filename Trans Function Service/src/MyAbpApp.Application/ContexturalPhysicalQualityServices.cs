using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using MyAbpApp.ContexturalPhysicalQualities;
using MyAbpApp.ContexturalPhysicalQualityDtos;
using MyAbpApp.IContexturalPhysicalQualityServices;
namespace MyAbpApp.ContexturalPhysicalQualityServices
{
    public class ContexturalPhysicalQualityService : ApplicationService, IContexturalPhysicalQualityService
    {
        private readonly IRepository<ContexturalPhysicalQuality, Guid> _contexturalPhysicalQualityRepository;

        public ContexturalPhysicalQualityService(IRepository<ContexturalPhysicalQuality, Guid> contexturalPhysicalQualityRepository)
        {
            _contexturalPhysicalQualityRepository = contexturalPhysicalQualityRepository;
        }

        ~ContexturalPhysicalQualityService()
        { }
        public async Task<ContexturalPhysicalQualityDto> CreateAsync(CreateOrUpdateContexturalPhysicalQualityDto input)
        {
            var contexturalPhysicalQuality = new ContexturalPhysicalQuality
            {
                Process = input.Process,
                DeviceId = input.DeviceId
            };

            await _contexturalPhysicalQualityRepository.InsertAsync(contexturalPhysicalQuality);

            return ObjectMapper.Map<ContexturalPhysicalQuality, ContexturalPhysicalQualityDto>(contexturalPhysicalQuality);
        }

        public async Task<ContexturalPhysicalQualityDto> GetAsync(Guid id)
        {
            var contexturalPhysicalQuality = await _contexturalPhysicalQualityRepository.GetAsync(id);
            return ObjectMapper.Map<ContexturalPhysicalQuality, ContexturalPhysicalQualityDto>(contexturalPhysicalQuality);
        }

        public async Task<ContexturalPhysicalQualityDto> UpdateAsync(Guid id, CreateOrUpdateContexturalPhysicalQualityDto input)
        {
            var contexturalPhysicalQuality = await _contexturalPhysicalQualityRepository.GetAsync(id);

            contexturalPhysicalQuality.Process = input.Process;

            await _contexturalPhysicalQualityRepository.UpdateAsync(contexturalPhysicalQuality);

            return ObjectMapper.Map<ContexturalPhysicalQuality, ContexturalPhysicalQualityDto>(contexturalPhysicalQuality);
        }

        public async Task DeleteAsync(Guid id)
        {
            var contexturalPhysicalQuality = await _contexturalPhysicalQualityRepository.GetAsync(id);
            await _contexturalPhysicalQualityRepository.DeleteAsync(contexturalPhysicalQuality);
        }
    }

}

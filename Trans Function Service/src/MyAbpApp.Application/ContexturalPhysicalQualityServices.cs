using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.UI;
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
            var existingEntity = await _contexturalPhysicalQualityRepository
                .FirstOrDefaultAsync(x => x.SensorId == input.SensorId);

            if (existingEntity != null)
            {
                throw new ArgumentException("The SensorId already exists in the database.");
            }

            // 如果不存在，則插入新的資料
            var contexturalPhysicalQuality = new ContexturalPhysicalQuality
            {
                Process = input.Process,
                SensorId = input.SensorId
            };

            await _contexturalPhysicalQualityRepository.InsertAsync(contexturalPhysicalQuality);

            // 返回 DTO
            return ObjectMapper.Map<ContexturalPhysicalQuality, ContexturalPhysicalQualityDto>(contexturalPhysicalQuality);
        }

        public async Task<ContexturalPhysicalQualityDto> GetAsync(Guid id)
        {
            var contexturalPhysicalQuality = await _contexturalPhysicalQualityRepository.GetAsync(id);
            return ObjectMapper.Map<ContexturalPhysicalQuality, ContexturalPhysicalQualityDto>(contexturalPhysicalQuality);
        }
        public async Task<List<ContexturalPhysicalQualityDto>> GetAllAsync()
        {
            var contexturalPhysicalQualities = await _contexturalPhysicalQualityRepository.GetListAsync();
            return ObjectMapper.Map<List<ContexturalPhysicalQuality>, List<ContexturalPhysicalQualityDto>>(contexturalPhysicalQualities);
        }
        public async Task<ContexturalPhysicalQualityDto> GetBySensorIdAsync(Guid sensorId)
        {
            // 根據 SensorId 查詢資料
            var contexturalPhysicalQuality = await _contexturalPhysicalQualityRepository
                .FirstOrDefaultAsync(x => x.SensorId == sensorId);

            if (contexturalPhysicalQuality == null)
            {
                throw new ArgumentException("Sensor ID not found.");
            }

            // 返回映射後的 DTO
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

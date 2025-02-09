using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using MyAbpApp.Compensations;
using MyAbpApp.CompensationDtos;
using MyAbpApp.ICompensationServices;
namespace MyAbpApp.CompensationServices
{

    public class CompensationService : ApplicationService, ICompensationService
    {
        private readonly IRepository<Compensation, Guid> _compensationRepository;

        public CompensationService(IRepository<Compensation, Guid> compensationRepository)
        {
            _compensationRepository = compensationRepository;
        }

        // 創建新的補償資料
        public async Task<CompensationDto> CreateAsync(CreateCompensationDto input)
        {
            var compensation = new Compensation
            {
                DeviceType = input.DeviceType,
                Version = input.Version,
                CompensationValue = input.CompensationValue
            };

            await _compensationRepository.InsertAsync(compensation);
            return ObjectMapper.Map<Compensation, CompensationDto>(compensation);
        }

        // 根據 ID 取得補償資料
        public async Task<CompensationDto> GetAsync(Guid id)
        {
            var compensation = await _compensationRepository.GetAsync(id);
            return ObjectMapper.Map<Compensation, CompensationDto>(compensation);
        }

        // 取得所有補償資料
        public async Task<GetCompensationDto> GetListAsync()
        {
            var compensations = await _compensationRepository.GetListAsync();
            var compensationDtos = ObjectMapper.Map<List<Compensation>, List<CompensationDto>>(compensations);

            return new GetCompensationDto
            {
                CompensationList = compensationDtos
            };
        }

        // 根據 ID 更新補償資料
        public async Task<CompensationDto> UpdateAsync(Guid id, CreateCompensationDto input)
        {
            var compensation = await _compensationRepository.GetAsync(id);
            compensation.DeviceType = input.DeviceType;
            compensation.Version = input.Version;
            compensation.CompensationValue = input.CompensationValue;

            await _compensationRepository.UpdateAsync(compensation);
            return ObjectMapper.Map<Compensation, CompensationDto>(compensation);
        }

        // 根據 ID 刪除補償資料
        public async Task DeleteAsync(Guid id)
        {
            var compensation = await _compensationRepository.GetAsync(id);
            await _compensationRepository.DeleteAsync(compensation);
        }
    }
}

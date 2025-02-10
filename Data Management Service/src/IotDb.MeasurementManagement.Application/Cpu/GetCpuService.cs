using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IotDb.MeasurementManagement.IotDb;
using Volo.Abp.Application.Dtos;

namespace IotDb.MeasurementManagement.Cpu
{
    public class GetCpuService : MeasurementManagementAppService, IGetCpuService
    {
        private readonly IIotDbRepository<CpuLoad> repository;

        public GetCpuService(IIotDbRepository<CpuLoad> repository)
        {
            this.repository = repository;
        }

        public async Task<PagedResultDto<CpuLoadDto>> GetCpuPageByTime(GetCpuPageByTimeRequest request)
        {
            var list = await repository.GetPageByTime(request.Device, request.StartDateTime, request.EndDateTime, request.Page.SkipCount, request.Page.MaxResultCount);
            var dto = ObjectMapper.Map<List<CpuLoad>, List<CpuLoadDto>>(list);
            return new PagedResultDto<CpuLoadDto>(dto.Count, dto);
        }
    }
}

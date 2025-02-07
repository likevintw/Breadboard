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
            if (request.StartDateTime.CompareTo(request.EndDateTime) > 0)
            {
                throw new ArgumentOutOfRangeException("StartDateTime date time", "StartDateTime is after EndDateTime.");
            }

            var list = await repository.GetPageByTime(request.StartDateTime, request.EndDateTime, request.Page.SkipCount, request.Page.MaxResultCount);
            var dto = ObjectMapper.Map<List<CpuLoad>, List<CpuLoadDto>>(list);
            return new PagedResultDto<CpuLoadDto>(dto.Count, dto);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IotDb.MeasurementManagement.IotDb;
using Volo.Abp.Application.Dtos;

namespace IotDb.MeasurementManagement.SoilMoisture
{
    public class GetSoilMoistureService : MeasurementManagementAppService, IQuerySoilMoistureService
    {
        private readonly IIotDbQueryService<SoilMoisture> _queryService;

        public GetSoilMoistureService(SoilMoistureDomainService service)
        {
            _queryService = service;
        }

        public async Task<PagedResultDto<SoilMoistureDto>> GetBySoilMoisturePageByTime(GetSoilMoistureByTimeRequest request)
        {
            if (request.StartDateTime.CompareTo(request.EndDateTime) > 0)
            {
                throw new ArgumentOutOfRangeException("StartDateTime date time", "StartDateTime date time is after end date time.");
            }
            List<SoilMoisture> list = await _queryService.GetPageByTime(request.StartDateTime, request.EndDateTime, request.Page.SkipCount, request.Page.MaxResultCount);
            List<SoilMoistureDto> dtos = ObjectMapper.Map<List<SoilMoisture>, List<SoilMoistureDto>>(list);
            return new PagedResultDto<SoilMoistureDto>(dtos.Count, dtos);
        }
    }
}

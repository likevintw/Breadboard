﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IotDb.MeasurementManagement.IotDb;
using Volo.Abp.Application.Dtos;

namespace IotDb.MeasurementManagement.Moisture
{
    public class GetSoilMoistureService : MeasurementManagementAppService, IQuerySoilMoistureService
    {
        private readonly IIotDbRepository<SoilMoisture> repository;

        public GetSoilMoistureService(IIotDbRepository<SoilMoisture> repository)
        {
            this.repository = repository;
        }

        public async Task<PagedResultDto<SoilMoistureDto>> GetBySoilMoisturePageByTime(GetSoilMoistureByTimeRequest request)
        {
            List<SoilMoisture> list = await repository.GetPageByTime(request.Device, request.StartDateTime, request.EndDateTime, request.Page.SkipCount, request.Page.MaxResultCount);
            List<SoilMoistureDto> dtos = ObjectMapper.Map<List<SoilMoisture>, List<SoilMoistureDto>>(list);
            return new PagedResultDto<SoilMoistureDto>(dtos.Count, dtos);
        }
    }
}

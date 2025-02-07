using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace IotDb.MeasurementManagement.Moisture
{
    public interface IQuerySoilMoistureService
    {
        Task<PagedResultDto<SoilMoistureDto>> GetBySoilMoisturePageByTime(GetSoilMoistureByTimeRequest request);
    }
}

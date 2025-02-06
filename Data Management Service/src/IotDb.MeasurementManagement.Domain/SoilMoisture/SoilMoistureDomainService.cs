using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IotDb.MeasurementManagement.IotDb;
using Volo.Abp.Domain.Services;

namespace IotDb.MeasurementManagement.SoilMoisture
{
    public class SoilMoistureDomainService : DomainService, IIotDbQueryService<SoilMoisture>
    {
        public Task<List<SoilMoisture>> GetPageByTime(DateTime start, DateTime end, int skip, int totalCount)
        {
            return Task.FromResult(new List<SoilMoisture>([new SoilMoisture() { Time = DateTime.Now, Timeseries = "moisture", Value = 1.1F }]));
        }
    }
}

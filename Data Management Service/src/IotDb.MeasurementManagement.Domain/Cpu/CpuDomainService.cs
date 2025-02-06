using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IotDb.MeasurementManagement.IotDb;
using Volo.Abp.Domain.Services;

namespace IotDb.MeasurementManagement.Cpu
{
    public class CpuDomainService : DomainService, IIotDbQueryService<CpuLoad>
    {
        public Task<List<CpuLoad>> GetPageByTime(DateTime start, DateTime end, int skip, int totalCount)
        {
            return Task.FromResult(new List<CpuLoad>([new CpuLoad { Timeseries = "", Time = DateTime.Now, Value = 0 }]));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace IotDb.MeasurementManagement.IotDb
{
    public interface IIotDbRepository<T>
    {
        Task<List<T>> GetPageByTime(string device, DateTime start, DateTime end, int skip, int totalCount);
        Task<int> Insert(string device, T t);
    }
}

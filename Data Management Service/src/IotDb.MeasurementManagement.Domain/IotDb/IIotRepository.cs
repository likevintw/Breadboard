using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotDb.MeasurementManagement.IotDb
{
    public interface IIotDbRepository<T>
    {
        Task<List<T>> GetPageByTime(DateTime start, DateTime end, int skip, int totalCount);
        Task<int> Insert(T t);
    }
}

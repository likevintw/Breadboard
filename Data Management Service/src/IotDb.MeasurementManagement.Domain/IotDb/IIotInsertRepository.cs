using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotDb.MeasurementManagement.IotDb
{
    public interface IIotInsertRepository<T>
    {
        public Task<int> Insert(T t);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.IoTDB;

namespace IotDb.MeasurementManagement.IotDb
{
    public interface IIotDbConnection
    {
        SessionPool GetSessionPool(string host, int port, int poolSize);
    }
}

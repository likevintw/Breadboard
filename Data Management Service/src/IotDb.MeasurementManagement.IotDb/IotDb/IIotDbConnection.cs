using Apache.IoTDB;

namespace IotDb.MeasurementManagement.IotDb
{
    public interface IIotDbConnection
    {
        public SessionPool GetSessionPool();
        public SessionPool GetSessionPool(string host, int port, int poolSize);
    }
}

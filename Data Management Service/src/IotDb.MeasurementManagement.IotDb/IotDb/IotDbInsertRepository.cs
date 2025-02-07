using Apache.IoTDB;
using Apache.IoTDB.DataStructure;
using Microsoft.Extensions.Logging;

namespace IotDb.MeasurementManagement.IotDb.IotDb
{
    public class IotDbInsertRepository<T> : IIotDbInsertRepository<T> where T : AbstractIotDb, new()
    {
        private readonly IIotDbConnection connection;

        public IotDbInsertRepository(IIotDbConnection connection)
        {
            this.connection = connection;
        }

        public async Task<int> Insert(T t)
        {
            SessionPool sessionPool = connection.GetSessionPool();
            int rtn = await sessionPool.InsertAlignedRecordAsync("root.device1", new RowRecord(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), [2F], [T.Measurement]));
            return rtn;
        }
    }
}

using Apache.IoTDB;
using Apache.IoTDB.DataStructure;
using IotDb.MeasurementManagement.IotDb;
using Microsoft.Extensions.Logging;

namespace IotDb.MeasurementManagement.Cpu
{
    public class IotDbRepository<T> : IIotDbRepository<T> where T : IAbstractIotDb, new()
    {
        private readonly IIotDbConnection iotDbConnection;
        private readonly ILogger<IotDbRepository<T>> logger;

        public IotDbRepository(ConnectionService iotDbConnection, ILogger<IotDbRepository<T>> logger)
        {
            this.iotDbConnection = iotDbConnection;
            this.logger = logger;
        }

        async Task<List<T>> IIotDbRepository<T>.GetPageByTime(string device, DateTime start, DateTime end, int skip, int totalCount)
        {
            SessionPool sessionPool = iotDbConnection.GetSessionPool();
            DateTimeOffset startOffset = DateTime.SpecifyKind(start, DateTimeKind.Utc);
            DateTimeOffset endOffset = DateTime.SpecifyKind(end, DateTimeKind.Utc);

            string script = $"Select {T.Measurement} from {device} " +
                $"where time between {startOffset.ToUnixTimeMilliseconds()} and {endOffset.ToUnixTimeMilliseconds()} " +
                $"limit {totalCount} offset {skip} ";

            logger.LogDebug(script);
            SessionDataSet dataSet = await sessionPool.ExecuteQueryStatementAsync(script);
            List<T> result = new();
            while (dataSet.HasNext())
            {
                RowRecord record = dataSet.Next();
                T entity = new()
                {
                    Time = record.GetDateTime(),
                    Timeseries = record.Measurements[0],
                    Value = record.Values[0]
                };
                result.Add(entity);
            }
            return result;
        }
        public async Task<int> Insert(string device, T t)
        {
            SessionPool sessionPool = iotDbConnection.GetSessionPool();
            int rtn = await sessionPool.InsertAlignedRecordAsync(device, new RowRecord(DateTime.UtcNow, [t.Value], [T.Measurement]));
            return rtn;
        }
    }
}

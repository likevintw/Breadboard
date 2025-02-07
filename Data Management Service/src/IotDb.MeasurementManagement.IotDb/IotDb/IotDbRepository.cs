using Apache.IoTDB;
using Apache.IoTDB.DataStructure;
using IotDb.MeasurementManagement.IotDb;
using Microsoft.Extensions.Logging;

namespace IotDb.MeasurementManagement.Cpu
{
    public class IotDbRepository<T> : IIotDbRepository<T> where T : AbstractIotDb, new()
    {
        private readonly IIotDbConnection iotDbConnection;
        private const string device = "root.device1";
        private readonly ILogger<IotDbRepository<T>> logger;

        public IotDbRepository(ConnectionService iotDbConnection, ILogger<IotDbRepository<T>> logger)
        {
            this.iotDbConnection = iotDbConnection;
            this.logger = logger;
        }

        Task<List<T>> IIotDbRepository<T>.GetPageByTime(DateTime start, DateTime end, int skip, int totalCount)
        {
            SessionPool sessionPool = iotDbConnection.GetSessionPool();
            DateTimeOffset startOffset = DateTime.SpecifyKind(start, DateTimeKind.Utc);
            DateTimeOffset endOffset = DateTime.SpecifyKind(end, DateTimeKind.Utc);

            string script = $"Select {T.Measurement} from {device} " +
                $"where time between {startOffset.ToUnixTimeMilliseconds()} and {endOffset.ToUnixTimeMilliseconds()} " +
                $"limit {totalCount} offset {skip} ";

            logger.LogDebug(script);
            SessionDataSet dataSet = sessionPool.ExecuteQueryStatementAsync(script).Result;
            List<T> result = new();
            while (dataSet.HasNext())
            {
                RowRecord record = dataSet.Next();
                T entity = new()
                {
                    Time = record.GetDateTime(),
                    Timeseries = record.Measurements[0],
                    Value = (float)record.Values[0]
                };
                result.Add(entity);
            }
            return Task.FromResult(result);
        }
        public async Task<int> Insert(T t)
        {
            SessionPool sessionPool = iotDbConnection.GetSessionPool();
            int rtn = await sessionPool.InsertAlignedRecordAsync("root.device1", new RowRecord(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), [2F], [T.Measurement]));
            return rtn;
        }
    }
}

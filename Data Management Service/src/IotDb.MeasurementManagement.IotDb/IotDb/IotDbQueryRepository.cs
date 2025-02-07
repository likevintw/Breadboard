using Apache.IoTDB;
using Apache.IoTDB.DataStructure;
using IotDb.MeasurementManagement.IotDb;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;

namespace IotDb.MeasurementManagement.Cpu
{
    public class IotDbQueryRepository<T> : IIotDbQueryRepository<T> where T : AbstractIotDb, new()
    {
        private readonly IIotDbConnection iotDbConnection;
        private const string device = "root.device1";
        private readonly ILogger<IotDbQueryRepository<T>> logger;

        public IotDbQueryRepository(ConnectionService iotDbConnection, ILogger<IotDbQueryRepository<T>> logger)
        {
            this.iotDbConnection = iotDbConnection;
            this.logger = logger;
        }

        Task<List<T>> IIotDbQueryRepository<T>.GetPageByTime(DateTime start, DateTime end, int skip, int totalCount)
        {
            SessionPool sessionPool = iotDbConnection.GetSessionPool();
            DateTimeOffset startOffset = DateTime.SpecifyKind(start, DateTimeKind.Utc);
            DateTimeOffset endOffset = DateTime.SpecifyKind(end, DateTimeKind.Utc);

            string script = $"Select {CpuLoad.Measurement} from {device} " +
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
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apache.IoTDB;
using Apache.IoTDB.DataStructure;
using IotDb.MeasurementManagement.IotDb;
using Volo.Abp.Domain.Services;

namespace IotDb.MeasurementManagement.Cpu
{
    public class CpuDomainService : DomainService, IIotDbQueryService<CpuLoad>
    {
        private readonly IIotDbConnection iotDbConnection;
        private const string device = "root.device1";

        public CpuDomainService(ConnectionService iotDbConnection)
        {
            this.iotDbConnection = iotDbConnection;
        }

        public CpuDomainService()
        {
        }

        public Task<List<CpuLoad>> GetPageByTime(DateTime start, DateTime end, int skip, int totalCount)
        {
            SessionPool sessionPool = iotDbConnection.GetSessionPool();
            DateTimeOffset startOffset = DateTime.SpecifyKind(start, DateTimeKind.Utc);
            DateTimeOffset endOffset = DateTime.SpecifyKind(end, DateTimeKind.Utc);
            SessionDataSet dataSet = sessionPool.ExecuteQueryStatementAsync($"Select * from {device}.{CpuLoad.Measurement} " +
                $"where time between {startOffset.ToUnixTimeMilliseconds()} and {endOffset.ToUnixTimeMilliseconds()} " +
                $"limit {totalCount} offset {skip} ").Result;
            List<CpuLoad> result = new();
            while (dataSet.HasNext())
            {
                RowRecord record = dataSet.Next();
                CpuLoad entity = new()
                {
                    Time = record.GetDateTime(),
                    Timeseries = $"{device}.{CpuLoad.Measurement}",
                    Value = (float)record.Values[0]
                };

            }
            return Task.FromResult(result);
        }
    }
}

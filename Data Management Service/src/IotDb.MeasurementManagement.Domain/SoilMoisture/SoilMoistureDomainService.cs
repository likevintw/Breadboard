using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apache.IoTDB.DataStructure;
using Apache.IoTDB;
using IotDb.MeasurementManagement.Cpu;
using IotDb.MeasurementManagement.IotDb;
using Volo.Abp.Domain.Services;

namespace IotDb.MeasurementManagement.SoilMoisture
{
    public class SoilMoistureDomainService : DomainService, IIotDbQueryService<SoilMoisture>
    {
        private readonly IIotDbConnection iotDbConnection;
        private const string device = "root.device1";

        public SoilMoistureDomainService(ConnectionService iotDbConnection)
        {
            this.iotDbConnection = iotDbConnection;
        }
        public Task<List<SoilMoisture>> GetPageByTime(DateTime start, DateTime end, int skip, int totalCount)
        {
            SessionPool sessionPool = iotDbConnection.GetSessionPool();
            DateTimeOffset startOffset = DateTime.SpecifyKind(start, DateTimeKind.Utc);
            DateTimeOffset endOffset = DateTime.SpecifyKind(end, DateTimeKind.Utc);
            SessionDataSet dataSet = sessionPool.ExecuteQueryStatementAsync($"Select * from {device}.{SoilMoisture.Measurement} " +
                $"where time between {startOffset.ToUnixTimeMilliseconds()} and {endOffset.ToUnixTimeMilliseconds()} " +
                $"limit {totalCount} offset {skip} ").Result;
            List<SoilMoisture> result = new();
            while (dataSet.HasNext())
            {
                RowRecord record = dataSet.Next();
                CpuLoad entity = new()
                {
                    Time = record.GetDateTime(),
                    Timeseries = $"{device}.{SoilMoisture.Measurement}",
                    Value = (float)record.Values[0]
                };

            }
            return Task.FromResult(result);
        }
    }
}

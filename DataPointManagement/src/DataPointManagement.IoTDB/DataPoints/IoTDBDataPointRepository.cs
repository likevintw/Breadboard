
// TODO: IoTDB/DataPoints/IoTdbDataPointsRepository.cs

using DataPointManagement.DataPoints;
using DataPointManagement.IoTDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;
using Apache.IoTDB;
using Apache.IoTDB.DataStructure;

//using Volo.Abp.Domain.Repositories.Implementation;

namespace DataPointManagement.DataPoints;

public class IoTDBDataPointsRepository<T> : IDataPointRepository<T> where T : DataPoint, new()
{
    private readonly IIoTDBConnectionManager _connectionManager;
    private readonly ILogger<IoTDBDataPointsRepository<T>> _logger;

    public IoTDBDataPointsRepository(IIoTDBConnectionManager connectionManager, ILogger<IoTDBDataPointsRepository<T>> logger)
    {
        _connectionManager = connectionManager;
        _logger = logger;
    }

    async Task<T> IDataPointRepository<T>.GetLatestDataByDeviceAsync(string deviceId)
    {
        try
        {
            SessionPool sessionPool = await _connectionManager.GetSessionPoolAsync();
            string query = $"SELECT * FROM root.{deviceId}.** ORDER BY time DESC LIMIT 1";
            _logger.LogDebug(query);

            SessionDataSet dataSet = sessionPool.ExecuteQueryStatementAsync(query).Result;
            if (dataSet.HasNext())
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                RowRecord record = dataSet.Next();
                
                DateTimeOffset tOffset = DateTime.SpecifyKind(record.GetDateTime(), DateTimeKind.Utc);
                for (var i = 0; i< record.Measurements.Count; i++)
                {
                    data.Add(record.Measurements[i], record.Values[i]);
                }
                T entity = new()
                {
                    DeviceId = deviceId,
                    Time = tOffset.ToUnixTimeMilliseconds(),
                    Data = data
                };
                return entity;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving latest data from IoTDB for device {DeviceId}", deviceId);
            throw; // Or handle the exception as appropriate for your application
        }
    }

    async Task<List<T>> IDataPointRepository<T>.GetDataByTimeRangeAsync(string deviceId, long startTime, long endTime, long offset, int pageSize)
    {
        try
        {
            SessionPool sessionPool = await _connectionManager.GetSessionPoolAsync();
            string query = $"SELECT * FROM root.{deviceId}.** WHERE time >= {startTime} AND time <= {endTime} limit {pageSize} offset {offset} ";
            _logger.LogDebug(query);

            SessionDataSet dataSet = sessionPool.ExecuteQueryStatementAsync(query).Result;
            List<T> result = new();
            while (dataSet.HasNext())
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                RowRecord record = dataSet.Next();
                DateTimeOffset tOffset = DateTime.SpecifyKind(record.GetDateTime(), DateTimeKind.Utc);
                for (var i = 0; i < record.Measurements.Count; i++)
                {
                    data.Add(record.Measurements[i], record.Values[i]);
                }
                T entity = new()
                {
                    DeviceId = deviceId,
                    Time = tOffset.ToUnixTimeMilliseconds(),
                    Data = data
                };
                result.Add(entity);
            }
            return result;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving data from IoTDB for device {DeviceId} between {StartTime} and {EndTime}", deviceId, startTime, endTime);
            throw;
        }
    }

    public async Task InsertAsync(DataPoint dataPoint)
    {
        try
        {
            var rowRecord = new RowRecord(dataPoint.Time, dataPoint.Data.Values.ToList(), dataPoint.Data.Keys.ToList());
            SessionPool sessionPool = await _connectionManager.GetSessionPoolAsync();
            await sessionPool.InsertAlignedRecordAsync(dataPoint.DeviceId, rowRecord);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting SensorData into IoTDB for device {DeviceId}", dataPoint.DeviceId);
            throw;
        }
    }

    // BasicRepository implements the basic operations.
    // You might need to override some of them for IoTDB specifics if the default implementations don't fit.

    // Helper method to map a row from the query result to a SensorData entity (adapt this to your client and schema)
    //private DataPoint MapRowToSensorData(IoTDBRow row)
    //{
    //    return new SensorData
    //    {
    //        Id = Guid.NewGuid(), // You might need to get this from the row if IoTDB provides it
    //        DeviceId = Guid.Parse(row.GetString("Device")), // Example - adjust based on how your data is structured
    //        Timestamp = row.GetDateTime("Time"),
    //        // ... map other fields
    //        Values = row.GetString("s1") // Example - adjust based on how your data is structured
    //    };
    //}

}
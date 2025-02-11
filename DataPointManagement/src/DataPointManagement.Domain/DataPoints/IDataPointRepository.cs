
// TODO: Domain/DataPoints/IDataPointRepository.cs

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DataPointManagement.DataPoints;

public interface IDataPointRepository<T>
{
    Task<T> GetLatestDataByDeviceAsync(string deviceId);
    Task<List<T>> GetDataByTimeRangeAsync(string deviceId, long startTime, long endTime, long offset, int pageSize);
    Task InsertAsync(DataPoint dataPoint);
}
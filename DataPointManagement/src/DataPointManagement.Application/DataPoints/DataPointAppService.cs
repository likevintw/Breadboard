
// TODO: Application/DataPoints/DataPointAppService


using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace DataPointManagement.DataPoints;

public class DataPointAppService : DataPointManagementAppService, IDataPointAppService
{
    private readonly IDataPointRepository<DataPoint> _repository;

    public DataPointAppService(IDataPointRepository<DataPoint> dataPointRepository)
    {
        _repository = dataPointRepository;
    }

    public async Task CreateAsync(CreateUpdateDataPointDto input)
    {
        var dataPoint = new DataPoint(input.DeviceId, input.Time, input.Data);
        await _repository.InsertAsync(dataPoint);
    }

    public async Task<DataPointDto> GetLastAsync(LastDataPointRequestDto input)
    {
        var dataPoint = await _repository.GetLatestDataByDeviceAsync(input.DeviceId);
        if (dataPoint != null)
        {
            var dto = new DataPointDto();
            dto.Time = dataPoint.Time;
            dto.Data = dataPoint.Data;
            return dto;
        }
        else
        {
            return null;
        }
    }

    public async Task<PagedResultDataPointListDto> GetPagedListAsync(PagedResultDataPointListRequestDto input)
    {
        var list = await _repository.GetDataByTimeRangeAsync(input.DeviceId, input.StartTime, input.EndTime, input.Offset, input.PageSize);

        var dto = new PagedResultDataPointListDto();

        dto.DeviceId = input.DeviceId;
        dto.Count = 0;
        dto.DataList = new List<DataPointDto>();

        foreach (var item in list) {
            var dp_dto = new DataPointDto();
            dp_dto.Time = item.Time;
            dp_dto.Data = item.Data;
            dto.DataList.Add(dp_dto);
        }
        return dto;
    }

    public async Task UpdateAsync(CreateUpdateDataPointDto input)
    {
        await CreateAsync(input);
    }
}
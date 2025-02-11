
// TODO: Application.Contracts/DataPoints/PagedResultDataPointListRequestDto

using System;

namespace DataPointManagement.DataPoints;

public class PagedResultDataPointListRequestDto
{
    public required string DeviceId { get; set; }
    public required string DataKey { get; set; }
    public required long StartTime { get; set; }
    public required long EndTime { get; set; }
    public required int PageSize { get; set; }
    public required long Offset { get; set; }

}


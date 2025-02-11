
// TODO: Application.Contracts/DataPoints/PagedResultDataPointListDto

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPointManagement.DataPoints;

public class PagedResultDataPointListDto
{
    public string DeviceId { get; set; }
    public long Count { get; set; }
    public List<DataPointDto> DataList { get; set; }

}

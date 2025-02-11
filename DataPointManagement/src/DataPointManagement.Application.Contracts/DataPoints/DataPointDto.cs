
// TODO: Application.Contracts/DataPoints/DataPointDto

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPointManagement.DataPoints;

public class DataPointDto
{
    public long Time { get; set; }
    public Dictionary<string, object>? Data { get; set; }

}

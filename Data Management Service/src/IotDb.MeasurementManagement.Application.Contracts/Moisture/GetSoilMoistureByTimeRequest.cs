using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace IotDb.MeasurementManagement.Moisture
{
    public class GetSoilMoistureByTimeRequest
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public PagedResultRequestDto Page { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace IotDb.MeasurementManagement.Moisture
{
    public class GetSoilMoistureByTimeRequest
    {
        [Required]
        public string Device { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }
        [Required]
        public PagedResultRequestDto Page { get; set; }
    }
}

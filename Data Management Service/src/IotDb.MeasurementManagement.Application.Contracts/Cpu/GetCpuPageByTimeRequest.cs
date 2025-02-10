using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace IotDb.MeasurementManagement.Cpu
{
    public class GetCpuPageByTimeRequest
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

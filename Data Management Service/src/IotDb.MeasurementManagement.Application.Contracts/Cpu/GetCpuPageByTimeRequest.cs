using System;
using Volo.Abp.Application.Dtos;

namespace IotDb.MeasurementManagement.Cpu
{
    public class GetCpuPageByTimeRequest
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public PagedResultRequestDto Page { get; set; }
    }
}

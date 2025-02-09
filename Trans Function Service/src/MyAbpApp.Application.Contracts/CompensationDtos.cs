using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
namespace MyAbpApp.CompensationDtos
{
    public class CreateCompensationDto : AuditedEntityDto<Guid>
    {
        public string DeviceType { get; set; }
        public string Version { get; set; }
        public double CompensationValue { get; set; }
    }

    public class CompensationDto
    {
        public string DeviceType { get; set; }
        public string Version { get; set; }
        public double CompensationValue { get; set; }
    }

    public class GetCompensationDto
    {
        public List<CompensationDto> CompensationList { get; set; }

        public GetCompensationDto()
        {
            CompensationList = new List<CompensationDto>();
        }
    }

}


using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
namespace MyAbpApp.ContexturalPhysicalQualityDtos
{
    public class ContexturalPhysicalQualityDto
    {
        public Guid Id { get; set; }
        public Guid SensorId { get; set; }
        public string Process { get; set; }
    }

    public class CreateOrUpdateContexturalPhysicalQualityDto
    {
        public Guid SensorId { get; set; }
        public string Process { get; set; }
    }


}


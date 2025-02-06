
using System;
using Volo.Abp.Application.Dtos;

namespace MyAbpApp.GyroscopeInsertRequestDtos
{
    public class GyroscopeInsertRequestDto : AuditedEntityDto<Guid>
    {
        public required string Database { get; set; }
        public required string Measurement { get; set; }
        public double Value { get; set; }
        public long Timestamp { get; set; }
    }
}

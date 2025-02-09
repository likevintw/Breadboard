using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyAbpApp.Compensations
{
    public class Compensation : AuditedAggregateRoot<Guid>
    {
        public required string DeviceType { get; set; }
        public required string? Version { get; set; }
        public double CompensationValue { get; set; }
    }
}



using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyAbpApp.ContexturalPhysicalQualities
{
    public class ContexturalPhysicalQuality : FullAuditedAggregateRoot<Guid>
    {
        public string Process { get; set; }
    }
}

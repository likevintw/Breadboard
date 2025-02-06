using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyAbpApp.Sensors
{
    public class Sensor : FullAuditedAggregateRoot<Guid>
    {
        public required string SensorName { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime Timestamp { get; set; }
    }
    // public class Sensor : Entity<Guid>
    // {
    //     public required string SensorName { get; set; }
    //     public double Temperature { get; set; }
    //     public double Humidity { get; set; }
    //     public DateTime Timestamp { get; set; }
    // }
}
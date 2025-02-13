using System;

namespace IotDb.MeasurementManagement.BackgroundWorker.Workers;

public class PhysicalQuality
{
    public string? DeviceId { get; set; }
    public double? OriginalValue { get; set; }
    public double? ResultValue { get; set; }
}

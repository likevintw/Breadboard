using System;

namespace IotDb.MeasurementManagement.IotDb
{
    public interface AbstractIotDb
    {
        public abstract static string Measurement { get; }
        public DateTime Time { get; set; }
        public string Timeseries { get; set; }
        public float Value { get; set; }
    }
}

using System;

namespace IotDb.MeasurementManagement.IotDb
{
    public interface IAbstractIotDb
    {
        public abstract static string Measurement { get; }
        public DateTime Time { get; set; }
        public string Timeseries { get; set; }
        public object Value { get; set; }
    }
}

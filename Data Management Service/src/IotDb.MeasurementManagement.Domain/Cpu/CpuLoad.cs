using System;
using IotDb.MeasurementManagement.IotDb;

namespace IotDb.MeasurementManagement.Cpu
{
    public class CpuLoad : IAbstractIotDb
    {
        public static string Measurement => "cpu";

        public DateTime Time { get; set; }
        public string Timeseries { get; set; }
        public object Value { get; set; }
    }
}

using System;
using IotDb.MeasurementManagement.IotDb;

namespace IotDb.MeasurementManagement.Cpu
{
    public class CpuLoad : AbstractIotDb
    {
        public static string Measurement => "cpu";

        public DateTime Time { get; set; }
        public string Timeseries { get; set; }
        public float Value { get; set; }
    }
}

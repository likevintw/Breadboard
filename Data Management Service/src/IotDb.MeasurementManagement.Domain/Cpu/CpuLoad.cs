using System;
using IotDb.MeasurementManagement.IotDb;

namespace IotDb.MeasurementManagement.Cpu
{
    public class CpuLoad : AbstractIotDb
    {
        public DateTime Time { get; set; }
        public required string Timeseries { get; set; }
        public float Value { get; set; }

        protected override string Measurement => "cpu";
    }
}

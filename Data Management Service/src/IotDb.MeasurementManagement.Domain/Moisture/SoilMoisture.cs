using System;
using IotDb.MeasurementManagement.IotDb;

namespace IotDb.MeasurementManagement.Moisture
{
    public class SoilMoisture : AbstractIotDb
    {
        public static string Measurement => "moisture";

        public DateTime Time { get; set; }
        public string Timeseries { get; set; }
        public float Value { get; set; }
    }
}

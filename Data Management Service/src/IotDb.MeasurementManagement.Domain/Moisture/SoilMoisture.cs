using System;
using IotDb.MeasurementManagement.IotDb;

namespace IotDb.MeasurementManagement.Moisture
{
    public class SoilMoisture : IAbstractIotDb
    {
        public static string Measurement => "moisture";

        public DateTime Time { get; set; }
        public string Timeseries { get; set; }
        public object Value { get; set; }
    }
}

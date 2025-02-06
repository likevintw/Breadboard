using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IotDb.MeasurementManagement.IotDb;

namespace IotDb.MeasurementManagement.SoilMoisture
{
    public class SoilMoisture : AbstractIotDb
    {
        protected override string Measurement => "moisture";
        public DateTime Time { get; set; }
        public string Timeseries { get; set; }
        public float Value { get; set; }
    }
}

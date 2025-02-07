using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotDb.MeasurementManagement.Moisture
{
    public class SoilMoistureDto
    {
        public DateTime Time { get; set; }
        public string Timeseries { get; set; }
        public float Value {  get; set; }
    }
}

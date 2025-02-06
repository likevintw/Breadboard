using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotDb.MeasurementManagement.Cpu
{
    public class CpuLoadDto
    {
        public DateTime Time { get; set; }
        public required string Timeseries { get; set; }
        public float Value { get; set; }
    }
}

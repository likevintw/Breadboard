using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotDb.MeasurementManagement.IotDb
{
    public abstract class AbstractIotDb
    {
        protected abstract string Measurement { get; }
    }
}

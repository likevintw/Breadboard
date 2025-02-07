using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace IotDb.MeasurementManagement.Moisture
{
    public class SoilMoistureAutoMapperProfile: Profile
    {
        public SoilMoistureAutoMapperProfile ()
        {
            CreateMap<SoilMoisture, SoilMoistureDto>();
        }
    }
}

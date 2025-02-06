using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace IotDb.MeasurementManagement.Cpu
{
    public class CpuAutoMapperProfile : Profile
    {
        public CpuAutoMapperProfile()
        {
            CreateMap<CpuLoad, CpuLoadDto>();
        }
    }
}

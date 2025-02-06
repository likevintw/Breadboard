using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace IotDb.MeasurementManagement.Cpu
{
    public interface IGetCpuService
    {
        Task<PagedResultDto<CpuLoadDto>> GetCpuPageByTime(GetCpuPageByTimeRequest request);
    }
}

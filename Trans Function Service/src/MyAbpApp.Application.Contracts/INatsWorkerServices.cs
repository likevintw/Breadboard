

using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyAbpApp.INatsWorkerServices
{
    public interface INatsWorkerService
    {
        Task GetWorkerList();
        Task CreateWorker(string serviceId, string serviceName, string serviceVersion);
        Task DeleteWorker(string serviceId);
    }
}

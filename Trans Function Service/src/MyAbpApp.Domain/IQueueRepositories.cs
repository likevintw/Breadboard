using System;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace MyAbpApp.IQueueRepositories
{
    public interface IQueueRepository
    {
        Task CreatePercentageWorker(string serviceName, string functionName, string serviceVersion, string description);
        Task GetPercentageWorkerValue();
        // Task<double> GetPercentageWorkerValue();
    }
}
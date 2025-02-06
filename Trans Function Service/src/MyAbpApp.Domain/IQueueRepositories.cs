using System;
using System.Threading.Tasks;

namespace MyAbpApp.IQueueRepositories
{
    public interface IQueueRepository
    {
        Task CreatePercentageWorker(string serviceName, string functionName, string serviceVersion, string description);
    }
}
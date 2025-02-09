using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;


namespace MyAbpApp.IQueueRepositories
{
    public interface IQueueRepository
    {
        Task CreatePercentageWorker(
            CancellationToken cancellationToken, string serviceName, string functionName, string serviceVersion, string description);
        Task<double> GetPercentageWorkerValue(CancellationToken cancellationToken);
        // Task<double> GetPercentageWorkerValue();
    }
}
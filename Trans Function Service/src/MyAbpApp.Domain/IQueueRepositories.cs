using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;


namespace MyAbpApp.IQueueRepositories
{
    public interface IQueueRepository
    {
        Task CreateCompensationWorker(
            CancellationToken cancellationToken, string serviceName, string functionName, string serviceVersion, string description);
        Task<double> GetCompensationWorkerValue(CancellationToken cancellationToken);
        // Task<double> GetPercentageWorkerValue();
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;


namespace MyAbpApp.IWorkManagers
{
    public interface IWorkManager
    {
        Task CreateContexturalPhysicalQualityWorker(
            CancellationToken cancellationToken, string serviceVersion, string serviceDescription);
        Task CreateTemperatureUnitTransferWorker(
            CancellationToken cancellationToken, string serviceVersion, string serviceDescription);
        Task CreatePercentageWorker(
            CancellationToken cancellationToken, string serviceVersion, string serviceDescription);
        Task CreateCompensationWorker(
            CancellationToken cancellationToken, string serviceVersion, string serviceDescription);
    }
}
using IotDb.MeasurementManagement.Cpu;
using IotDb.MeasurementManagement.IotDb;
using IotDb.MeasurementManagement.Moisture;
using Microsoft.Extensions.Logging;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Net;
using Volo.Abp.BackgroundWorkers;

namespace IotDb.MeasurementManagement.BackgroundWorker.Workers
{
    public class SubscribeCpuWorker : BackgroundWorkerBase
    {
        private readonly NatsClient natsClient;
        private const string streamName = "stream";
        private const string consumerName = "measurement-insert";
        private IIotDbRepository<CpuLoad> iotDbRepository;
        private ILogger<SubscribeCpuWorker> logger;
        private readonly CpuWorker cpuWorker;
        private INatsJSContext context;
        private INatsJSConsumer consumer;
        public SubscribeCpuWorker(IIotDbRepository<CpuLoad> iotDbRepository, ILogger<SubscribeCpuWorker> logger, CpuWorker cpuWorker, INatsConnection natsConnection)
        {
            this.iotDbRepository = iotDbRepository;
            this.logger = logger;
            this.cpuWorker = cpuWorker;
            this.natsClient = natsConnection.GetClient();
        }

        public override async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await base.StartAsync(cancellationToken);
            Task task = Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await cpuWorker.Work();
                }
            }, cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await base.StopAsync(cancellationToken);
            logger.LogDebug("Shut down SubscribeCpuWorker");
        }
    }
}

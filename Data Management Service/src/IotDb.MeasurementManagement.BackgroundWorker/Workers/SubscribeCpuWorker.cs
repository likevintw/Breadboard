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
        private readonly CpuRawWorker cpuWorker;
        private INatsJSContext context;
        private INatsJSConsumer consumer;
        public SubscribeCpuWorker(IIotDbRepository<CpuLoad> iotDbRepository, ILogger<SubscribeCpuWorker> logger, CpuRawWorker cpuWorker, INatsConnection natsConnection)
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
        private async Task InsertRawDataAsync(NatsJSMsg<double> msg)
        {
            logger.LogDebug($"Received subject:{msg.Subject}, data:{msg.Data}");
            CpuLoad cpu = new()
            {
                Time = DateTime.UtcNow,
                Timeseries = $"root.device1.{CpuLoad.Measurement}",
                Value = msg.Data
            };
            await iotDbRepository.Insert("root.device1", cpu);
        }
        private async Task InsertCpqAsync(NatsJSMsg<double> msg)
        {
            var res = await natsClient.RequestAsync<string, string>("ContexturalPhysicalQualityService.ReturnContexturalPhysicalQualityValue", msg.Data.ToString());
            CpuLoad cpu = new()
            {
                Timeseries = $"root.device1.cpq.{CpuLoad.Measurement}",
                Time = DateTime.UtcNow,
                Value = msg.Data
            };
            await iotDbRepository.Insert($"root.device1.cpq.{CpuLoad.Measurement}", cpu);
        }
    }
}

using IotDb.MeasurementManagement.Cpu;
using IotDb.MeasurementManagement.IotDb;
using IotDb.MeasurementManagement.Moisture;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Net;
using Volo.Abp.BackgroundWorkers;

namespace IotDb.MeasurementManagement.BackgroundWorker.Workers
{
    public class SubscribeSoilMositureWorker : BackgroundWorkerBase
    {
        private const string streamName = "stream";
        private const string consumerName = "measurement-insert";
        private IIotDbRepository<SoilMoisture> iotDbRepository;
        private ILogger<SubscribeSoilMositureWorker> logger;
        private readonly SoilMoistureWorker soilMoistureWorker;
        public SubscribeSoilMositureWorker(ILogger<SubscribeSoilMositureWorker> logger, SoilMoistureWorker worker)
        {
            this.logger = logger;
            this.soilMoistureWorker = worker;
        }

        public override async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await base.StartAsync(cancellationToken);
            Task task = Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await soilMoistureWorker.Work();
                }
            }, cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await base.StopAsync(cancellationToken);
            logger.LogDebug("Shut down SubscribeCpuWorker");
        }

        // private async Task InsertCpqAsync(NatsJSMsg<double> msg)
        // {
        //     var res = await natsClient.RequestAsync<string, string>("ContexturalPhysicalQualityService.ReturnContexturalPhysicalQualityValue", msg.Data.ToString());
        //     SoilMoisture soilMoisture = new()
        //     {
        //         Timeseries = $"root.device1.cpq.{SoilMoisture.Measurement}",
        //         Time = DateTime.UtcNow,
        //         Value = msg.Data
        //     };
        //     await iotDbRepository.Insert($"root.device1.cpq.{SoilMoisture.Measurement}", soilMoisture);
        // }
    }
}

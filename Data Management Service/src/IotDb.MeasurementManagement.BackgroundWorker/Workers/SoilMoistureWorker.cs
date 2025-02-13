using IotDb.MeasurementManagement.IotDb;
using IotDb.MeasurementManagement.Moisture;
using Microsoft.Extensions.Logging;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Net;
using Volo.Abp.DependencyInjection;

namespace IotDb.MeasurementManagement.BackgroundWorker.Workers;

public class SoilMoistureWorker : ITransientDependency
{
    private readonly ILogger<CpuRawWorker> logger;
    private readonly IIotDbRepository<SoilMoisture> iotDbRepository;
    private const string streamName = "stream";
    private const string consumerName = "soil-moisture-raw-insert";
    private readonly NatsClient natsClient;
    private INatsJSContext context;
    private INatsJSConsumer consumer;

    public SoilMoistureWorker(ILogger<CpuRawWorker> logger, IIotDbRepository<SoilMoisture> iotDbRepository, INatsConnection natsConnection)
    {
        this.logger = logger;
        this.iotDbRepository = iotDbRepository;
        natsClient = natsConnection.GetClient();
    }
    public async Task Work()
    {
        context = natsClient.CreateJetStreamContext();
        consumer = await context.CreateConsumerAsync(streamName, new ConsumerConfig(consumerName) { FilterSubject = "events.breadboard.soilMoisture" });
        await foreach (var msg in consumer.FetchAsync<double>(opts: new NatsJSFetchOpts
        {
            MaxMsgs = 100,
            Expires = TimeSpan.FromMinutes(1)
        }))
        {
            logger.LogDebug($"Received subject:{msg.Subject}, data:{msg.Data}");
            SoilMoisture moisture = new()
            {
                Time = DateTime.UtcNow,
                Timeseries = $"root.device1.{SoilMoisture.Measurement}",
                Value = msg.Data
            };
            await iotDbRepository.Insert("root.device1", moisture);
            await msg.AckAsync();
        }
    }
}
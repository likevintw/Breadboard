using IotDb.MeasurementManagement.Cpu;
using IotDb.MeasurementManagement.IotDb;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Net;
using Volo.Abp.DependencyInjection;

namespace IotDb.MeasurementManagement.BackgroundWorker.Workers;

public class CpuWorker : ITransientDependency
{
    private readonly ILogger<CpuWorker> logger;
    private readonly IIotDbRepository<CpuLoad> iotDbRepository;
    private const string streamName = "stream1";
    private const string consumerName = "cpu-raw-insert";
    private readonly NatsClient natsClient;
    private INatsJSContext context;
    private INatsJSConsumer consumer;
    private readonly CpqService cpqWorker;

    public CpuWorker(ILogger<CpuWorker> logger, IIotDbRepository<CpuLoad> iotDbRepository, INatsConnection natsConnection, CpqService cpqWorker)
    {
        this.logger = logger;
        this.iotDbRepository = iotDbRepository;
        natsClient = natsConnection.GetClient(new NatsOpts
        {
            Url = "nats://172.17.20.184:4222",
            // TlsOpts = new NatsTlsOpts { Mode = TlsMode.Auto, InsecureSkipVerify = true },
            // AuthOpts = NatsAuthOpts.Default with { CredsFile = "" },
        });
        this.cpqWorker = cpqWorker;
    }
    public async Task Work()
    {
        context = natsClient.CreateJetStreamContext();
        consumer = await context.CreateOrUpdateConsumerAsync(streamName, new ConsumerConfig(consumerName) { FilterSubject = "eco1j.*.*.dm.dt.rl.enr" });

        await foreach (var msg in consumer.FetchAsync<double>(opts: new NatsJSFetchOpts
        {
            MaxMsgs = 100,
            Expires = TimeSpan.FromMinutes(1)
        }))
        {
            logger.LogDebug($"Received subject:{msg.Subject}, data:{msg.Data}");
            CpuLoad cpu = new()
            {
                Time = DateTime.UtcNow,
                Timeseries = $"root.device1.{CpuLoad.Measurement}",
                Value = msg.Data
            };
            await iotDbRepository.Insert("root.device1", cpu);
            //Percentage
            PhysicalQuality physicalQuality = new()
            {
                DeviceId = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                OriginalValue = msg.Data,
            };
            await cpqWorker.Start(physicalQuality);
            await msg.AckAsync();
        }
    }
}

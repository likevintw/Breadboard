using IotDb.MeasurementManagement.Cpu;
using IotDb.MeasurementManagement.IotDb;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Net;
using Volo.Abp.DependencyInjection;

namespace IotDb.MeasurementManagement.BackgroundWorker.Workers;

public class CpuRawWorker : ITransientDependency
{
    private readonly ILogger<CpuRawWorker> logger;
    private readonly IIotDbRepository<CpuLoad> iotDbRepository;
    private const string streamName = "stream";
    private const string consumerName = "cpu-raw-insert";
    private readonly NatsClient natsClient;
    private INatsJSContext context;
    private INatsJSConsumer consumer;

    public CpuRawWorker(ILogger<CpuRawWorker> logger, IIotDbRepository<CpuLoad> iotDbRepository, INatsConnection natsConnection)
    {
        this.logger = logger;
        this.iotDbRepository = iotDbRepository;
        natsClient = natsConnection.GetClient(new NatsOpts
        {
            Url = "172.17.20.184:4222",
            TlsOpts = new NatsTlsOpts { Mode = TlsMode.Auto, InsecureSkipVerify = true },
            AuthOpts = NatsAuthOpts.Default with { CredsFile = "" },
        });
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
            await msg.AckAsync();
        }
    }
}

using System;
using IotDb.MeasurementManagement.Cpu;
using IotDb.MeasurementManagement.IotDb;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using NATS.Net;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace IotDb.MeasurementManagement.BackgroundWorker.Workers;
public class CpqService : ITransientDependency
{
    private readonly NatsClient natsClient;
    private readonly ILogger<CpqService> logger;
    private readonly IIotDbRepository<CpuLoad> repository;

    public CpqService(INatsConnection natsConnection, ILogger<CpqService> logger, IIotDbRepository<CpuLoad> repository)
    {
        this.natsClient = natsConnection.GetClient();
        this.logger = logger;
        this.repository = repository;
    }

    public async Task Start(PhysicalQuality physicalQuality)
    {
        NatsMsg<PhysicalQuality> msg = await natsClient.RequestAsync<PhysicalQuality, PhysicalQuality>(subject: "ContexturalPhysicalQualityService.ReturnContexturalPhysicalQualityValue", data: physicalQuality);
        CpuLoad cpuLoad = new CpuLoad()
        {
            Timeseries = "root.device1.cpq",
            Time = DateTime.UtcNow,
            Value = msg.Data.ResultValue
        };
        var ack = await repository.Insert("root.device1.cpq", cpuLoad);
        logger.LogDebug($"Insert cpu cpq: {msg.Data}, result = {ack}");
    }
}

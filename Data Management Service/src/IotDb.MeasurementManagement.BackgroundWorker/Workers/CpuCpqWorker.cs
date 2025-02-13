using System;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Net;

namespace IotDb.MeasurementManagement.BackgroundWorker.Workers;

public class CpuCpqWorker
{
    private readonly NatsClient client;
    public CpuCpqWorker(INatsConnection natsConnection)
    {
        this.client = natsConnection.GetClient();
    }
    public async Task Work(NatsJSMsg<double> msg)
    {
        string data = "";
        NatsMsg<double> cpq = await client.RequestAsync<string, double>(subject: "", data: data);
    }
}

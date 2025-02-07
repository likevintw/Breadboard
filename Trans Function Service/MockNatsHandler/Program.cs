

using System.Diagnostics;
using NATS.Net;
using NATS.Client.Core;
using NATS.Client.JetStream.Models;
using NATS.Client.JetStream;
using NATS.Client.Serializers.Json;
using NATS.Client.Services;

class Program
{
    public static async Task CreatePercentageProducer()
    {
        var url = Environment.GetEnvironmentVariable("NATS_URL") ?? "nats_demo:4222";
        var username = Environment.GetEnvironmentVariable("NATS_USERNAME") ?? "username";
        var password = Environment.GetEnvironmentVariable("NATS_PASSWORD") ?? "password";

        await using var nc = new NatsClient(new NatsOpts
        {
            Url = url,
            AuthOpts = new NatsAuthOpts
            {
                Username = username,
                Password = password,
            }
        });
        var svc = nc.CreateServicesContext();

        string serviceName = "Percentager";
        string functionName = "ReturnPercentage";

        var result = await nc.RequestAsync<double, double>(subject: $"{serviceName}.{functionName}", data: 0);

        for (double i = 1; i < 150; i++)
        {
            result = await nc.RequestAsync<double, double>(subject: $"{serviceName}.{functionName}", data: i);
            Console.WriteLine($"{i} got reply: {result.Data}");
            Console.WriteLine("data type: " + result.Data.GetType());
            await Task.Delay(100);
        }
        Console.WriteLine("Bye!");
    }
    public static async Task CreateHoneywellProducer()
    {
        var url = Environment.GetEnvironmentVariable("NATS_URL") ?? "nats_demo:4222";
        var username = Environment.GetEnvironmentVariable("NATS_USERNAME") ?? "username";
        var password = Environment.GetEnvironmentVariable("NATS_PASSWORD") ?? "password";

        await using var nc = new NatsClient(new NatsOpts
        {
            Url = url,
            AuthOpts = new NatsAuthOpts
            {
                Username = username,
                Password = password,
            }
        });
        var svc = nc.CreateServicesContext();

        string serviceName = "HoneywellCE3245";
        string functionName = "GetCompensation";

        var result = await nc.RequestAsync<double, double>(subject: $"{serviceName}.{functionName}", data: 0);

        for (double i = 1; i < 100; i++)
        {
            result = await nc.RequestAsync<double, double>(subject: $"{serviceName}.{functionName}", data: i);
            Console.WriteLine($"{i} got reply: {result.Data}");
            Console.WriteLine("data type: " + result.Data.GetType());
            await Task.Delay(1000);
        }
        Console.WriteLine("Bye!");
    }
    public static async Task Main(string[] args)
    {
        CreatePercentageProducer();
    }
}

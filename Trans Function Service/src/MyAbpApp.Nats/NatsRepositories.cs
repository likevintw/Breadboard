
using System;
using System.Threading.Tasks;
using NATS.Net;
using NATS.Client.Core;
using NATS.Client.Services;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Client.Serializers.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using MyAbpApp.IQueueRepositories;

namespace MyAbpApp.NatsRepositories
{
    public class NatsRepository : IQueueRepository
    {
        private readonly NatsClient _Client;
        private Channel<double> _percentageWorkerChannel;

        public NatsRepository()
        {
            var url = Environment.GetEnvironmentVariable("NATS_URL") ?? "nats_demo:4222";
            var username = Environment.GetEnvironmentVariable("NATS_USERNAME") ?? "username";
            var password = Environment.GetEnvironmentVariable("NATS_PASSWORD") ?? "password";

            _Client = new NatsClient(new NatsOpts
            {
                Url = url,
                AuthOpts = new NatsAuthOpts
                {
                    Username = username,
                    Password = password,
                }
            });
            _percentageWorkerChannel = Channel.CreateUnbounded<double>();
        }
        ~NatsRepository()
        {
        }
        public async Task CreatePercentageWorker(string serviceName, string functionName, string serviceVersion, string serviceDescription)
        {
            var svc = _Client.CreateServicesContext();
            var service = await svc.AddServiceAsync(new NatsSvcConfig(serviceName, serviceVersion)
            {
                Description = serviceDescription
            });
            var root = await service.AddGroupAsync(serviceName, serviceVersion);
            await root.AddEndpointAsync(ReturnPercentage, functionName, serializer: NatsJsonSerializer<double>.Default);
            Console.WriteLine($"add {serviceName} service, version {serviceVersion}");

            await Task.Delay(int.MaxValue);

            async ValueTask ReturnPercentage(NatsSvcMsg<double> msg)
            {
                Console.WriteLine($"show on worker {msg.Data}");
                // todo, if not 1>=msg.Data>=-1, return fail
                await _percentageWorkerChannel.Writer.WriteAsync(msg.Data);
                await msg.ReplyAsync($"{msg.Data * 100}");
                _percentageWorkerChannel.Writer.Complete(); // 完成寫入
            }
        }
        public async Task<double> GetPercentageWorkerValue()
        {
            await foreach (var item in _percentageWorkerChannel.Reader.ReadAllAsync())
            {
                Console.WriteLine($"\t消費者讀取: {item}");
                break;
            }
            return 0.0;
        }
    }
}

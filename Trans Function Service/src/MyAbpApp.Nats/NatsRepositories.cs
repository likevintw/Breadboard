
using System;
using System.Threading.Tasks;
using NATS.Net;
using NATS.Client.Core;
using NATS.Client.Services;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Client.Serializers.Json;
using MyAbpApp.IQueueRepositories;

namespace MyAbpApp.NatsRepositories
{
    public class NatsRepository : IQueueRepository
    {
        private readonly NatsClient _Client;

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


            ValueTask ReturnPercentage(NatsSvcMsg<double> msg)
            {
                Console.WriteLine($"show on worker {msg.Data}");
                // todo, if not 1>=msg.Data>=-1, return fail
                return msg.ReplyAsync($"{msg.Data * 100}");
            }
        }
        public async Task CreateServiceHoneywellCe3245(
           string ServiceName, string FunctionName, string ServiceVersion, string ServiceDescription)
        {
            var svc = _Client.CreateServicesContext();

            var inputService = await svc.AddServiceAsync(new NatsSvcConfig(ServiceName, ServiceVersion)
            {
                Description = ServiceDescription
            });
            var root = await inputService.AddGroupAsync(ServiceName, ServiceVersion);
            await root.AddEndpointAsync(GetHoneywell_ce3245Compensation, FunctionName, serializer: NatsJsonSerializer<double>.Default);
            Console.WriteLine($"add {ServiceName} service, version {ServiceVersion}");

            await Task.Delay(int.MaxValue);


            ValueTask GetHoneywell_ce3245Compensation(NatsSvcMsg<double> msg)
            {
                Console.WriteLine($"show on worker {msg.Data}");
                //todo, to go pg
                return msg.ReplyAsync($"{msg.Data * 100}");
            }
        }
    }
}

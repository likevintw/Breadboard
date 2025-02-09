
using System;
using System.Threading;
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
        private Channel<double> _percentageWorkerChannel = Channel.CreateUnbounded<double>();

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
        public async Task CreatePercentageWorker(
            CancellationToken cancellationToken, string serviceName, string functionName, string serviceVersion, string serviceDescription)
        {
            var svc = _Client.CreateServicesContext();

            var service = await svc.AddServiceAsync(new NatsSvcConfig(serviceName, serviceVersion)
            {
                Description = serviceDescription
            });


            var root = await service.AddGroupAsync(serviceName, serviceVersion);
            await root.AddEndpointAsync(ReturnPercentage, functionName, serializer: NatsJsonSerializer<double>.Default);
            Console.WriteLine($"add {serviceName} service, version {serviceVersion}");


            try
            {
                // 使用 CancellationToken 在長時間等待時取消
                await Task.Delay(int.MaxValue, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // 當取消請求時，捕捉 OperationCanceledException 並處理取消邏輯
                Console.WriteLine("CreatePercentageWorker was canceled.");
            }


            async ValueTask ReturnPercentage(NatsSvcMsg<double> msg)
            {
                Console.WriteLine($"show on worker {msg.Data}");

                // 將數據寫入 channel
                await _percentageWorkerChannel.Writer.WriteAsync(msg.Data);

                // 進行回覆
                await msg.ReplyAsync($"{msg.Data * 100}");
            }
        }

        // public async Task GetPercentageWorkerValue(CancellationToken cancellationToken)
        // {
        //     double value = 0;

        //     // 這個外層循環會在 CancellationToken 被取消時退出
        //     while (!cancellationToken.IsCancellationRequested)
        //     {
        //         try
        //         {
        //             // 讀取 channel 的資料，這是非同步操作
        //             value = await _percentageWorkerChannel.Reader.ReadAsync(cancellationToken);  // 傳遞 CancellationToken 來響應取消請求
        //             Console.WriteLine($"Value {value} read from channel.");
        //         }
        //         catch (OperationCanceledException)
        //         {
        //             // 如果 cancellationToken 被取消，捕捉異常並退出循環
        //             Console.WriteLine("Reading from channel was canceled.");
        //             break;
        //         }
        //     }

        //     Console.WriteLine("GetPercentageWorkerValue has been canceled and is exiting.");

        // }
        public async Task<double> GetPercentageWorkerValue(CancellationToken cancellationToken)
        {
            double value = 0;

            // 這個外層循環會在 CancellationToken 被取消時退出
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    // 讀取 channel 的資料，這是非同步操作
                    value = await _percentageWorkerChannel.Reader.ReadAsync(cancellationToken);  // 傳遞 CancellationToken 來響應取消請求
                    Console.WriteLine($"Value {value} read from channel.");
                    return value;
                }
                catch (OperationCanceledException)
                {
                    // 如果 cancellationToken 被取消，捕捉異常並退出循環
                    Console.WriteLine("Reading from channel was canceled.");
                    break;
                }
            }

            Console.WriteLine("GetPercentageWorkerValue has been canceled and is exiting.");

            return 0.0;
        }
    }
}

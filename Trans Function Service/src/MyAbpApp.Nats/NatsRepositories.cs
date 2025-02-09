
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

            Console.WriteLine("PPPPPPPPPP1111");

            var service = await svc.AddServiceAsync(new NatsSvcConfig(serviceName, serviceVersion)
            {
                Description = serviceDescription
            });

            Console.WriteLine("PPPPPPPPPP22222");

            var root = await service.AddGroupAsync(serviceName, serviceVersion);

            Console.WriteLine("PPPPPPPPPP33333");

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

                // 假設需要檢查 msg.Data 是否在 -1 和 1 之間
                if (msg.Data < -1 || msg.Data > 1)
                {
                    // 如果不在範圍內，則返回錯誤
                    await msg.ReplyAsync("fail");
                    return;
                }

                // 將數據寫入 channel
                await _percentageWorkerChannel.Writer.WriteAsync(msg.Data);

                // 進行回覆
                await msg.ReplyAsync($"{msg.Data * 100}");
            }
        }
        // public aGetPercentageWorkerValue
        public async Task GetPercentageWorkerValue(CancellationToken cancellationToken)
        {
            double value = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                while (true)
                {
                    // 從 channel 讀取資料
                    value = await _percentageWorkerChannel.Reader.ReadAsync();
                    Console.WriteLine($"Value {value} read from channel.");
                }
            }

        }
    }
}

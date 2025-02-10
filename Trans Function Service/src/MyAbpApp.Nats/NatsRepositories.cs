
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;
using NATS.Net;
using NATS.Client.Core;
using NATS.Client.Services;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Client.Serializers.Json;
using MyAbpApp.IQueueRepositories;
using Volo.Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MyAbpApp.Compensations;
using MyAbpApp.EntityFrameworkCore;

namespace MyAbpApp.NatsRepositories
{
    public class NatsRepository : IQueueRepository
    {

        Guid compensationId = Guid.Parse("3a17ffc5-70ce-b0b3-6e8d-14b99adabe92");
        private NatsClient _Client;
        private readonly MyAbpAppDbContext _dbContext;
        private readonly IRepository<Compensation, Guid> _compensationRepository;  // 使用 IRepository
        private Channel<double> _percentageWorkerChannel = Channel.CreateUnbounded<double>();
        private Compensation compensation = null;

        public NatsRepository(MyAbpAppDbContext dbContext, IRepository<Compensation, Guid> compensationRepository)
        {
            _dbContext = dbContext;
            _compensationRepository = compensationRepository;

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
        public async Task CreateTemperatureUnitTransferWorker(
            CancellationToken cancellationToken, string serviceVersion, string serviceDescription)
        {
            string ServiceName = "TemeratureUnitTransfer";
            string FahrenheitToCelsius = "FahrenheitToCelsius";
            string CelsiusToFahrenheit = "CelsiusToFahrenheit";

            var svc = _Client.CreateServicesContext();
            var service = await svc.AddServiceAsync(new NatsSvcConfig(ServiceName, serviceVersion)
            {
                Description = serviceDescription
            });
            var root = await service.AddGroupAsync(ServiceName, serviceVersion);
            await root.AddEndpointAsync(TurnFahrenheitToCelsius, FahrenheitToCelsius, serializer: NatsJsonSerializer<double>.Default);
            await root.AddEndpointAsync(TurnCelsiusToFahrenheit, CelsiusToFahrenheit, serializer: NatsJsonSerializer<double>.Default);
            Console.WriteLine($"add {ServiceName} service, version {serviceVersion}");
            try
            {
                await Task.Delay(int.MaxValue, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("CreateTemperatureFahrenheitToCelsiusWorker was canceled.");
            }
            async ValueTask TurnFahrenheitToCelsius(NatsSvcMsg<double> msg)
            {
                Console.WriteLine($"show on worker {msg.Data}");
                double CelsiusDegree = Math.Round((msg.Data - 32) * 5 / 9, 2);
                await msg.ReplyAsync($"{CelsiusDegree}");
            }
            async ValueTask TurnCelsiusToFahrenheit(NatsSvcMsg<double> msg)
            {
                Console.WriteLine($"show on worker {msg.Data}");
                double FahrenheitDegree = Math.Round(msg.Data * 9 / 5 + 32, 2);
                await msg.ReplyAsync($"{FahrenheitDegree}");
            }
        }

        public async Task CreatePercentageWorker(
            CancellationToken cancellationToken, string serviceVersion, string serviceDescription)
        {
            string ServiceName = "PercentageHandler";
            string FunctionName = "ReturnPercentage";
            var svc = _Client.CreateServicesContext();
            var service = await svc.AddServiceAsync(new NatsSvcConfig(ServiceName, serviceVersion)
            {
                Description = serviceDescription
            });
            var root = await service.AddGroupAsync(ServiceName, serviceVersion);
            await root.AddEndpointAsync(ReturnPercentage, FunctionName, serializer: NatsJsonSerializer<double>.Default);
            Console.WriteLine($"add {ServiceName} service, version {serviceVersion}");
            try
            {
                await Task.Delay(int.MaxValue, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("CreatePercentageWorker was canceled.");
            }
            async ValueTask ReturnPercentage(NatsSvcMsg<double> msg)
            {
                Console.WriteLine($"show on worker {msg.Data}");
                await _percentageWorkerChannel.Writer.WriteAsync(msg.Data);
                await msg.ReplyAsync($"{msg.Data * 100}");
            }
        }
        public async Task CreateCompensationWorker(
            CancellationToken cancellationToken, string serviceVersion, string serviceDescription)
        {
            string ServiceName = "Compensator";
            string FunctionName = "ReturnCompensatedValue";
            var svc = _Client.CreateServicesContext();

            var service = await svc.AddServiceAsync(new NatsSvcConfig(ServiceName, serviceVersion)
            {
                Description = serviceDescription
            });


            var root = await service.AddGroupAsync(ServiceName, serviceVersion);
            await root.AddEndpointAsync(ReturnCompensatedValue, FunctionName, serializer: NatsJsonSerializer<double>.Default);
            Console.WriteLine($"add {ServiceName} service, version {serviceVersion}");

            try
            {
                // 使用 CancellationToken 在長時間等待時取消
                await Task.Delay(int.MaxValue, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // 當取消請求時，捕捉 OperationCanceledException 並處理取消邏輯
                Console.WriteLine("CreateCompensationWorker was canceled.");
            }

            async ValueTask ReturnCompensatedValue(NatsSvcMsg<double> msg)
            {
                Console.WriteLine($"show on worker {msg.Data}");
                // PG Dbcontext
                var compensation = await _compensationRepository.GetAsync(compensationId);
                Console.WriteLine($"CompensationDto ID: {compensation.Id}");
                Console.WriteLine($"CompensationDto Value: {compensation.CompensationValue}");

                // 將數據寫入 channel
                await _percentageWorkerChannel.Writer.WriteAsync(msg.Data + compensation.CompensationValue);

                // 進行回覆
                await msg.ReplyAsync($"{msg.Data + compensation.CompensationValue}");
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
        public async Task<double> GetCompensationWorkerValue(CancellationToken cancellationToken)
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
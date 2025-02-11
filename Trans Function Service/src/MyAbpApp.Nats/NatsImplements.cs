
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
using MyAbpApp.IWorkManagers;
using Volo.Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MyAbpApp.Compensations;
using MyAbpApp.ContexturalPhysicalQualities;
using MyAbpApp.EntityFrameworkCore;

namespace MyAbpApp.NatsImplements
{
    public class PhysicalQuality
    {
        public string? DeviceId { get; set; }
        public double? OriginalValue { get; set; }
        public double? ResultValue { get; set; }
        public string? Message { get; set; }
    }
    public class NatsImplement : IWorkManager
    {
        Guid compensationId = Guid.Parse("3a17ffc5-70ce-b0b3-6e8d-14b99adabe92");
        Guid deviceId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        private NatsClient _Client;
        private readonly IRepository<Compensation, Guid> _compensationRepository;  // 使用 IRepository
        private readonly IRepository<ContexturalPhysicalQuality, Guid> _contexturalPhysicalQualityRepository;  // 使用 IRepository
        private Compensation compensation = null;

        public NatsImplement(MyAbpAppDbContext dbContext, IRepository<Compensation, Guid> compensationRepository)
        {
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
        ~NatsImplement()
        {
        }

        public async Task CreateContexturalPhysicalQualityWorker(
            CancellationToken cancellationToken, string serviceVersion, string serviceDescription)
        {
            string ServiceName = "ContexturalPhysicalQualityService";
            string FunctionName = "ReturnContexturalPhysicalQualityValue";
            var svc = _Client.CreateServicesContext();

            var service = await svc.AddServiceAsync(new NatsSvcConfig(ServiceName, serviceVersion)
            {
                Description = serviceDescription
            });

            var root = await service.AddGroupAsync(ServiceName, serviceVersion);
            await root.AddEndpointAsync(ReturnCpqValue, FunctionName, serializer: NatsJsonSerializer<PhysicalQuality>.Default);
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

            async ValueTask ReturnCpqValue(NatsSvcMsg<PhysicalQuality> msg)
            {
                Console.WriteLine($"got {msg.Data.DeviceId}");
                Console.WriteLine($"got {msg.Data.OriginalValue}");

                Console.WriteLine($"{deviceId}");
                // var contexturalPhysicalQuality = await _contexturalPhysicalQualityRepository.FirstOrDefaultAsync(x => x.Id == deviceId);
                // if (contexturalPhysicalQuality == null)
                // {
                //     // 如果查詢不到資料，可以拋出自定義異常或返回錯誤
                //     // throw new UserFriendlyException("DeviceId not found.");
                //     Console.WriteLine($"No Device ID");
                // }
                // Console.WriteLine($"YYYYYYYYYY");
                // Console.WriteLine($"{contexturalPhysicalQuality.DeviceId}");
                // Console.WriteLine($"{contexturalPhysicalQuality.Process}");

                msg.Data.ResultValue = 99.99;
                await msg.ReplyAsync(msg.Data);
            }
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
            await root.AddEndpointAsync(ReturnCompensatedValue, FunctionName, serializer: NatsJsonSerializer<string>.Default);
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
            async ValueTask ReturnCompensatedValue(NatsSvcMsg<string> msg)
            {
                Console.WriteLine($"show on worker {msg.Data}");
                // PG Dbcontext
                var compensation = await _compensationRepository.GetAsync(compensationId);
                Console.WriteLine($"CompensationDto ID: {compensation.Id}");
                Console.WriteLine($"CompensationDto Value: {compensation.CompensationValue}");

                // 進行回覆
                await msg.ReplyAsync($"{msg.Data + compensation.CompensationValue}");
            }
        }

    }
}
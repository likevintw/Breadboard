// IoTDBService.cs

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Threading.Tasks;
using MyAbpApp.IQueueRepositories;

namespace MyAbpApp.NatsEventHandlers
{
    public class NatsEventHandler : IHostedService
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IIotRepository _iotRepository;
        private Channel<double> _percentageWorkerChannel;
        public CpqService(IQueueRepository queueRepository, IIotRepository iotRepository)
        {
            _queueRepository = queueRepository;
            _iotRepository = iotRepository;
            _percentageWorkerChannel = _queueRepository.GetPercentageWorkChannel();
        }
        ~CpqService()
        {
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => _queueRepository.CreatePercentageWorker("Percentager", "ReturnPercentage", "1.0.1", "transfer to percentage"));
            Task.Run(() => SubPercentageChannel());
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // 停止背景任務的邏輯
        }

        public async Task<NatsMicroservices> GetAllNatsMicroservice()
        {
            await Task.Delay(1);
            var result = new NatsMicroservice
            {
                ServiceName = "1",
                ServiceVersion = "1",
                ServiceDescription = "1",
                ServiceId = "1"
            };

            return new NatsMicroservices
            {
                Results = new NatsMicroservice[] { result }
            };
        }
        public async Task<(string result, string serviceId)> CreateServiceTasto_ks456(
            string ServiceName, string serviceVersion, string ServiceDescription)
        {
            await Task.Delay(1);
            string result = "todo";
            string serviceId = "todo";
            return (result, serviceId);
        }
        public async Task<(string result, string serviceId)> CreateServiceHoneywellCe3245(
            string ServiceName, string FunctionName, string ServiceVersion, string ServiceDescription)
        {
            await Task.Delay(1);
            string result = "todo";
            string serviceId = "todo";
            return (result, serviceId);
        }
        public async Task SubPercentageChannel(
            string ServiceName, string serviceVersion, string ServiceDescription)
        {

            Task.Run(async () =>
            {
                await foreach (var item in _percentageWorkerChannel.Reader.ReadAllAsync())
                {
                    Console.WriteLine($"SubPercentageChannel heart beat");
                    await Task.Delay(1000);
                }
                await foreach (var item in _percentageWorkerChannel.Reader.ReadAllAsync())
                {
                    Console.WriteLine($"\t消費者讀取: {item}");
                    await Task.Delay(220);
                }
            })
            string result = "todo";
            string serviceId = "todo";
        }

    }
}

// IoTDBService.cs

using System;
using System.Threading;
using System.Threading.Tasks;
using Apache.IoTDB;
using Apache.IoTDB.DataStructure;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using MyAbpApp.ICpqServices;
using MyAbpApp.IIotRepositories;
using MyAbpApp.IQueueRepositories;
using System.Runtime.CompilerServices;

namespace MyAbpApp.CpqServices
{
    public class CpqService : ICpqService, IHostedService
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IIotRepository _iotRepository;
        public CpqService(IQueueRepository queueRepository, IIotRepository iotRepository)
        {
            _queueRepository = queueRepository;
            _iotRepository = iotRepository;
        }
        ~CpqService()
        {
        }

        // public Task StartAsync(CancellationToken cancellationToken)
        // {
        // }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => _queueRepository.CreatePercentageWorker("Percentager", "ReturnPercentage", "1.0.1", "transfer to percentage"));
            // return Task.CompletedTask;
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
        public async Task<(string result, string serviceId)> CreateServicePercentage(
            string ServiceName, string serviceVersion, string ServiceDescription)
        {
            await Task.Delay(1);
            string result = "todo";
            string serviceId = "todo";
            return (result, serviceId);
        }
        public async Task<string> DeleteNatsService(string serviceId)
        {
            await Task.Delay(1);
            string result = "todo";
            return result;
        }

    }
}

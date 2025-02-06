// IoTDBService.cs

using System;
using System.Threading.Tasks;
using Apache.IoTDB;
using Apache.IoTDB.DataStructure;
using System.Collections.Generic;
using MyAbpApp.ICpqServices;
using MyAbpApp.IIotRepositories;
using MyAbpApp.IQueueRepositories;
using System.Runtime.CompilerServices;

namespace MyAbpApp.CpqServices
{
    public class CpqService : ICpqService
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
        public async Task<(string result, string serviceId)> CreateServiceHoneywell_ce3245(
            string ServiceName, string serviceVersion, string ServiceDescription)
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

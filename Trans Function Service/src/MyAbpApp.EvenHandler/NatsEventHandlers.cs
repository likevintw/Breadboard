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
using MyAbpApp.IIotRepositories;

namespace MyAbpApp.NatsEventHandlers
{
    public class NatsEventHandler : IHostedService
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IIotRepository _iotRepository;
        Channel<double> _percentageWorkerChannel;
        public NatsEventHandler(IQueueRepository queueRepository, IIotRepository iotRepository)
        {
            _queueRepository = queueRepository;
            _iotRepository = iotRepository;
            _percentageWorkerChannel = Channel.CreateUnbounded<double>();

        }
        ~NatsEventHandler()
        {
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"Run Background Process");
            Task.Run(() => _queueRepository.CreatePercentageWorker("Percentager", "ReturnPercentage", "1.0.1", "transfer to percentage"));
            await _queueRepository.GetPercentageWorkerValue();
            Console.WriteLine($"Run Background Process END");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // 停止背景任務的邏輯
        }

        public async Task SubPercentageChannel()
        {
            double value = 0.0;
            Console.WriteLine($"2222222222");
            while (true)  // Infinite loop
            {
                // value = _queueRepository.GetPercentageWorkerValue();
                // Console.WriteLine($"SubPercentageChannel got {value}");
                await _queueRepository.GetPercentageWorkerValue();
                await Task.Delay(2000);
            }

            Console.WriteLine($"33333333");
        }

    }
}

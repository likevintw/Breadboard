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
            SubPercentageChannel();
            Console.WriteLine($"Run Background Process END");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // 停止背景任務的邏輯
        }

        public async Task SubPercentageChannel()
        {

            Console.WriteLine($"2222222222");
            while (true)  // Infinite loop
            {
                Console.WriteLine($"SubPercentageChannel got {_queueRepository.GetPercentageWorkerValue()}");
                await Task.Delay(2000);
            }

            Console.WriteLine($"33333333");
        }

    }
}

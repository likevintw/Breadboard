// IoTDBService.cs

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Entities;
using MyAbpApp.IQueueRepositories;
using MyAbpApp.IIotRepositories;
using MyAbpApp.Products;

namespace MyAbpApp.NatsEventHandlers
{
    public class NatsEventHandler : IHostedService
    {
        private IQueueRepository? _queueRepository;
        private IIotRepository? _iotRepository;
        IRepository<Product, Guid> _productRepository;
        private Channel<double> _percentageWorkerChannel;
        private Task? _backgroundTask;
        private CancellationTokenSource? _cts;

        public NatsEventHandler(
            IQueueRepository queueRepository,
            IIotRepository iotRepository,
            IRepository<Product, Guid> productRepository)
        {
            _queueRepository = queueRepository;
            _iotRepository = iotRepository;
            _percentageWorkerChannel = Channel.CreateUnbounded<double>();
            _productRepository = productRepository;
        }
        ~NatsEventHandler()
        { }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // 使用 CancellationTokenSource 來管理取消信號
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            // 啟動背景任務，並在適當的地方處理取消邏輯
            _backgroundTask = Task.Run(async () =>
            {
                await BackgroundWork(_cts.Token);
            }, _cts.Token);

            Console.WriteLine("Background work started.");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // 停止背景任務
            if (_cts != null)
            {
                _cts.Cancel();
                await _backgroundTask;  // 等待背景任務完成
            }

            Console.WriteLine("Background work stopped.");
        }

        private async Task BackgroundWork(CancellationToken cancellationToken)
        {
            // 這裡處理實際的背景工作邏輯
            while (!cancellationToken.IsCancellationRequested)
            {
                // var CelsiusToFahrenheitTask = _queueRepository.CreateTemperatureCelsiusToFahrenheitWorker(cancellationToken, "1.4.1", "Celsius To Fahrenheit");
                var FahrenheitToCelsiusTask = _queueRepository.CreateTemperatureUnitTransferWorker(cancellationToken, "1.2.1", "Temperature Unit Transfer");
                var percentageWorkerTask = _queueRepository.CreatePercentageWorker(cancellationToken, "2.9.3", "Get Percentage Value");
                var compensationWorkerTask = _queueRepository.CreateCompensationWorker(cancellationToken, "3.2.6", "Get Compensated Value");
                var compensationWorkerValueTask = _queueRepository.GetCompensationWorkerValue(cancellationToken);

                // await Task.WhenAll(CelsiusToFahrenheitTask, FahrenheitToCelsiusTask, percentageWorkerTask, compensationWorkerTask, compensationWorkerValueTask);
                await Task.WhenAll(FahrenheitToCelsiusTask, percentageWorkerTask, compensationWorkerTask, compensationWorkerValueTask);

                _ = Task.Run(() => SubPercentageChannel(cancellationToken));
                await Task.Delay(1000, cancellationToken);
            }
        }

        public async Task SubPercentageChannel(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                double value = 0.0;
                // 改成有限次數循環而不是無窮循環
                value = await _queueRepository?.GetCompensationWorkerValue(cancellationToken);
                Console.WriteLine($"SubPercentageChannel got {value}");

                await Task.Delay(2000, cancellationToken);  // 等待2秒
            }
        }
    }

}

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
using MyAbpApp.IWorkManagers;
using MyAbpApp.IIotRepositories;

namespace MyAbpApp.BackgroundHandlers
{
    public class BackgroundHandler : IHostedService
    {
        private IWorkManager? _workManager;
        private IIotRepository? _iotRepository;
        private Channel<double> _percentageWorkerChannel;
        private Task? _backgroundTask;
        private CancellationTokenSource? _cts;

        public BackgroundHandler(
            IWorkManager queueRepository,
            IIotRepository iotRepository)
        {
            _workManager = queueRepository;
            _iotRepository = iotRepository;
            _percentageWorkerChannel = Channel.CreateUnbounded<double>();
        }
        ~BackgroundHandler()
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

                // var TemperatureUnitTransferTask = _workManager.CreateTemperatureUnitTransferWorker(cancellationToken, "1.2.1", "Temperature Unit Transfer");
                // var percentageWorkerTask = _workManager.CreatePercentageWorker(cancellationToken, "2.9.3", "Get Percentage Value");
                // var compensationWorkerTask = _workManager.CreateCompensationWorker(cancellationToken, "3.2.6", "Get Compensated Value");
                // var compensationWorkerValueTask = _workManager.GetCompensationWorkerValue(cancellationToken);
                var cpqTask = _workManager.CreateContexturalPhysicalQualityWorker(cancellationToken, "5.7.7", "Contextural Physical Quality Service");

                await Task.WhenAll(cpqTask);

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
                value = await _workManager?.GetCompensationWorkerValue(cancellationToken);
                Console.WriteLine($"SubPercentageChannel got {value}");

                await Task.Delay(2000, cancellationToken);  // 等待2秒
            }
        }
    }

}

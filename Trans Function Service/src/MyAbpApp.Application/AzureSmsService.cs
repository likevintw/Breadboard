
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Volo.Abp.DependencyInjection;
using MyAbpApp.ISmsServices;

namespace MyAbpApp.AzureSmsServices
{
    public class AzureSmsService : ISmsService, ITransientDependency
    {
        private readonly IConfiguration _configuration;
        public AzureSmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendAsync(string phoneNumber, string message)
        {
            //TODO: ..
            await Task.Delay(1);
            Console.WriteLine("RUN AZURE SMS SendAsync");
        }
    }
}
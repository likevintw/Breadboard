

using System.Diagnostics;
using NATS.Net;
using NATS.Client.Core;
using NATS.Client.JetStream.Models;
using NATS.Client.JetStream;
using NATS.Client.Serializers.Json;
using NATS.Client.Services;

namespace MockNatsClient
{
    public class PhysicalQuality
    {
        public string? DeviceId { get; set; }
        public double? OriginalValue { get; set; }
        public double? ResultValue { get; set; }
    }
    class Program
    {
        public static async Task CreateCompensatorProducer()
        {
            var url = Environment.GetEnvironmentVariable("NATS_URL") ?? "nats_demo:4222";
            var username = Environment.GetEnvironmentVariable("NATS_USERNAME") ?? "username";
            var password = Environment.GetEnvironmentVariable("NATS_PASSWORD") ?? "password";

            await using var nc = new NatsClient(new NatsOpts
            {
                Url = url,
                AuthOpts = new NatsAuthOpts
                {
                    Username = username,
                    Password = password,
                }
            });
            var svc = nc.CreateServicesContext();

            string serviceName = "Compensator";
            string functionName = "ReturnCompensatedValue";

            var result = await nc.RequestAsync<double, double>(subject: $"{serviceName}.{functionName}", data: 0);

            for (double i = 1; i < 150; i++)
            {
                i = Math.Round(i, 2);
                result = await nc.RequestAsync<double, double>(subject: $"{serviceName}.{functionName}", data: i);
                double roundedValue = Math.Round(result.Data, 2);
                Console.WriteLine($"Compensation {i} got reply: {roundedValue}");
                // Console.WriteLine("data type: " + result.Data.GetType());
                await Task.Delay(1600);
            }
            Console.WriteLine("Bye!");
        }
        public static async Task CreatePercentageHandlerProducer()
        {
            var url = Environment.GetEnvironmentVariable("NATS_URL") ?? "nats_demo:4222";
            var username = Environment.GetEnvironmentVariable("NATS_USERNAME") ?? "username";
            var password = Environment.GetEnvironmentVariable("NATS_PASSWORD") ?? "password";

            await using var nc = new NatsClient(new NatsOpts
            {
                Url = url,
                AuthOpts = new NatsAuthOpts
                {
                    Username = username,
                    Password = password,
                }
            });
            var svc = nc.CreateServicesContext();

            string serviceName = "PercentageHandler";
            string functionName = "ReturnPercentage";

            var result = await nc.RequestAsync<double, double>(subject: $"{serviceName}.{functionName}", data: 0);
            double messageData = 0.0;
            for (double i = 1; i < 100; i++)
            {
                messageData = Math.Round(i / 100, 2);
                result = await nc.RequestAsync<double, double>(subject: $"{serviceName}.{functionName}", data: messageData);
                double roundedValue = Math.Round(result.Data, 2);
                Console.WriteLine($"percentage {messageData} got reply: {roundedValue}%");
                // Console.WriteLine("data type: " + result.Data.GetType());
                await Task.Delay(1600);
            }
            Console.WriteLine("Bye!");
        }
        public static async Task CreateContexturalPhysicalQualityProducer()
        {
            var url = Environment.GetEnvironmentVariable("NATS_URL") ?? "nats_demo:4222";
            var username = Environment.GetEnvironmentVariable("NATS_USERNAME") ?? "username";
            var password = Environment.GetEnvironmentVariable("NATS_PASSWORD") ?? "password";

            await using var nc = new NatsClient(new NatsOpts
            {
                Url = url,
                AuthOpts = new NatsAuthOpts
                {
                    Username = username,
                    Password = password,
                }
            });
            var svc = nc.CreateServicesContext();

            string serviceName = "ContexturalPhysicalQualityService";
            string functionName = "ReturnContexturalPhysicalQualityValue";
            List<PhysicalQuality> testCases = new List<PhysicalQuality>();

            PhysicalQuality percetageOne = new PhysicalQuality
            {
                DeviceId = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                OriginalValue = 0.12,
                ResultValue = 0.0
            };
            testCases.Add(percetageOne);

            PhysicalQuality fahrenheitToCelsius = new PhysicalQuality
            {
                DeviceId = "8fa85f64-4562-4562-b3fc-2c963f66afa6",
                OriginalValue = 10.2,
                ResultValue = 0.0
            };
            testCases.Add(fahrenheitToCelsius);

            PhysicalQuality celsiusToFahrenheit = new PhysicalQuality
            {
                DeviceId = "d1c2c9fb-59f4-4b02-8c39-680b212a73e2",
                OriginalValue = 10.2,
                ResultValue = 0.0
            };
            testCases.Add(celsiusToFahrenheit);

            PhysicalQuality honeywellCompensation = new PhysicalQuality
            {
                DeviceId = "cd69ccf2-4f99-42bc-b5ad-281b8b3fdb61",
                OriginalValue = 10.2,
                ResultValue = 0.0
            };
            testCases.Add(honeywellCompensation);
            PhysicalQuality sonyCompensation = new PhysicalQuality
            {
                DeviceId = "62a37b23-3fd9-4042-88f2-5f8251a36f80",
                OriginalValue = 128.98,
                ResultValue = 0.0
            };
            testCases.Add(sonyCompensation);

            foreach (var testCaes in testCases)
            {
                var result = await nc.RequestAsync<PhysicalQuality, PhysicalQuality>(subject: $"{serviceName}.{functionName}", data: testCaes);
                Console.WriteLine($"ContexturalPhysicalQualityProducer got reply: {result.Data.DeviceId}");
                Console.WriteLine($"ContexturalPhysicalQualityProducer got reply: {result.Data.OriginalValue}");
                Console.WriteLine($"ContexturalPhysicalQualityProducer got reply: {result.Data.ResultValue}");
                Console.WriteLine("");
            }
            Console.WriteLine("Bye!");
        }
        public static async Task CreateFahrenheitToCelsiusProducer()
        {
            var url = Environment.GetEnvironmentVariable("NATS_URL") ?? "nats_demo:4222";
            var username = Environment.GetEnvironmentVariable("NATS_USERNAME") ?? "username";
            var password = Environment.GetEnvironmentVariable("NATS_PASSWORD") ?? "password";

            await using var nc = new NatsClient(new NatsOpts
            {
                Url = url,
                AuthOpts = new NatsAuthOpts
                {
                    Username = username,
                    Password = password,
                }
            });
            var svc = nc.CreateServicesContext();

            string serviceName = "TemeratureUnitTransfer";
            string functionName = "FahrenheitToCelsius";
            var result = await nc.RequestAsync<double, double>(subject: $"{serviceName}.{functionName}", data: 0);
            for (double i = 68; i < 120; i++)
            {
                i = Math.Round(i, 2);
                result = await nc.RequestAsync<double, double>(subject: $"{serviceName}.{functionName}", data: i);
                double roundedValue = Math.Round(result.Data, 2);
                Console.WriteLine($"F to C {i} got reply: {roundedValue}");
                // Console.WriteLine("data type: " + result.Data.GetType());
                await Task.Delay(1600);
            }
            Console.WriteLine("Bye!");
        }
        public static async Task CreateCelsiusToFahrenheitProducer()
        {
            var url = Environment.GetEnvironmentVariable("NATS_URL") ?? "nats_demo:4222";
            var username = Environment.GetEnvironmentVariable("NATS_USERNAME") ?? "username";
            var password = Environment.GetEnvironmentVariable("NATS_PASSWORD") ?? "password";

            await using var nc = new NatsClient(new NatsOpts
            {
                Url = url,
                AuthOpts = new NatsAuthOpts
                {
                    Username = username,
                    Password = password,
                }
            });
            var svc = nc.CreateServicesContext();

            string serviceName = "TemeratureUnitTransfer";
            string functionName = "CelsiusToFahrenheit";

            var result = await nc.RequestAsync<double, double>(subject: $"{serviceName}.{functionName}", data: 0);

            for (double i = 2; i < 30; i++)
            {
                i = Math.Round(i, 2);
                result = await nc.RequestAsync<double, double>(subject: $"{serviceName}.{functionName}", data: i);
                double roundedValue = Math.Round(result.Data, 2);
                Console.WriteLine($"C to F {i} got reply: {roundedValue}");
                // Console.WriteLine("data type: " + result.Data.GetType());
                await Task.Delay(1600);
            }
            Console.WriteLine("Bye!");
        }
        public static async Task Main(string[] args)
        {
            // var compensatorProducer = Task.Run(async () => await CreateCompensatorProducer());
            // var percentageProducer = Task.Run(async () => await CreatePercentageHandlerProducer());
            // var FahrenheitToCelsiusProducer = Task.Run(async () => await CreateFahrenheitToCelsiusProducer());
            // var CelsiusToFahrenheitProducer = Task.Run(async () => await CreateCelsiusToFahrenheitProducer());
            // await Task.WhenAll(compensatorProducer, percentageProducer, FahrenheitToCelsiusProducer, CelsiusToFahrenheitProducer);
            var CpqProducer = Task.Run(async () => await CreateContexturalPhysicalQualityProducer());
            await Task.WhenAll(CpqProducer);
        }
    }

}
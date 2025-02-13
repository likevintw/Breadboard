using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Net;
using Volo.Abp.DependencyInjection;

namespace IotDb.MeasurementManagement.BackgroundWorker
{
    public class ConnectionService : INatsConnection, ISingletonDependency
    {
        private readonly string defaultUrl;
        private readonly string userName;
        private readonly string password;
        private NatsClient? natsClient;
        public ConnectionService()
        {
            defaultUrl = Environment.GetEnvironmentVariable("NATS_URL") ?? "172.22.23.75:2222";
            // defaultUrl = Environment.GetEnvironmentVariable("NATS_URL") ?? "localhost:4222";
            userName = Environment.GetEnvironmentVariable("NATS_USERNAME") ?? "wils8wrh";
            password = Environment.GetEnvironmentVariable("NATS_PASSWORD") ?? "nsdjmckwn";
        }
        public NatsClient GetClient(NatsOpts opts)
        {
            return new NatsClient(opts);
        }

        public NatsClient GetClient(string url, string userName, string password)
        {
            if (natsClient != null)
            {
                return natsClient;
            }
            natsClient = new NatsClient(new NatsOpts
            {
                Url = url,
                AuthOpts = new NatsAuthOpts
                {
                    Username = userName,
                    Password = password
                }
            });
            return natsClient;
        }

        public NatsClient GetClient()
        {
            return GetClient(defaultUrl, userName, password);
        }
    }
}

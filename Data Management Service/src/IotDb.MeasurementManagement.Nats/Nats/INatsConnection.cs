using NATS.Client.Core;
using NATS.Net;

namespace IotDb.MeasurementManagement.BackgroundWorker
{
    public interface INatsConnection
    {
        public NatsClient GetClient();
        public NatsClient GetClient(string url, string userName, string password);
        public NatsClient GetClient(NatsOpts opts);
    }
}

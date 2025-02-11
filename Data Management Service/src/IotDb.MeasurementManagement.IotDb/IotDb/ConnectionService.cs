using Apache.IoTDB;
using IotDb.MeasurementManagement.IotDb.IotDb;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace IotDb.MeasurementManagement.IotDb
{
    public class ConnectionService : IIotDbConnection, ISingletonDependency
    {
        private readonly string defaultHost;
        private readonly int defaultPort;
        private readonly int defaultPoolSize;
        private SessionPool? sessionPool;
        private readonly IoTDBOptions iotDbOption;
        public ConnectionService(IOptions<IoTDBOptions> options)
        {
            iotDbOption = options.Value;
            defaultHost = iotDbOption.Host;
            defaultPort = iotDbOption.Port;
            defaultPoolSize = iotDbOption.PoolSize;
        }
        public SessionPool GetSessionPool(string host = "localhost", int port = 6667, int poolSize = 5)
        {

            if (sessionPool != null)
            {
                if (!sessionPool.IsOpen())
                {
                    sessionPool.Reconnect();
                }
                return sessionPool;
            }
            SessionPool session = new(host, port, poolSize);

            // Open Session
            session.Open(false).Wait();

            sessionPool = session;
            return session;
        }

        public SessionPool GetSessionPool()
        {
            return GetSessionPool(defaultHost, defaultPort, defaultPoolSize);
        }
    }
}

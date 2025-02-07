using Apache.IoTDB;
using Volo.Abp.DependencyInjection;

namespace IotDb.MeasurementManagement.IotDb
{
    public class ConnectionService : IIotDbConnection, ISingletonDependency
    {
        private const string defaultHost = "localhost";
        private const int defaultPort = 6667;
        private const int defaultPoolSize = 5;
        private SessionPool? sessionPool;
        public SessionPool GetSessionPool(string host = defaultHost, int port = defaultPort, int poolSize = defaultPoolSize)
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
            return GetSessionPool(defaultHost, defaultPort);
        }
    }
}

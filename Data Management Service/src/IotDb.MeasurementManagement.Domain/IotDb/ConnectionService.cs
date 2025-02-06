using Apache.IoTDB;

namespace IotDb.MeasurementManagement.IotDb
{
    public class ConnectionService : IIotDbConnection
    {
        private SessionPool? sessionPool;
        SessionPool IIotDbConnection.GetSessionPool(string host= "172.23.69.174", int port=6667, int poolSize=5)
        {

            if (sessionPool != null)
            {
                if (!sessionPool.IsOpen())
                {
                    sessionPool.Reconnect();
                }
                return sessionPool;
            }
            //const string hostSetting = host??"172.23.69.174";
            //const int portSetting = port??6667;
            //const int poolSize = 5;
            SessionPool session = new SessionPool(host, port, poolSize);

            // Open Session
            session.Open(false).Wait();

            sessionPool = session;
            return session;
        }
    }
}

using IotDb.MeasurementManagement.IotDb;
using IotDb.MeasurementManagement.IotDb.IotDb;
using Microsoft.Extensions.Options;
using Xunit;

namespace IotDb.MeasurementManagement.Tests.IotDb
{
    public class ConnectionServiceTest
    {
        private readonly IIotDbConnection iotDbConnection;

        public ConnectionServiceTest()
        {
            IOptions<IoTDBOptions> option = Options.Create(new IoTDBOptions() { Host = "localhost", Password = "root", PoolSize = 5, Port = 6667, Username = "root" });
            this.iotDbConnection = new ConnectionService(option);
        }

        [Fact]
        public void GetSessionTest()
        {
            var session = iotDbConnection.GetSessionPool("localhost", 6667, 5);
            Assert.NotNull(session);
            Assert.True(true == session.IsOpen());
        }

        [Fact]
        public void GetSessionWithoutParamTest()
        {
            var session = iotDbConnection.GetSessionPool();
            Assert.NotNull(session);
            Assert.True(true == session.IsOpen());
        }
    }
}

using IotDb.MeasurementManagement.IotDb;
using Xunit;

namespace IotDb.MeasurementManagement.Tests.IotDb
{
    public class ConnectionServiceTest
    {
        private readonly IIotDbConnection iotDbConnection;

        public ConnectionServiceTest()
        {
            this.iotDbConnection = new ConnectionService();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Xunit;

namespace IotDb.MeasurementManagement.IotDb
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
            Assert.True(true==session.IsOpen());
        }
    }
}

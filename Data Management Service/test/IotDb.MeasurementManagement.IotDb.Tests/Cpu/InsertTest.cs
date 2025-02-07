using Apache.IoTDB;
using IotDb.MeasurementManagement.IotDb;
using Xunit;

namespace IotDb.MeasurementManagement.Cpu
{
    public class InsertTest
    {
        private readonly IIotDbConnection _connection;

        public InsertTest()
        {
            _connection = new ConnectionService();
        }

        [Fact]
        public async Task InsertTestAsync()
        {
            SessionPool sessionPool = _connection.GetSessionPool();
            int cnt= await sessionPool.InsertAlignedRecordAsync("root.device1", new Apache.IoTDB.DataStructure.RowRecord(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), [2F], ["moisture"]));
            Assert.Equal(1, cnt);
        }
    }
}

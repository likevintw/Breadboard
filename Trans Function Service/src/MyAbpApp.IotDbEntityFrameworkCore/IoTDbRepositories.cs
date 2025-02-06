// IotDbRepositories

using System;
using System.Threading.Tasks;
using Apache.IoTDB;
using Apache.IoTDB.DataStructure;
using MyAbpApp.IIotRepositories;
namespace MyAbpApp.IoTDbRepositories
{
    public class IoTDbRepository : IIotRepository
    {
        private readonly SessionPool _iotSessionPool;

        public IoTDbRepository()
        {
            var host = Environment.GetEnvironmentVariable("IOTDB_URL") ?? "localhost";
            var port = int.Parse(Environment.GetEnvironmentVariable("IOTDB_PORT") ?? "6667");
            var username = Environment.GetEnvironmentVariable("IOTDB_USERNAME") ?? "username";
            var password = Environment.GetEnvironmentVariable("IOTDB_PASSWORD") ?? "password";

            int pool_size = 2;
            var session_pool = new SessionPool(host, port, pool_size);

            session_pool.Open(false);
            _iotSessionPool = session_pool;
        }
        ~IoTDbRepository()
        {
            _iotSessionPool.Close();
        }
        public async Task InsertAsync(string database, long timestamp, List<object> values, List<string> measurements)
        {
            try
            {
                DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
                var rowRecord = new RowRecord(dateTime, values, measurements);
                await _iotSessionPool.InsertRecordAsync(database, rowRecord);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting data to IoTDB: {ex.Message}");
            }
        }
    }
}

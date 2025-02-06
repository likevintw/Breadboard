// IoTDBService.cs

using System;
using System.Threading.Tasks;
using Apache.IoTDB;
using Apache.IoTDB.DataStructure;
using System.Collections.Generic;
using MyAbpApp.IGyroscopeServices;
using MyAbpApp.IIotRepositories;
namespace MyAbpApp.GyroscopeServices
{
    public class GyroscopeService : IGyroscopeService
    {
        private readonly IIotRepository _iotRepository;
        public GyroscopeService(IIotRepository iotRepository)
        {
            _iotRepository = iotRepository;
        }

        public async Task InsertDataAsync(string database, long timestamp, double value, string measure)
        {
            try
            {
                var measurements = new List<string> { measure };
                var values = new List<object> { value };
                // DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
                Console.WriteLine($"{timestamp}");
                Console.WriteLine($"{values}");
                Console.WriteLine($"{measurements}");
                // var rowRecord = new RowRecord(dateTime, values, measurements);
                await _iotRepository.InsertAsync(database, timestamp, values, measurements);
                Console.WriteLine($"Inser IotDB, ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting data to IoTDB: {ex.Message}");
            }
        }

    }
}

using IotDb.MeasurementManagement.Cpu;
using IotDb.MeasurementManagement.IotDb.IotDb;
using IotDb.MeasurementManagement.Moisture;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Volo.Abp.Modularity;
using Xunit;

namespace IotDb.MeasurementManagement.IotDb.Tests.IotDb
{
    public class IotDbInsertRepositoryTest
    {
        private readonly IIotDbRepository<SoilMoisture> repository;

        public IotDbInsertRepositoryTest()
        {
            Mock<ILogger<IotDbRepository<SoilMoisture>>> logger = new();
            IOptions<IoTDBOptions> options = Options.Create(new IoTDBOptions() { Host = "localhost", Password = "root", PoolSize = 5, Port = 6667, Username = "root" });
            this.repository = new IotDbRepository<SoilMoisture>(new ConnectionService(options), logger.Object);
        }

        [Fact]
        public void InsertTest()
        {
            int rtn = repository.Insert("root.device1", new SoilMoisture()
            {
                Time = DateTime.Now,
                Timeseries = "root.device1.moisture",
                Value = 4F
            }).Result;
            Assert.Equal(0, rtn);
        }
    }
}

using IotDb.MeasurementManagement.IotDb.IotDb;
using IotDb.MeasurementManagement.Moisture;
using Volo.Abp.Modularity;
using Xunit;

namespace IotDb.MeasurementManagement.IotDb.Tests.IotDb
{
    public class IotDbInsertRepositoryTest
    {
        private readonly IIotDbInsertRepository<SoilMoisture> repository;

        public IotDbInsertRepositoryTest()
        {
            this.repository = new IotDbInsertRepository<SoilMoisture>(new ConnectionService());
        }

        [Fact]
        public void InsertTest()
        {
            int rtn = repository.Insert(new SoilMoisture()
            {
                Time = DateTime.Now,
                Timeseries = "root.device1.moisture",
                Value = 2F
            }).Result;
            Assert.Equal(0, rtn);
        }
    }
}

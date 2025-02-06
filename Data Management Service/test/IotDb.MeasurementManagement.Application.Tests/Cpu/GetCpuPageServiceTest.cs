using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;
using Volo.Abp.Testing;
using Xunit;

namespace IotDb.MeasurementManagement.Cpu
{
    public class GetCpuPageServiceTest : AbpIntegratedTest<MeasurementManagementApplicationModule>
    {
        private readonly IGetCpuService _getCpuService;
        //private readonly Mock<IRepository<CpuLoad>> repository;
        private readonly Mock<IAsyncQueryableExecuter> asyncQueryableExecuter;
        private readonly DateTime now = DateTime.Now;

        public GetCpuPageServiceTest()
        {
            //repository = new Mock<IRepository<CpuLoad>>();
            //asyncQueryableExecuter = new Mock<IAsyncQueryableExecuter>();
            //List<CpuLoad> cpuLoads = [new() { Time = now, TimeSeries = "" }];
            //repository.Setup(rep => rep.GetQueryableAsync()).ReturnsAsync(cpuLoads.AsQueryable);
            //asyncQueryableExecuter
            //    .Setup(rep => rep.ToListAsync(It.IsAny<IQueryable<CpuLoad>>(), It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(cpuLoads);
            //_getCpuService = new GetCpuService(null);
        }
        [Fact]
        public async Task GetCpuLoad()
        {
            GetCpuPageByTimeRequest request = new();
            PagedResultDto<CpuLoadDto> dto = await _getCpuService.GetCpuPageByTime(request);
            Assert.Equal(now, dto.Items[0].Time);
        }
    }
}

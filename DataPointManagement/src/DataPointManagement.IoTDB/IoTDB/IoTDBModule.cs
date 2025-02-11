
// TODO: IoTDB/IoTDBModule.cs

using DataPointManagement.DataPoints;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace DataPointManagement.IoTDB;

[DependsOn(
    typeof(DataPointManagementDomainModule)
    )]
public class IoTDBModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<IoTDBOptions>(configuration.GetSection("IoTDB"));
        context.Services.AddTransient(typeof(IDataPointRepository<>), typeof(IoTDBDataPointsRepository<>));
    }

}



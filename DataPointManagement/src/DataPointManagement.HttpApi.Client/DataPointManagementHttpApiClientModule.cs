using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace DataPointManagement;

[DependsOn(
    typeof(DataPointManagementApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class DataPointManagementHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DataPointManagementApplicationContractsModule).Assembly,
            DataPointManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DataPointManagementHttpApiClientModule>();
        });

    }
}

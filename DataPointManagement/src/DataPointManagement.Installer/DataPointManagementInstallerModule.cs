using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace DataPointManagement;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class DataPointManagementInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DataPointManagementInstallerModule>();
        });
    }
}

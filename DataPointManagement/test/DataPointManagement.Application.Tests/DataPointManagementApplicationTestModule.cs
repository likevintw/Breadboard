using Volo.Abp.Modularity;

namespace DataPointManagement;

[DependsOn(
    typeof(DataPointManagementApplicationModule),
    typeof(DataPointManagementDomainTestModule)
    )]
public class DataPointManagementApplicationTestModule : AbpModule
{

}

using Volo.Abp.Modularity;

namespace DataPointManagement;

[DependsOn(
    typeof(DataPointManagementDomainModule),
    typeof(DataPointManagementTestBaseModule)
)]
public class DataPointManagementDomainTestModule : AbpModule
{

}

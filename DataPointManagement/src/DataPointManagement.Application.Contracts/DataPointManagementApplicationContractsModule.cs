using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace DataPointManagement;

[DependsOn(
    typeof(DataPointManagementDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class DataPointManagementApplicationContractsModule : AbpModule
{

}

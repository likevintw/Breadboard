using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace DataPointManagement;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DataPointManagementDomainSharedModule)
)]
public class DataPointManagementDomainModule : AbpModule
{

}

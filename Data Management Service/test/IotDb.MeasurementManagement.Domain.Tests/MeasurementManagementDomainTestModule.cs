using Volo.Abp.Modularity;

namespace IotDb.MeasurementManagement;

[DependsOn(
    typeof(MeasurementManagementDomainModule),
    typeof(MeasurementManagementTestBaseModule)
)]
public class MeasurementManagementDomainTestModule : AbpModule
{

}

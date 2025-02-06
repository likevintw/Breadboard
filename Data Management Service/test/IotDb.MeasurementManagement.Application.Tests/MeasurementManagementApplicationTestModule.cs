using Volo.Abp.Modularity;

namespace IotDb.MeasurementManagement;

[DependsOn(
    typeof(MeasurementManagementApplicationModule),
    typeof(MeasurementManagementDomainTestModule)
)]
public class MeasurementManagementApplicationTestModule : AbpModule
{

}

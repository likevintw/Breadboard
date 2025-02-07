using Volo.Abp.Modularity;

namespace IotDb.MeasurementManagement;

[DependsOn(
    typeof(MeasurementManagementIobDbModule),
    typeof(MeasurementManagementTestBaseModule)
)]
public class MeasurementManagementIobDbModule : AbpModule
{

}

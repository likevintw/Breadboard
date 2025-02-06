using Volo.Abp.Modularity;

namespace IotDb.MeasurementManagement;

public abstract class MeasurementManagementApplicationTestBase<TStartupModule> : MeasurementManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

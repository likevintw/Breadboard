using Volo.Abp.Modularity;

namespace IotDb.MeasurementManagement;

/* Inherit from this class for your iotdb layer tests. */
public abstract class MeasurementManagementIotDbTestBase<TStartupModule> : MeasurementManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

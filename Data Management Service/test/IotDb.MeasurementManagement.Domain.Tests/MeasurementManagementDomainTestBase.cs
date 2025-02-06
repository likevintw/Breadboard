using Volo.Abp.Modularity;

namespace IotDb.MeasurementManagement;

/* Inherit from this class for your domain layer tests. */
public abstract class MeasurementManagementDomainTestBase<TStartupModule> : MeasurementManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

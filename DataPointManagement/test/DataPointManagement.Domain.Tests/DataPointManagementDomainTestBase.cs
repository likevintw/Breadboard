using Volo.Abp.Modularity;

namespace DataPointManagement;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class DataPointManagementDomainTestBase<TStartupModule> : DataPointManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

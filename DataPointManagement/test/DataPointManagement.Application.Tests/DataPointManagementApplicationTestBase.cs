using Volo.Abp.Modularity;

namespace DataPointManagement;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class DataPointManagementApplicationTestBase<TStartupModule> : DataPointManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

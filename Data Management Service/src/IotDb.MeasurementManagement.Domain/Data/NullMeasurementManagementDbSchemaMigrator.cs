using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace IotDb.MeasurementManagement.Data;

/* This is used if database provider does't define
 * IMeasurementManagementDbSchemaMigrator implementation.
 */
public class NullMeasurementManagementDbSchemaMigrator : IMeasurementManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

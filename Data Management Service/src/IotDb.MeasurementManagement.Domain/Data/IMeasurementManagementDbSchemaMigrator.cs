using System.Threading.Tasks;

namespace IotDb.MeasurementManagement.Data;

public interface IMeasurementManagementDbSchemaMigrator
{
    Task MigrateAsync();
}

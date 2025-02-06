using IotDb.MeasurementManagement.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace IotDb.MeasurementManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MeasurementManagementEntityFrameworkCoreModule),
    typeof(MeasurementManagementApplicationContractsModule)
)]
public class MeasurementManagementDbMigratorModule : AbpModule
{
}

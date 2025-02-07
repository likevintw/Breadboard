using IotDb.MeasurementManagement.Cpu;
using IotDb.MeasurementManagement.IotDb;
using IotDb.MeasurementManagement.Moisture;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace IotDb.MeasurementManagement.EntityFrameworkCore;

[DependsOn(
    typeof(MeasurementManagementDomainModule)
    //typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    //typeof(AbpSettingManagementEntityFrameworkCoreModule),
    //typeof(AbpEntityFrameworkCorePostgreSqlModule),
    //typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    //typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    //typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    //typeof(AbpIdentityEntityFrameworkCoreModule),
    //typeof(AbpOpenIddictEntityFrameworkCoreModule),
    //typeof(AbpTenantManagementEntityFrameworkCoreModule),
    //typeof(BlobStoringDatabaseEntityFrameworkCoreModule)
    )]
public class MeasurementManagementIotDbModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        //MeasurementManagementEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IIotDbQueryRepository<CpuLoad>, IotDbQueryRepository<CpuLoad>>();
        context.Services.AddTransient<IIotDbQueryRepository<SoilMoisture>, IotDbQueryRepository<SoilMoisture>>();
        //context.Services.AddAbpDbContext<MeasurementManagementDbContext>(options =>
        //{
        //        /* Remove "includeAllEntities: true" to create
        //         * default repositories only for aggregate roots */
        //    options.AddDefaultRepositories(includeAllEntities: true);
        //});

        //Configure<AbpDbContextOptions>(options =>
        //{
        //        /* The main point to change your DBMS.
        //         * See also MeasurementManagementDbContextFactory for EF Core tooling. */
        //    options.UseNpgsql();
        //});

    }
}

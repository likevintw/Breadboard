using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp;
using Microsoft.Extensions.DependencyInjection;
using IotDb.MeasurementManagement.BackgroundWorker;

namespace IotDb.MeasurementManagement;

[DependsOn(
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpBackgroundWorkersModule)
    )]
public class MeasurementManagementNatsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<INatsConnection, ConnectionService>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MeasurementManagementNatsModule>();
        });
    }
}

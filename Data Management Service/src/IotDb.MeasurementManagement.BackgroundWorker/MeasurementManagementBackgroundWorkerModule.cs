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
using IotDb.MeasurementManagement.BackgroundWorker;
using Microsoft.Extensions.DependencyInjection;
using IotDb.MeasurementManagement.BackgroundWorker.Workers;

namespace IotDb.MeasurementManagement;

[DependsOn(
    typeof(MeasurementManagementDomainModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpBackgroundWorkersModule)
    )]
public class MeasurementManagementBackgroundWorkerModule : AbpModule
{
    public override async Task OnApplicationInitializationAsync(
        ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<SubscribeCpuWorker>();
    }
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<INatsConnection, ConnectionService>();
        // var configuration = context.Services.GetConfiguration();
        // Configure<NatsOptions>(configuration.GetSection("NATS"));
        // Configure<AbpAutoMapperOptions>(options =>
        // {
        //     options.AddMaps<MeasurementManagementBackgroundWorkerModule>();
        // });
    }
}

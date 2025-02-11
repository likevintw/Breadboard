using Localization.Resources.AbpUi;
using DataPointManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace DataPointManagement;

[DependsOn(
    typeof(DataPointManagementApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class DataPointManagementHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DataPointManagementHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DataPointManagementResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}


using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyAbpApp.EntityFrameworkCore;
using MyAbpApp.Localization;
using MyAbpApp.MultiTenancy;
using MyAbpApp.Permissions;
using MyAbpApp.Web.Menus;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.Studio;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Identity.Web;
using Volo.Abp.FeatureManagement;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp.TenantManagement.Web;
using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Identity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.OpenIddict;
using Volo.Abp.Security.Claims;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Studio.Client.AspNetCore;

using MyAbpApp.ISmsServices;
using MyAbpApp.AzureSmsServices;
using MyAbpApp.GyroscopeServices;
using MyAbpApp.IGyroscopeServices;
using MyAbpApp.IIotRepositories;
using MyAbpApp.IoTDbRepositories;
using MyAbpApp.IQueueRepositories;
using MyAbpApp.NatsRepositories;
using MyAbpApp.NatsEventHandlers;
namespace MyAbpApp.Web;

[DependsOn(
    typeof(MyAbpAppHttpApiModule),
    typeof(MyAbpAppApplicationModule),
    typeof(MyAbpAppEntityFrameworkCoreModule),
    typeof(AbpAutofacModule),
    typeof(AbpStudioClientAspNetCoreModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpAspNetCoreMvcUiBasicThemeModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpTenantManagementWebModule),
    typeof(AbpFeatureManagementWebModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule)
)]
public class MyAbpAppWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(MyAbpAppResource),
                typeof(MyAbpAppDomainModule).Assembly,
                typeof(MyAbpAppDomainSharedModule).Assembly,
                typeof(MyAbpAppApplicationModule).Assembly,
                typeof(MyAbpAppApplicationContractsModule).Assembly,
                typeof(MyAbpAppWebModule).Assembly
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("MyAbpApp");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });

        if (!hostingEnvironment.IsDevelopment())
        {
            PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
            {
                options.AddDevelopmentEncryptionAndSigningCertificate = false;
            });

            PreConfigure<OpenIddictServerBuilder>(serverBuilder =>
            {
                serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx", configuration["AuthServer:CertificatePassPhrase"]!);
                serverBuilder.SetIssuer(new Uri(configuration["AuthServer:Authority"]!));
            });
        }
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<ISmsService, AzureSmsService>();
        context.Services.AddSingleton<IGyroscopeService, GyroscopeService>();
        context.Services.AddSingleton<IIotRepository, IoTDbRepository>();
        context.Services.AddSingleton<IQueueRepository, NatsRepository>();
        context.Services.AddHostedService<NatsEventHandler>();
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        if (!configuration.GetValue<bool>("App:DisablePII"))
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.LogCompleteSecurityArtifact = true;
        }

        if (!configuration.GetValue<bool>("AuthServer:RequireHttpsMetadata"))
        {
            Configure<OpenIddictServerAspNetCoreOptions>(options =>
            {
                options.DisableTransportSecurityRequirement = true;
            });

            Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
            });
        }

        ConfigureBundles();
        ConfigureUrls(configuration);
        ConfigureAuthentication(context);
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureNavigationServices();
        ConfigureAutoApiControllers();
        ConfigureSwaggerServices(context.Services);

        Configure<PermissionManagementOptions>(options =>
        {
            options.IsDynamicPermissionStoreEnabled = true;
        });
    }


    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                BasicThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-scripts.js");
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MyAbpAppWebModule>();
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MyAbpAppWebModule>();

            if (hostingEnvironment.IsDevelopment())
            {
                options.FileSets.ReplaceEmbeddedByPhysical<MyAbpAppDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}MyAbpApp.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyAbpAppDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}MyAbpApp.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyAbpAppApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}MyAbpApp.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyAbpAppApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}MyAbpApp.Application", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyAbpAppHttpApiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}MyAbpApp.HttpApi", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyAbpAppWebModule>(hostingEnvironment.ContentRootPath);
            }
        });
    }

    private void ConfigureNavigationServices()
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new MyAbpAppMenuContributor());
        });

        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new MyAbpAppToolbarContributor());
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(MyAbpAppApplicationModule).Assembly);
        });
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAbpApp API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }


    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        app.UseForwardedHeaders();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
            app.UseHsts();
        }

        app.UseCorrelationId();
        app.MapAbpStaticAssets();
        app.UseAbpStudioLink();
        app.UseRouting();
        app.UseAbpSecurityHeaders();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAbpApp API");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}

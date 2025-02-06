using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using MyAbpApp.Localization;

namespace MyAbpApp.Web;

[Dependency(ReplaceServices = true)]
public class MyAbpAppBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<MyAbpAppResource> _localizer;

    public MyAbpAppBrandingProvider(IStringLocalizer<MyAbpAppResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}

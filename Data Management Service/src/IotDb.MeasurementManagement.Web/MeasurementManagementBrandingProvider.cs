using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using IotDb.MeasurementManagement.Localization;

namespace IotDb.MeasurementManagement.Web;

[Dependency(ReplaceServices = true)]
public class MeasurementManagementBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<MeasurementManagementResource> _localizer;

    public MeasurementManagementBrandingProvider(IStringLocalizer<MeasurementManagementResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}

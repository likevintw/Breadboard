using IotDb.MeasurementManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace IotDb.MeasurementManagement.Web.Pages;

public abstract class MeasurementManagementPageModel : AbpPageModel
{
    protected MeasurementManagementPageModel()
    {
        LocalizationResourceType = typeof(MeasurementManagementResource);
    }
}

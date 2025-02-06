using IotDb.MeasurementManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace IotDb.MeasurementManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MeasurementManagementController : AbpControllerBase
{
    protected MeasurementManagementController()
    {
        LocalizationResource = typeof(MeasurementManagementResource);
    }
}

using IotDb.MeasurementManagement.Localization;
using Volo.Abp.Application.Services;

namespace IotDb.MeasurementManagement;

/* Inherit your application services from this class.
 */
public abstract class MeasurementManagementAppService : ApplicationService
{
    protected MeasurementManagementAppService()
    {
        LocalizationResource = typeof(MeasurementManagementResource);
    }
}

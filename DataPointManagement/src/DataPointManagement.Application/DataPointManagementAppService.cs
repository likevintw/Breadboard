using DataPointManagement.Localization;
using Volo.Abp.Application.Services;

namespace DataPointManagement;

public abstract class DataPointManagementAppService : ApplicationService
{
    protected DataPointManagementAppService()
    {
        LocalizationResource = typeof(DataPointManagementResource);
        ObjectMapperContext = typeof(DataPointManagementApplicationModule);
    }
}

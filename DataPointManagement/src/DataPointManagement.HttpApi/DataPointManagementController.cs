using DataPointManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DataPointManagement;

public abstract class DataPointManagementController : AbpControllerBase
{
    protected DataPointManagementController()
    {
        LocalizationResource = typeof(DataPointManagementResource);
    }
}

using DataPointManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DataPointManagement.Permissions;

public class DataPointManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DataPointManagementPermissions.GroupName, L("Permission:DataPointManagement"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DataPointManagementResource>(name);
    }
}

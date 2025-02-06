using IotDb.MeasurementManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace IotDb.MeasurementManagement.Permissions;

public class MeasurementManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MeasurementManagementPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(MeasurementManagementPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MeasurementManagementResource>(name);
    }
}

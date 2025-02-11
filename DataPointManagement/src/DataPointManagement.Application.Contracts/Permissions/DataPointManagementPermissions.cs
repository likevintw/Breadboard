using Volo.Abp.Reflection;

namespace DataPointManagement.Permissions;

public class DataPointManagementPermissions
{
    public const string GroupName = "DataPointManagement";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DataPointManagementPermissions));
    }
}

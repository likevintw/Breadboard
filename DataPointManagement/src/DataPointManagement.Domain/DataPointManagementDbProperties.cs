namespace DataPointManagement;

public static class DataPointManagementDbProperties
{
    public static string DbTablePrefix { get; set; } = "DataPointManagement";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "DataPointManagement";
}


// TODO: IoTDB/IoTDB/IoTdbOptions.cs

namespace DataPointManagement;

public class IoTDBOptions
{
    public required string Host { get; set; }
    public int Port { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

    public required int PoolSize { get; set; }

}
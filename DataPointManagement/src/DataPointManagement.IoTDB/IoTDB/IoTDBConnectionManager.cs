
// TODO: IoTDB/IoTDBConnectionManager.cs

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Apache.IoTDB;
using System.Diagnostics;

namespace DataPointManagement.IoTDB;

public interface IIoTDBConnectionManager
{
    Task<SessionPool> GetSessionPoolAsync(); // Assuming your client has a connection mechanism
    Task CloseClientAsync(SessionPool sessionPool);
}

public class IoTDBConnectionManager : IIoTDBConnectionManager, ITransientDependency
{
    private readonly IoTDBOptions _options;
    private readonly ILogger<IoTDBConnectionManager> _logger;

    public IoTDBConnectionManager(IOptions<IoTDBOptions> options, ILogger<IoTDBConnectionManager> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public async Task<SessionPool> GetSessionPoolAsync()
    {
        var session_pool = new SessionPool(_options.Host, _options.Port, _options.Username, _options.Password);
        await session_pool.Open(false);

        return session_pool;
    }
    public async Task CloseClientAsync(SessionPool sessionPool)
    {
        // Logic to close the client connection
        await sessionPool.Close();
    }
}
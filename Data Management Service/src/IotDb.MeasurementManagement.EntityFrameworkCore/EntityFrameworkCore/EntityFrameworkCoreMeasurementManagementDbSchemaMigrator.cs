using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IotDb.MeasurementManagement.Data;
using Volo.Abp.DependencyInjection;

namespace IotDb.MeasurementManagement.EntityFrameworkCore;

public class EntityFrameworkCoreMeasurementManagementDbSchemaMigrator
    : IMeasurementManagementDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMeasurementManagementDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MeasurementManagementDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MeasurementManagementDbContext>()
            .Database
            .MigrateAsync();
    }
}

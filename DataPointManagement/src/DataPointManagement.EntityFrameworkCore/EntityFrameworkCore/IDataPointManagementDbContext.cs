using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DataPointManagement.EntityFrameworkCore;

[ConnectionStringName(DataPointManagementDbProperties.ConnectionStringName)]
public interface IDataPointManagementDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}

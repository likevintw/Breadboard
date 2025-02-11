using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DataPointManagement.EntityFrameworkCore;

[ConnectionStringName(DataPointManagementDbProperties.ConnectionStringName)]
public class DataPointManagementDbContext : AbpDbContext<DataPointManagementDbContext>, IDataPointManagementDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DataPointManagementDbContext(DbContextOptions<DataPointManagementDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDataPointManagement();
    }
}

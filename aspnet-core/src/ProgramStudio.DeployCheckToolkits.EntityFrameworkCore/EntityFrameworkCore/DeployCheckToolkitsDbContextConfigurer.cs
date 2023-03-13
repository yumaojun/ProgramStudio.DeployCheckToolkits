using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ProgramStudio.DeployCheckToolkits.EntityFrameworkCore
{
    public static class DeployCheckToolkitsDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DeployCheckToolkitsDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<DeployCheckToolkitsDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ProgramStudio.DeployCheckToolkits.Configuration;
using ProgramStudio.DeployCheckToolkits.Web;

namespace ProgramStudio.DeployCheckToolkits.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class DeployCheckToolkitsDbContextFactory : IDesignTimeDbContextFactory<DeployCheckToolkitsDbContext>
    {
        public DeployCheckToolkitsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DeployCheckToolkitsDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DeployCheckToolkitsDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DeployCheckToolkitsConsts.ConnectionStringName));

            return new DeployCheckToolkitsDbContext(builder.Options);
        }
    }
}

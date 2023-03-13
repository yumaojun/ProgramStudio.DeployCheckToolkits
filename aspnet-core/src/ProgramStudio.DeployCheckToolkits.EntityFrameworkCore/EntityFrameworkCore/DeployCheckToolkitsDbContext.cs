using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ProgramStudio.DeployCheckToolkits.Authorization.Roles;
using ProgramStudio.DeployCheckToolkits.Authorization.Users;
using ProgramStudio.DeployCheckToolkits.MultiTenancy;

namespace ProgramStudio.DeployCheckToolkits.EntityFrameworkCore
{
    public class DeployCheckToolkitsDbContext : AbpZeroDbContext<Tenant, Role, User, DeployCheckToolkitsDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public DeployCheckToolkitsDbContext(DbContextOptions<DeployCheckToolkitsDbContext> options)
            : base(options)
        {
        }
    }
}

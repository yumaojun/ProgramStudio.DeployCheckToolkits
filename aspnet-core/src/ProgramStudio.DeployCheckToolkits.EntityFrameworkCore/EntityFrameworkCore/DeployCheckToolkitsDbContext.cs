using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ProgramStudio.DeployCheckToolkits.Authorization.Roles;
using ProgramStudio.DeployCheckToolkits.Authorization.Users;
using ProgramStudio.DeployCheckToolkits.MultiTenancy;
using ProgramStudio.DeployCheckToolkits.DeployCheck;
using ProgramStudio.DeployCheckToolkits.BaseData;
using ProgramStudio.DeployCheckToolkits.CutoverPlan;

namespace ProgramStudio.DeployCheckToolkits.EntityFrameworkCore
{
    public class DeployCheckToolkitsDbContext : AbpZeroDbContext<Tenant, Role, User, DeployCheckToolkitsDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<GrepConfiguration> GrepConfigurations { get; set; }
        public DbSet<GrepHistory> GrepHistories { get; set; }

        public DbSet<ProjectInfo> ProjectInfos { get; set; }
        public DbSet<CutoverPlanHead> CutoverPlanHeads { get; set; }
        public DbSet<CutoverPlanInfo> CutoverPlanInfos { get; set; }

        public DeployCheckToolkitsDbContext(DbContextOptions<DeployCheckToolkitsDbContext> options)
            : base(options)
        {
        }
    }
}

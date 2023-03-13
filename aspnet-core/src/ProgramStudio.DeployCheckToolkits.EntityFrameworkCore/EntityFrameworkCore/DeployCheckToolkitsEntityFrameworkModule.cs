using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using ProgramStudio.DeployCheckToolkits.EntityFrameworkCore.Seed;

namespace ProgramStudio.DeployCheckToolkits.EntityFrameworkCore
{
    [DependsOn(
        typeof(DeployCheckToolkitsCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class DeployCheckToolkitsEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<DeployCheckToolkitsDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        DeployCheckToolkitsDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        DeployCheckToolkitsDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DeployCheckToolkitsEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}

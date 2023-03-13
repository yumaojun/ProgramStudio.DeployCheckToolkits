using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProgramStudio.DeployCheckToolkits.Configuration;
using ProgramStudio.DeployCheckToolkits.EntityFrameworkCore;
using ProgramStudio.DeployCheckToolkits.Migrator.DependencyInjection;

namespace ProgramStudio.DeployCheckToolkits.Migrator
{
    [DependsOn(typeof(DeployCheckToolkitsEntityFrameworkModule))]
    public class DeployCheckToolkitsMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public DeployCheckToolkitsMigratorModule(DeployCheckToolkitsEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(DeployCheckToolkitsMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                DeployCheckToolkitsConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DeployCheckToolkitsMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}

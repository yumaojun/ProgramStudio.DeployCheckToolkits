using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProgramStudio.DeployCheckToolkits.Authorization;

namespace ProgramStudio.DeployCheckToolkits
{
    [DependsOn(
        typeof(DeployCheckToolkitsCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class DeployCheckToolkitsApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<DeployCheckToolkitsAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(DeployCheckToolkitsApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}

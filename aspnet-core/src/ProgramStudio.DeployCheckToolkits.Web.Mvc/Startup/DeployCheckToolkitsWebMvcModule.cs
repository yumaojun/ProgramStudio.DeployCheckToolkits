using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProgramStudio.DeployCheckToolkits.Configuration;

namespace ProgramStudio.DeployCheckToolkits.Web.Startup
{
    [DependsOn(typeof(DeployCheckToolkitsWebCoreModule))]
    public class DeployCheckToolkitsWebMvcModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DeployCheckToolkitsWebMvcModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<DeployCheckToolkitsNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DeployCheckToolkitsWebMvcModule).GetAssembly());
        }
    }
}

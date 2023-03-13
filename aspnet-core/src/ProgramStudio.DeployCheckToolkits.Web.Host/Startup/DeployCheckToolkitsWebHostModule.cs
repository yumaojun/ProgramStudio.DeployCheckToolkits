using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProgramStudio.DeployCheckToolkits.Configuration;

namespace ProgramStudio.DeployCheckToolkits.Web.Host.Startup
{
    [DependsOn(
       typeof(DeployCheckToolkitsWebCoreModule))]
    public class DeployCheckToolkitsWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DeployCheckToolkitsWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DeployCheckToolkitsWebHostModule).GetAssembly());
        }
    }
}

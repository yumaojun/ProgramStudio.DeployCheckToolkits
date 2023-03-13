using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ProgramStudio.DeployCheckToolkits.Configuration.Dto;

namespace ProgramStudio.DeployCheckToolkits.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : DeployCheckToolkitsAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}

using System.Threading.Tasks;
using ProgramStudio.DeployCheckToolkits.Configuration.Dto;

namespace ProgramStudio.DeployCheckToolkits.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

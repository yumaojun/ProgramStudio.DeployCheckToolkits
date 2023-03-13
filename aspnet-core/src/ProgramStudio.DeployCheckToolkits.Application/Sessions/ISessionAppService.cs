using System.Threading.Tasks;
using Abp.Application.Services;
using ProgramStudio.DeployCheckToolkits.Sessions.Dto;

namespace ProgramStudio.DeployCheckToolkits.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

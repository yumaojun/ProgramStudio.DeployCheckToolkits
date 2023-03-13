using System.Threading.Tasks;
using Abp.Application.Services;
using ProgramStudio.DeployCheckToolkits.Authorization.Accounts.Dto;

namespace ProgramStudio.DeployCheckToolkits.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}

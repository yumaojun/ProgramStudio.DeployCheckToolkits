using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProgramStudio.DeployCheckToolkits.Roles.Dto;
using ProgramStudio.DeployCheckToolkits.Users.Dto;

namespace ProgramStudio.DeployCheckToolkits.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}

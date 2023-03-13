using System.Collections.Generic;
using ProgramStudio.DeployCheckToolkits.Roles.Dto;
using ProgramStudio.DeployCheckToolkits.Users.Dto;

namespace ProgramStudio.DeployCheckToolkits.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}

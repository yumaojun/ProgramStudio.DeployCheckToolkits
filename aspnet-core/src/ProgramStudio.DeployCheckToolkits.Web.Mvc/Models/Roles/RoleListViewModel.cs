using System.Collections.Generic;
using ProgramStudio.DeployCheckToolkits.Roles.Dto;

namespace ProgramStudio.DeployCheckToolkits.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleListDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}

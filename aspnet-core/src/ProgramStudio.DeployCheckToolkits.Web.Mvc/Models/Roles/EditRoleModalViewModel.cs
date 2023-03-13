using Abp.AutoMapper;
using ProgramStudio.DeployCheckToolkits.Roles.Dto;
using ProgramStudio.DeployCheckToolkits.Web.Models.Common;

namespace ProgramStudio.DeployCheckToolkits.Web.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool HasPermission(FlatPermissionDto permission)
        {
            return GrantedPermissionNames.Contains(permission.Name);
        }
    }
}

using System.Collections.Generic;
using ProgramStudio.DeployCheckToolkits.Roles.Dto;

namespace ProgramStudio.DeployCheckToolkits.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}
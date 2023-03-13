using Abp.Authorization;
using ProgramStudio.DeployCheckToolkits.Authorization.Roles;
using ProgramStudio.DeployCheckToolkits.Authorization.Users;

namespace ProgramStudio.DeployCheckToolkits.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

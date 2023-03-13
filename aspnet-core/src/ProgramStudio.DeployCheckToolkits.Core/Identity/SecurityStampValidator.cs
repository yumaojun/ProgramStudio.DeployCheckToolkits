using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using ProgramStudio.DeployCheckToolkits.Authorization.Roles;
using ProgramStudio.DeployCheckToolkits.Authorization.Users;
using ProgramStudio.DeployCheckToolkits.MultiTenancy;

namespace ProgramStudio.DeployCheckToolkits.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options, 
            SignInManager signInManager,
            ISystemClock systemClock) 
            : base(
                  options, 
                  signInManager, 
                  systemClock)
        {
        }
    }
}

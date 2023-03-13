using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProgramStudio.DeployCheckToolkits.Authorization;
using ProgramStudio.DeployCheckToolkits.Authorization.Roles;
using ProgramStudio.DeployCheckToolkits.Authorization.Users;
using ProgramStudio.DeployCheckToolkits.Editions;
using ProgramStudio.DeployCheckToolkits.MultiTenancy;

namespace ProgramStudio.DeployCheckToolkits.Identity
{
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>()
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpLogInManager<LogInManager>()
                .AddAbpSignInManager<SignInManager>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}

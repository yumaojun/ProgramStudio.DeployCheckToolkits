using Abp.Application.Navigation;
using Abp.Localization;
using ProgramStudio.DeployCheckToolkits.Authorization;

namespace ProgramStudio.DeployCheckToolkits.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class DeployCheckToolkitsNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.DeployCheck,
                        L("DeployCheck"),
                        url: "DeployCheck",
                        icon: "home",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Settings,
                        L("Settings"),
                        url: "Settings",
                        icon: "menu",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.CutoverPlan,
                        L("CutoverPlan"),
                        url: "CutoverPlan",
                        icon: "apps",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Tenants"),
                        url: "Tenants",
                        icon: "business",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "people",
                        requiredPermissionName: PermissionNames.Pages_Users
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "local_offer",
                        requiredPermissionName: PermissionNames.Pages_Roles
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.About,
                        L("About"),
                        url: "About",
                        icon: "info"
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, DeployCheckToolkitsConsts.LocalizationSourceName);
        }
    }
}

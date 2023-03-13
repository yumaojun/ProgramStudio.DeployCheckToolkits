using Abp.AspNetCore.Mvc.ViewComponents;

namespace ProgramStudio.DeployCheckToolkits.Web.Views
{
    public abstract class DeployCheckToolkitsViewComponent : AbpViewComponent
    {
        protected DeployCheckToolkitsViewComponent()
        {
            LocalizationSourceName = DeployCheckToolkitsConsts.LocalizationSourceName;
        }
    }
}

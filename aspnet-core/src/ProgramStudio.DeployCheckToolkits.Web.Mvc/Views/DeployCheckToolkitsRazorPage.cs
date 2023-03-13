using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace ProgramStudio.DeployCheckToolkits.Web.Views
{
    public abstract class DeployCheckToolkitsRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected DeployCheckToolkitsRazorPage()
        {
            LocalizationSourceName = DeployCheckToolkitsConsts.LocalizationSourceName;
        }
    }
}

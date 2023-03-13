using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using ProgramStudio.DeployCheckToolkits.Controllers;

namespace ProgramStudio.DeployCheckToolkits.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : DeployCheckToolkitsControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}

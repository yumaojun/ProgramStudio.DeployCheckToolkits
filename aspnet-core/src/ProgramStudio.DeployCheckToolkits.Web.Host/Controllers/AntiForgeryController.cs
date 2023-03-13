using Microsoft.AspNetCore.Antiforgery;
using ProgramStudio.DeployCheckToolkits.Controllers;

namespace ProgramStudio.DeployCheckToolkits.Web.Host.Controllers
{
    public class AntiForgeryController : DeployCheckToolkitsControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}

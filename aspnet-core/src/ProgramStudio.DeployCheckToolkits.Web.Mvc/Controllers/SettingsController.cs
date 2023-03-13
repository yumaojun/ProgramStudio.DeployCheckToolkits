using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgramStudio.DeployCheckToolkits.Controllers;

namespace ProgramStudio.DeployCheckToolkits.Web.Controllers
{
    [AbpMvcAuthorize]
    public class SettingsController :  DeployCheckToolkitsControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View("Edit");
        }
    }
}

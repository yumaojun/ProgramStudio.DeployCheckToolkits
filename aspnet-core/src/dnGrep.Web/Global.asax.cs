using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace dnGrep.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Init7Zip();

        }

        private void Init7Zip()
        {
            var path = Server.MapPath("~/bin");
            if (Environment.Is64BitProcess)
                SevenZip.SevenZipBase.SetLibraryPath(Path.Combine(path, @"7z64.dll"));
            else
                SevenZip.SevenZipBase.SetLibraryPath(Path.Combine(path, @"7z.dll"));
        }
    }
}

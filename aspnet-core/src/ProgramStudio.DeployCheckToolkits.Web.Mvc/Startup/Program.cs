using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace ProgramStudio.DeployCheckToolkits.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Init7Zip();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }

        private static void Init7Zip()
        {
            if (Environment.Is64BitProcess)
                SevenZip.SevenZipBase.SetLibraryPath(Path.Combine(AppContext.BaseDirectory,  @"7z64.dll"));
            else
                SevenZip.SevenZipBase.SetLibraryPath(Path.Combine(AppContext.BaseDirectory, @"7z.dll"));
        }
    }
}

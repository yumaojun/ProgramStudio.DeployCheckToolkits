using System.Collections.Generic;

namespace ProgramStudio.DeployCheckToolkits.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}

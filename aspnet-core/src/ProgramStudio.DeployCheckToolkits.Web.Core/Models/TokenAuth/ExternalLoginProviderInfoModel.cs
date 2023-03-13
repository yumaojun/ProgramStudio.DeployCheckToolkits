using Abp.AutoMapper;
using ProgramStudio.DeployCheckToolkits.Authentication.External;

namespace ProgramStudio.DeployCheckToolkits.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}

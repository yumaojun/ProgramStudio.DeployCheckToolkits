using Abp.AutoMapper;
using ProgramStudio.DeployCheckToolkits.Sessions.Dto;

namespace ProgramStudio.DeployCheckToolkits.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}

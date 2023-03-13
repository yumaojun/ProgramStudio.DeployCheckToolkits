using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProgramStudio.DeployCheckToolkits.MultiTenancy;

namespace ProgramStudio.DeployCheckToolkits.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}

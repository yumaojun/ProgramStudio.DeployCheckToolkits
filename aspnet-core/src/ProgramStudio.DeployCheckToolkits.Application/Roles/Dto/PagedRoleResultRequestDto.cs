using Abp.Application.Services.Dto;

namespace ProgramStudio.DeployCheckToolkits.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}


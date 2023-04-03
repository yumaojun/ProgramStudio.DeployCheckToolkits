using Abp.Application.Services.Dto;

namespace ProgramStudio.DeployCheckToolkits.DeployCheck.Dto
{
    public class PagedGrepConfigurationResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}


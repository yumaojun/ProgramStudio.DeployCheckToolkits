using Abp.Application.Services.Dto;

namespace ProgramStudio.DeployCheckToolkits.BaseData.Dto
{
    public class PagedProjectInfoResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}


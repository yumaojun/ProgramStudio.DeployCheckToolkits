using Abp.Application.Services.Dto;

namespace ProgramStudio.DeployCheckToolkits.DeployCheck.Dto
{
    public class PagedGrepHistoryResultRequestDto : PagedResultRequestDto
    {
        public long UserId { get; set; }
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}


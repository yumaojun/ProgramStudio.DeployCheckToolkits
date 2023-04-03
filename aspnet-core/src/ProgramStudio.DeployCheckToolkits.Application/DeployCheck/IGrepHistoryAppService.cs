using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProgramStudio.DeployCheckToolkits.DeployCheck.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.DeployCheck
{
    public interface IGrepHistoryAppService : IAsyncCrudAppService<GrepHistoryDto, long, PagedGrepHistoryResultRequestDto, CreateGrepHistoryDto, GrepHistoryDto>
    {
        PagedResultDto<GrepHistoryDto> GetPagedGrepHistories(PagedGrepHistoryResultRequestDto requestDto);
    }
}

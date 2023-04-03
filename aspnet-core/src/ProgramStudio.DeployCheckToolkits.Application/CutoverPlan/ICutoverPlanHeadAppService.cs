using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProgramStudio.DeployCheckToolkits.CutoverPlan.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.CutoverPlan
{
    public interface ICutoverPlanHeadAppService : IAsyncCrudAppService<CutoverPlanHeadDto, int, PagedCutoverPlanHeadResultRequestDto, CreateCutoverPlanHeadDto, CutoverPlanHeadDto>
    {
        /// <summary>
        /// 返回分页数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        PagedResultDto<CutoverPlanHeadDto> GetPagedCutoverPlanHeadHistories(PagedCutoverPlanHeadResultRequestDto requestDto);
    }
}

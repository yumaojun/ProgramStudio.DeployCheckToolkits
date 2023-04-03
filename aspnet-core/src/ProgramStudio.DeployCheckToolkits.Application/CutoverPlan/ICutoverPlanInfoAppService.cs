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
    public interface ICutoverPlanInfoAppService : IAsyncCrudAppService<CutoverPlanInfoDto, int, PagedCutoverPlanInfoResultRequestDto, CreateCutoverPlanInfoDto, CutoverPlanInfoDto>
    {
        /// <summary>
        /// 返回当前项目的CutoverPlan
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        List<CutoverPlanInfoDto> GetCutoverPlanByProjectNameOrderById(string projectName);

        /// <summary>
        /// 返回分页数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        PagedResultDto<CutoverPlanInfoDto> GetPagedCutoverPlanInfoHistories(PagedCutoverPlanInfoResultRequestDto requestDto);
    }
}

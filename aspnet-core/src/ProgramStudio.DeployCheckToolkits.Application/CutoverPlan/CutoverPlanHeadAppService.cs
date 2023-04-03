using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using ProgramStudio.DeployCheckToolkits.Authorization;
using ProgramStudio.DeployCheckToolkits.CutoverPlan.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.CutoverPlan
{
    [AbpAuthorize]
    public class CutoverPlanHeadAppService : AsyncCrudAppService<CutoverPlanHead, CutoverPlanHeadDto, int, PagedCutoverPlanHeadResultRequestDto, CreateCutoverPlanHeadDto, CutoverPlanHeadDto>, ICutoverPlanHeadAppService
    {
        public CutoverPlanHeadAppService(
               IRepository<CutoverPlanHead, int> repository)
               : base(repository)
        {
        }

        /// <summary>
        /// 返回分页数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public PagedResultDto<CutoverPlanHeadDto> GetPagedCutoverPlanHeadHistories(PagedCutoverPlanHeadResultRequestDto requestDto)
        {
            var count = Repository.Count();
            var result = Repository.GetAll().OrderByDescending(x => x.Id).PageBy(requestDto).ToList();

            return new PagedResultDto<CutoverPlanHeadDto>
            {
                TotalCount = count,
                Items = ObjectMapper.Map<List<CutoverPlanHeadDto>>(result)
            };
        }
    }
}

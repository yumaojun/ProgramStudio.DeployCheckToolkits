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
    public class CutoverPlanInfoAppService : AsyncCrudAppService<CutoverPlanInfo, CutoverPlanInfoDto, int, PagedCutoverPlanInfoResultRequestDto, CreateCutoverPlanInfoDto, CutoverPlanInfoDto>, ICutoverPlanInfoAppService
    {
        public CutoverPlanInfoAppService(
               IRepository<CutoverPlanInfo, int> repository)
               : base(repository)
        {
        }

        /// <summary>
        /// 返回当前项目的CutoverPlan
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public List<CutoverPlanInfoDto> GetCutoverPlanByProjectNameOrderById(string projectName)
        {
            if (projectName == null || projectName.Trim().Length == 0)
            {
                return new List<CutoverPlanInfoDto>();
            }

            var result = Repository.GetAll().Where(x => x.ProjectName == projectName).OrderBy(x => x.Id).ToList();
            var items = ObjectMapper.Map<List<CutoverPlanInfoDto>>(result);
            return items;
        }

        /// <summary>
        /// 返回分页数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public PagedResultDto<CutoverPlanInfoDto> GetPagedCutoverPlanInfoHistories(PagedCutoverPlanInfoResultRequestDto requestDto)
        {
            var count = Repository.Count();
            var result = Repository.GetAllIncluding(x => x.CutoverPlanHead).OrderByDescending(x => x.Id).PageBy(requestDto).ToList();

            return new PagedResultDto<CutoverPlanInfoDto>
            {
                TotalCount = count,
                Items = ObjectMapper.Map<List<CutoverPlanInfoDto>>(result)
            };
        }
    }
}

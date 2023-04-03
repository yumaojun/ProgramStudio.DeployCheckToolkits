using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using ProgramStudio.DeployCheckToolkits.Authorization;
using ProgramStudio.DeployCheckToolkits.DeployCheck.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.DeployCheck
{
    [AbpAuthorize]
    public class GrepHistoryAppService : AsyncCrudAppService<GrepHistory, GrepHistoryDto, long, PagedGrepHistoryResultRequestDto, CreateGrepHistoryDto, GrepHistoryDto>, IGrepHistoryAppService
    {
        public GrepHistoryAppService(
               IRepository<GrepHistory, long> repository)
               : base(repository)
        {
        }

        /// <summary>
        /// 返回分页数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public PagedResultDto<GrepHistoryDto> GetPagedGrepHistories(PagedGrepHistoryResultRequestDto requestDto)
        {
            var count = Repository.Count(x => x.CreatorUserId == requestDto.UserId);
            var result = Repository.GetAll().Where(x => x.CreatorUserId == requestDto.UserId).OrderByDescending(x => x.Id).PageBy(requestDto).ToList();

            return new PagedResultDto<GrepHistoryDto>
            {
                TotalCount = count,
                Items = ObjectMapper.Map<List<GrepHistoryDto>>(result)
            };
        }
    }
}

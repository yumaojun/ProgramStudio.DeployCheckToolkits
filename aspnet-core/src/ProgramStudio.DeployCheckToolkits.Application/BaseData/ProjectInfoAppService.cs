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
using ProgramStudio.DeployCheckToolkits.BaseData.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.BaseData
{
    [AbpAuthorize]
    public class ProjectInfoAppService : AsyncCrudAppService<ProjectInfo, ProjectInfoDto, int, PagedProjectInfoResultRequestDto, CreateProjectInfoDto, UpdateProjectInfoDto>, IProjectInfoAppService
    {
        public ProjectInfoAppService(
               IRepository<ProjectInfo, int> repository)
               : base(repository)
        {
        }
    }
}

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
using ProgramStudio.DeployCheckToolkits.DeployCheck.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.DeployCheck
{
    [AbpAuthorize]
    public class GrepConfigurationAppService : AsyncCrudAppService<GrepConfiguration, GrepConfigurationDto, int, PagedGrepConfigurationResultRequestDto, CreateGrepConfigurationDto, UpdateGrepConfigurationDto>, IGrepConfigurationAppService
    {
        public GrepConfigurationAppService(
               IRepository<GrepConfiguration, int> repository)
               : base(repository)
        {
        }
    }
}

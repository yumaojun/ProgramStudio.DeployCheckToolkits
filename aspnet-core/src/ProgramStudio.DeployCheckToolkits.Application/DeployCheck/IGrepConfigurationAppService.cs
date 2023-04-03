using Abp.Application.Services;
using ProgramStudio.DeployCheckToolkits.DeployCheck.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.DeployCheck
{
    public interface IGrepConfigurationAppService : IAsyncCrudAppService<GrepConfigurationDto, int, PagedGrepConfigurationResultRequestDto, CreateGrepConfigurationDto, UpdateGrepConfigurationDto>
    {
    }
}

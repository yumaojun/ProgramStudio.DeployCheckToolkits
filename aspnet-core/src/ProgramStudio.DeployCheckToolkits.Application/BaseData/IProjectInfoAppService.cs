using Abp.Application.Services;
using ProgramStudio.DeployCheckToolkits.BaseData.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.BaseData
{
    public interface IProjectInfoAppService : IAsyncCrudAppService<ProjectInfoDto, int, PagedProjectInfoResultRequestDto, CreateProjectInfoDto, UpdateProjectInfoDto>
    {
    }
}

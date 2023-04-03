using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using ProgramStudio.DeployCheckToolkits.BaseData;

namespace ProgramStudio.DeployCheckToolkits.BaseData.Dto
{
    [AutoMapTo(typeof(ProjectInfo))]
    public class DeleteProjectInfoDto : EntityDto<int>
    {
    }
}

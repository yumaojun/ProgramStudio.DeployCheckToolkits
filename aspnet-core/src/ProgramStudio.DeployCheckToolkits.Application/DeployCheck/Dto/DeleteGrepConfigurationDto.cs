using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using ProgramStudio.DeployCheckToolkits.DeployCheck;

namespace ProgramStudio.DeployCheckToolkits.DeployCheck.Dto
{
    [AutoMapTo(typeof(GrepConfiguration))]
    public class DeleteGrepConfigurationDto: EntityDto<int>
    {
    }
}

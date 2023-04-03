using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using ProgramStudio.DeployCheckToolkits.BaseData;

namespace ProgramStudio.DeployCheckToolkits.BaseData.Dto
{
    [AutoMapTo(typeof(ProjectInfo))]
    public class CreateProjectInfoDto
    {
        [Required]
        [StringLength(ProjectInfo.MaxProjectNameLength)]
        public string ProjectName { get; set; }

        public bool IsActive {get; set;}
    }
}

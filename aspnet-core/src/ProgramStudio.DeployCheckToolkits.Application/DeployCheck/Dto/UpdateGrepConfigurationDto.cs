using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using ProgramStudio.DeployCheckToolkits.DeployCheck;

namespace ProgramStudio.DeployCheckToolkits.DeployCheck.Dto
{
    [AutoMapTo(typeof(GrepConfiguration))]
    public class UpdateGrepConfigurationDto: EntityDto<int>
    {
        [Required]
        [StringLength(GrepConfiguration.MaxProjectNameLength)]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(GrepConfiguration.MaxDeployNameLength)]
        public string DeployName { get; set; }

        [Required]
        [StringLength(GrepConfiguration.MaxXmlContentLength)]
        public string XmlContent { get; set; }

        public bool IsActive {get; set;}
    }
}

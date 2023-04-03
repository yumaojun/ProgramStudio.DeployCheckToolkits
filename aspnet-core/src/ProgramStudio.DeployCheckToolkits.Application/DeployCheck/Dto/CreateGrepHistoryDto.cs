using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using ProgramStudio.DeployCheckToolkits.DeployCheck;

namespace ProgramStudio.DeployCheckToolkits.DeployCheck.Dto
{
    [AutoMapTo(typeof(GrepHistory))]
    public class CreateGrepHistoryDto
    {
        [Required]
        [StringLength(GrepHistory.MaxProjectNameLength)]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(GrepHistory.MaxDeployNameLength)]
        public string DeployName { get; set; }

        public  string FileName { get; set; }
        public  string FileExtension { get; set; }
        public  string Result { get; set; }
    }
}

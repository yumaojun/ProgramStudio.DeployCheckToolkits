using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using ProgramStudio.DeployCheckToolkits.CutoverPlan;

namespace ProgramStudio.DeployCheckToolkits.CutoverPlan.Dto
{
    [AutoMapTo(typeof(CutoverPlanInfo))]
    public class CreateCutoverPlanInfoDto
    {
        [Required]
        [StringLength(CutoverPlanInfo.MaxProjectNameLength)]
        public string ProjectName { get; set; }

        [Required]
        public CutoverPlanHead CutoverPlanHead { get; set; }

        [Required]
        public string DeployItemName { get; set; }

        public string DeployVersion { get; set; }

        public string RollbackVersion { get; set; }
    }
}

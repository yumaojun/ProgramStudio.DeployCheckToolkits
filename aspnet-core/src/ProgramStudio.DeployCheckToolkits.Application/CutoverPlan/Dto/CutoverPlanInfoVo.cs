using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using ProgramStudio.DeployCheckToolkits.CutoverPlan;

namespace ProgramStudio.DeployCheckToolkits.CutoverPlan.Dto
{
    [AutoMapFrom(typeof(CutoverPlanInfo))]
    public class CutoverPlanInfoDto : AuditedEntityDto
    {
        [Required]
        [StringLength(CutoverPlanInfo.MaxProjectNameLength)]
        public string ProjectName { get; set; }

        [Required]
        public string DeployItemName { get; set; }

        public string DeployVersion { get; set; }

        public string RollbackVersion { get; set; }

        public string LastVersion { get; set; }

        public bool IsActive { get; set; }
    }
}

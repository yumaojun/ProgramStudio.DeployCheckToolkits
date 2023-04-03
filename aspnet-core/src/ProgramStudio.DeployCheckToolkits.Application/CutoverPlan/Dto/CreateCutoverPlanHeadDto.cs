using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using ProgramStudio.DeployCheckToolkits.CutoverPlan;

namespace ProgramStudio.DeployCheckToolkits.CutoverPlan.Dto
{
    [AutoMapTo(typeof(CutoverPlanHead))]
    public class CreateCutoverPlanHeadDto
    {
        [Required]
        [StringLength(CutoverPlanHead.MaxProjectNameLength)]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(CutoverPlanHead.MaxFileNameLength)]
        public string FileName { get; set; }

        public string Result { get; set; }

        public List<CutoverPlanInfo> CutoverPlanInfos { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using ProgramStudio.DeployCheckToolkits.BaseData;

namespace ProgramStudio.DeployCheckToolkits.BaseData.Dto
{
    [AutoMapFrom(typeof(ProjectInfo))]
    public class ProjectInfoDto : AuditedEntityDto
    {
        [Required]
        [StringLength(ProjectInfo.MaxProjectNameLength)]
        public string ProjectName { get; set; }

        public bool IsActive { get; set; }
    }
}

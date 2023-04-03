using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.BaseData
{
    /// <summary>
    /// Project Info
    /// </summary>
    public class ProjectInfo : AuditedEntity<int>
    {
        public const int MaxProjectNameLength = 100;

        public ProjectInfo():base()
        {
        }

        public virtual string ProjectName { get; set; }

        public virtual bool IsActive { get; set; }
    }
}

using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.DeployCheck
{
    /// <summary>
    /// Grep History
    /// </summary>
    public class GrepHistory : AuditedEntity<long>
    {
        public const int MaxProjectNameLength = 100;
        public const int MaxDeployNameLength = 100;
        public const int MaxResultLength = 4000;

        public GrepHistory() : base()
        {
        }

        public virtual string ProjectName { get; set; }
        public virtual string DeployName { get; set; }
        public virtual string FileName { get; set; }
        public virtual string FileExtension { get; set; }
        public virtual string Result { get; set; }
    }
}

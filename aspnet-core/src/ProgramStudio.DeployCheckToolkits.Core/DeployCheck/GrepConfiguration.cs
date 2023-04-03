using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.DeployCheck
{
    /// <summary>
    /// Grep Configuration
    /// </summary>
    public class GrepConfiguration : AuditedEntity<int>
    {
        public const int MaxProjectNameLength = 100;
        public const int MaxDeployNameLength = 100;
        public const int MaxXmlContentLength = 2000;

        public GrepConfiguration():base()
        {
        }

        public virtual string ProjectName { get; set; }

        public virtual string DeployName { get; set; }

        public virtual string XmlContent { get; set; }

        public virtual bool IsActive { get; set; }
    }
}

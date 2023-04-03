using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.CutoverPlan
{
    /// <summary>
    /// 项目名称，部署内容，部署版本号，回滚版本号，
    /// </summary>
    public class CutoverPlanInfo : AuditedEntity<int>
    {
        public const int MaxProjectNameLength = 100;

        public CutoverPlanInfo():base()
        {
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string ProjectName { get; set; }

        /// <summary>
        /// 部署内容
        /// </summary>
        public virtual string DeployItemName { get; set; }

        /// <summary>
        /// 部署版本号
        /// </summary>
        public virtual string DeployVersion { get; set; }

        /// <summary>
        /// 回滚版本号
        /// </summary>
        public virtual string RollbackVersion { get; set; }
        
        public virtual bool IsActive { get; set; }
    }
}

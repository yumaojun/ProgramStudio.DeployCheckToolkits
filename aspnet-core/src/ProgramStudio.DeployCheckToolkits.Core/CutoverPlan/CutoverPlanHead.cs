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
    /// 项目名称，文件，检查结果
    /// </summary>
    public class CutoverPlanHead : AuditedEntity<int>
    {
        public const int MaxProjectNameLength = 100;
        public const int MaxFileNameLength = 300;

        public CutoverPlanHead() : base()
        {
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string ProjectName { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        public virtual string FileName { get; set; }

        /// <summary>
        /// 检查结果
        /// </summary>
        public virtual string Result { get; set; }

        /// <summary>
        /// 检查项
        /// </summary>
        public virtual List<CutoverPlanInfo> CutoverPlanInfos { get; set; }
    }
}

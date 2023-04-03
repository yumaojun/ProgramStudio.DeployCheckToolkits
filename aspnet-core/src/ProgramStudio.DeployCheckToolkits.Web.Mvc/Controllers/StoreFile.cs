using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.Web
{
    /// <summary>
    /// 上传的文件信息
    /// </summary>
    public class StoreFile
    {
        /// <summary>
        /// 文件Md5
        /// </summary>
        public string Md5 { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }

    public class WebUploadFileInfo
    {
        public string FileName1 { get; set; }


        public string FileName2 { get; set; }
    }
}

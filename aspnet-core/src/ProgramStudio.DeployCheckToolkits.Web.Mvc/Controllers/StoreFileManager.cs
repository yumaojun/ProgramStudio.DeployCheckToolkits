using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;
using NLog;

namespace ProgramStudio.DeployCheckToolkits.Web
{
    /// <summary>
    /// 文件管理
    /// </summary>
    public class StoreFileManager
    {
        public const string AllowedExts = "txt,xml,apk,ipa,zip,7z,jar,war,ear,rar,cab,gz,gzip,tar,rpm,iso,isx,bz2,bzip2,tbz2,tbz,tgz,arj,cpio,deb,dmg,hfs,hfsx,lzh,lha,lzma,z,taz,xar,pkg,xz,txz,zipx,epub,wim,chm";
        public const string RelativeFilePath = "Files";
        private const int _seconds = 60 * 60 * 24; // 文件过期时间：秒数 // 24Hours
        private Timer _timer;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly ConcurrentQueue<StoreFile> _cache = new ConcurrentQueue<StoreFile>();
        private static StoreFileManager _instance;

        public StoreFile[] Items
        {
            get
            {
                return _cache.ToArray();
            }
        }

        private StoreFileManager()
        {
            _timer = new Timer(DoWork, null, 500, 1000 * _seconds);
        }

        ~StoreFileManager()
        {
            _timer.Dispose();
            _timer = null;
        }

        public static StoreFileManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StoreFileManager();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="obj"></param>
        public void Add(StoreFile obj)
        {
            obj.CreationTime = DateTime.Now;
            _cache.Enqueue(obj);
        }

        void DoWork(object obj)
        {
            StoreFile tryGetOne = null;
            while (_cache.TryPeek(out tryGetOne))
            {
                try
                {
                    var ts = DateTime.Now.Subtract(tryGetOne.CreationTime);
                    if (ts.TotalSeconds > _seconds)
                    {
                        File.Delete(tryGetOne.FileName);
                        _cache.TryDequeue(out tryGetOne);
                        tryGetOne = null;
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Failed to DoWork: " + ex.Message);
                }
            }
        }
    }
}


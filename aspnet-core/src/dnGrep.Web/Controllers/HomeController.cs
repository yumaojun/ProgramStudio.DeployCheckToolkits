using dnGREP.Common;
using dnGREP.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace dnGrep.Web.Controllers
{
    public class HomeController : Controller
    {
        protected GrepSettings Settings => GrepSettings.Instance;
        protected PathSearchText PathSearchText { get; private set; } = new PathSearchText();

        private string fileOrFolderPath;
        public string FileOrFolderPath
        {
            get { return fileOrFolderPath; }
            set
            {
                if (value == fileOrFolderPath)
                    return;

                fileOrFolderPath = value;
                PathSearchText.FileOrFolderPath = value;
                //base.OnPropertyChanged(nameof(FileOrFolderPath));
            }
        }

        WebSearchCriteria CreateParam()
        {
            var xmlName = Server.MapPath("~/App_Data/XMLFile2.xml");
            //var list = WebSearchCriteria.DeserializeFromXML(xmlName);

            //return list.First();

            var criteria = new WebSearchCriteria() { SearchFor = "qa" };
            var list = new List<WebSearchCriteria>();
            list.Add(criteria);
            //WebSearchCriteria.SerializeToXML(list, xmlName);
            return criteria;
        }

        public ActionResult Index()
        {
            object result = new List<GrepSearchResult>();
            //FileOrFolderPath = @"D:\Works\deployCheck\HKAmwayHub.ipa.zip";
            if (Request.Files.Count > 0)
            {
                var filename = Server.MapPath("~/") + Request.Files[0].FileName;
                Request.Files[0].SaveAs(filename);
                FileOrFolderPath = filename;
            }
            else
            {
                return View(result);
            }

            WebSearchCriteria param = CreateParam();

            int sizeFrom = 0;
            int sizeTo = 0;
            if (param.UseFileSizeFilter == FileSizeFilter.Yes)
            {
                sizeFrom = param.SizeFrom;
                sizeTo = param.SizeTo;
            }

            DateTime? startTime = null, endTime = null;
            if (param.UseFileDateFilter != FileDateFilter.None)
            {
                if (param.TypeOfTimeRangeFilter == FileTimeRange.Dates)
                {
                    startTime = param.StartDate;
                    endTime = param.EndDate;
                    // the end date should go through the end of the day
                    if (endTime.HasValue)
                        endTime = endTime.Value.AddDays(1.0);
                }
                else if (param.TypeOfTimeRangeFilter == FileTimeRange.Hours)
                {
                    int low = Math.Min(param.HoursFrom, param.HoursTo);
                    int high = Math.Max(param.HoursFrom, param.HoursTo);
                    startTime = DateTime.Now.AddHours(-1 * high);
                    endTime = DateTime.Now.AddHours(-1 * low);
                }
            }

            string filePatternInclude = "*.*";
            if (param.TypeOfFileSearch == FileSearchType.Regex)
                filePatternInclude = ".*";
            else if (param.TypeOfFileSearch == FileSearchType.Everything)
                filePatternInclude = string.Empty;

            if (!string.IsNullOrEmpty(param.FilePattern))
                filePatternInclude = param.FilePattern;

            string filePatternExclude = "";
            if (!string.IsNullOrEmpty(param.FilePatternIgnore))
                filePatternExclude = param.FilePatternIgnore;

            //IEnumerable<FileData> fileInfos = null;
            IEnumerable<string> files = null;

            bool skipRemoteCloudStorageFiles = true; // 跳过远程文件

            Utils.CancelSearch = false;

            FileFilter fileParams = new FileFilter(FileOrFolderPath, filePatternInclude, filePatternExclude,
                            param.TypeOfFileSearch == FileSearchType.Regex, param.UseGitIgnore, param.TypeOfFileSearch == FileSearchType.Everything,
                            param.IncludeSubfolder, param.MaxSubfolderDepth, param.IncludeHidden, param.IncludeBinary, param.IncludeArchive,
                            param.FollowSymlinks, sizeFrom, sizeTo, param.UseFileDateFilter, startTime, endTime,
                            skipRemoteCloudStorageFiles);

            files = Utils.GetFileListEx(fileParams);

            if (Utils.CancelSearch)
            {
                result = null;
                //return;
            }

            if (param.TypeOfSearch == SearchType.Regex)
            {
                try
                {
                    Regex pattern = new Regex(param.SearchFor);
                }
                catch (ArgumentException regException)
                {
                    result = regException.Message;
                    //return;
                }
            }

            bool searchParallel = true; // 平行

            GrepCore grep = new GrepCore();
            grep.SearchParams = new GrepEngineInitParams(
                            Settings.Get<bool>(GrepSettings.Key.ShowLinesInContext),
                            Settings.Get<int>(GrepSettings.Key.ContextLinesBefore),
                            Settings.Get<int>(GrepSettings.Key.ContextLinesAfter),
                            Settings.Get<double>(GrepSettings.Key.FuzzyMatchThreshold),
                            Settings.Get<bool>(GrepSettings.Key.ShowVerboseMatchCount),
                            searchParallel);

            grep.FileFilter = new FileFilter(FileOrFolderPath, filePatternInclude, filePatternExclude,
                param.TypeOfFileSearch == FileSearchType.Regex, param.UseGitIgnore, param.TypeOfFileSearch == FileSearchType.Everything,
                param.IncludeSubfolder, param.MaxSubfolderDepth, param.IncludeHidden, param.IncludeBinary, param.IncludeArchive,
                param.FollowSymlinks, sizeFrom, sizeTo, param.UseFileDateFilter, startTime, endTime,
                skipRemoteCloudStorageFiles);

            GrepSearchOption searchOptions = GrepSearchOption.None;
            //if (Multiline)
            //    searchOptions |= GrepSearchOption.Multiline;
            //if (CaseSensitive)
            //    searchOptions |= GrepSearchOption.CaseSensitive;
            //if (Singleline)
            //    searchOptions |= GrepSearchOption.SingleLine;
            //if (WholeWord)
            //    searchOptions |= GrepSearchOption.WholeWord;
            //if (BooleanOperators)
            //    searchOptions |= GrepSearchOption.BooleanOperators;
            //if (StopAfterFirstMatch)
            //    searchOptions |= GrepSearchOption.StopAfterFirstMatch;

            //if (param.UseGitignore)
            //{
            //    // this will be the first search performed, and may take a long time
            //    // the message will allow the user to see the cost of the operation
            //    StatusMessage = Resources.Main_Status_SearchingForGitignore;
            //}

            grep.ProcessedFile += GrepCore_ProcessedFile;

            result = grep.Search(files, param.TypeOfSearch, param.SearchFor, searchOptions, param.CodePage);

            grep.ProcessedFile -= GrepCore_ProcessedFile;
            //try
            //{
            //    System.IO.File.Delete(FileOrFolderPath);
            //    FileOrFolderPath = null;
            //}
            //catch
            //{
            //}
            return View(result);
        }

        void GrepCore_ProcessedFile(object sender, ProgressStatus progress)
        {
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ProgramStudio.DeployCheckToolkits.Controllers;
//using dnGREP.Common;
//using dnGREP.Engines;

namespace ProgramStudio.DeployCheckToolkits.Web.Controllers
{
    [AbpMvcAuthorize]
    public class DeployCheckController : DeployCheckToolkitsControllerBase
    {
        //protected GrepSettings Settings => GrepSettings.Instance;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Uploader([FromForm] IFormCollection formData)
        {
            string filePath = null;
            var files = formData.Files;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    filePath = Path.Combine(AppContext.BaseDirectory, formFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return Ok(filePath);
        }

        //private void GrepCore_ProcessedFile(object sender, ProgressStatus progress)
        //{

        //}

        //[HttpPost]
        //public async Task<IActionResult> Grep([FromForm] IFormCollection formData)
        //{
        //    object result = new List<GrepSearchResult>();
        //    var FileOrFolderPath = Path.Combine(AppContext.BaseDirectory, "");
        //    var xmlName = Path.Combine(AppContext.BaseDirectory, "\\App_Data\\GREP\\queryKeyword.xml");
        //    var listCriteria = dnGrep.Web.WebSearchCriteria.DeserializeFromXML(xmlName);
        //    dnGrep.Web.WebSearchCriteria param = listCriteria.FirstOrDefault();

        //    if (param == null)
        //    {
        //        return NotFound();
        //    }

        //    int sizeFrom = 0;
        //    int sizeTo = 0;
        //    if (param.UseFileSizeFilter == FileSizeFilter.Yes)
        //    {
        //        sizeFrom = param.SizeFrom;
        //        sizeTo = param.SizeTo;
        //    }

        //    DateTime? startTime = null, endTime = null;
        //    if (param.UseFileDateFilter != FileDateFilter.None)
        //    {
        //        if (param.TypeOfTimeRangeFilter == FileTimeRange.Dates)
        //        {
        //            startTime = param.StartDate;
        //            endTime = param.EndDate;
        //            if (endTime.HasValue)
        //                endTime = endTime.Value.AddDays(1.0);
        //        }
        //        else if (param.TypeOfTimeRangeFilter == FileTimeRange.Hours)
        //        {
        //            int low = Math.Min(param.HoursFrom, param.HoursTo);
        //            int high = Math.Max(param.HoursFrom, param.HoursTo);
        //            startTime = DateTime.Now.AddHours(-1 * high);
        //            endTime = DateTime.Now.AddHours(-1 * low);
        //        }
        //    }

        //    string filePatternInclude = "*.*";
        //    if (param.TypeOfFileSearch == FileSearchType.Regex)
        //        filePatternInclude = ".*";
        //    else if (param.TypeOfFileSearch == FileSearchType.Everything)
        //        filePatternInclude = string.Empty;

        //    if (!string.IsNullOrEmpty(param.FilePattern))
        //        filePatternInclude = param.FilePattern;

        //    string filePatternExclude = "";
        //    if (!string.IsNullOrEmpty(param.FilePatternIgnore))
        //        filePatternExclude = param.FilePatternIgnore;

        //    //IEnumerable<FileData> fileInfos = null;
        //    IEnumerable<string> files = null;

        //    bool skipRemoteCloudStorageFiles = true; // 跳过远程文件

        //    Utils.CancelSearch = false;

        //    FileFilter fileParams = new FileFilter(FileOrFolderPath, filePatternInclude, filePatternExclude,
        //                    param.TypeOfFileSearch == FileSearchType.Regex, param.UseGitIgnore, param.TypeOfFileSearch == FileSearchType.Everything,
        //                    param.IncludeSubfolder, param.MaxSubfolderDepth, param.IncludeHidden, param.IncludeBinary, param.IncludeArchive,
        //                    param.FollowSymlinks, sizeFrom, sizeTo, param.UseFileDateFilter, startTime, endTime,
        //                    skipRemoteCloudStorageFiles);

        //    files = Utils.GetFileListEx(fileParams);

        //    if (Utils.CancelSearch)
        //    {
        //        result = null;
        //    }

        //    if (param.TypeOfSearch == SearchType.Regex)
        //    {
        //        try
        //        {
        //            Regex pattern = new Regex(param.SearchFor);
        //        }
        //        catch (ArgumentException regException)
        //        {
        //            result = regException.Message;
        //        }
        //    }

        //    bool searchParallel = true; // 平行

        //    GrepCore grep = new GrepCore();
        //    grep.SearchParams = new GrepEngineInitParams(
        //                    Settings.Get<bool>(GrepSettings.Key.ShowLinesInContext),
        //                    Settings.Get<int>(GrepSettings.Key.ContextLinesBefore),
        //                    Settings.Get<int>(GrepSettings.Key.ContextLinesAfter),
        //                    Settings.Get<double>(GrepSettings.Key.FuzzyMatchThreshold),
        //                    Settings.Get<bool>(GrepSettings.Key.ShowVerboseMatchCount),
        //                    searchParallel);

        //    grep.FileFilter = new FileFilter(FileOrFolderPath, filePatternInclude, filePatternExclude,
        //        param.TypeOfFileSearch == FileSearchType.Regex, param.UseGitIgnore, param.TypeOfFileSearch == FileSearchType.Everything,
        //        param.IncludeSubfolder, param.MaxSubfolderDepth, param.IncludeHidden, param.IncludeBinary, param.IncludeArchive,
        //        param.FollowSymlinks, sizeFrom, sizeTo, param.UseFileDateFilter, startTime, endTime,
        //        skipRemoteCloudStorageFiles);

        //    GrepSearchOption searchOptions = GrepSearchOption.None;

        //    grep.ProcessedFile += GrepCore_ProcessedFile;

        //    result = grep.Search(files, param.TypeOfSearch, param.SearchFor, searchOptions, param.CodePage);

        //    grep.ProcessedFile -= GrepCore_ProcessedFile;

        //    return Ok(result);
        //}
    }
}

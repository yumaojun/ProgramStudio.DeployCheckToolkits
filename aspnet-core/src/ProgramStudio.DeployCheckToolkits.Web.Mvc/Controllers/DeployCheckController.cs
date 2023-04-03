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
using ProgramStudio.DeployCheckToolkits.DeployCheck;
using ProgramStudio.DeployCheckToolkits.DeployCheck.Dto;
using dnGREP.Common;
using dnGREP.Engines;
using X.PagedList;

namespace ProgramStudio.DeployCheckToolkits.Web.Controllers
{
    [AbpMvcAuthorize]
    public class DeployCheckController : DeployCheckToolkitsControllerBase
    {
        private readonly IGrepConfigurationAppService _grepConfigurationAppService;
        private readonly IGrepHistoryAppService _grepHistoryAppService;
        private int _processedFiles;

        protected GrepSettings Settings => GrepSettings.Instance;
        protected StoreFileManager FileManager => StoreFileManager.Instance;

        public DeployCheckController(IGrepConfigurationAppService grepConfigurationAppService, IGrepHistoryAppService grepHistoryAppService)
        {
            _processedFiles = 0;
            _grepConfigurationAppService = grepConfigurationAppService;
            _grepHistoryAppService = grepHistoryAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Configuration()
        {
            var output = await _grepConfigurationAppService.GetAll(new PagedGrepConfigurationResultRequestDto { IsActive = true, MaxResultCount = int.MaxValue });
            var projectNames = output.Items.Select(x => x.ProjectName).Distinct().ToList();
            var result = new ProjectDeployVo(projectNames, output.Items.ToList());
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Uploader([FromForm] IFormCollection formData)
        {
            string fileName = null;
            string filePath = Path.Combine(AppContext.BaseDirectory, StoreFileManager.RelativeFilePath, AbpSession.UserId.ToString());
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            foreach (var formFile in formData.Files)
            {
                if (formFile.Length > 0)
                {
                    fileName = formFile.FileName;
                    var ext = Path.GetExtension(fileName);
                    var extNoneDot = ext.TrimStart('.');
                    if (!StoreFileManager.AllowedExts.Split(',').Contains(extNoneDot.ToLower()))
                    {
                        return BadRequest(L("SelectFileError"));
                    }
                    if (ext.Equals(".apk", StringComparison.OrdinalIgnoreCase) || ext.Equals(".ipa", StringComparison.OrdinalIgnoreCase))
                    {
                        fileName += ".zip";
                    }
                    filePath = Path.Combine(filePath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        FileManager.Add(new StoreFile() { FileName = filePath });
                    }
                }
            }
            return Ok(fileName);
        }

        void GrepCore_ProcessedFile(object sender, ProgressStatus progress)
        {
            _processedFiles = (int)progress.ProcessedFiles;
        }

        [HttpPost]
        public async Task<IActionResult> Grep(string projectName, string deployName, string xmlContent, string fileName)
        {
            DateTime timer = DateTime.Now;

            object result = new List<GrepSearchResult>();
            var fileOrFolderPath = Path.Combine(AppContext.BaseDirectory, StoreFileManager.RelativeFilePath, AbpSession.UserId.ToString(), fileName);
            if (!System.IO.File.Exists(fileOrFolderPath))
            {
                return NotFound();
            }

            var listCriteria = WebSearchCriteria.DeserializeFromXML(xmlContent);
            WebSearchCriteria param = listCriteria.FirstOrDefault();

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

            IEnumerable<string> files = null;
            bool skipRemoteCloudStorageFiles = true; // 跳过远程文件
            Utils.CancelSearch = false;

            FileFilter fileParams = new FileFilter(fileOrFolderPath, filePatternInclude, filePatternExclude,
                            param.TypeOfFileSearch == FileSearchType.Regex, param.UseGitIgnore, param.TypeOfFileSearch == FileSearchType.Everything,
                            param.IncludeSubfolder, param.MaxSubfolderDepth, param.IncludeHidden, param.IncludeBinary, param.IncludeArchive,
                            param.FollowSymlinks, sizeFrom, sizeTo, param.UseFileDateFilter, startTime, endTime,
                            skipRemoteCloudStorageFiles);

            files = Utils.GetFileListEx(fileParams);

            if (Utils.CancelSearch)
            {
                result = null;
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

            grep.FileFilter = new FileFilter(fileOrFolderPath, filePatternInclude, filePatternExclude,
                param.TypeOfFileSearch == FileSearchType.Regex, param.UseGitIgnore, param.TypeOfFileSearch == FileSearchType.Everything,
                param.IncludeSubfolder, param.MaxSubfolderDepth, param.IncludeHidden, param.IncludeBinary, param.IncludeArchive,
                param.FollowSymlinks, sizeFrom, sizeTo, param.UseFileDateFilter, startTime, endTime,
                skipRemoteCloudStorageFiles);

            GrepSearchOption searchOptions = GrepSearchOption.None;

            grep.ProcessedFile += GrepCore_ProcessedFile;

            result = grep.Search(files, param.TypeOfSearch, param.SearchFor, searchOptions, param.CodePage);

            grep.ProcessedFile -= GrepCore_ProcessedFile;

            TimeSpan duration = DateTime.Now.Subtract(timer);
            string durationStr = duration.GetPrettyString();
            int successCount = 0;
            int matchCount = 0;
            if (result is List<GrepSearchResult> results)
            {
                successCount = results.Where(r => r.IsSuccess).Count();
                matchCount = results.Where(r => r.IsSuccess).SelectMany(r => r.Matches).Count();
            }

            if (fileName.EndsWith(".apk.zip", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".ipa.zip", StringComparison.OrdinalIgnoreCase))
            {
                fileName = fileName.Remove(fileName.Length - ".zip".Length);
            }
            var ext = Path.GetExtension(fileName).TrimStart('.');
            var msgResult = L("SearchResultTips").TrimStart('：').TrimStart(':').Replace("${duration}", durationStr).Replace("${matchCount}", matchCount.ToString())
                .Replace("${successCount}", successCount.ToString()).Replace("${processedFiles}", _processedFiles.ToString());
            var grepHistoryDto = new CreateGrepHistoryDto() { ProjectName = projectName, DeployName = deployName, FileName = fileName, FileExtension = ext, Result = msgResult };
            var obj = await _grepHistoryAppService.Create(grepHistoryDto);

            return Ok(new { duration = durationStr, matchCount = matchCount, successCount = successCount, processedFiles = _processedFiles, dataResult = result });
        }

        public ActionResult History(int? page)
        {
            var pageSize = 5;
            var pageNumber = 1;
            if (page.HasValue && page.Value > pageNumber)
            {
                pageNumber = page.Value;
            }

            var pageDto = new PagedGrepHistoryResultRequestDto()
            {
                UserId = AbpSession.UserId.Value,
                SkipCount = (pageNumber - 1) * pageSize,
                MaxResultCount = pageSize
            };

            var pagedList = _grepHistoryAppService.GetPagedGrepHistories(pageDto);
            var result = new StaticPagedList<GrepHistoryDto>(pagedList.Items, pageNumber, pageSize, pagedList.TotalCount);

            return View("History", result);
        }

        public ActionResult Files()
        {
            string filePath = null;
            if (AbpSession.UserId > 1)
            {
                 filePath = Path.Combine(AppContext.BaseDirectory, StoreFileManager.RelativeFilePath, AbpSession.UserId.ToString());
            }
            else
            {
                filePath = Path.Combine(AppContext.BaseDirectory, StoreFileManager.RelativeFilePath);
            }
            var fileNames = Directory.GetFiles(filePath, "*", SearchOption.AllDirectories);
            //var result = new string[fileNames.Length];
            List<WebUploadFileInfo> list = new List<WebUploadFileInfo>();
            for (int i = 0; i< fileNames.Length; i++)
            {
                var f = fileNames[i].Replace(Path.Combine(AppContext.BaseDirectory, StoreFileManager.RelativeFilePath, AbpSession.UserId.ToString()) + "\\", "");
                var webUploadFile = new WebUploadFileInfo() { FileName1 = f };
                if (WebUploadFileExists(f))
                {
                    webUploadFile.FileName2 = f;
                }
                list.Add(webUploadFile);
                //result[i] = f;
            }
            return View("Files", list);
        }

        private bool WebUploadFileExists(string fileName)
        {
            return FileManager.Items.Any(x => x.FileName.Equals(fileName));
        }
    }
}

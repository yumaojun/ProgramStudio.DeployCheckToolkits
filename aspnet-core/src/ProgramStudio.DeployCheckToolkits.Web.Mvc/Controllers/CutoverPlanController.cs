using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgramStudio.DeployCheckToolkits.Controllers;
using ProgramStudio.DeployCheckToolkits.BaseData;
using ProgramStudio.DeployCheckToolkits.BaseData.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using ProgramStudio.DeployCheckToolkits.CutoverPlan.Dto;
using ProgramStudio.DeployCheckToolkits.CutoverPlan;
using X.PagedList;

namespace ProgramStudio.DeployCheckToolkits.Web.Controllers
{
    [AbpMvcAuthorize]
    public class CutoverPlanController : DeployCheckToolkitsControllerBase
    {
        private const string FilePath = "CutoverPlan";
        private readonly IProjectInfoAppService _projectInfoAppService;
        private readonly ICutoverPlanInfoAppService _cutoverPlanInfoAppService;

        public CutoverPlanController(IProjectInfoAppService projectInfoAppService, ICutoverPlanInfoAppService cutoverPlanInfoAppService)
        {
            _projectInfoAppService = projectInfoAppService;
            _cutoverPlanInfoAppService = cutoverPlanInfoAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Configuration()
        {
            var output = await _projectInfoAppService.GetAll(new PagedProjectInfoResultRequestDto { IsActive = true, MaxResultCount = int.MaxValue });
            var projectNames = output.Items.Select(x => x.ProjectName).Distinct().ToList();
            return Json(projectNames);
        }

        [HttpPost]
        public async Task<IActionResult> Uploader([FromForm] IFormCollection formData)
        {
            string fileName = null;
            string filePath = Path.Combine(AppContext.BaseDirectory, FilePath, AbpSession.UserId.ToString());
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
                    if (!"xls".Equals(extNoneDot.ToLower()) && !"xlsx".Equals(extNoneDot.ToLower()))
                    {
                        return BadRequest(L("SelectFileError"));
                    }
                    filePath = Path.Combine(filePath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return Ok(fileName);
        }

        [HttpPost]
        public async Task<IActionResult> Check(string projectName, string fileName)
        {
            List<CutoverPlanInfoDto> cutoverPlanList = new List<CutoverPlanInfoDto>();
            List<CutoverPlanInfoDto> rollbackList = new List<CutoverPlanInfoDto>();
            string filePath = Path.Combine(AppContext.BaseDirectory, FilePath, AbpSession.UserId.ToString(), fileName);

            using (var stream = System.IO.File.OpenRead(filePath))
            {
                IWorkbook workbook = null;
                ISheet sheet0 = null;
                ISheet sheet1 = null;

                var extension = System.IO.Path.GetExtension(filePath);
                if (extension.Equals(".xls"))
                {
                    workbook = new HSSFWorkbook(stream);
                }
                else
                {
                    workbook = WorkbookFactory.Create(stream);
                }

                // 第1个sheet
                sheet0 = workbook.GetSheetAt(0);
                if (sheet0 != null)
                {
                    int titleRowStart = 0;
                    IRow titleRow = sheet0.GetRow(titleRowStart);
                    int rowCellCount = titleRow.LastCellNum;

                    int dataRowStart = 1;
                    int dataRowCount = sheet0.LastRowNum; // 行数

                    // 任务类别、任务项、任务描述
                    int taskTypeColPos = -1, taskItemColPos = -1, taskDescColPos = -1;
                    for (int i = 0; i < rowCellCount; i++)
                    {
                        var titleCell = titleRow.GetCell(i);
                        switch (titleCell.StringCellValue)
                        {
                            case "任务类别":
                            case "任務類別":
                                taskTypeColPos = i;
                                break;
                            case "任务项":
                            case "任務項":
                                taskItemColPos = i;
                                break;
                            case "任务描述":
                            case "任務描述":
                                taskDescColPos = i;
                                break;
                            default:
                                break;
                        }
                    }

                    if (taskTypeColPos < 0)
                    {
                        return BadRequest("部署信息表缺少'任务类别'列");
                    }

                    if (taskItemColPos < 0)
                    {
                        return BadRequest("部署信息表缺少'任务项'列");
                    }

                    if (taskDescColPos < 0)
                    {
                        return BadRequest("部署信息表缺少'任务描述'列");
                    }

                    for (int i = dataRowStart; i <= dataRowCount; i++)
                    {
                        IRow row = sheet0.GetRow(i);
                        if (row == null)
                            continue;

                        var taskType = row.GetCell(taskTypeColPos)?.StringCellValue;
                        var taskItem = row.GetCell(taskItemColPos)?.StringCellValue;
                        var taskDesc = row.GetCell(taskDescColPos)?.StringCellValue;

                        // 条件：projectName='当前选择项目' 且taskType=应用部署
                        if (taskType == null || (!taskType.Equals("应用部署") && !taskType.Equals("應用部署")))
                            continue;
                        // 任务项不能为空
                        if (taskItem == null || taskItem.Trim().Length == 0)
                            continue;

                        var obj = new CutoverPlanInfoDto() { ProjectName = projectName, DeployItemName = taskItem, DeployVersion = taskDesc };
                        cutoverPlanList.Add(obj);
                    }
                }

                if (cutoverPlanList.Count == 0)
                {
                    return BadRequest("部署信息表未查询到相应记录");
                }

                // 第2个sheet
                sheet1 = workbook.GetSheetAt(1);
                if (sheet1 != null)
                {
                    int titleRowStart = 0;
                    IRow titleRow = sheet1.GetRow(titleRowStart);
                    int rowCellCount = titleRow.LastCellNum;

                    int dataRowStart = 1;
                    int dataRowCount = sheet1.LastRowNum; // 行数

                    // 任务类别、任务项、任务描述
                    int taskTypeColPos = -1, taskItemColPos = -1, taskDescColPos = -1;
                    for (int i = 0; i < rowCellCount; i++)
                    {
                        var titleCell = titleRow.GetCell(i);
                        switch (titleCell.StringCellValue)
                        {
                            case "任务类别":
                            case "任務類別":
                                taskTypeColPos = i;
                                break;
                            case "任务项":
                            case "任務項":
                                taskItemColPos = i;
                                break;
                            case "任务描述":
                            case "任務描述":
                                taskDescColPos = i;
                                break;
                            default:
                                break;
                        }
                    }

                    if (taskTypeColPos < 0)
                    {
                        return BadRequest("回滚信息表缺少'任务类别'列");
                    }

                    if (taskItemColPos < 0)
                    {
                        return BadRequest("回滚信息表缺少'任务项'列");
                    }

                    if (taskDescColPos < 0)
                    {
                        return BadRequest("回滚信息表缺少'任务描述'列");
                    }

                    for (int i = dataRowStart; i <= dataRowCount; i++)
                    {
                        IRow row = sheet1.GetRow(i);
                        if (row == null)
                            continue;

                        var taskType = row.GetCell(taskTypeColPos)?.StringCellValue;
                        var taskItem = row.GetCell(taskItemColPos)?.StringCellValue;
                        var taskDesc = row.GetCell(taskDescColPos)?.StringCellValue;

                        if (taskType == null || (!taskType.Equals("版本回滚") && !taskType.Equals("版本回滾")))
                            continue;

                        var obj = new CutoverPlanInfoDto() { ProjectName = projectName, DeployItemName = taskItem, RollbackVersion = taskDesc };
                        rollbackList.Add(obj);
                    }
                }
            }

            // 查询历史记录
            var cutoverPlanHistories = _cutoverPlanInfoAppService.GetCutoverPlanByProjectNameOrderById(projectName);

            foreach (var item in cutoverPlanList)
            {
                var lastCutoverPlanItem = cutoverPlanHistories.FindLast(x => x.DeployItemName == item.DeployItemName && x.DeployVersion != item.DeployVersion);
                if (lastCutoverPlanItem != null)
                {
                    item.LastVersion = lastCutoverPlanItem.DeployVersion;
                    item.CreationTime = lastCutoverPlanItem.CreationTime;
                }

                var oneRollbackItem = rollbackList.FindLast(x => x.DeployItemName == item.DeployItemName);
                if (oneRollbackItem != null)
                {
                    item.RollbackVersion = oneRollbackItem.RollbackVersion;
                }

                await _cutoverPlanInfoAppService.Create(new CreateCutoverPlanInfoDto()
                {
                    ProjectName = item.ProjectName,
                    DeployItemName = item.DeployItemName,
                    DeployVersion = item.DeployVersion,
                    RollbackVersion = item.RollbackVersion
                });
            }

            List<string> messages = new List<string>();

            foreach (var item in cutoverPlanList)
            {
                bool hasMsg = false;
                bool hasDone = false;

                string strStart = $"部署内容{item.DeployItemName}的";

                if (string.IsNullOrEmpty(item.DeployVersion))
                {
                    strStart += "部署版本号为空";
                    hasMsg = true;
                }
                else if (item.DeployVersion.Equals(item.RollbackVersion))
                {
                    strStart += "部署版本号与回滚版本号相同";
                    hasMsg = true;
                }
                //与任一历史版本号相同

                if (string.IsNullOrEmpty(item.RollbackVersion))
                {
                    strStart += (hasMsg ? "，" : "") + "回滚版本号为空";
                    hasDone = true;
                }
                else if (!item.RollbackVersion.Equals(item.LastVersion))
                {
                    strStart += (hasMsg ? "，" : "") + "回滚版本号与最近历史版本号不相同";
                    hasDone = true;
                }
                //与任一历史版本号都不相同

                if (hasMsg || hasDone)
                {
                    messages.Add(strStart);
                }
            }

            return Ok(new { dataResult = cutoverPlanList, messages = messages });
        }

        public ActionResult History(int? page)
        {
            var pageSize = 5;
            var pageNumber = 1;
            if (page.HasValue && page.Value > pageNumber)
            {
                pageNumber = page.Value;
            }

            var pageDto = new PagedCutoverPlanInfoResultRequestDto()
            {
                UserId = AbpSession.UserId.Value,
                SkipCount = (pageNumber - 1) * pageSize,
                MaxResultCount = pageSize
            };

            var pagedList = _cutoverPlanInfoAppService.GetPagedCutoverPlanHistories(pageDto);
            var result = new StaticPagedList<CutoverPlanInfoDto>(pagedList.Items, pageNumber, pageSize, pagedList.TotalCount);

            return View("History", result);
        }
    }
}

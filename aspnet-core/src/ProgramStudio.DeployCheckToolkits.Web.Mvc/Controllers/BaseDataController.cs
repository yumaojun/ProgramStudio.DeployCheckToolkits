using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgramStudio.DeployCheckToolkits.Controllers;
using ProgramStudio.DeployCheckToolkits.BaseData;
using ProgramStudio.DeployCheckToolkits.BaseData.Dto;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System.IO;

namespace ProgramStudio.DeployCheckToolkits.Web.Controllers
{
    [AbpMvcAuthorize]
    public class BaseDataController : DeployCheckToolkitsControllerBase
    {
        private readonly IProjectInfoAppService _projectInfoAppService;

        public BaseDataController(IProjectInfoAppService projectInfoAppService)
        {
            _projectInfoAppService = projectInfoAppService;
        }

        public async Task<ActionResult> Index()
        {
            var output = await _projectInfoAppService.GetAll(new PagedProjectInfoResultRequestDto { MaxResultCount = int.MaxValue }); // Paging not implemented yet
            return View(output);
        }

        [HttpPost]
        public async Task<JsonResult> Create()
        {
            using (var streamReader = new StreamReader(Request.Body))
            {
                var jsonString = streamReader.ReadToEnd();
                CreateProjectInfoDto grepConfigurationDto = JsonConvert.DeserializeObject<CreateProjectInfoDto>(jsonString);
                var result = await _projectInfoAppService.Create(grepConfigurationDto);
                return Json(result);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Update()
        {
            using (var streamReader = new StreamReader(Request.Body))
            {
                var jsonString = streamReader.ReadToEnd();
                UpdateProjectInfoDto grepConfigurationDto = JsonConvert.DeserializeObject<UpdateProjectInfoDto>(jsonString);
                var result = await _projectInfoAppService.Update(grepConfigurationDto);
                return Json(result);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete()
        {
            using (var streamReader = new StreamReader(Request.Body))
            {
                var jsonString = streamReader.ReadToEnd();
                DeleteProjectInfoDto grepConfigurationDto = JsonConvert.DeserializeObject<DeleteProjectInfoDto>(jsonString);
                await _projectInfoAppService.Delete(grepConfigurationDto);
                return Ok();
            }
        }

        public async Task<ActionResult> EditModal(int projectId)
        {
            var projectInfoDto = await _projectInfoAppService.Get(new EntityDto(projectId));
            return View("_EditModal", projectInfoDto);
        }
    }
}

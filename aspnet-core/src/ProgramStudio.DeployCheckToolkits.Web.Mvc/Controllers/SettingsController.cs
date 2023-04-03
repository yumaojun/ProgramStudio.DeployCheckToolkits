using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgramStudio.DeployCheckToolkits.Controllers;
using ProgramStudio.DeployCheckToolkits.DeployCheck;
using ProgramStudio.DeployCheckToolkits.DeployCheck.Dto;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System.IO;

namespace ProgramStudio.DeployCheckToolkits.Web.Controllers
{
    [AbpMvcAuthorize]
    public class SettingsController : DeployCheckToolkitsControllerBase
    {
        private readonly IGrepConfigurationAppService _grepConfigurationAppService;

        public SettingsController(IGrepConfigurationAppService grepConfigurationAppService)
        {
            _grepConfigurationAppService = grepConfigurationAppService;
        }

        public async Task<ActionResult> Index()
        {
            var output = await _grepConfigurationAppService.GetAll(new PagedGrepConfigurationResultRequestDto { MaxResultCount = int.MaxValue }); // Paging not implemented yet
            return View(output);
        }

        public IActionResult Edit()
        {
            return View("Edit");
        }

        public IActionResult Configuration()
        {
            return View("Configuration");
        }

        [HttpPost]
        public async Task<JsonResult> Create()
        {
            using (var streamReader = new StreamReader(Request.Body))
            {
                var jsonString = streamReader.ReadToEnd();
                CreateGrepConfigurationDto grepConfigurationDto = JsonConvert.DeserializeObject<CreateGrepConfigurationDto>(jsonString);
                var result = await _grepConfigurationAppService.Create(grepConfigurationDto);
                return Json(result);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Update()
        {
            using (var streamReader = new StreamReader(Request.Body))
            {
                var jsonString = streamReader.ReadToEnd();
                UpdateGrepConfigurationDto grepConfigurationDto = JsonConvert.DeserializeObject<UpdateGrepConfigurationDto>(jsonString);
                var result = await _grepConfigurationAppService.Update(grepConfigurationDto);
                return Json(result);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete()
        {
            using (var streamReader = new StreamReader(Request.Body))
            {
                var jsonString = streamReader.ReadToEnd();
                DeleteGrepConfigurationDto grepConfigurationDto = JsonConvert.DeserializeObject<DeleteGrepConfigurationDto>(jsonString);
                await _grepConfigurationAppService.Delete(grepConfigurationDto);
                return Ok();
            }
        }

        public async Task<ActionResult> EditSettingModal(int settingId)
        {
            var grepConfigurationDto = await _grepConfigurationAppService.Get(new EntityDto(settingId));
            return View("_EditSettingModal", grepConfigurationDto);
        }
    }
}

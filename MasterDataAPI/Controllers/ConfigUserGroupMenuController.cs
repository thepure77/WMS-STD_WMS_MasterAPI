﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/ConfigUserGroupMenu")]
    [ApiController]
    public class ConfigUserGroupMenuController : ControllerBase
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new ConfigUserGroupMenuService();
                var Models = new SearchConfigUserGroupMenuViewModel();
                Models = JsonConvert.DeserializeObject<SearchConfigUserGroupMenuViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getUserGroupMenu")]
        public IActionResult getUserGroupMenu([FromBody]JObject body)
        {
            try
            {
                var service = new ConfigUserGroupMenuService();
                var Models = new ConfigUserGroupMenuViewModel();
                Models = JsonConvert.DeserializeObject<ConfigUserGroupMenuViewModel>(body.ToString());
                var result = service.getUserGroupMenu(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("confirm")]
        public IActionResult confirm([FromBody]JObject body)
        {
            try
            {
                var service = new ConfigUserGroupMenuService();
                var Models = new actionResultConfigUserGroupMenuViewModel();
                Models = JsonConvert.DeserializeObject<actionResultConfigUserGroupMenuViewModel>(body.ToString());
                var result = service.confirm(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterConfigUserGroupMenu")]
        public IActionResult filterConfigUserGroupMenu([FromBody]JObject body)
        {
            try
            {
                var service = new ConfigUserGroupMenuService();
                var Models = new SearchConfigUserGroupMenuViewModel();
                Models = JsonConvert.DeserializeObject<SearchConfigUserGroupMenuViewModel>(body.ToString());
                var result = service.filterConfigUserGroupMenu(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // EXPORT api/<controller>
        [HttpPost("Export")]
        public IActionResult Export([FromBody] JObject body)
        {
            try
            {
                var service = new ConfigUserGroupMenuService();
                var Models = new ConfigUserGroupMenuExportViewModel();
                Models = JsonConvert.DeserializeObject<ConfigUserGroupMenuExportViewModel>(body.ToString());
                var result = service.Export(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
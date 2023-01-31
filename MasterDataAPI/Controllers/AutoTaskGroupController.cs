using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MasterDataAPI.Controllers
{
    [Route("api/autoTaskGroup")]
    [ApiController]
    public class AutoTaskGroupController : Controller
    {

        [HttpPost("autoTaskGroupSearch")]
        public IActionResult autoTaskGroupSearch([FromBody]JObject body)
        {
            try
            {
                var service = new TaskGroupService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoTaskGroup(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("autoTaskGroupSearchFilter")]
        public IActionResult autoTaskGroupSearchFilter([FromBody]JObject body)
        {
            try
            {
                var service = new TaskGroupService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoTaskGroupSearchFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
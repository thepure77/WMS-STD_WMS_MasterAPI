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
    [Route("api/autoTaskGroupUser")]
    [ApiController]
    public class AutoTaskGroupUserController : Controller
    {


        [HttpPost("autoTaskGroupUserSearchFilter")]
        public IActionResult autoTaskGroupUserSearchFilter([FromBody]JObject body)
        {
            try
            {
                var service = new TaskGroupUserService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoTaskGroupUserSearchFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
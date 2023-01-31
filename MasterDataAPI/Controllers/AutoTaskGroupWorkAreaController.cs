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
    [Route("api/autoTaskGroupWorkArea")]
    [ApiController]
    public class AutoTaskGroupWorkAreaController : Controller
    {


        [HttpPost("autoTaskGroupWorkAreaSearchFilter")]
        public IActionResult autoTaskGroupWorkAreaSearchFilter([FromBody]JObject body)
        {
            try
            {
                var service = new TaskGroupWorkAreaService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoTaskGroupWorkAreaSearchFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
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
    [Route("api/autoWorkArea")]
    [ApiController]
    public class AutoLWorkAreaController : Controller
    {


        [HttpPost("autoSearchWorkArea")]
        public IActionResult autoSearchWorkArea([FromBody]JObject body)
        {
            try
            {
                var service = new WorkAreaService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchWorkArea(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("autoSearchWorkAreaFilter")]
        public IActionResult autoSearchWorkAreaFilter([FromBody]JObject body)
        {
            try
            {
                var service = new WorkAreaService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchWorkAreaFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
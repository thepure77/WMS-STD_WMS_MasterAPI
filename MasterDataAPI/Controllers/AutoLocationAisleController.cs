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
    [Route("api/autoLocationAisle")]
    [ApiController]
    public class AutoLocationAisleController : Controller
    {
        [HttpPost("autoSearchLocationType")]
        public IActionResult autoSearchLocationAisle([FromBody]JObject body)
        {
            try
            {
                var service = new LocationAisleService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchLocationAisle(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("autoSearchLocationAisleFilter")]
        public IActionResult autoSearchLocationAisleFilter([FromBody]JObject body)
        {
            try
            {
                var service = new LocationAisleService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchLocationAisleFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
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
    [Route("api/autoSoldTo")]
    [ApiController]
    public class AutoSoldToController : Controller
    {
        [HttpPost("autoSearchSoldTo")]
        public IActionResult autoSearchSoldTo([FromBody]JObject body)
        {
            try
            {
                var service = new SoldToService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchSoldTo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        

        [HttpPost("autoSearchSoldToFilter")]
        public IActionResult autoSearchSoldToFilter([FromBody]JObject body)
        {
            try
            {
                var service = new SoldToService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchSoldToFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
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
    [Route("api/autoSoldToShipTo")]
    [ApiController]
    public class AutoSoldToShipToController : Controller
    {
   
        [HttpPost("autoSearchSoldToShipToFilter")]
        public IActionResult autoSearchSoldToShipToFilter([FromBody]JObject body)
        {
            try
            {
                var service = new SoldToShipToService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchSoldToShipToFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
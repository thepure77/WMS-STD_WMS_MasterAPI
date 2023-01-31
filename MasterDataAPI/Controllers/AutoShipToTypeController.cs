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
    [Route("api/autoShipToType")]
    [ApiController]
    public class AutoShipTypeController : Controller
    {
        [HttpPost("autoShipToType")]
        public IActionResult AutoShipToType([FromBody]JObject body)
        {
            try
            {
                var service = new ShipToTypeService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoShipToType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost("autoSearchShipToTypeFilter")]
        public IActionResult AutoSearchShipToTypeFilter([FromBody]JObject body)
        {
            try
            {
                var service = new ShipToTypeService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoSearchShipToTypeFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
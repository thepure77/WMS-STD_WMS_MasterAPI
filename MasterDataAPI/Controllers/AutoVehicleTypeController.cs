using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.VehicleType;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MasterDataAPI.Controllers
{
    [Route("api/autoVehicleType")]
    [ApiController]
    public class AutoVehicleTypeController : Controller
    {
        [HttpPost("autoSearchVehicleTypeFilter")]
        public IActionResult autoSearchVehicleTypeFilter([FromBody] JObject body)
        {
             try
            {
                var service = new VehicleTypeService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoVehicleType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

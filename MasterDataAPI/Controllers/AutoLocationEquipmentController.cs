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
    [Route("api/autoLocationEquipment")]
    [ApiController]
    public class AutoLocationEquipmentController : Controller
    {
        [HttpPost("autoSearchLocationEquipmentFilter")]
        public IActionResult autoSearchLocationEquipmentFilter([FromBody]JObject body)
        {
            try
            {
                var service = new LocationEquipmentService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchLocationEquipmentFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpPost("autoSearchLocationEquipmentFilter")]
        //public IActionResult autoSearchLocationEquipmentFilter([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new LocationEquipmentService();
        //        var Models = new ItemListViewModel();
        //        Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
        //        var result = service.autoSearchLocationEquipmentFilter(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
    }
}
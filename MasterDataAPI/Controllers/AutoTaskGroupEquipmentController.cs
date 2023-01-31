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
    [Route("api/autoTaskGroupEquipment")]
    [ApiController]
    public class AutoTaskGroupEquipmentController : Controller
    {


        [HttpPost("autoTaskGroupEquipmentSearchFilter")]
        public IActionResult autoTaskGroupEquipmentSearchFilter([FromBody]JObject body)
        {
            try
            {
                var service = new TaskGroupEquipmentService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoTaskGroupEquipmentSearchFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
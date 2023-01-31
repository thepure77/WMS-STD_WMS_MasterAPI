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
    [Route("api/autoEquipmentType")]
    [ApiController]
    public class AutoEquipmentTypeController : ControllerBase
    {


        [HttpPost("autoEquipmentType")]
        public IActionResult AutoEquipmentType([FromBody]JObject body)
        {
            try
            {
                var service = new EquipmentTypeService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoEquipmentType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("autoSearchEquipmentTypeFilter")]
        public IActionResult AutoSearchEquipmentTypeFilter([FromBody]JObject body)
        {
            try
            {
                var service = new EquipmentTypeService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoSearchEquipmentTypeFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
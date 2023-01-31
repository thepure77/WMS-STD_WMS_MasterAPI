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
    [Route("api/autoEquipmentSubType")]
    [ApiController]
    public class AutoEquipmentSubTypeController : ControllerBase
    {


        [HttpPost("autoEquipmentSubType")]
        public IActionResult AutoEquipmentSubType([FromBody]JObject body)
        {
            try
            {
                var service = new EquipmentSubTypeService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoEquipmentSubType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("autoSearchEquipmentSubTypeFilter")]
        public IActionResult AutoSearchEquipmentSubTypeFilter([FromBody]JObject body)
        {
            try
            {
                var service = new EquipmentSubTypeService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoSearchEquipmentSubTypeFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
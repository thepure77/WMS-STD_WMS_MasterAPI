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
    [Route("api/autoEquipment")]
    [ApiController]
    public class AutoEquipmentController : ControllerBase
    {


        #region AutoEquipment
        [HttpPost("autoSearchEquipment")]
        public IActionResult autoSearchEquipment([FromBody]JObject body)
        {
            try
            {
                var service = new EquipmentService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoEquipment(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
        #region AutoEquipmentFilter
        [HttpPost("autoSearchEquipmentFilter")]
        public IActionResult AutoSearchEquipmentFilter([FromBody]JObject body)
        {
            try
            {
                var service = new EquipmentService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoSearchEquipmentFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


    }
}
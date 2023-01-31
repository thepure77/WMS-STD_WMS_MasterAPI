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
    [Route("api/EquipmentStatus")]
    public class EquipmentStatusController : Controller
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new EquipmentStatusService();
                var result = service.find();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("updateCrane_status")]
        public IActionResult updateCrane_status([FromBody]JObject body)
        {
            try
            {
                var service = new EquipmentStatusService();
                var Models = new EquipmentStatusViewModel();
                Models = JsonConvert.DeserializeObject<EquipmentStatusViewModel>(body.ToString());
                var result = service.update_Crane(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

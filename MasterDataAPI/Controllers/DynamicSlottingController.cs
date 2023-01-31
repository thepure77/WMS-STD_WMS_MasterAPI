using MasterDataBusiness;
using MasterDataBusiness.Currency;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/DynamicSlotting")]
    [ApiController]
    public class DynamicSlottingController : ControllerBase
    {
       
            
        [HttpPost("filterDynamicSlotting")]
        public IActionResult filterDynamicSlotting([FromBody]JObject body)
        {
            try
            {
                var service = new DynamicSlottingService();
                DynamicSlottingViewModel Models = JsonConvert.DeserializeObject<DynamicSlottingViewModel>(body.ToString());
                var result = service.filterDynamicSlotting(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("createDynamicSlotting")]
        public IActionResult createDynamicSlotting([FromBody]JObject body)
        {
            try
            {
                var service = new DynamicSlottingService();
                DynamicSlottingViewModel Models = JsonConvert.DeserializeObject<DynamicSlottingViewModel>(body.ToString());
                var result = service.createDynamicSlotting(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("deleteDynamicSlotting")]
        public IActionResult deleteDynamicSlotting([FromBody]JObject body)
        {
            try
            {
                var service = new DynamicSlottingService();
                DynamicSlottingViewModel Models = JsonConvert.DeserializeObject<DynamicSlottingViewModel>(body.ToString());
                var result = service.deleteDynamicSlotting(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
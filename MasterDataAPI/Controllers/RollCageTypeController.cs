using MasterDataBusiness.RollCageType;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/RollCageType")]
    [ApiController]
    public class RollCageTypeController : ControllerBase
    {
       
            
        [HttpPost("rollCageTypedropdown")]
        public IActionResult rollCageTypedropdown([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageTypeService();
                var Models = new RollCageTypeViewModel();
                Models = JsonConvert.DeserializeObject<RollCageTypeViewModel>(body.ToString());
                var result = service.rollCageTypedropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
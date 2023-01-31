using MasterDataBusiness.RollCage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/RollCage")]
    [ApiController]
    public class RollCageController : ControllerBase
    {
       
            
        [HttpPost("rollCagedropdown")]
        public IActionResult rollCagedropdown([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new RollCageViewModel();
                Models = JsonConvert.DeserializeObject<RollCageViewModel>(body.ToString());
                var result = service.rollCagedropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
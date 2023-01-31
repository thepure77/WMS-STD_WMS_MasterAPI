using MasterDataBusiness.Currency;
using MasterDataBusiness.Weight;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/Weight")]
    [ApiController]
    public class WeightController : ControllerBase
    {
       
            
        [HttpPost("weightdropdown")]
        public IActionResult weightdropdown([FromBody]JObject body)
        {
            try
            {
                var service = new WeightService();
                var Models = new WeightViewModel();
                Models = JsonConvert.DeserializeObject<WeightViewModel>(body.ToString());
                var result = service.weightdropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
using MasterDataBusiness.Chute;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/Chute")]
    [ApiController]
    public class ChuteController : ControllerBase
    {
       
            
        [HttpPost("chutedropdown")]
        public IActionResult chutedropdown([FromBody]JObject body)
        {
            try
            {
                var service = new ChuteService();
                var Models = new ChuteViewModel();
                Models = JsonConvert.DeserializeObject<ChuteViewModel>(body.ToString());
                var result = service.chutedropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
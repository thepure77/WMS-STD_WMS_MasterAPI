using MasterDataBusiness.Currency;
using MasterDataBusiness.Volume;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/Volume")]
    [ApiController]
    public class VolumeController : ControllerBase
    {
       
            
        [HttpPost("volumedropdown")]
        public IActionResult volumedropdown([FromBody]JObject body)
        {
            try
            {
                var service = new VolumeService();
                var Models = new VolumeViewModel();
                Models = JsonConvert.DeserializeObject<VolumeViewModel>(body.ToString());
                var result = service.volumedropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
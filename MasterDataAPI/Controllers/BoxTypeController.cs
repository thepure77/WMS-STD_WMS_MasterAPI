using MasterDataBusiness.BoxType;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/BoxType")]
    [ApiController]
    public class BoxTypeController : ControllerBase
    {
       
            
        [HttpPost("boxTypedropdown")]
        public IActionResult boxTypedropdown([FromBody]JObject body)
        {
            try
            {
                var service = new BoxTypeService();
                var Models = new BoxTypeViewModel();
                Models = JsonConvert.DeserializeObject<BoxTypeViewModel>(body.ToString());
                var result = service.boxTypedropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
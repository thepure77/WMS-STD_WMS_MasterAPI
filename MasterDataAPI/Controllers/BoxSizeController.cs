using MasterDataBusiness.BoxSize;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/BoxSize")]
    [ApiController]
    public class BoxSizeController : ControllerBase
    {
       
            
        [HttpPost("boxSizedropdown")]
        public IActionResult boxSizedropdown([FromBody]JObject body)
        {
            try
            {
                var service = new BoxSizeService();
                var Models = new BoxSizeViewModel();
                Models = JsonConvert.DeserializeObject<BoxSizeViewModel>(body.ToString());
                var result = service.boxSizedropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
using MasterDataBusiness.Rebuild;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/Rebuild")]
    [ApiController]
    public class RebuildController : ControllerBase
    {

        [HttpPost("RebuildIndex")]
        public IActionResult RebuildIndex([FromBody]JObject body)
        {
            try
            {
                var service = new RebuildService();
                var Models = new CloseWaveViewModel();
                Models = JsonConvert.DeserializeObject<CloseWaveViewModel>(body.ToString());
                var result = service.RebuildIndex(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("RebuildSearch")]
        public IActionResult RebuildSearch([FromBody]JObject body)
        {
            try
            {
                var service = new RebuildService();
                var Models = new CloseWaveViewModel();
                Models = JsonConvert.DeserializeObject<CloseWaveViewModel>(body.ToString());
                var result = service.RebuildSearch(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
using MasterDataBusiness.HistoryCloseWave;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/HistoryCloseWave")]
    [ApiController]
    public class HistoryCloseWaveController : ControllerBase
    {

        [HttpPost("HistoryCloseWaveSearch")]
        public IActionResult ControlWave([FromBody]JObject body)
        {
            try
            {
                var service = new HistoryCloseWaveService();
                var Models = new HistoryCloseWaveViewModel();
                Models = JsonConvert.DeserializeObject<HistoryCloseWaveViewModel>(body.ToString());
                var result = service.HistoryCloseWaveSearch(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
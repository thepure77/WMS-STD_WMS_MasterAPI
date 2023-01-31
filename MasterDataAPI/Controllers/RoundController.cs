using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MasterDataBusiness;
using PlanGIBusiness.Round;
using MasterDataBusiness.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/Round")]
    [ApiController]
    public class RoundController : ControllerBase
    {
        [HttpGet("filterRound")]
        public IActionResult getRound()
        {
            try
            {
                var service = new RoundService();

                var result = service.FilterRound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("search")]
        public IActionResult Get([FromBody]JObject body)
        {
            try
            {
                var service = new RoundService();
                var Models = new RoundViewModel();
                Models = JsonConvert.DeserializeObject<RoundViewModel>(body.ToString());
                var result = service.searchRound(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("roundfilter")]
        public IActionResult documentTypefilter([FromBody]JObject body)

        {
            try
            {
                var service = new RoundService();
                var Models = new roundDocViewModel();
                Models = JsonConvert.DeserializeObject<roundDocViewModel>(body.ToString());
                var result = service.roundfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}

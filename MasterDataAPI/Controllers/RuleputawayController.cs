using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/Ruleputaway")]
    [ApiController]
    public class RuleputawayController : ControllerBase
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new RuleputawayService();
                var Models = new SearchRuleputawayViewModel();
                Models = JsonConvert.DeserializeObject<SearchRuleputawayViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new RuleputawayService();
                var result = service.find(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SaveChanges")]
        public IActionResult SaveChanges([FromBody]JObject body)
        {
            try
            {
                var service = new RuleputawayService();
                var Models = new RuleputawayViewModel();
                Models = JsonConvert.DeserializeObject<RuleputawayViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody]JObject body)
        {
            try
            {
                var service = new RuleputawayService();
                var Models = new RuleputawayViewModel();
                Models = JsonConvert.DeserializeObject<RuleputawayViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("GetSugesstionPutaway")]
        public IActionResult GetSugesstionPutaway([FromBody]JObject body)
        {
            try
            {
                var service = new RuleputawayService();
                var Models = new RuleputawayViewModel();
                Models = JsonConvert.DeserializeObject<RuleputawayViewModel>(body.ToString());
                var result = service.GetSugesstionPutaway(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("GetRuleputaway")]
        public IActionResult GetRuleputaway([FromBody]JObject body)
        {
            try
            {
                var service = new RuleputawayService();
                var Models = new RuleputawayViewModel();
                Models = JsonConvert.DeserializeObject<RuleputawayViewModel>(body.ToString());
                var result = service.GetRuleputaway(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

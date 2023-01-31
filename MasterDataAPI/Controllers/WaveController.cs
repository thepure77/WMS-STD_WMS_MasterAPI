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
    [Route("api/Wave")]
    [ApiController]
    public class WaveController : ControllerBase
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new WaveService();
                var Models = new SearchWaveViewModel();
                Models = JsonConvert.DeserializeObject<SearchWaveViewModel>(body.ToString());
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
                var service = new WaveService();
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
                var service = new WaveService();
                var Models = new WaveViewModel();
                Models = JsonConvert.DeserializeObject<WaveViewModel>(body.ToString());
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
                var service = new WaveService();
                var Models = new WaveViewModel();
                Models = JsonConvert.DeserializeObject<WaveViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("waveFilter")]
        public IActionResult zoneFilter([FromBody]JObject body)
        {
            try
            {
                var service = new WaveService();
                var Models = JsonConvert.DeserializeObject<WaveViewModel>(body.ToString());
                var result = service.waveFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("GetWaveRule")]
        public IActionResult GetWaveRule([FromBody]JObject body)
        {
            try
            {
                var service = new WaveService();
                var Models = JsonConvert.DeserializeObject<WaveRuleFilterViewModel>(body.ToString());
                var result = service.GetWaveRule(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("GetViewWaveTemplate")]
        public IActionResult GetViewWaveTemplate([FromBody]JObject body)
        {
            try
            {
                var service = new WaveService();
                var Models = JsonConvert.DeserializeObject<WaveTemplateFilterViewModel>(body.ToString());
                var result = service.GetViewWaveTemplate(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

using MasterDataBusiness.Currency;
using MasterDataBusiness.Forwarder;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/Forwarder")]
    [ApiController]
    public class ForwarderController : ControllerBase
    {
       
            
        [HttpPost("ForwarderFilter")]
        public IActionResult ForwarderFilter([FromBody]JObject body)
        {
            try
            {
                var service = new ForwarderService();
                var Models = new SearchForwarderViewModel();
                Models = JsonConvert.DeserializeObject<SearchForwarderViewModel>(body.ToString());
                var result = service.ForwarderFilter(Models);
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
                var service = new ForwarderService();
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
                var service = new ForwarderService();
                var Models = new ForwarderViewModel();
                Models = JsonConvert.DeserializeObject<ForwarderViewModel>(body.ToString());
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
                var service = new ForwarderService();
                var Models = new ForwarderViewModel();
                Models = JsonConvert.DeserializeObject<ForwarderViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("forwarderDropdown")]
        public IActionResult forwarderDropdown([FromBody]JObject body)
        {
            try
            {
                var service = new ForwarderService();
                var Models = new ForwarderViewModel();
                Models = JsonConvert.DeserializeObject<ForwarderViewModel>(body.ToString());
                var result = service.forwarderDropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
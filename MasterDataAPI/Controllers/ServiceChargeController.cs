using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/ServiceCharge")]
    [ApiController]
    public class ServiceChargeController : ControllerBase
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeService();
                var Models = new SearchServiceChargeViewModel();
                Models = JsonConvert.DeserializeObject<SearchServiceChargeViewModel>(body.ToString());
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
                var service = new ServiceChargeService();
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
                var service = new ServiceChargeService();
                var Models = new ServiceChargeViewModel();
                Models = JsonConvert.DeserializeObject<ServiceChargeViewModel>(body.ToString());
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
                var service = new ServiceChargeService();
                var Models = new ServiceChargeViewModel();
                Models = JsonConvert.DeserializeObject<ServiceChargeViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("dropDownServiceCharge")]
        public IActionResult dropDownServiceCharge([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeService();
                var Models = new ServiceChargeViewModel();
                Models = JsonConvert.DeserializeObject<ServiceChargeViewModel>(body.ToString());
                var result = service.dropDownServiceCharge(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("dropDownServiceChargeFix")]
        public IActionResult dropDownServiceChargeFix([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeService();
                var Models = new View_ServiceChargeFixViewModel();
                Models = JsonConvert.DeserializeObject<View_ServiceChargeFixViewModel>(body.ToString());
                var result = service.dropDownServiceChargeFix(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        

    }
}
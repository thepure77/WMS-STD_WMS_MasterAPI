using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.LocationZoneputaway;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/LocationZoneputaway")]
    [ApiController]
    public class LocationZoneputawayController : ControllerBase
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new LocationZoneputawayService();
                var Models = new SearchLocationZoneputawayViewModel();
                Models = JsonConvert.DeserializeObject<SearchLocationZoneputawayViewModel>(body.ToString());
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
                var service = new LocationZoneputawayService();
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
                var service = new LocationZoneputawayService();
                var Models = new LocationZoneputawayViewModel();
                Models = JsonConvert.DeserializeObject<LocationZoneputawayViewModel>(body.ToString());
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
                var service = new LocationZoneputawayService();
                var Models = new LocationZoneputawayViewModel();
                Models = JsonConvert.DeserializeObject<LocationZoneputawayViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("getLocationZoneputaway")]
        public IActionResult getLocationZoneputaway([FromBody]JObject body)
        {
            try
            {
                var service = new LocationZoneputawayService();
                var Models = new SearchLocationZoneputawayViewModel();
                Models = JsonConvert.DeserializeObject<SearchLocationZoneputawayViewModel>(body.ToString());
                var result = service.getLocationZoneputaway(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ExportExcel")]
        public IActionResult Export([FromBody] JObject body)
        {
            try
            {
                var service = new LocationZoneputawayService();
                var Models = new ResultLocationZoneputawayExportViewModel();
                Models = JsonConvert.DeserializeObject<ResultLocationZoneputawayExportViewModel>(body.ToString());
                var result = service.Export(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

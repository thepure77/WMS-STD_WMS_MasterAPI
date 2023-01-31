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
    [Route("api/Location")]
    public class LocationController : Controller
    {



        // GET api/<controller>/5
        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new LocationService();
                var result = service.find(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<controller>
        [HttpPost("SaveChanges")]
        public IActionResult SaveChanges([FromBody]JObject body)
        {
            try
            {
                var service = new LocationService();
                var Models = new LocationViewModel();
                Models = JsonConvert.DeserializeObject<LocationViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterLocation")]
        public IActionResult filterLocation([FromBody]JObject body)
        {
            try
            {
                var service = new LocationService();
                var Models = new SearchLocationViewModel();
                Models = JsonConvert.DeserializeObject<SearchLocationViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody]JObject body)
        {
            try
            {
                var service = new LocationService();
                var Models = new  LocationViewModel();
                Models = JsonConvert.DeserializeObject<LocationViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("locationFilter")]
        public IActionResult locationFilter([FromBody]JObject body)
        {
            try
            {
                var service = new LocationService();
                var Models = new LocationViewModel();
                Models = JsonConvert.DeserializeObject<LocationViewModel>(body.ToString());
                var result = service.locationFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("GetLocation")]
        public IActionResult GetLocation([FromBody]JObject body)
        {
            try
            {
                var service = new LocationService();
                var Models = new LocationViewModel();
                Models = JsonConvert.DeserializeObject<LocationViewModel>(body.ToString());
                var result = service.GetLocation(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("GetLocationV2")]
        public IActionResult GetLocationV2([FromBody]JObject body)
        {
            try
            {
                var service = new LocationService();
                var Models = new LocationViewModel();
                Models = JsonConvert.DeserializeObject<LocationViewModel>(body.ToString());
                var result = service.GetLocationV2(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("LocationConfig")]
        public IActionResult LocationConfig([FromBody]JObject body)
        {
            try
            {
                var service = new LocationService();
                var Models = new LocationConfigViewModel();
                Models = JsonConvert.DeserializeObject<LocationConfigViewModel>(body.ToString());
                var result = service.LocationConfig(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ConfigViewCyclecount")]
        public IActionResult ConfigViewCyclecount([FromBody]JObject body)
        {
            try
            {
                var service = new LocationService();
                var Models = new View_LocatinCyclecountViewModel();
                Models = JsonConvert.DeserializeObject<View_LocatinCyclecountViewModel>(body.ToString());
                var result = service.ConfigViewCyclecount(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ConfigViewTaskGroupLocationWorkArea")]
        public IActionResult ConfigViewTaskGroupLocationWorkArea([FromBody]JObject body)
        {
            try
            {
                var service = new LocationService();
                var Models = new View_TaskGroupLocationWorkAreaViewModel();
                Models = JsonConvert.DeserializeObject<View_TaskGroupLocationWorkAreaViewModel>(body.ToString());
                var result = service.ConfigViewTaskGroupLocationWorkArea(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ConfigfindlocationType")]
        public IActionResult ConfigfindlocationType([FromBody]JObject body)
        {
            try
            {
                var service = new LocationService();
                var Models = new LocationConfigViewModel();
                Models = JsonConvert.DeserializeObject<LocationConfigViewModel>(body.ToString());
                var result = service.ConfigfindlocationType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("checkLocation")]
        public IActionResult checkLocation([FromBody]JObject body)
        {
            try
            {
                var service = new LocationService();
                var Models = new LocationViewModel();
                Models = JsonConvert.DeserializeObject<LocationViewModel>(body.ToString());
                var result = service.checkLocation(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Export")]
        public IActionResult Export([FromBody] JObject body)
        {
            try
            {
                var service = new LocationService();
                var Models = new LocationExportViewModel();
                Models = JsonConvert.DeserializeObject<LocationExportViewModel>(body.ToString());
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

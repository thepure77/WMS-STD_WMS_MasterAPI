using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.LocationLock;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/LocationAisle")]
    public class LocationAisleController : Controller
    {
        // GET api/<controller>/5
        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new LocationAisleService();
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
                var service = new LocationAisleService();
                var Models = new LocationAisleViewModel();
                Models = JsonConvert.DeserializeObject<LocationAisleViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterLocationAisle")]
        public IActionResult filterLocationAisle([FromBody]JObject body)
        {
            try
            {
                var service = new LocationAisleService();
                var Models = new SearchLocationAisleViewModel();
                Models = JsonConvert.DeserializeObject<SearchLocationAisleViewModel>(body.ToString());
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
                var service = new LocationAisleService();
                var Models = new LocationAisleViewModel();
                Models = JsonConvert.DeserializeObject<LocationAisleViewModel>(body.ToString());
                var result = service.getDelete(Models);
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
                var service = new LocationAisleService();
                var Models = new ResultLocationAisleExportViewModel();
                Models = JsonConvert.DeserializeObject<ResultLocationAisleExportViewModel>(body.ToString());
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

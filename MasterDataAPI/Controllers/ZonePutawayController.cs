using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using MasterDataBusiness.ZonePutaway;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MasterDataAPI.Controllers
{
    [Route("api/ZonePutaway")]
    [ApiController]
    public class ZonePutawayController : ControllerBase
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new ZonePutawayService();
                var Models = new SearchZonePutawayViewModel();
                Models = JsonConvert.DeserializeObject<SearchZonePutawayViewModel>(body.ToString());
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
                var service = new ZonePutawayService();
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
                var service = new ZonePutawayService();
                var Models = new ZonePutawayViewModel();
                Models = JsonConvert.DeserializeObject<ZonePutawayViewModel>(body.ToString());
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
                var service = new ZonePutawayService();
                var Models = new ZonePutawayViewModel();
                Models = JsonConvert.DeserializeObject<ZonePutawayViewModel>(body.ToString());
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
                var service = new ZonePutawayService();
                var Models = new ResultZonePutawayExportViewModel();
                Models = JsonConvert.DeserializeObject<ResultZonePutawayExportViewModel>(body.ToString());
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
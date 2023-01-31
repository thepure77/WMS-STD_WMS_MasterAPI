using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterDataAPI.Controllers
{
    [Route("api/Sloc")]
    public class SlocController : Controller
    {

        [HttpPost("filterSloc")]
        public IActionResult filterSloc([FromBody] JObject body)
        {
            try
            {
                var service = new SlocService();
                var Models = new SearchSlocViewModel();
                Models = JsonConvert.DeserializeObject<SearchSlocViewModel>(body.ToString());
                var result = service.filterSloc(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        // GET api/<controller>/5
        [HttpGet("find/{id}")]
        public IActionResult find(string id)
        {
            try
            {
                var service = new SlocService();
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
        public IActionResult SaveChanges([FromBody] JObject body)
        {
            try
            {
                var service = new SlocService();
                var Models = new SlocViewModel();
                Models = JsonConvert.DeserializeObject<SlocViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] JObject body)
        {
            try
            {
                var service = new SlocService();
                var Models = new SlocViewModel();
                Models = JsonConvert.DeserializeObject<SlocViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // EXPORT api/<controller>
        [HttpPost("Export")]
        public IActionResult Export([FromBody] JObject body)
        {
            try
            {
                var service = new SlocService();
                var Models = new SlocExportViewModel();
                Models = JsonConvert.DeserializeObject<SlocExportViewModel>(body.ToString());
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

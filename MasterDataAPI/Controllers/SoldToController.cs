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
    [Route("api/SoldTo")]
    public class SoldToController : Controller
    {
        


        // GET api/<controller>/5
        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new SoldToService();
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
                var service = new SoldToService();
                var Models = new SoldToViewModel();
                Models = JsonConvert.DeserializeObject<SoldToViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filter_in")]
        public IActionResult FilterInClause([FromBody]JObject body)
        {
            try
            {
                var service = new SoldToService();
                var result = service.FilterInClause(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterSoldTo")]
        public IActionResult filterSoldTo([FromBody]JObject body)
        {
            try
            {
                var service = new SoldToService();
                var Models = new SearchSoldToViewModel();
                Models = JsonConvert.DeserializeObject<SearchSoldToViewModel>(body.ToString());
                var result = service.filterSoldTo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterPopupSoldTo")]
        public IActionResult filterPopupSoldTo([FromBody]JObject body)
        {
            try
            {
                var service = new SoldToService();
                var Models = new SoldToPopupViewModel();
                Models = JsonConvert.DeserializeObject<SoldToPopupViewModel>(body.ToString());
                var result = service.filterPopupSoldTo(Models);
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
                var service = new SoldToService();
                var Models = new SoldToViewModel();
                Models = JsonConvert.DeserializeObject<SoldToViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new SoldToService();
                var Models = new SoldToViewModel();
                Models = JsonConvert.DeserializeObject<SoldToViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ExportExcel")]
        public IActionResult ExportExcel([FromBody]JObject body)
        {
            try
            {
                var service = new SoldToService();
                var Models = new SearchSoldToViewModel();
                Models = JsonConvert.DeserializeObject<SearchSoldToViewModel>(body.ToString());
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

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
    [Route("api/SoldToShipTo")]
    public class SoldToShipToController : Controller
    {//GET api/<controller>/5
        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new SoldToShipToService();
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
                var service = new SoldToShipToService();
                var Models = new SoldToShipToViewModel();
                Models = JsonConvert.DeserializeObject<SoldToShipToViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterSoldToShipTo")]
        public IActionResult filterSoldToShipTo([FromBody]JObject body)
        {
            try
            {
                var service = new SoldToShipToService();
                var Models = new SearchSoldToShipToViewModel();
                Models = JsonConvert.DeserializeObject<SearchSoldToShipToViewModel>(body.ToString());
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
                var service = new SoldToShipToService();
                var Models = new SoldToShipToViewModel();
                Models = JsonConvert.DeserializeObject<SoldToShipToViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SaveSoldToShipToList")]
        public IActionResult SaveSoldToShipToList([FromBody]JObject body)
        {
            try
            {
                var service = new SoldToShipToService();
                var Models = new SoldToShipToViewModel();
                Models = JsonConvert.DeserializeObject<SoldToShipToViewModel>(body.ToString());
                var result = service.SaveSoldToShipToList(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPost("filterShipTo")]
        public IActionResult filterShipTo([FromBody]JObject body)
        {
            try
            {
                var service = new SoldToShipToService();
                var Models = new SearchSoldToShipToViewModel();
                Models = JsonConvert.DeserializeObject<SearchSoldToShipToViewModel>(body.ToString());
                var result = service.filterShipTo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}

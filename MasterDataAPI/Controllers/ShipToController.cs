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
    [Route("api/ShipTo")]
    public class ShipToController : Controller
    {



        // GET api/<controller>/5
        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new ShipToService();
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
                var service = new ShipToService();
                var Models = new ShipToViewModel();
                Models = JsonConvert.DeserializeObject<ShipToViewModel>(body.ToString());
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
                var service = new ShipToService();
                var result = service.FilterInClause(body?.ToString() ?? string.Empty);
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
                var service = new ShipToService();
                var Models = new SearchShipToViewModel();
                Models = JsonConvert.DeserializeObject<SearchShipToViewModel>(body.ToString());
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
                var service = new ShipToService();
                var Models = new ShipToViewModel();
                Models = JsonConvert.DeserializeObject<ShipToViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
               
        [HttpPost("filterShiptoPopup")]
        public IActionResult filterShiptoPopup([FromBody]JObject body)
        {
            try
            {
                var service = new ShipToService();
                var Models = new SearchShipToViewModel();
                Models = JsonConvert.DeserializeObject<SearchShipToViewModel>(body.ToString());
                var result = service.filterShiptoPopup(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
                
        [HttpPost("shipToFilter")]
        public IActionResult shipToFilter([FromBody]JObject body)

        {
            try
            {
                var service = new ShipToService();
                var Models = new ShipToViewModel();
                Models = JsonConvert.DeserializeObject<ShipToViewModel>(body.ToString());
                var result = service.shipToFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("shipTotypeFilter")]
        public IActionResult shipTotypeFilter([FromBody]JObject body)

        {
            try
            {
                var service = new ShipToService();
                var Models = new ShipToViewModel();
                Models = JsonConvert.DeserializeObject<ShipToViewModel>(body.ToString());
                var result = service.shipTotypeFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }


        [HttpPost("genBranchCode")]
        public IActionResult genBranchCode([FromBody]JObject body)

        {
            try
            {
                var service = new ShipToService();
                var Models = new BranchCodeViewModel();
                Models = JsonConvert.DeserializeObject<BranchCodeViewModel>(body.ToString());
                var result = service.genBranchCode(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        [HttpPost("Export")]
        public IActionResult Export([FromBody] JObject body)
        {
            try
            {
                var service = new ShipToService();
                var Models = new ShipToExportViewModel();
                Models = JsonConvert.DeserializeObject<ShipToExportViewModel>(body.ToString());
                var result = service.Export(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("updateShipToTMS")]
        public IActionResult updateShipToByTMS([FromBody]JObject body)

        {
            try
            {
                var service = new ShipToService();
                var Models = new ShipToViewModel();
                Models = JsonConvert.DeserializeObject<ShipToViewModel>(body.ToString());
                var result = service.updateShipToByTMS(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}

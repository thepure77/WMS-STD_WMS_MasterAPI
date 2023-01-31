using MasterDataBusiness.Currency;
using MasterDataBusiness.ShipmentType;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/ShipmentType")]
    [ApiController]
    public class ShipmentTypeController : ControllerBase
    {
       
            
        [HttpPost("ShipmentTypeFilter")]
        public IActionResult ShipmentTypeFilter([FromBody]JObject body)
        {
            try
            {
                var service = new ShipmentTypeService();
                var Models = new SearchShipmentTypeViewModel();
                Models = JsonConvert.DeserializeObject<SearchShipmentTypeViewModel>(body.ToString());
                var result = service.ShipmentTypeFilter(Models);
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
                var service = new ShipmentTypeService();
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
                var service = new ShipmentTypeService();
                var Models = new ShipmentTypeViewModel();
                Models = JsonConvert.DeserializeObject<ShipmentTypeViewModel>(body.ToString());
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
                var service = new ShipmentTypeService();
                var Models = new ShipmentTypeViewModel();
                Models = JsonConvert.DeserializeObject<ShipmentTypeViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ShipmentTypeDropdown")]
        public IActionResult ShipmentTypeDropdown([FromBody]JObject body)
        {
            try
            {
                var service = new ShipmentTypeService();
                var Models = new ShipmentTypeViewModel();
                Models = JsonConvert.DeserializeObject<ShipmentTypeViewModel>(body.ToString());
                var result = service.ShipmentTypeDropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
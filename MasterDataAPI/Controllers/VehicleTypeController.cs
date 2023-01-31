using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness.VehicleType;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/VehicleType")]
    public class VehicleTypeController : Controller
    {
        [HttpPost("vehicleTypedropdown")]
        public IActionResult vehicleTypedropdown([FromBody]JObject body)
        {
            try
            {
                var service = new VehicleTypeService();
                var Models = new VehicleTypeViewModel();
                Models = JsonConvert.DeserializeObject<VehicleTypeViewModel>(body.ToString());
                var result = service.vehicleTypedropdown(Models);
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
                var service = new VehicleTypeService();
                var Models = new SearchVehicleTypeViewModel();
                Models = JsonConvert.DeserializeObject<SearchVehicleTypeViewModel>(body.ToString());
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
                var service = new VehicleTypeService();
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
                var service = new VehicleTypeService();
                var Models = new VehicleTypeViewModel();
                Models = JsonConvert.DeserializeObject<VehicleTypeViewModel>(body.ToString());
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
                var service = new VehicleTypeService();
                var Models = new VehicleTypeViewModel();
                Models = JsonConvert.DeserializeObject<VehicleTypeViewModel>(body.ToString());
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
                var service = new VehicleTypeService();
                var Models = new VehicleTypeExportViewModel();
                Models = JsonConvert.DeserializeObject<VehicleTypeExportViewModel>(body.ToString());
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

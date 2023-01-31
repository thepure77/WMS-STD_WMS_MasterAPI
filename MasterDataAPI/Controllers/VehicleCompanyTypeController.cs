using MasterDataBusiness.VehicleCompanyType;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/VehicleCompanyType")]
    [ApiController]
    public class VehicleCompanyTypeController : ControllerBase
    {
       
            
        [HttpPost("vehicleCompanyTypedropdown")]
        public IActionResult vehicleCompanyTypedropdown([FromBody]JObject body)
        {
            try
            {
                var service = new VehicleCompanyTypeService();
                var Models = new VehicleCompanyTypeViewModel();
                Models = JsonConvert.DeserializeObject<VehicleCompanyTypeViewModel>(body.ToString());
                var result = service.vehicleCompanyTypedropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
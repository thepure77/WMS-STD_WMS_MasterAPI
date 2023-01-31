using MasterDataBusiness. VehicleCompany;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/VehicleCompany")]
    [ApiController]
    public class  VehicleCompanyController : ControllerBase
    {
       
            
        [HttpPost("vehicleCompanydropdown")]
        public IActionResult  vehicleCompanydropdown([FromBody]JObject body)
        {
            try
            {
                var service = new  VehicleCompanyService();
                var Models = new  VehicleCompanyViewModel();
                Models = JsonConvert.DeserializeObject< VehicleCompanyViewModel>(body.ToString());
                var result = service.vehicleCompanydropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness.CargoType;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/CargoType")]
    public class CargoTypeController : Controller
    {
        [HttpPost("cargoTypedropdown")]
        public IActionResult cargoTypedropdown([FromBody]JObject body)
        {
            try
            {
                var service = new CargoTypeService();
                var Models = new CargoTypeViewModel();
                Models = JsonConvert.DeserializeObject<CargoTypeViewModel>(body.ToString());
                var result = service.cargoTypedropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}

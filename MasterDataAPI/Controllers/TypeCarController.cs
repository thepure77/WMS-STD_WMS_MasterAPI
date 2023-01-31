using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using MasterDataBusiness.ZoneLocation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/TypeCar")]
    public class TypeCarController : Controller
    {
        [HttpPost("typeCarfilter")]
        public IActionResult documentTypefilter([FromBody]JObject body)

        {
            try
            {
                var service = new TypeCarService();
                var Models = new TypeCarViewModel();
                Models = JsonConvert.DeserializeObject<TypeCarViewModel>(body.ToString());
                var result = service.TypeCarilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }


    }
}

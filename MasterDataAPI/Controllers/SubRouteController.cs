using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlanGIBusiness.Route;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/SubRoute")]
    [ApiController]
    public class SubRouteController : ControllerBase
    {
    
        [HttpPost("subRoutefilter")]
        public IActionResult subRoutefilter([FromBody]JObject body)

        {
            try
            {
                var service = new SubRouteService();
                var Models = new SubRouteViewModel();
                Models = JsonConvert.DeserializeObject<SubRouteViewModel>(body.ToString());
                var result = service.subRoutefilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}

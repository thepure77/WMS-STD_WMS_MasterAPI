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
    [Route("api/Route")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        [HttpGet("filterRoute")]
        public IActionResult getRoute()
        {
            try
            {
                var service = new RouteService();

                var result = service.FilterRoute();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("search")]
        public IActionResult Get([FromBody]JObject body)
        {
            try
            {
                var service = new RouteService();
                var Models = new RouteViewModel();
                Models = JsonConvert.DeserializeObject<RouteViewModel>(body.ToString());
                var result = service.search(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("routefilter")]
        public IActionResult documentTypefilter([FromBody]JObject body)

        {
            try
            {
                var service = new RouteService();
                var Models = new RouteViewModelV2();
                Models = JsonConvert.DeserializeObject<RouteViewModelV2>(body.ToString());
                var result = service.routefilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}

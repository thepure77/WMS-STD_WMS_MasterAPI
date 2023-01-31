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
    [Route("api/Transport")]
    public class TransportController : Controller
    {
        [HttpPost("transportfilter")]
        public IActionResult documentTypefilter([FromBody]JObject body)

        {
            try
            {
                var service = new TransportService();
                var Models = new TransportViewModel();
                Models = JsonConvert.DeserializeObject<TransportViewModel>(body.ToString());
                var result = service.transportfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}

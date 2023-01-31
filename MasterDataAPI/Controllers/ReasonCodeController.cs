using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MasterDataAPI.Controllers
{
    [Route("api/ReasonCode")]

    public class ReasonCodeController : Controller
    {
        [HttpPost("GetReasonCode")]
        public IActionResult GetReasonCode([FromBody]JObject body)
        {
            try
            {
                var service = new ReasonCodeService();
                var Models = JsonConvert.DeserializeObject<ReasonCodeFilterViewModel>(body.ToString());
                var result = service.GetReasonCode(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}

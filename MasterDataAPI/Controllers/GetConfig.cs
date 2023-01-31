using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/GetConfig")]
    [ApiController]
    public class GetConfig : ControllerBase
    {
        [HttpPost("GetConfig")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new GetConfigFromBaseService();
                var Models = new GetConfigFromBaseViewModel();
                Models = JsonConvert.DeserializeObject<GetConfigFromBaseViewModel>(body.ToString());
                var result = service.GetConfigFromBase(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("GetConfigListString")]
        public IActionResult GetConfigListString([FromBody]JObject body)
        {
            try
            {
                var service = new GetConfigFromBaseService();
                var Models = new GetConfigFromBaseViewModel();
                Models = JsonConvert.DeserializeObject<GetConfigFromBaseViewModel>(body.ToString());
                var result = service.GetConfigListString(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

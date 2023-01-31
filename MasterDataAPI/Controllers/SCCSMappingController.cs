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
    [Route("api/SCCSMapping")]
    public class SCCSMappingController : Controller
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody] JObject body)
        {
            try
            {
                var service = new SCCSMappingService();
                var Models = new SearchSCCSMappingViewModel();
                Models = JsonConvert.DeserializeObject<SearchSCCSMappingViewModel>(body.ToString());
                var result = service.filter(Models);
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
                var service = new SCCSMappingService();
                var Models = new SCCSMappingExportViewModel();
                Models = JsonConvert.DeserializeObject<SCCSMappingExportViewModel>(body.ToString());
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

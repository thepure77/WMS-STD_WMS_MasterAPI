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
    [Route("api/ImportFile")]
    [ApiController]
    public class ImportFileController : ControllerBase
    {
        [HttpPost("ImportFile")]
        public IActionResult ImportFile([FromBody]JObject body)
        {
            try
            {
                var service = new ImportFileService();
                var Models = new ImportFileViewModel();
                Models = JsonConvert.DeserializeObject<ImportFileViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness.DocumentPriority;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/DocumentPriority")]
    public class DocumentPriorityController : Controller
    {
        [HttpPost("DocumentPrioritydropdown")]
        public IActionResult DocumentPrioritydropdown([FromBody]JObject body)
        {
            try
            {
                var service = new DocumentPriorityService();
                var Models = new DocumentPriorityViewModel();
                Models = JsonConvert.DeserializeObject<DocumentPriorityViewModel>(body.ToString());
                var result = service.DocumentPrioritydropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}

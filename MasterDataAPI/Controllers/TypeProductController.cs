using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterDataAPI.Controllers
{
    [Route("api/TypeProduct")]
    public class TypeProductController : ControllerBase
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody] JObject body)
        {
            try
            {
                var service = new TypeProductService();
                var Models = new SearchTypeProductViewModel();
                Models = JsonConvert.DeserializeObject<SearchTypeProductViewModel>(body.ToString());
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

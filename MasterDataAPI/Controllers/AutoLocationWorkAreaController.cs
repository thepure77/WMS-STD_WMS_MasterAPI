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
    [Route("api/autoLocationWorkArea")]
    [ApiController]
    public class AutoLocationWorkAreaController : Controller
    {
       

        [HttpPost("autoLocationSearchFilter")]
        public IActionResult autoLocationSearchFilter([FromBody]JObject body)
        {
            try
            {
                var service = new LocationWorkAreaService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoLocationSearchFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
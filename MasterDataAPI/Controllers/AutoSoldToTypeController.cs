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
    [Route("api/autoSoldToType")]
    [ApiController]
    public class AutoSoldTypeController : Controller
    {
        [HttpPost("autoSoldToType")]
        public IActionResult AutoSoldToType([FromBody]JObject body)
        {
            try
            {
                var service = new SoldToTypeService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoSoldToType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost("autoSearchSoldToTypeFilter")]
        public IActionResult AutoSearchSoldToTypeFilter([FromBody]JObject body)
        {
            try
            {
                var service = new SoldToTypeService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoSearchSoldToTypeFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
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
    [Route("api/autoItemStatus")]
    [ApiController]
    public class AutoItemStatusController : Controller
    {
        //[HttpPost("autoSearchItemStatus")]
        //public IActionResult autoSearchItemStatus([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ItemStatusService();
        //        var Models = new ItemListViewModel();
        //        Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
        //        var result = service.autoSearchItemStatus(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [HttpPost("autoSearchItemStatusFilter")]
        public IActionResult autoSearchItemStatusFilter([FromBody]JObject body)
        {
            try
            {
                var service = new ItemStatusService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchItemStatusFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
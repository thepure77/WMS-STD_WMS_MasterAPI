﻿using System;
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
    [Route("api/autoSloc")]
    [ApiController]
    public class AutoSlocController : Controller
    {
        [HttpPost("autoSearchSlocFilter")]
        public IActionResult autoSearchSlocFilter([FromBody] JObject body)
        {
            try
            {
                var service = new SlocService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSloc(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

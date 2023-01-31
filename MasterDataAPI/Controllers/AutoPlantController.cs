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
    [Route("api/autoPlant")]
    [ApiController]
    public class AutoPlantController : Controller
    {
        [HttpPost("autoSearchPlantFilter")]
        public IActionResult autoSearchPlantFilter([FromBody] JObject body)
        {
            try
            {
                var service = new PlantService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchPlantFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #region autoPlantType
        [HttpPost("autoPlantType")]
        public IActionResult autoPlantType([FromBody] JObject body)
        {
            try
            {
                var service = new PlantService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoPlantType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        #endregion
    }
}

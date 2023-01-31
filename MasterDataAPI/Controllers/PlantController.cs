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
    [Route("api/Plant")]
    public class PlantController : Controller
    {
        [HttpPost ("filterPlant")]
        public IActionResult filterPlant([FromBody] JObject body)
        {
            try
            {
                var service = new PlantService();
                var Models = new SearchPlantViewModel();
                Models = JsonConvert.DeserializeObject<SearchPlantViewModel>(body.ToString());
                var result = service.filterPlant(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new PlantService();
                var result = service.find(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<controller>
        [HttpPost("SaveChanges")]
        public IActionResult SaveChanges([FromBody] JObject body)
        {
            try
            {
                var service = new PlantService();
                var Models = new PlantViewModel();
                Models = JsonConvert.DeserializeObject<PlantViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] JObject body)
        {
            try
            {
                var service = new PlantService();
                var Models = new PlantViewModel();
                Models = JsonConvert.DeserializeObject<PlantViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Export")]
        public IActionResult Export([FromBody] JObject body)
        {
            try
            {
                var service = new PlantService();
                var Models = new PlantExportViewModel();
                Models = JsonConvert.DeserializeObject<PlantExportViewModel>(body.ToString());
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

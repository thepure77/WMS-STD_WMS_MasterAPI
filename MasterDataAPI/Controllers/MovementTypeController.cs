using MasterDataBusiness.CostCenter;
using MasterDataBusiness.MovementType;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/MovementType")]
    [ApiController]
    public class MovementTypeController : ControllerBase
    {
        [HttpPost("MovementTypefilter")]
        public IActionResult MovementTypefilter([FromBody]JObject body)
        {
            try
            {
                var service = new MovementTypeService();
                var Models = new SearchMovementTypeViewModel();
                Models = JsonConvert.DeserializeObject<SearchMovementTypeViewModel>(body.ToString());
                var result = service.movementTypefilter(Models);
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
                var service = new MovementTypeService();
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
        public IActionResult SaveChanges([FromBody]JObject body)
        {
            try
            {
                var service = new MovementTypeService();
                var Models = new MovementTypeViewModel();
                Models = JsonConvert.DeserializeObject<MovementTypeViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody]JObject body)
        {
            try
            {
                var service = new MovementTypeService();
                var Models = new MovementTypeViewModel();
                Models = JsonConvert.DeserializeObject<MovementTypeViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("autoSearchMovementTypeFilter")]
        public IActionResult autoSearchMovementTypeFilter([FromBody]JObject body)
        {
            try
            {
                var service = new MovementTypeService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchMovementTypeFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
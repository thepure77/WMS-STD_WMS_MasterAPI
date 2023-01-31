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
    [Route("api/Warehouse")]
    public class WarehouseController : Controller
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new WarehouseService();
                var Models = new SearchWarehouseViewModel();
                Models = JsonConvert.DeserializeObject<SearchWarehouseViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filter_in")]
        public IActionResult FilterInClause([FromBody]JObject body)
        {
            try
            {
                var service = new WarehouseService();
                var result = service.FilterInClause(body is null ? string.Empty : body.ToString());
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
                var service = new WarehouseService();
                var result = service.find(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SaveChanges")]
        public IActionResult SaveChanges([FromBody]JObject body)
        {
            try
            {
                var service = new WarehouseService();
                var Models = new WarehouseViewModel();
                Models = JsonConvert.DeserializeObject<WarehouseViewModel>(body.ToString());
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
                var service = new WarehouseService();
                var Models = new WarehouseViewModel();
                Models = JsonConvert.DeserializeObject<WarehouseViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("warehousefilter")]
        public IActionResult warehousefilter([FromBody]JObject body)
        {
            try
            {
                var service = new WarehouseService();
                var Models = new warehouseDocViewModel();
                Models = JsonConvert.DeserializeObject<warehouseDocViewModel>(body.ToString());
                var result = service.warehousefilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}

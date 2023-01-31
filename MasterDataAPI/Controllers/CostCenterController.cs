using MasterDataBusiness.CostCenter;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/CostCenter")]
    [ApiController]
    public class CostCenterController : ControllerBase
    {
        [HttpPost("CostCenterfilter")]
        public IActionResult CostCenterfilter([FromBody]JObject body)
        {
            try
            {
                var service = new CostCenterService();
                var Models = new SearchCostCenterViewModel();
                Models = JsonConvert.DeserializeObject<SearchCostCenterViewModel>(body.ToString());
                var result = service.costCenterfilter(Models);
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
                var service = new CostCenterService();
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
                var service = new CostCenterService();
                var Models = new CostCenterViewModel();
                Models = JsonConvert.DeserializeObject<CostCenterViewModel>(body.ToString());
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
                var service = new CostCenterService();
                var Models = new CostCenterViewModel();
                Models = JsonConvert.DeserializeObject<CostCenterViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("autoSearchCostCenterFilter")]
        public IActionResult autoSearchCostCenterFilter([FromBody]JObject body)
        {
            try
            {
                var service = new CostCenterService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchCostCenterFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("CostCenterfilterdropdown")]
        public IActionResult CostCenterfilterdropdown([FromBody]JObject body)
        {
            try
            {
                var service = new CostCenterService();
                var Models = new CostCenterViewModel();
                Models = JsonConvert.DeserializeObject<CostCenterViewModel>(body.ToString());
                var result = service.CostCenterfilterdropdown(Models);
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
                var service = new CostCenterService();
                var Models = new CostCenterExportViewModel();
                Models = JsonConvert.DeserializeObject<CostCenterExportViewModel>(body.ToString());
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
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
    [Route("api/Owner")]
    public class OwnerController : Controller
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new OwnerService();
                var Models = new SearchOwnerViewModel();
                Models = JsonConvert.DeserializeObject<SearchOwnerViewModel>(body.ToString());
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
                var service = new OwnerService();
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
                var service = new OwnerService();
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
                var service = new OwnerService();
                var Models = new OwnerViewModel();
                Models = JsonConvert.DeserializeObject<OwnerViewModel>(body.ToString());
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
                var service = new OwnerService();
                var Models = new OwnerViewModel();
                Models = JsonConvert.DeserializeObject<OwnerViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ownerfilter")]
        public IActionResult ownerfilter([FromBody]JObject body)

        {
            try
            {
                var service = new OwnerService();
                var Models = new OwnerViewModel();
                Models = JsonConvert.DeserializeObject<OwnerViewModel>(body.ToString());
                var result = service.ownerfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("filterOwnerPopupV2")]
        public IActionResult filterOwnerPopupV2([FromBody]JObject body)
        {
            try
            {
                var service = new OwnerService();
                var Models = new SearchOwnerViewModel();
                Models = JsonConvert.DeserializeObject<SearchOwnerViewModel>(body.ToString());
                var result = service.filterOwnerPopupV2(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

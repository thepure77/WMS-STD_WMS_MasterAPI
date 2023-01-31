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
    [Route("api/ProductAssembly")]
    public class ProductAssemblyController : Controller
    {



        // GET api/<controller>/5
        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new ProductAssemblyService();
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
                var service = new ProductAssemblyService();
                var Models = new ProductAssemblyViewModel();
                Models = JsonConvert.DeserializeObject<ProductAssemblyViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterProductAssembly")]
        public IActionResult filterProductAssembly([FromBody]JObject body)
        {
            try
            {
                var service = new ProductAssemblyService();
                var Models = new SearchProductAssemblyViewModel();
                Models = JsonConvert.DeserializeObject<SearchProductAssemblyViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody]JObject body)
        {
            try
            {
                var service = new ProductAssemblyService();
                var Models = new ProductAssemblyViewModel();
                Models = JsonConvert.DeserializeObject<ProductAssemblyViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}

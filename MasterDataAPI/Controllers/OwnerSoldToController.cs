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
    [Route("api/OwnerSoldTo")]
    public class OwnerSoldToController : Controller
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new OwnerSoldToService();
                var Models = new SearchOwnerSoldToViewModel();
                Models = JsonConvert.DeserializeObject<SearchOwnerSoldToViewModel>(body.ToString());
                var result = service.filter(Models);
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
                var service = new OwnerSoldToService();
                var result = service.find(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("findOwnerSoldTo/{id}")]
        public IActionResult findOwnerSoldTo(Guid id)
        {
            try
            {
                var service = new OwnerSoldToService();
                var result = service.findOwnerSoldTo(id);
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
                var service = new OwnerSoldToService();
                var Models = new OwnerSoldToViewModel();
                Models = JsonConvert.DeserializeObject<OwnerSoldToViewModel>(body.ToString());
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
                var service = new OwnerSoldToService();
                var Models = new OwnerSoldToViewModel();
                Models = JsonConvert.DeserializeObject<OwnerSoldToViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //// GET: api/<controller>
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        var service = new OwnerSoldToService();

        //        var result = service.Filter();

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public IActionResult Get(Guid id)
        //{
        //    try
        //    {
        //        var service = new OwnerSoldToService();

        //        var result = service.getId(id);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public IActionResult Post([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new OwnerSoldToService();
        //        var Models = new OwnerSoldToViewModel();
        //        Models = JsonConvert.DeserializeObject<OwnerSoldToViewModel>(body.ToString());
        //        var result = service.SaveChanges(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    try
        //    {
        //        var service = new OwnerSoldToService();

        //        var result = service.getDelete(id);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
        //[HttpPost("search")]
        //public IActionResult Get([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new OwnerSoldToService();
        //        var Models = new OwnerSoldToViewModel();
        //        Models = JsonConvert.DeserializeObject<OwnerSoldToViewModel>(body.ToString());
        //        var result = service.search(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
        //[HttpGet("SoldToPopup/{id}")]
        //public IActionResult SoldToPopup(Guid id)
        //{
        //    try
        //    {
        //        var service = new OwnerSoldToService();

        //        var result = service.SoldToPopup(id);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
    }
}

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
    [Route("api/Zone")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new ZoneService();
                var Models = new SearchZoneViewModel();
                Models = JsonConvert.DeserializeObject<SearchZoneViewModel>(body.ToString());
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
                var service = new ZoneService();
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
                var service = new ZoneService();
                var Models = new ZoneViewModel();
                Models = JsonConvert.DeserializeObject<ZoneViewModel>(body.ToString());
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
                var service = new ZoneService();
                var Models = new ZoneViewModel();
                Models = JsonConvert.DeserializeObject<ZoneViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //// GET: api/<controller>
        //[HttpGet("filter")]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        var service = new ZoneService();

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
        //        var service = new ZoneService();

        //        var result = service.getId(id);

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
        //        var service = new ZoneService();
        //        var Models = new ZoneViewModel();
        //        Models = JsonConvert.DeserializeObject<ZoneViewModel>(body.ToString()); 
        //        var result = service.search(Models);
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
        //        var service = new ZoneService();
        //        var Models = new ZoneViewModel();
        //        Models = JsonConvert.DeserializeObject<ZoneViewModel>(body.ToString());
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
        //        var service = new ZoneService();

        //        var result = service.getDelete(id);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [HttpPost("zoneFilter")]
        public IActionResult zoneFilter([FromBody]JObject body)
        {
            try
            {
                var service = new ZoneService();
                var Models = new ZoneViewModel();
                Models = JsonConvert.DeserializeObject<ZoneViewModel>(body.ToString());
                var result = service.zoneFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

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
    //[Route("api/EquipmentSubType")]
    //public class EquipmentSubTypeController : Controller
    //{
    //    // GET: api/<controller>
    //    [HttpGet("filter")]
    //    public IActionResult Get()
    //    {
    //        try
    //        {
    //            var service = new EquipmentSubTypeService();

    //            var result = service.Filter();

    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex);
    //        }
    //    }

    //    [HttpPost("getEquipmentType")]
    //    public IActionResult getEquipmentType([FromBody]JObject body)
    //    {
    //        try
    //        {
    //            var service = new EquipmentSubTypeService();
    //            var Models = new EquipmentSubTypeViewModel();
    //            Models = JsonConvert.DeserializeObject<EquipmentSubTypeViewModel>(body.ToString());
    //            var result = service.FilterEquipmentType(Models);
    //            return Ok(result);


    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex);
    //        }
    //    }
    //    // GET api/<controller>/5
    //    [HttpGet("{id}")]
    //    public IActionResult Get(Guid id)
    //    {
    //        try
    //        {
    //            var service = new EquipmentSubTypeService();

    //            var result = service.getId(id);

    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex);
    //        }
    //    }

    //    // POST api/<controller>
    //    [HttpPost]
    //    public IActionResult Post([FromBody]JObject body)
    //    {
    //        try
    //        {
    //            var service = new EquipmentSubTypeService();
    //            var Models = new EquipmentSubTypeViewModel();
    //            Models = JsonConvert.DeserializeObject<EquipmentSubTypeViewModel>(body.ToString());
    //            var result = service.SaveChanges(Models);
    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex);
    //        }
    //    }

    //    [HttpPost("search")]
    //    public IActionResult Get([FromBody]JObject body)
    //    {
    //        try
    //        {
    //            var service = new EquipmentSubTypeService();
    //            var Models = new EquipmentSubTypeViewModel();
    //            Models = JsonConvert.DeserializeObject<EquipmentSubTypeViewModel>(body.ToString());
    //            var result = service.search(Models);
    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex);
    //        }
    //    }

    //    // DELETE api/<controller>/5
    //    [HttpDelete("{id}")]
    //    public IActionResult Delete(Guid id)
    //    {
    //        try
    //        {
    //            var service = new EquipmentSubTypeService();

    //            var result = service.getDelete(id);

    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex);
    //        }
    //    }
    //}
    [Route("api/EquipmentSubType")]
    public class EquipmentSubTypeController : Controller
    {



        // GET api/<controller>/5
        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new EquipmentSubTypeService();
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
                var service = new EquipmentSubTypeService();
                var Models = new EquipmentSubTypeViewModel();
                Models = JsonConvert.DeserializeObject<EquipmentSubTypeViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterEquipmentSubType")]
        public IActionResult filterEquipmentSubType([FromBody]JObject body)
        {
            try
            {
                var service = new EquipmentSubTypeService();
                var Models = new SearchEquipmentSubTypeViewModel();
                Models = JsonConvert.DeserializeObject<SearchEquipmentSubTypeViewModel>(body.ToString());
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
                var service = new EquipmentSubTypeService();
                var Models = new EquipmentSubTypeViewModel();
                Models = JsonConvert.DeserializeObject<EquipmentSubTypeViewModel>(body.ToString());
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

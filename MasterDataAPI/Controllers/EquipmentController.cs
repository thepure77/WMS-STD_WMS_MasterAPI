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
    //[Route("api/Equipment")]
    //public class EquipmentController : Controller
    //{
    //    // GET: api/<controller>
    //    [HttpGet("filter")]
    //    public IActionResult Get()
    //    {
    //        try
    //        {
    //            var service = new EquipmentService();

    //            var result = service.Filter();

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
    //            var service = new EquipmentService();

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
    //            var service = new EquipmentService();
    //            var Models = new EquipmentViewModel();
    //            Models = JsonConvert.DeserializeObject<EquipmentViewModel>(body.ToString());
    //            var result = service.SaveChanges(Models);
    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex);
    //        }
    //    }

    //    [HttpDelete("{id}")]
    //    public IActionResult Delete(Guid id)
    //    {
    //        try
    //        {
    //            var service = new EquipmentService();

    //            var result = service.getDelete(id);

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
    //            var service = new EquipmentService();
    //            var Models = new EquipmentViewModel();
    //            Models = JsonConvert.DeserializeObject<EquipmentViewModel>(body.ToString());
    //            var result = service.search(Models);
    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex);
    //        }
    //    }

    //    [HttpPost("CartNumber")]
    //    public IActionResult GetCartNumber([FromBody]JObject body)
    //    {
    //        try
    //        {
    //            var service = new EquipmentService();
    //            var Models = new EquipmentItemViewModel();
    //            Models = JsonConvert.DeserializeObject<EquipmentItemViewModel>(body.ToString());
    //            var result = service.cartNumber(Models);
    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex);
    //        }
    //    }

    //    [HttpPost("UpdateCartAssign")]
    //    public IActionResult GetUpdateCartAssign([FromBody]JObject body)
    //    {
    //        try
    //        {
    //            var service = new EquipmentService();
    //            var Models = new EquipmentItemViewModel();
    //            Models = JsonConvert.DeserializeObject<EquipmentItemViewModel>(body.ToString());
    //            var result = service.UpdateCartAssign(Models);
    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex);
    //        }
    //    }
    //}

    [Route("api/Equipment")]
    public class EquipmentController : Controller
    {
        // GET api/<controller>/5
        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new EquipmentService();
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
                var service = new EquipmentService();
                var Models = new EquipmentViewModel();
                Models = JsonConvert.DeserializeObject<EquipmentViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterEquipment")]
        public IActionResult filterEquipment([FromBody]JObject body)
        {
            try
            {
                var service = new EquipmentService();
                var Models = new SearchEquipmentViewModel();
                Models = JsonConvert.DeserializeObject<SearchEquipmentViewModel>(body.ToString());
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
                var service = new EquipmentService();
                var Models = new EquipmentViewModel();
                Models = JsonConvert.DeserializeObject<EquipmentViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ExportExcel")]
        public IActionResult ExportExcel([FromBody]JObject body)
        {
            try
            {
                var service = new EquipmentService();
                var Models = new SearchEquipmentViewModel();
                Models = JsonConvert.DeserializeObject<SearchEquipmentViewModel>(body.ToString());
                var result = service.ExportExcel(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

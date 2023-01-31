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
    [Route("api/masterGrade")]
    public class ItemStatusController : Controller
    {
        // GET: api/<controller>
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        var service = new ItemStatusService();

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
        //        var service = new ItemStatusService();

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
        //        var service = new ItemStatusService();
        //        var Models = new ItemStatusViewModel();
        //        Models = JsonConvert.DeserializeObject<ItemStatusViewModel>(body.ToString());
        //        var result = service.SaveChanges(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}


        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    try
        //    {
        //        var service = new ItemStatusService();

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
        //        var service = new ItemStatusService();
        //        var Models = new ItemStatusDocViewModel();
        //        Models = JsonConvert.DeserializeObject<ItemStatusDocViewModel>(body.ToString());
        //        var result = service.search(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [HttpPost("itemStatusfilter")]
        public IActionResult itemStatusfilter([FromBody]JObject body)

        {
            try
            {
                var service = new ItemStatusService();
                var Models = new ItemStatusDocViewModel();
                Models = JsonConvert.DeserializeObject<ItemStatusDocViewModel>(body.ToString());
                var result = service.itemStatusfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new ItemStatusService();
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
                var service = new ItemStatusService();
                var Models = new ItemStatusViewModel();
                Models = JsonConvert.DeserializeObject<ItemStatusViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterItemStatus")]
        public IActionResult filterItemStatus([FromBody]JObject body)
        {
            try
            {
                var service = new ItemStatusService();
                var Models = new SearchItemStatusViewModel();
                Models = JsonConvert.DeserializeObject<SearchItemStatusViewModel>(body.ToString());
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
                var service = new ItemStatusService();
                var Models = new ItemStatusViewModel();
                Models = JsonConvert.DeserializeObject<ItemStatusViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ConfigItemStatusDoc")]
        public IActionResult ConfigItemStatusDoc([FromBody]JObject body)
        {
            try
            {
                var service = new ItemStatusService();
                var Models = new ItemStatusDocViewModel();
                Models = JsonConvert.DeserializeObject<ItemStatusDocViewModel>(body.ToString());
                var result = service.ConfigItemStatusDoc(Models);
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
                var service = new ItemStatusService();
                var Models = new SearchItemStatusViewModel();
                Models = JsonConvert.DeserializeObject<SearchItemStatusViewModel>(body.ToString());
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

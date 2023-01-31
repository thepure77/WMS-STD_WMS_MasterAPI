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
    [Route("api/RuleConditionField")]
    public class RuleConditionFieldController : Controller
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new RuleConditionFieldService();
                var Models = new SearchRuleConditionFieldViewModel();
                Models = JsonConvert.DeserializeObject<SearchRuleConditionFieldViewModel>(body.ToString());
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
                var service = new RuleConditionFieldService();
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
                var service = new RuleConditionFieldService();
                var Models = new RuleConditionFieldViewModel();
                Models = JsonConvert.DeserializeObject<RuleConditionFieldViewModel>(body.ToString());
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
                var service = new RuleConditionFieldService();
                var Models = new RuleConditionFieldViewModel();
                Models = JsonConvert.DeserializeObject<RuleConditionFieldViewModel>(body.ToString());
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
//{
    //[Route("api/[controller]")]
    //public class RuleConditionFieldController : Controller
    //{
        

        //// GET: api/<controller>
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        var service = new RuleConditionFieldService();

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
        //public IActionResult Get(int id)
        //{
        //    try
        //    {
        //        var service = new RuleConditionFieldService();

        //        var result = service.Filter();

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public IActionResult Post([FromBody]string value)
        //{
        //    try
        //    {
        //        var service = new RuleConditionFieldService();

        //        var result = service.Filter();

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        ////// PUT api/<controller>/5
        ////[HttpPut("{id}")]
        ////public IActionResult Put(int id, [FromBody]string value)
        ////{
        ////    try
        ////    {
        ////        var service = new RuleConditionFieldService();

        ////        var result = service.Filter();

        ////        return Ok(result);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        return BadRequest(ex);
        ////    }
        ////}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var service = new RuleConditionFieldService();

        //        var result = service.Filter();

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
//    }
//}

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
    [Route("api/RuleConditionFieldPopup")]
    public class RuleConditionFieldPopupController : Controller
    {
        


        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new RuleConditionFieldPopupService();
                var Models = new RuleConditionFieldPopupViewModel();
                Models = JsonConvert.DeserializeObject<RuleConditionFieldPopupViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("filterruleconditionfield")]
        public IActionResult filterruleconditionfield([FromBody]JObject body)
        {
            try
            {
                var service = new RuleConditionFieldPopupService();
                var Models = new RuleConditionFieldPopupViewModel();
                Models = JsonConvert.DeserializeObject<RuleConditionFieldPopupViewModel>(body.ToString());
                var result = service.filterruleconditionfield(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        //[HttpGet("find/{id}")]
        //public IActionResult find(Guid id)
        //{
        //    try
        //    {
        //        var service = new RuleConditionFieldService();
        //        var result = service.find(id);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("SaveChanges")]
        //public IActionResult SaveChanges([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new RuleConditionFieldService();
        //        var Models = new RuleConditionFieldViewModel();
        //        Models = JsonConvert.DeserializeObject<RuleConditionFieldViewModel>(body.ToString());
        //        var result = service.SaveChanges(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("Delete")]
        //public IActionResult Delete([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new RuleConditionFieldService();
        //        var Models = new RuleConditionFieldViewModel();
        //        Models = JsonConvert.DeserializeObject<RuleConditionFieldViewModel>(body.ToString());
        //        var result = service.getDelete(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
    }
}
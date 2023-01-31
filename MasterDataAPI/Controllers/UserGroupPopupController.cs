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
    [Route("api/UserGroupPopup")]
    public class UserGroupPopupController : Controller
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new UserGroupPopupService();
                var Models = new UserGroupPopupViewModel();
                Models = JsonConvert.DeserializeObject<UserGroupPopupViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpPost("search")]
        //public IActionResult search([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ProcessV2Service();
        //        var Models = new ProcessV2ViewModel();
        //        Models = JsonConvert.DeserializeObject<ProcessV2ViewModel>(body.ToString());
        //        var result = service.search(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

    }
}

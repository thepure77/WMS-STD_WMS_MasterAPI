using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MasterDataAPI.Controllers
{
    [Route("api/autoOwnerVendor")]
    [ApiController]
    public class AutoOwnerVendorController : ControllerBase
    {

        #region AutoOwnerVendor
        [HttpPost("autoOwnerVendor")]
        public IActionResult autoOwnerVendor([FromBody]JObject body)
        {
            try
            {
                var service = new OwnerVendorService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoOwnerVendor(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


    }
}

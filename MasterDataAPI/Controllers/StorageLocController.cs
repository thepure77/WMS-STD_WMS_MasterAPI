using MasterDataBusiness.CostCenter;
using MasterDataBusiness.StorageLoc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/StorageLoc")]
    [ApiController]
    public class StorageLocController : ControllerBase
    {
        [HttpPost("storageLocfilter")]
        public IActionResult storageLocfilter([FromBody]JObject body)
        {
            try
            {
                var service = new StorageLocService();
                var Models = new StorageLocViewModel();
                Models = JsonConvert.DeserializeObject<StorageLocViewModel>(body.ToString());
                var result = service.storageLocfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
using MasterDataBusiness.Currency;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/Currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
       
            
        [HttpPost("currencydropdown")]
        public IActionResult currencydropdown([FromBody]JObject body)
        {
            try
            {
                var service = new CurrencyService();
                var Models = new CurrencyViewModel();
                Models = JsonConvert.DeserializeObject<CurrencyViewModel>(body.ToString());
                var result = service.currencydropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
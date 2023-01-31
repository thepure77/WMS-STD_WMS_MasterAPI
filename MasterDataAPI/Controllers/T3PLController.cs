using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness.T3PL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/3PL")]
    public class T3PLController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var service = new T3PLService();

                var result = service.Filter();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }      
        

        

        [HttpPost("search")]
        public IActionResult Get([FromBody]JObject body)
        {
            try
            {
                var service = new T3PLService();
                var Models = new T3PLViewModel();
                Models = JsonConvert.DeserializeObject<T3PLViewModel>(body.ToString());
                var result = service.search(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        
    }
}

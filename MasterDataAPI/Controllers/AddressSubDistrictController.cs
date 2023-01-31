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
    [Route("api/AddressSubDistrict")]
    public class AddressSubDistrictController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var service = new AddressSubDistrictService();

                var result = service.Filter();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getSubDistrict")]
        public IActionResult getSubDistrict([FromBody]JObject body)
        {
            try
            {
                var service = new AddressSubDistrictService();
                var Models = new AddressSubDistrictViewModel();
                Models = JsonConvert.DeserializeObject<AddressSubDistrictViewModel>(body.ToString());
                var result = service.FilterSubDistrict(Models);
                return Ok(result);

          
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var service = new AddressSubDistrictService();

                var result = service.getId(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]JObject body)
        {
            try
            {
                var service = new AddressSubDistrictService();
                var Models = new AddressSubDistrictViewModel();
                Models = JsonConvert.DeserializeObject<AddressSubDistrictViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var service = new AddressSubDistrictService();

                var result = service.getDelete(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

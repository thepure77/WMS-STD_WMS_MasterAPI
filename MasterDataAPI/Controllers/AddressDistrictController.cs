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
    [Route("api/AddressDistrict")]
    public class AddressDistrictController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var service = new AddressDistrictService();

                var result = service.Filter();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("getDistrict")]
        public IActionResult getDistrict([FromBody]JObject body)
        {
            try
            {
                var service = new AddressDistrictService();
                var Models = new AddressDistrictViewModel();
                Models = JsonConvert.DeserializeObject<AddressDistrictViewModel>(body.ToString());
                var result = service.FilterDistrict(Models);
                return Ok(result);

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
                var service = new AddressDistrictService();

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
                var service = new AddressDistrictService();
                var Models = new AddressDistrictViewModel();
                Models = JsonConvert.DeserializeObject<AddressDistrictViewModel>(body.ToString());
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
                var service = new AddressDistrictService();

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

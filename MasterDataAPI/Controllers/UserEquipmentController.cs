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
    [Route("api/UserEquipment")]
    public class UserEquipmentController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var service = new UserEquipmentService();

                var result = service.Filter();

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
                var service = new UserEquipmentService();

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
                var service = new UserEquipmentService();
                var Models = new UserEquipmentViewModel();
                Models = JsonConvert.DeserializeObject<UserEquipmentViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var service = new UserEquipmentService();

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

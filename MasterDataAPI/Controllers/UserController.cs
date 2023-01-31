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
    [Route("api/User")]
    public class UserController : Controller
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new UserService();
                var Models = new SeaechUserViewModel();
                Models = JsonConvert.DeserializeObject<SeaechUserViewModel>(body.ToString());
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
                var service = new UserService();
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
                var service = new UserService();
                var Models = new userViewModelV2();
                Models = JsonConvert.DeserializeObject<userViewModelV2>(body.ToString());
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
                var service = new UserService();
                var Models = new userViewModel();
                Models = JsonConvert.DeserializeObject<userViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("addUser")]
        public IActionResult addUser([FromBody]JObject body)
        {
            try
            {
                var service = new UserService();
                var Models = new UserMenuViewModel();
                Models = JsonConvert.DeserializeObject<UserMenuViewModel>(body.ToString());
                var result = service.addUser(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("autoUser")]
        public IActionResult autoUser([FromBody]JObject body)
        {
            try
            {
                var service = new UserService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoUser(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("configUserTaskGroup")]
        public IActionResult configUserTaskGroup([FromBody]JObject body)
        {
            try
            {
                var service = new UserService();
                var Models = new View_UserTaskGroupViewModel();
                Models = JsonConvert.DeserializeObject<View_UserTaskGroupViewModel>(body.ToString());
                var result = service.configUserTaskGroup(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("dropdownUser")]
        public IActionResult dropdownUser([FromBody]JObject body)
        {
            try
            {
                var service = new UserService();
                var Models = new userViewModel();
                Models = JsonConvert.DeserializeObject<userViewModel>(body.ToString());
                var result = service.dropdownUser(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("checkStatusUser")]
        public IActionResult checkStatusUser([FromBody]JObject body)
        {
            try
            {
                var service = new UserService();
                var Models = new userViewModelV2();
                Models = JsonConvert.DeserializeObject<userViewModelV2>(body.ToString());
                var result = service.checkStatusUser(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Export")]
        public IActionResult Export([FromBody] JObject body)
        {
            try
            {
                var service = new UserService();
                var Models = new UserExportViewModel();
                Models = JsonConvert.DeserializeObject<UserExportViewModel>(body.ToString());
                var result = service.Export(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}

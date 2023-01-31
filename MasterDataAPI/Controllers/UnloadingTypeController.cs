using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ContainerType;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{

    [Route("api/UnloadingType")]
    public class UnloadingTypeController : Controller
    {
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        var service = new UnloadingTypeService();

        //        var result = service.Filter();

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("search")]
        //public IActionResult Get([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new UnloadingTypeService();
        //        var Models = new UnloadingTypeViewModel();
        //        Models = JsonConvert.DeserializeObject<UnloadingTypeViewModel>(body.ToString());
        //        var result = service.search(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new UnloadingTypeService();
                var Models = new SearchUnloadingTypeViewModel();
                Models = JsonConvert.DeserializeObject<SearchUnloadingTypeViewModel>(body.ToString());
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
                var service = new UnloadingTypeService();
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
                var service = new UnloadingTypeService();
                var Models = new UnloadingTypeViewModel();
                Models = JsonConvert.DeserializeObject<UnloadingTypeViewModel>(body.ToString());
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
                var service = new UnloadingTypeService();
                var Models = new UnloadingTypeViewModel();
                Models = JsonConvert.DeserializeObject<UnloadingTypeViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("unloadingTypedropdown")]
        public IActionResult unloadingTypedropdown([FromBody]JObject body)
        {
            try
            {
                var service = new UnloadingTypeService();
                var Models = new UnloadingTypeViewModel();
                Models = JsonConvert.DeserializeObject<UnloadingTypeViewModel>(body.ToString());
                var result = service.unloadingTypedropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

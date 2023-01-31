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
    [Route("api/Vendor")]
    public class VendorController : Controller
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new VendorService();
                var Models = new SearchVendorViewModel();
                Models = JsonConvert.DeserializeObject<SearchVendorViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterPopupVendor")]
        public IActionResult filterPopupVendor([FromBody]JObject body)
        {
            try
            {
                var service = new VendorService();
                var Models = new VendorPopupViewModel();
                Models = JsonConvert.DeserializeObject<VendorPopupViewModel>(body.ToString());
                var result = service.filterPopupVendor(Models);
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
                var service = new VendorService();
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
                var service = new VendorService();
                var Models = new VendorViewModel();
                Models = JsonConvert.DeserializeObject<VendorViewModel>(body.ToString());
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
                var service = new VendorService();
                var Models = new VendorViewModel();
                Models = JsonConvert.DeserializeObject<VendorViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterPopup")]
        public IActionResult filterPopup([FromBody]JObject body)
        {
            try
            {
                var service = new VendorService();
                var Models = new SearchVendorViewModel();
                Models = JsonConvert.DeserializeObject<SearchVendorViewModel>(body.ToString());
                var result = service.filterPopup(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ExportExcel")]
        public IActionResult ExportExcel ([FromBody]JObject body)
        {
            try
            {
                var service = new VendorService();
                var Models = new SearchVendorViewModel();
                Models = JsonConvert.DeserializeObject<SearchVendorViewModel>(body.ToString());
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

using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasterDataAPI.Controllers
{
    [Route("api/ServiceChargeList")]
    [ApiController]
    public class ServiceChargeListController : ControllerBase
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeListService();
                var Models = new SearchServiceChargeListViewModel();
                Models = JsonConvert.DeserializeObject<SearchServiceChargeListViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterServiceChargePopup")]
        public IActionResult filterServiceChargePopup([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeListService();
                var Models = new SearchServiceChargeViewModel();
                Models = JsonConvert.DeserializeObject<SearchServiceChargeViewModel>(body.ToString());
                var result = service.filterServiceChargePopup(Models);
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
                var service = new ServiceChargeListService();
                var Models = new ServiceChargeListViewModel();
                Models = JsonConvert.DeserializeObject<ServiceChargeListViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterServiceChargeFix")]
        public IActionResult filterServiceChargeFix([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeListService();
                var Models = new serviceChargeFixViewModel();
                Models = JsonConvert.DeserializeObject<serviceChargeFixViewModel>(body.ToString());
                var result = service.filterServiceChargeFix(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SaveServiceChargeFix")]
        public IActionResult SaveServiceChargeFix([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeListService();
                var Models = new serviceChargeFixViewModel();
                Models = JsonConvert.DeserializeObject<serviceChargeFixViewModel>(body.ToString());
                var result = service.SaveServiceChargeFix(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("deleteServiceChargeFix")]
        public IActionResult deleteServiceChargeFix([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeListService();
                var Models = new serviceChargeFixViewModel();
                Models = JsonConvert.DeserializeObject<serviceChargeFixViewModel>(body.ToString());
                var result = service.deleteServiceChargeFix(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterView_WRL")]
        public IActionResult filterView_WRL([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeListService();
                var Models = new WRLViewModel();
                Models = JsonConvert.DeserializeObject<WRLViewModel>(body.ToString());
                var result = service.filterView_WRL(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SaveStorageCharge")]
        public IActionResult SaveStorageCharge([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeListService();
                var Models = new storageSaveViewModel();
                Models = JsonConvert.DeserializeObject<storageSaveViewModel>(body.ToString());
                var result = service.SaveStorageCharge(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpPost("findStorageCharge")]
        public IActionResult findStorageCharge([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeListService();
                var Models = new storageSaveViewModel();
                Models = JsonConvert.DeserializeObject<storageSaveViewModel>(body.ToString());
                var result = service.findStorageCharge(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getConfigStorageCharge")]
        public IActionResult getConfigStorageCharge([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeListService();
                var Models = new storageChargeModel();
                Models = JsonConvert.DeserializeObject<storageChargeModel>(body.ToString());
                var result = service.getConfigStorageCharge(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("getConfigLocationServiceCharge")]
        public IActionResult getConfigLocationServiceCharge([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeListService();
                var Models = new locationServiceChargeViewModel();
                Models = JsonConvert.DeserializeObject<locationServiceChargeViewModel>(body.ToString());
                var result = service.getConfigLocationServiceCharge(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("dropDownStorageCharge")]
        public IActionResult dropDownStorageCharge([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeListService();
                var Models = new storageChargeModel();
                Models = JsonConvert.DeserializeObject<storageChargeModel>(body.ToString());
                var result = service.dropDownStorageCharge(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("filterSelectAll")]
        public IActionResult filterSelectAll([FromBody]JObject body)
        {
            try
            {
                var service = new ServiceChargeListService();
                var Models = new WRLViewModel();
                Models = JsonConvert.DeserializeObject<WRLViewModel>(body.ToString());
                var result = service.filterSelectAll(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }





    }
}
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/ProductConversionBarcode")]
    public class ProductConversionBarcodeController : Controller
    {

        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new ProductConversionBarcodeService();
                var Models = new SearchProductConversionBarcodeViewModel();
                Models = JsonConvert.DeserializeObject<SearchProductConversionBarcodeViewModel>(body.ToString());
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
                var service = new ProductConversionBarcodeService();
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
                var service = new ProductConversionBarcodeService();
                var Models = new ProductConversionBarcodeViewModel();
                Models = JsonConvert.DeserializeObject<ProductConversionBarcodeViewModel>(body.ToString());
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
                var service = new ProductConversionBarcodeService();
                var Models = new ProductConversionBarcodeViewModel();
                Models = JsonConvert.DeserializeObject<ProductConversionBarcodeViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getPackBarcode")]
        public IActionResult getPackBarcode([FromBody]JObject body)
        {
            try
            {
                var service = new ProductConversionBarcodeService();
                var Models = new ProductConversionBarcodeViewModel();
                Models = JsonConvert.DeserializeObject<ProductConversionBarcodeViewModel>(body.ToString());
                var result = service.getPackBarcode(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterPopupProductConversionBarcode")]
        public IActionResult filterPopupProductConversionBarcode([FromBody]JObject body)
        {
            try
            {
                var service = new ProductConversionBarcodeService();
                var Models = new ProductConversionBarcodePopupViewModel();
                Models = JsonConvert.DeserializeObject<ProductConversionBarcodePopupViewModel>(body.ToString());
                var result = service.filterPopupProductConversionBarcode(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("filterProductConversionBarcode/{id}/{i}/{data}")]
        public IActionResult filterProductConversionBarcode(Guid id,Guid i, string data)
        {
            try
            {
                var service = new ProductConversionBarcodeService();
                var result = service.filterProductConversionBarcode(id,i,data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("productConversionBarcodeDropdown")]
        public IActionResult productConversionDropdown([FromBody]JObject body)
        {
            try
            {
                var service = new ProductConversionBarcodeService();
                var Models = new ProductConversionBarcodeViewModel();
                Models = JsonConvert.DeserializeObject<ProductConversionBarcodeViewModel>(body.ToString());
                var result = service.productConversionBarcodeDropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Export")]
        public IActionResult export([FromBody]JObject body)
        {
            try
            {
                var service = new ProductConversionBarcodeService();
                var Models = new SearchProductConversionBarcodeViewModel();
                Models = JsonConvert.DeserializeObject<SearchProductConversionBarcodeViewModel>(body.ToString());
                var result = service.export(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        //// GET: api/<controller>
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        var service = new ProductConversionBarcodeService();

        //        var result = service.Filter();

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("ScanProductCVBarcode/{id}")]
        //public IActionResult GetScanProductCVBarcode(string id)
        //{
        //    try
        //    {
        //        ProductConversionBarcodeService service = new ProductConversionBarcodeService();
        //        var result = service.ScanProductCVBarcode(id);
        //        return this.Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.BadRequest(ex);
        //    }
        //}

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public IActionResult Get(Guid id)
        //{
        //    try
        //    {
        //        var service = new ProductConversionBarcodeService();

        //        var result = service.getId(id);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public IActionResult Post([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ProductConversionBarcodeService();
        //        var Models = new ProductConversionBarcodeViewModel();
        //        Models = JsonConvert.DeserializeObject<ProductConversionBarcodeViewModel>(body.ToString());
        //        var result = service.SaveChanges(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}


        //[HttpPost("ScanBarcode")]
        //public IActionResult GetScanBarcode([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ProductConversionBarcodeService();
        //        var Models = new ScanBarcodeViewModel();
        //        Models = JsonConvert.DeserializeObject<ScanBarcodeViewModel>(body.ToString());
        //        var result = service.ScanBarcode(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    try
        //    {
        //        var service = new ProductConversionBarcodeService();

        //        var result = service.getDelete(id);

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
        //        var service = new ProductConversionBarcodeService();
        //        var Models = new ProductConversionBarcodeViewModel();
        //        Models = JsonConvert.DeserializeObject<ProductConversionBarcodeViewModel>(body.ToString());
        //        var result = service.search(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
    }
}

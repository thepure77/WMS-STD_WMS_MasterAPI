using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ProductConversion;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/ProductConversion")]
    public class ProductConversionController : Controller
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new ProductConversionService();
                var Models = new SearchProductConversionViewModel();
                Models = JsonConvert.DeserializeObject<SearchProductConversionViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filter_in")]
        public IActionResult FilterInClause([FromBody]JObject body)
        {
            try
            {
                var service = new ProductConversionService();
                var result = service.FilterInClause(body?.ToString() ?? string.Empty);
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
                var service = new ProductConversionService();
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
                var service = new ProductConversionService();
                var Models = new ProductConversionViewModel();
                Models = JsonConvert.DeserializeObject<ProductConversionViewModel>(body.ToString());
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
                var service = new ProductConversionService();
                var Models = new ProductConversionViewModel();
                Models = JsonConvert.DeserializeObject<ProductConversionViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterV2")]
        public IActionResult filterV2([FromBody]JObject body)
        {
            try
            {
                var service = new ProductConversionService();
                var Models = new SearchProductConversionViewModel();
                Models = JsonConvert.DeserializeObject<SearchProductConversionViewModel>(body.ToString());
                var result = service.filterV2(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("productConversionDropdown")]
        public IActionResult productConversionDropdown([FromBody]JObject body)
        {
            try
            {
                var service = new ProductConversionService();
                var Models = new ProductConversionViewModel();
                Models = JsonConvert.DeserializeObject<ProductConversionViewModel>(body.ToString());
                var result = service.productConversionDropdown(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("conversion_sale_unit")]
        public IActionResult conversion_sale_unit([FromBody]JObject body)
        {
            try
            {
                var service = new ProductConversionService();
                var Models = new SearchProductConversionViewModel();
                Models = JsonConvert.DeserializeObject<SearchProductConversionViewModel>(body.ToString());
                var result = service.conversion_sale_unit(Models);
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
                var service = new ProductConversionService();
                var Models = new SearchProductConversionViewModel();
                Models = JsonConvert.DeserializeObject<SearchProductConversionViewModel>(body.ToString());
                var result = service.export(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //// GET: api/<controller>
        //[HttpGet("filter")]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        var service = new ProductConversionService();

        //        var result = service.Filter(new ProductConversionViewModel());

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public IActionResult Get(Guid id)
        //{
        //    try
        //    {
        //        var service = new ProductConversionService();

        //        var result = service.getId(id);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
        //[HttpPost("{id}")]
        //public IActionResult GetItem(Guid id)
        //{
        //    try
        //    {
        //        var service = new ProductConversionService();

        //        var result = service.getItem(id);

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
        //        var service = new ProductConversionService();
        //        var Models = new ProductConversionViewModel();
        //        Models = JsonConvert.DeserializeObject<ProductConversionViewModel>(body.ToString());
        //        var result = service.SaveChanges(Models);
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
        //        var service = new ProductConversionService();

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
        //        var service = new ProductConversionService();
        //        var Models = new ProductConversionViewModel();
        //        Models = JsonConvert.DeserializeObject<ProductConversionViewModel>(body.ToString());
        //        var result = service.Filter(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpGet("filterPopup")]
        //public IActionResult post()
        //{
        //    try
        //    {
        //        var service = new ProductConversionService();

        //        var result = service.FilterPopup();

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
        //[HttpPost("productConversionPopupSearch")]
        //public IActionResult productConversionPopupSearch([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ProductConversionService();
        //        var Models = new ProductConversionViewModel();
        //        Models = JsonConvert.DeserializeObject<ProductConversionViewModel>(body.ToString());
        //        var result = service.productConversionPopupSearch(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("PopupSearch")]
        //public IActionResult PopupSearch([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ProductConversionService();
        //        var Models = new ProductConversionViewModelDoc();
        //        Models = JsonConvert.DeserializeObject<ProductConversionViewModelDoc>(body.ToString());
        //        var result = service.PopupSearch(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [HttpPost("productConversionfilter")]
        public IActionResult productConversionfilter([FromBody]JObject body)

        {
            try
            {
                var service = new ProductConversionService();
                var Models = new ProductConversionViewModelDoc();
                Models = JsonConvert.DeserializeObject<ProductConversionViewModelDoc>(body.ToString());
                var result = service.productConversionfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("productConversionfilterV2")]
        public IActionResult productConversionfilterV2([FromBody]JObject body)

        {
            try
            {
                var service = new ProductConversionService();
                var Models = new ProductConversionViewModelDoc();
                Models = JsonConvert.DeserializeObject<ProductConversionViewModelDoc>(body.ToString());
                var result = service.productConversionfilterV2(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("SaveChangesV2")]
        public IActionResult SaveChangesV2([FromBody]JObject body)
        {
            try
            {
                var service = new ProductConversionService();
                var Models = new ProductConversionViewModel();
                Models = JsonConvert.DeserializeObject<ProductConversionViewModel>(body.ToString());
                var result = service.SaveChangesV2(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

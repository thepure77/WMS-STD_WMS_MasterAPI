using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ProductOwner;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/ProductOwner")]
    public class ProductOwnerController : Controller
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new ProductOwnerService();
                var Models = new SearchProductOwnerViewModel();
                Models = JsonConvert.DeserializeObject<SearchProductOwnerViewModel>(body.ToString());
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
                var service = new ProductOwnerService();
                var result = service.FilterInClause(body?.ToString() ?? string.Empty);
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
                var service = new ProductOwnerService();
                var Models = new SearchProductOwnerViewModel();
                Models = JsonConvert.DeserializeObject<SearchProductOwnerViewModel>(body.ToString());
                var result = service.filterV2(Models);
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
                var service = new ProductOwnerService();
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
                var service = new ProductOwnerService();
                var Models = new ProductOwnerViewModel();
                Models = JsonConvert.DeserializeObject<ProductOwnerViewModel>(body.ToString());
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
                var service = new ProductOwnerService();
                var Models = new ProductOwnerViewModel();
                Models = JsonConvert.DeserializeObject<ProductOwnerViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPost("SaveProductOwnerList")]
        public IActionResult SaveProductOwnerList([FromBody]JObject body)
        {
            try
            {
                var service = new ProductOwnerService();
                var Models = new ProductOwnerViewModel();
                Models = JsonConvert.DeserializeObject<ProductOwnerViewModel>(body.ToString());
                var result = service.SaveProductOwnerList(Models);
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
                var service = new ProductOwnerService();
                var Models = new SearchProductOwnerViewModel();
                Models = JsonConvert.DeserializeObject<SearchProductOwnerViewModel>(body.ToString());
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
        //        var service = new ProductOwnerService();

        //        var result = service.Filter(new ProductOwnerViewModel());

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
        //        var service = new ProductOwnerService();

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
        //        var service = new ProductOwnerService();
        //        var Models = new ProductOwnerViewModel();
        //        Models = JsonConvert.DeserializeObject<ProductOwnerViewModel>(body.ToString());
        //        var result = service.SaveChanges(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    try
        //    {
        //        var service = new ProductOwnerService();

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
        //        var service = new ProductOwnerService();
        //        var Models = new ProductOwnerViewModel();
        //        Models = JsonConvert.DeserializeObject<ProductOwnerViewModel>(body.ToString());
        //        var result = service.Filter(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
        //[HttpGet("ProductPopup/{id}")]
        //public IActionResult ProductPopup(Guid id)
        //{
        //    try
        //    {
        //        var service = new ProductOwnerService();

        //        var result = service.productPopup(id);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
        //[HttpPost("productPopupSearch")]
        //public IActionResult productPopupSearch([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ProductOwnerService();
        //        var Models = new ProductOwnerViewModel();
        //        Models = JsonConvert.DeserializeObject<ProductOwnerViewModel>(body.ToString());
        //        var result = service.productPopupSearch(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("getProduct")]
        //public IActionResult getProduct([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ProductOwnerService();
        //        var Models = new ProductViewModel();
        //        Models = JsonConvert.DeserializeObject<ProductViewModel>(body.ToString());
        //        var result = service.FilterProduct(Models);
        //        return Ok(result);


        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
        //[HttpPost("getProductV2")]
        //public IActionResult getProductV2([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ProductOwnerService();
        //        var Models = new ProductViewModel();
        //        Models = JsonConvert.DeserializeObject<ProductViewModel>(body.ToString());
        //        var result = service.FilterPopupProduct(Models);
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
        //        var service = new ProductOwnerService();
        //        var Models = new ProductViewModel();
        //        Models = JsonConvert.DeserializeObject<ProductViewModel>(body.ToString());
        //        var result = service.PopupSearch(Models);
        //        return Ok(result);


        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("PopupSearchV2")]
        //public IActionResult PopupSearchV2([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ProductOwnerService();
        //        var Models = new View_GetProductOwnerViewModel();
        //        Models = JsonConvert.DeserializeObject<View_GetProductOwnerViewModel>(body.ToString());
        //        var result = service.PopupSearchV2(Models);
        //        return Ok(result);


        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
    }
}

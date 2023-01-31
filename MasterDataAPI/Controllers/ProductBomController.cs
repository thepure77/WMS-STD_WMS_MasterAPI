using System;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/ProductBom")]
    public class ProductBomController : Controller
    {

        #region autoProductBOM
        [HttpPost("autoProductBOM")]
        public IActionResult autoProductBOM([FromBody]JObject body)
        {
            try
            {
                var service = new ProductBomService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductBOM(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region findProductBOMItem
        [HttpPost("findProductBOMItem")]
        public IActionResult findProductBOMItem([FromBody]JObject body)
        {
            try
            {
                var service = new ProductBomService();
                var Models = new ProductBOMItemViewModel();
                Models = JsonConvert.DeserializeObject<ProductBOMItemViewModel>(body.ToString());
                var result = service.findProductBOMItem(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        [HttpPost("ProductBOM")]
        public IActionResult Product([FromBody]JObject body)
        {
            try
            {
                var service = new ProductBomService();
                var Models = new ProductViewModel();
                Models = JsonConvert.DeserializeObject<ProductViewModel>(body.ToString());
                var result = service.ProductBOM(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}

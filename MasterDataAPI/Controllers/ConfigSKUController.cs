using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.Product;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PTTPL.OMS.Business.Documents;
using PTTPL.TMS.Business.Common;
using PTTPL.TMS.Business.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/ConfigSKU")]
    public class ConfigSKUController : Controller
    {
        #region ConfigSKUController
        private readonly IHostingEnvironment _hostingEnvironment;
        public ConfigSKUController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region filter
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                ConfigSKUService service = new ConfigSKUService();
                ConfigSKUViewModel result = service.filter();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region filter
        [HttpPost("filter_Zonetype")]
        public IActionResult filter_Zonetype([FromBody]JObject body)
        {
            try
            {
                ConfigSKUService service = new ConfigSKUService();
                SearchConfigSKUViewModel Models = new SearchConfigSKUViewModel();
                Models = JsonConvert.DeserializeObject<SearchConfigSKUViewModel>(body.ToString());
                var result = service.filter_Zonetype(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region SaveChanges
        [HttpPost("SaveChanges")]
        public IActionResult SaveChanges([FromBody]JObject body)
        {
            try
            {
                var service = new ProductService();
                var Models = new ProductViewModel();
                Models = JsonConvert.DeserializeObject<ProductViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
    }
}

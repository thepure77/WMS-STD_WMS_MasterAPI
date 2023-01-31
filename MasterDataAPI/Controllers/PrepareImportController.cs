using System;
using System.Collections.Generic;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/PrepareImport")]
    [ApiController]
    public class PrepareImportController : Controller
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {

                //var service = new ProductService();
                //var Models = new SearchProductViewModel();
                //Models = JsonConvert.DeserializeObject<SearchProductViewModel>(body.ToString());
                //var result = service.filter(Models);
                //return Ok(result);

                var service = new Prepare_Imports_Services();
                var Models = new SearchPrepare_ImportsViewModel();
                Models = JsonConvert.DeserializeObject<SearchPrepare_ImportsViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpPost("InsertData")]
        public IActionResult InsertData(dynamic body)
        {
            try
            {
                //var service = new ProductService();
                //var Models = new ProductViewModel();
                //Models = JsonConvert.DeserializeObject<ProductViewModel>(body.ToString());
                //var result = service.SaveChanges(Models);
                //return Ok(result);

                var service = new Prepare_Imports_Services();
                var Models = new List<Prepare_ImportsViewModel>();
                Models = JsonConvert.DeserializeObject<List<Prepare_ImportsViewModel>>(body.ToString());
                var result = service.Insert_Prepare_Imports(Models);
                return Ok(result);
      
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

   
      
    }
}

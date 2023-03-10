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
    [Route("api/DocumentTypeItemStatus")]
    public class DocumentTypeItemStatusController : Controller
    {
        // GET api/<controller>/5
        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new DocumentTypeItemStatusService();
                var result = service.find(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        //POST api/<controller>
        [HttpPost("SaveChanges")]
        public IActionResult SaveChanges([FromBody]JObject body)
        {
            try
            {
                var service = new DocumentTypeItemStatusService();
                var Models = new DocumentTypeItemStatusViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeItemStatusViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost("filterDocumentTypeItemStatus")]
        public IActionResult filterDocumentTypeItemStatus([FromBody]JObject body)
        {
            try
            {
                var service = new DocumentTypeItemStatusService();
                var Models = new SearchDocumentTypeItemStatusViewModel();
                Models = JsonConvert.DeserializeObject<SearchDocumentTypeItemStatusViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //// DELETE api/<controller>/5
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody]JObject body)
        {
            try
            {
                var service = new DocumentTypeItemStatusService();
                var Models = new DocumentTypeItemStatusViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeItemStatusViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

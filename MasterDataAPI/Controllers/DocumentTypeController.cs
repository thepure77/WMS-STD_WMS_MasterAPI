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
    [Route("api/DocumentType")]
    public class DocumentTypeController : Controller
    {
        [HttpPost("filterDocumentType")]
        public IActionResult filterDocumentType([FromBody]JObject body)
        {
            try
            {
                var service = new DocumentTypeService();
                var Models = new SearchDocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<SearchDocumentTypeViewModel>(body.ToString());
                var result = service.filterDocumentType(Models);
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
                var service = new DocumentTypeService();
                var result = service.find(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SaveChangesDocumentType")]
        public IActionResult SaveChangesDocumentType([FromBody]JObject body)
        {
            try
            {
                var service = new DocumentTypeService();
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.SaveChangesDocumentType(Models);
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
                var service = new DocumentTypeService();
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/<controller>
        [HttpGet("filter")]
        public IActionResult Get()
        {
            try
            {
                var service = new DocumentTypeService();

                var result = service.Filter();

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
                var service = new DocumentTypeService();
                var result = service.FilterInClause(body is null ? string.Empty : body.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("filterGR/{id}")]
        public IActionResult post(Guid id)
        {
            try
            {
                var service = new DocumentTypeService();

                var result = service.FilterGR(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }       

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var service = new DocumentTypeService();

                var result = service.getId(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody]JObject body)
        {
            try
            {
                var service = new DocumentTypeService();
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.SaveChanges(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var service = new DocumentTypeService();

                var result = service.getDelete(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("filterPlanGR")]
        public IActionResult filterPlanGR()
        {
            try
            {
                var service = new DocumentTypeService();

                var result = service.FilterPlanGR();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("search")]
        public IActionResult Get([FromBody]JObject body)
        {
            try
            {
                var service = new DocumentTypeService();
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.search(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("autoGRSearch")]
        public IActionResult autoGRSearch([FromBody]JObject body)
        {
            try
            {
                var service = new DocumentTypeService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoGRSearch(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

      

        [HttpPost("documentTypefilter")]
        public IActionResult documentTypefilter([FromBody]JObject body)

        {
            try
            {
                var service = new DocumentTypeService();
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.documentTypefilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("getFormat")]
        public IActionResult getFormat([FromBody]JObject body)
        {
            try
            {
                var service = new DocumentTypeService();
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.getFormat(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Export")]
        public IActionResult Export([FromBody] JObject body)
        {
            try
            {
                var service = new DocumentTypeService();
                var Models = new DocumentTypeExportViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeExportViewModel>(body.ToString());
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

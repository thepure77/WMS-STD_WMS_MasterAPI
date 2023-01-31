using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterDataAPI.Controllers
{
    [Route("api/Log")]
    public class LogController : Controller
    {
        [HttpPost ("filterLog")]
        public IActionResult filterLog([FromBody] JObject body)
        {
            try
            {
                var service = new LogService();
                var Models = new SearchLogViewModel();
                Models = JsonConvert.DeserializeObject<SearchLogViewModel>(body.ToString());
                var result = service.filterLog(Models);
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
                var service = new LogService();
                var Models = new LogExportViewModel();
                Models = JsonConvert.DeserializeObject<LogExportViewModel>(body.ToString());
                var result = service.Export(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterLogtf")]
        public IActionResult filterLogtf([FromBody] JObject body)
        {
            try
            {
                var service = new LogtfService();
                var Models = new SearchLogTfViewModel();
                Models = JsonConvert.DeserializeObject<SearchLogTfViewModel>(body.ToString());
                var result = service.filterLogtf(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Exporttf")]
        public IActionResult Exporttf([FromBody] JObject body)
        {
            try
            {
                var service = new LogtfService();
                var Models = new LogTfExportViewModel();
                Models = JsonConvert.DeserializeObject<LogTfExportViewModel>(body.ToString());
                var result = service.Exporttf(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterLoggi")]
        public IActionResult filterLoggi([FromBody] JObject body)
        {
            try
            {
                var service = new LogGiService();
                var Models = new SearchLogGiViewModel();
                Models = JsonConvert.DeserializeObject<SearchLogGiViewModel>(body.ToString());
                var result = service.filterLoggi(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Exportgi")]
        public IActionResult Exportgi([FromBody] JObject body)
        {
            try
            {
                var service = new LogGiService();
                var Models = new LogGiExportViewModel();
                Models = JsonConvert.DeserializeObject<LogGiExportViewModel>(body.ToString());
                var result = service.Exportgi(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterLogGr")]
        public IActionResult filterLogGr([FromBody] JObject body)
        {
            try
            {
                var service = new LogGrService();
                var Models = new SearchLogGrViewModel();
                Models = JsonConvert.DeserializeObject<SearchLogGrViewModel>(body.ToString());
                var result = service.filterLogGr(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("Exportgr")]
        public IActionResult Exportgr([FromBody] JObject body)
        {
            try
            {
                var service = new LogGrService();
                var Models = new LogGrExportViewModel();
                Models = JsonConvert.DeserializeObject<LogGrExportViewModel>(body.ToString());
                var result = service.Exportgr(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterLogCancel")]
        public IActionResult filterLogCancel([FromBody] JObject body)
        {
            try
            {
                var service = new LogCancelService();
                var Models = new SearchLogCancelViewModel();
                Models = JsonConvert.DeserializeObject<SearchLogCancelViewModel>(body.ToString());
                var result = service.filterLogCancel(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ExportCancel")]
        public IActionResult ExportCancel([FromBody] JObject body)
        {
            try
            {
                var service = new LogCancelService();
                var Models = new LogCancelExportViewModel();
                Models = JsonConvert.DeserializeObject<LogCancelExportViewModel>(body.ToString());
                var result = service.ExportCancel(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



    }
}

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
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProductController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new ProductService();
                var Models = new SearchProductViewModel();
                Models = JsonConvert.DeserializeObject<SearchProductViewModel>(body.ToString());
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
                var service = new ProductService();
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
                var service = new ProductService();
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

        [HttpPost("SaveChangesV2")]
        public IActionResult SaveChangesV2([FromBody]JObject body)
        {
            try
            {
                var service = new ProductService();
                var Models = new ProductViewModel();
                Models = JsonConvert.DeserializeObject<ProductViewModel>(body.ToString());
                var result = service.SaveChangesV2(Models);
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
                var service = new ProductService();
                var Models = new ProductViewModel();
                Models = JsonConvert.DeserializeObject<ProductViewModel>(body.ToString());
                var result = service.getDelete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        //    // GET: api/<controller>
        //    [HttpGet("filter")]
        //    public IActionResult Get()
        //    {
        //        try
        //        {
        //            var service = new ProductService();

        //            var result = service.Filter();

        //            return Ok(result);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex);
        //        }
        //    }

        //    // GET api/<controller>/5
        //    [HttpGet("{id}")]
        //    public IActionResult Get(Guid id)
        //    {
        //        try
        //        {
        //            var service = new ProductService();

        //            var result = service.getId(id);

        //            return Ok(result);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex);
        //        }
        //    }

        //    // POST api/<controller>
        //    [HttpPost]
        //    public IActionResult Post([FromBody]JObject body)
        //    {
        //        try
        //        {
        //            var service = new ProductService();
        //            var Models = new ProductViewModel();
        //            Models = JsonConvert.DeserializeObject<ProductViewModel>(body.ToString());
        //            var result = service.SaveChanges(Models);
        //            return Ok(result);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex);
        //        }
        //    }

        //    // DELETE api/<controller>/5
        //    [HttpDelete("{id}")]
        //    public IActionResult Delete(Guid id)
        //    {
        //        try
        //        {
        //            var service = new ProductService();

        //            var result = service.getDelete(id);

        //            return Ok(result);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex);
        //        }
        //    }
        //[HttpPost("search")]
        //public IActionResult Get([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ProductService();
        //        var Models = new ProductViewModel();
        //        Models = JsonConvert.DeserializeObject<ProductViewModel>(body.ToString());
        //        var result = service.search(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
        //[HttpPost("autoSearch")]
        //public IActionResult autoSearch([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ProductService();
        //        var Models = new ItemListViewModel();
        //        Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
        //        var result = service.autoSearch(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [HttpPost("productDetail")]
        public IActionResult productDetail([FromBody]JObject body)
        {
            try
            {
                var service = new ProductService();
                var Models = new ProductDetailViewModel();
                Models = JsonConvert.DeserializeObject<ProductDetailViewModel>(body.ToString());
                var result = service.productDetail(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Product")]
        public IActionResult Product([FromBody]JObject body)
        {
            try
            {
                var service = new ProductService();
                var Models = new ProductViewModel();
                Models = JsonConvert.DeserializeObject<ProductViewModel>(body.ToString());
                var result = service.Product(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("GetProduct")]
        public IActionResult GetProduct([FromBody]JObject body)
        {
            try
            {
                var service = new ProductService();
                var Models = new ProductViewModel();
                Models = JsonConvert.DeserializeObject<ProductViewModel>(body.ToString());
                var result = service.GetProduct(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ConfigViewProductDetail")]
        public IActionResult ConfigViewProductDetail([FromBody]JObject body)
        {
            try
            {
                var service = new ProductService();
                var Models = new ProductDetailViewModel();
                Models = JsonConvert.DeserializeObject<ProductDetailViewModel>(body.ToString());
                var result = service.ConfigViewProductDetail(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ConfigBarcode")]
        public IActionResult ConfigBarcode([FromBody]JObject body)
        {
            try
            {
                var service = new ProductService();
                var Models = new BarcodeViewModel();
                Models = JsonConvert.DeserializeObject<BarcodeViewModel>(body.ToString());
                var result = service.ConfigBarcode(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #region importFilePic
        [HttpPost("importFilePic")]
        public async Task<IActionResult> Index(EngineerVM engineerVM)
        {
            try
            {
                resultViewModel items = new resultViewModel();
                if (engineerVM.File != null)
                {
                    //upload files to wwwroot
                    var fileName = Path.GetFileName(engineerVM.File.FileName);
                    var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Uploads", fileName);

                    string memoryPath = AppsInfo.upload;
                    string memoryDocument = AppsInfo.document_upload;
                    string virtualDocument = AppsInfo.document_path;

                    string root = _hostingEnvironment.ContentRootPath + memoryPath;

                    DirectoryInfo dir = new DirectoryInfo(root);
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }

                    // path = root;
                    var provider = new MultipartFormDataStreamProvider(root);

                    var path = _hostingEnvironment.ContentRootPath + memoryDocument;

                    string guidName = "";
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await engineerVM.File.CopyToAsync(fileSteam);
                        var item = new fileViewModel();
                        guidName = Guid.NewGuid().ToString().ToUpper().Replace("-", "_");
                        string extension = fileAppService.getExtension(engineerVM.File.ContentType);

                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Trim('"');
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fileName = Path.GetFileName(fileName);
                        }

                        string newPath = path + guidName + "\\" + fileName;
                        string thumbPath = path + guidName + "\\" + "thumb\\" + fileName;

                        if (fileAppService.getTypeImage(engineerVM.File.ContentType))
                            fileAppService.setThumbnail(newPath, thumbPath);
                        else
                            thumbPath = "";

                        item.name = fileName;
                        item.extension = extension;
                        item.virtualPath = virtualDocument;
                        item.path = virtualDocument + "/" + fileName;
                        item.orginal = fileName;
                        item.thumb = virtualDocument + guidName + "thumb/" + fileName;
                        if (thumbPath == "")
                            item.fileType = "document";
                        else
                            item.fileType = "image";
                        path = item.path;
                        if (path.StartsWith("~"))
                        {
                            path = path.Substring(1);
                        }
                    }
                    //your logic to save filePath to database, for example

                    items.result = true;
                    items.value = filePath;
                    string baseUrl = AppsInfo.upload_host;
                    items.url = baseUrl + path;
                    return this.Ok(items);
                }
                else
                {

                }
                return View();
            }
            catch (System.Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        [HttpPost("Export")]
        public IActionResult Export([FromBody]JObject body)
        {
            try
            {
                var service = new ProductService();
                var Models = new ProductExportViewModel();
                Models = JsonConvert.DeserializeObject<ProductExportViewModel>(body.ToString());
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

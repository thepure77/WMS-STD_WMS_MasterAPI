using System;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/Autocomplete")]
    public class AutocompleteController : Controller
    {

        #region autoUser
        [HttpPost("autoUser")]
        public IActionResult autoUser([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoUser(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoUserGroup
        [HttpPost("autoUserGroup")]
        public IActionResult autoUserGroup([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoUserGroup(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoDocumentType
        [HttpPost("autoDocumentType")]
        public IActionResult autoDocumentType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoDocumentType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoOwner
        [HttpPost("autoOwner")]
        public IActionResult autoOwner([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoOwner(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoVendor
        [HttpPost("autoVendor")]
        public IActionResult autoVendor([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoVendor(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProcessStatus
        [HttpPost("autoProcessStatus")]
        public IActionResult autoProcessStatus([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProcessStatus(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoWarehouse
        [HttpPost("autoWarehouse")]
        public IActionResult autoWarehouse([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoWarehouse(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSku
        [HttpPost("autoSku")]
        public IActionResult autoSku([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSku(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProduct
        [HttpPost("autoProduct")]
        public IActionResult autoProduct([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProduct(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoVehicleType
        [HttpPost("autoVehicleType")]
        public IActionResult autoVehicleType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoVehicleType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoDockDoor
        [HttpPost("autoDockDoor")]
        public IActionResult autoDockDoor([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoDockDoor(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoContainerType
        [HttpPost("autoContainerType")]
        public IActionResult autoContainerType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoContainerType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoWHOwner
        [HttpPost("autoWHOwner")]
        public IActionResult autoWHOwner([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoWHOwner(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoRuleConditionOperationField
        [HttpPost("autoRuleConditionOperationField")]
        public IActionResult autoRuleConditionOperationField([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoRuleConditionOperationField(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoRuleConditionOperation
        [HttpPost("autoRuleConditionOperation")]
        public IActionResult autoRuleConditionOperation([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoRuleConditionOperation(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


        #region autoSearchProductAssembly
        [HttpPost("autoSearchProductAssembly")]
        public IActionResult autoSearchProductAssembly([FromBody]JObject body)
        {
            try
            {
                var service = new ProductAssemblyService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchProductAssembly(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
        #region autoSoldTo
        [HttpPost("autoSoldTo")]
        public IActionResult autoSoldTo([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSoldTo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoVendorType
        [HttpPost("autoVendorType")]
        public IActionResult autoVendorType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoVendorType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoOwnerType
        [HttpPost("autoOwnerType")]
        public IActionResult autoOwnerType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoOwnerType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoAddressCountry
        [HttpPost("autoAddressCountry")]
        public IActionResult autoAddressCountry([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoAddressCountry(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        #endregion



        #region AutoAddressDistrict
        [HttpPost("autoAddressDistrict")]
        public IActionResult autoAddressDistrict([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoAddressDistrict(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoAddressPostcode
        [HttpPost("autoAddressPostcode")]
        public IActionResult autoAddressPostcode([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoAddressPostcode(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoAddressProvince
        [HttpPost("autoAddressProvince")]
        public IActionResult autoAddressProvince([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoAddressProvince(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoAddressSubDistrict
        [HttpPost("autoAddressSubDistrict")]
        public IActionResult autoAddressSubDistrict([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoAddressSubDistrict(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSoldToType
        [HttpPost("autoSoldToType")]
        public IActionResult AutoSoldToType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoSoldToType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        #endregion

        #region autoShipToType
        [HttpPost("autoShipToType")]
        public IActionResult autoShipToType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoShipToType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        #endregion

        #region autoShipTo
        [HttpPost("autoShipTo")]
        public IActionResult autoSearch([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoShipTo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoRoom
        [HttpPost("autoRoom")]
        public IActionResult autoRoom([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoRoom(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoLocationAisle
        [HttpPost("autoLocationAisle")]
        public IActionResult autoLocationAisle([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoLocationAisle(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoLocation
        [HttpPost("autoLocation")]
        public IActionResult autoLocation([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoLocation(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoLocationType
        [HttpPost("autoLocationType")]
        public IActionResult autoLocationType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoLocationType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoWHOwnerType
        [HttpPost("autoWHOwnerType")]
        public IActionResult autoWHOwnerType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoWHOwnerType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductCategory
        [HttpPost("autoProductCategory")]
        public IActionResult autoProductCategory([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductCategory(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductConversionAssembly
        [HttpPost("autoProductConversionAssembly")]
        public IActionResult autoProductConversionAssembly([FromBody]JObject body)
      {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductConversionAssembly(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductType
        [HttpPost("autoProductType")]
        public IActionResult autoProductType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductSubType
        [HttpPost("autoProductSubType")]
        public IActionResult autoProductSubType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductSubType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductOwner
        [HttpPost("autoProductOwner")]
        public IActionResult autoProductOwner([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductOwner(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductConversion
        [HttpPost("autoProductConversion")]
        public IActionResult autoProductConversion([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductConversion(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductId
        [HttpPost("autoProductId")]
        public IActionResult autoProductId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoOwnerId
        [HttpPost("autoOwnerId")]
        public IActionResult autoOwnerId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoOwnerId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductConversionId
        [HttpPost("autoProductConversionId")]
        public IActionResult autoProductConversionId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductConversionId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchProductConvertionBarcodeOwner
        [HttpPost("autoSearchProductConvertionBarcodeOwner")]
        public IActionResult autoProductConvertionBarcodeOwner([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchProductConvertionBarcodeOwner(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductCategoryId
        [HttpPost("autoProductCategoryId")]
        public IActionResult autoProductCategoryId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductCategoryId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductTypeId
        [HttpPost("autoProductTypeId")]
        public IActionResult autoProductTypeId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductTypeId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductSubTypeId
        [HttpPost("autoProductSubTypeId")]
        public IActionResult autoProductSubTypeId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductSubTypeId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchProduct
        [HttpPost("autoSearchProduct")]
        public IActionResult autoSearchProduct([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchProduct(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchProductType
        [HttpPost("autoSearchProductType")]
        public IActionResult autoSearchProductType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchProductType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchProductCategory
        [HttpPost("autoSearchProductCategory")]
        public IActionResult autoSearchProductCategory([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchProductCategory(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchProductSubType
        [HttpPost("autoSearchProductSubType")]
        public IActionResult autoSearchProductSubType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchProductSubType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchProductConversion
        [HttpPost("autoSearchProductConversion")]
        public IActionResult autoSearchProductConversion([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchProductConversion(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchVendor
        [HttpPost("autoSearchVendor")]
        public IActionResult autoSearchVendor([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchVendor(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchVendorType
        [HttpPost("autoSearchVendorType")]
        public IActionResult autoSearchVendorType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchVendorType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchRoom
        [HttpPost("autoSearchRoom")]
        public IActionResult autoSearchRoom([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchRoom(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchWareHouse
        [HttpPost("autoSearchWareHouse")]
        public IActionResult autoSearchWareHouse([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchWareHouse(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchFacility
        [HttpPost("autoSearchFacility")]
        public IActionResult autoSearchFacility([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchFacility(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchWHOwner
        [HttpPost("autoSearchWHOwner")]
        public IActionResult autoSearchWHOwner([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchWHOwner(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchWHOwnerType
        [HttpPost("autoSearchWHOwnerType")]
        public IActionResult autoSearchWHOwnerType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchWHOwnerType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchOwner
        [HttpPost("autoSearchOwner")]
        public IActionResult autoSearchOwner([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchOwner(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchOwnerType
        [HttpPost("autoSearchOwnerType")]
        public IActionResult autoSearchOwnerType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchOwnerType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchOwnerSoldTo
        [HttpPost("autoSearchOwnerSoldTo")]
        public IActionResult autoSearchOwnerSoldTo([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchOwnerSoldTo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSoldToId
        [HttpPost("autoSoldToId")]
        public IActionResult autoSoldToId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSoldToId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchZone
        [HttpPost("autoSearchZone")]
        public IActionResult autoSearchZone([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchZone(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchZoneLocation
        [HttpPost("autoSearchZoneLocation")]
        public IActionResult autoSearchZoneLocation([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchZoneLocation(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoZone
        [HttpPost("autoZone")]
        public IActionResult autoZone([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoZone(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoZoneId
        [HttpPost("autoZoneId")]
        public IActionResult autoZoneId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoZoneId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoLocationId
        [HttpPost("autoLocationId")]
        public IActionResult autoLocationId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoLocationId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchFacilityType
        [HttpPost("autoSearchFacilityType")]
        public IActionResult autoSearchFacilityType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchFacilityType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoVendorTypeAndVendorTypeId
        [HttpPost("autoVendorTypeAndVendorTypeId")]
        public IActionResult autoVendorTypeAndVendorTypeId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoVendorTypeAndVendorTypeId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoOwnerTypeAndOwnerTypeId
        [HttpPost("autoOwnerTypeAndOwnerTypeId")]
        public IActionResult autoOwnerTypeAndOwnerTypeId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoOwnerTypeAndOwnerTypeId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoOwnerAndOwnerId
        [HttpPost("autoOwnerAndOwnerId")]
        public IActionResult autoOwnerAndOwnerId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoOwnerAndOwnerId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoVendorAndVendorId
        [HttpPost("autoVendorAndVendorId")]
        public IActionResult autoVendorAndVendorId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoVendorAndVendorId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSoldToAndSoldToId
        [HttpPost("autoSoldToAndSoldToId")]
        public IActionResult autoSoldToAndSoldToId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSoldToAndSoldToId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductCategoryAndProductCategoryId
        [HttpPost("autoProductCategoryAndProductCategoryId")]
        public IActionResult autoProductCategoryAndProductCategoryId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductCategoryAndProductCategoryId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductTypeAndProductTypeId
        [HttpPost("autoProductTypeAndProductTypeId")]
        public IActionResult autoProductTypeAndProductTypeId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductTypeAndProductTypeId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductSubTypeAndProductSubTypeId
        [HttpPost("autoProductSubTypeAndProductSubTypeId")]
        public IActionResult autoProductSubTypeAndProductSubTypeId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductSubTypeAndProductSubTypeId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductAndProductId
        [HttpPost("autoProductAndProductId")]
        public IActionResult autoProductAndProductId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductAndProductId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductConversionAndProductConversionId
        [HttpPost("autoProductConversionAndProductConversionId")]
        public IActionResult autoProductConversionAndProductConversionId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductConversionAndProductConversionId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoWarehouseAndWarehouseId
        [HttpPost("autoWarehouseAndWarehouseId")]
        public IActionResult autoWarehouseAndWarehouseId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoWarehouseAndWarehouseId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoZoneAndZoneId
        [HttpPost("autoZoneAndZoneId")]
        public IActionResult autoZoneAndZoneId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoZoneAndZoneId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchZonePutaway
        [HttpPost("autoSearchZonePutaway")]
        public IActionResult autoSearchZonePutaway([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchZonePutaway(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoLocationAndLocationId
        [HttpPost("autoLocationAndLocationId")]
        public IActionResult autoLocationAndLocationId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoLocationAndLocationId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoWHOwnerTypeAndWHOwnerTypeId
        [HttpPost("autoWHOwnerTypeAndWHOwnerTypeId")]
        public IActionResult autoWHOwnerTypeAndWHOwnerTypeId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoWHOwnerTypeAndWHOwnerTypeId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoFacilityTypeAndFacilityTypeId
        [HttpPost("autoFacilityTypeAndFacilityTypeId")]
        public IActionResult autoFacilityTypeAndFacilityTypeId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoFacilityTypeAndFacilityTypeId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchRuleConditionField
        [HttpPost("autoSearchRuleConditionField")]
        public IActionResult autoSearchRuleConditionField([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchRuleConditionField(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchRuleConditionOperation
        [HttpPost("autoSearchRuleConditionOperation")]
        public IActionResult autoSearchRuleConditionOperation([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchRuleConditionOperation(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProcessAndProcessId
        [HttpPost("autoProcessAndProcessId")]
        public IActionResult autoProcessAndProcessId([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProcessAndProcessId(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchProcess
        [HttpPost("autoSearchProcess")]
        public IActionResult autoSearchProcess([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchProcess(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchRule
        [HttpPost("autoSearchRule")]
        public IActionResult autoSearchRule([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchRule(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchWave
        [HttpPost("autoSearchWave")]
        public IActionResult autoSearchWave([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchWave(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchLocationZoneputaway
        [HttpPost("autoSearchLocationZoneputaway")]
        public IActionResult autoSearchLocationZoneputaway([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchLocationZoneputaway(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoZoneputaway
        [HttpPost("autoZoneputaway")]
        public IActionResult autoZoneputaway([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoZoneputaway(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchRuleputawayConditionField
        [HttpPost("autoSearchRuleputawayConditionField")]
        public IActionResult autoSearchRuleputawayConditionField([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchRuleputawayConditionField(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchRuleputawayCondition
        [HttpPost("autoSearchRuleputawayCondition")]
        public IActionResult autoSearchRuleputawayCondition([FromBody]JObject body)

        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
           
                var result = service.autoSearchRuleputawayCondition(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoRuleputawayConditionField
        [HttpPost("autoRuleputawayConditionField")]
        public IActionResult autoRuleputawayConditionField([FromBody]JObject body)

        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());

                var result = service.autoRuleputawayConditionField(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoItemStatus
        [HttpPost("autoItemStatus")]
        public IActionResult autoItemStatus([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoItemStatus(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoRuleputawayCondition
        [HttpPost("autoRuleputawayCondition")]
        public IActionResult autoRuleputawayCondition([FromBody]JObject body)

        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
 
                var result = service.autoRuleputawayCondition(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoDocumentTypeItemStatusSearch
        [HttpPost("autoDocumentTypeItemStatusSearch")]
        public IActionResult autoDocumentTypeItemStatusSearch([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoDocumentTypeItemStatusSearch(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchRuleputaway
        [HttpPost("autoSearchRuleputaway")]
        public IActionResult autoSearchRuleputaway([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchRuleputaway(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchUserGroup
        [HttpPost("autoSearchUserGroup")]
        public IActionResult autoSearchUserGroup([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchUserGroup(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchUserGroupMenu
        [HttpPost("autoSearchUserGroupMenu")]
        public IActionResult autoSearchUserGroupMenu([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchUserGroupMenu(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


        #region autoSearchUserGroupZone
        [HttpPost("autoSearchUserGroupZone")]
        public IActionResult autoSearchUserGroupZone([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchUserGroupZone(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoMovementType
        [HttpPost("autoMovementType")]
        public IActionResult autoMovementType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoMovementType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoUserName
        [HttpPost("autoUserName")]
        public IActionResult autoUserName([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoUserName(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductBarcodeConversion
        [HttpPost("autoProductBarcodeConversion")]
        public IActionResult autoProductBarcodeConversion([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductBarcodeConversion(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


        #region autoCostCenter
        [HttpPost("autoCostCenter")]
        public IActionResult autoCostCenter([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoCostCenter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchDocumentType
        [HttpPost("autoSearchDocumentType")]
        public IActionResult autoSearchDocumentType([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());

                var result = service.autoSearchDocumentType(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchUser
        [HttpPost("autoSearchUser")]
        public IActionResult autoSearchUser([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchUser(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchRuleCondition
        [HttpPost("autoSearchRuleCondition")]
        public IActionResult autoSearchRuleCondition([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchRuleCondition(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoRule
        [HttpPost("autoRule")]
        public IActionResult autoRule([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoRule(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchProductConversionV2
        [HttpPost("autoSearchProductConversionV2")]
        public IActionResult autoSearchProductConversionV2([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchProductConversionV2(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchOwnerV2
        [HttpPost("autoSearchOwnerV2")]
        public IActionResult autoSearchOwnerV2([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchOwnerV2(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductAssembly
        [HttpPost("autoProductAssembly")]
        public IActionResult autoProductAssembly([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductAssembly(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoServiceCharge
        [HttpPost("autoServiceCharge")]
        public IActionResult autoServiceCharge([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoServiceCharge(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoRoomWH
        [HttpPost("autoRoomWH")]
        public IActionResult autoRoomWH([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoRoomWH(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoCostCenterFull_Name_Id
        [HttpPost("autoCostCenterFull_Name_Id")]
        public IActionResult autoCostCenterFull_Name_Id([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoCostCenterFull_Name_Id(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoLocationAndLocationIdPickFace
        [HttpPost("autoLocationAndLocationIdPickFace")]
        public IActionResult autoLocationAndLocationIdPickFace([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoLocationAndLocationIdPickFace(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchProductConversionV3
        [HttpPost("autoSearchProductConversionV3")]
        public IActionResult autoSearchProductConversionV3([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchProductConversionV3(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoSearchDocument
        [HttpPost("AutoSearchDocument")]
        public IActionResult AutoSearchDocument([FromBody]JObject body)
        {
            try
            {
                var service = new AutocompleteService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoSearchDocument(Models);
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

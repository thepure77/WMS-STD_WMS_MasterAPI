using Business.Library;
using DataAccess;
using MasterDataBusiness.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterDataBusiness
{
    public class AutocompleteService
    {

        private MasterDataDbContext db;

        public AutocompleteService()
        {
            db = new MasterDataDbContext();
        }

        public AutocompleteService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region autoUser
        public List<ItemListViewModel> autoUser(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_User.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.User_Id.Contains(data.key)
                                                || c.User_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.User_Name, c.User_Index, c.User_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.User_Index,
                            id = item.User_Id,
                            name = item.User_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoUserGroup
        public List<ItemListViewModel> autoUserGroup(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_UserGroup.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.UserGroup_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.UserGroup_Index, c.UserGroup_Id, c.UserGroup_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.UserGroup_Index,
                            id = item.UserGroup_Id,
                            name = item.UserGroup_Name
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoDocumentType
        public List<ItemListViewModel> autoDocumentType(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_DocumentType.AsQueryable();

                    if (data.key == "-")
                    {

                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.DocumentType_Name.Contains(data.key));
                    }
                    else if (data.index != new Guid("00000000-0000-0000-0000-000000000000".ToString()))
                    {
                        query = query.Where(c => c.DocumentType_Index == data.index);
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.DocumentType_Index, c.DocumentType_Id, c.DocumentType_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.DocumentType_Index,
                            id = item.DocumentType_Id,
                            name = item.DocumentType_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoOwner
        public List<ItemListViewModel> autoOwner(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Owner.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Owner_Name.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Owner_Index, c.Owner_Id, c.Owner_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Owner_Index,
                            id = item.Owner_Id,
                            name = item.Owner_Name,

                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoVendor
        public List<ItemListViewModel> autoVendor(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_Vendor.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Vendor_Name.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Vendor_Index, c.Vendor_Id, c.Vendor_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Vendor_Index,
                            id = item.Vendor_Id,
                            name = item.Vendor_Name,
                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoProcessStatus
        public List<ItemListViewModel> autoProcessStatus(ItemListViewModel model)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.sy_ProcessStatus.AsQueryable();
                    var items = new List<ItemListViewModel>();

                    //Status PlanGR
                    if (model.chk == 1)
                    {
                        if (!string.IsNullOrEmpty(model.key))
                        {
                            query = query.Where(c => c.ProcessStatus_Name.Contains(model.key));

                            query = query.Where(c => c.Process_Index == Guid.Parse("C2A3F847-BAA6-46FE-B502-44F2D5826A1C"));

                        }
                    }
                    //Status GR
                    if (model.chk == 2)
                    {

                        if (!string.IsNullOrEmpty(model.key))
                        {
                            query = query.Where(c => c.ProcessStatus_Name.Contains(model.key));

                            query = query.Where(c => c.Process_Index == new Guid("5F147725-520C-4CA6-B1D2-2C0E65E7AAAA"));

                        }
                    }
                    //Status PlanGI
                    if (model.chk == 3)
                    {
                        if (!string.IsNullOrEmpty(model.key))
                        {
                            query = query.Where(c => c.ProcessStatus_Name.Contains(model.key));

                            query = query.Where(c => c.Process_Index == new Guid("22744590-55D8-4448-88EF-5997C252111F"));

                        }
                    }
                    //Status PlanGI
                    if (model.chk == 4)
                    {
                        if (!string.IsNullOrEmpty(model.key))
                        {
                            query = query.Where(c => c.ProcessStatus_Name.Contains(model.key));

                            query = query.Where(c => c.Process_Index == new Guid("19CCA9B3-B4D2-4BC5-87F7-DE64C1E75CBF"));

                        }
                    }
                    //Status POS
                    if (model.chk == 5)
                    {
                        if (!string.IsNullOrEmpty(model.key))
                        {
                            query = query.Where(c => c.ProcessStatus_Name.Contains(model.key));

                            query = query.Where(c => c.Process_Index == new Guid("04CD52B8-0ADD-483A-8E9B-7417537159FC"));

                        }
                    }
                    //Status Call Center
                    if (model.chk == 6)
                    {
                        if (!string.IsNullOrEmpty(model.key))
                        {
                            query = query.Where(c => c.ProcessStatus_Name.Contains(model.key));

                            query = query.Where(c => c.Process_Index == new Guid("13B42EA6-1765-4655-A3A8-2B9472B5DC2B"));

                        }
                    }
                    //Status Confirm Marshal
                    if (model.chk == 7)
                    {
                        if (!string.IsNullOrEmpty(model.key))
                        {
                            query = query.Where(c => c.ProcessStatus_Name.Contains(model.key));

                            query = query.Where(c => c.Process_Index == new Guid("D38183D2-6105-4FB8-BC18-836CBB83FB28"));

                        }
                    }


                    var result = query.Select(c => new { c.ProcessStatus_Index, c.ProcessStatus_Id, c.ProcessStatus_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProcessStatus_Index,
                            id = item.ProcessStatus_Id,
                            name = item.ProcessStatus_Name

                        };

                        items.Add(resultItem);
                    }

                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoWarehouse
        public List<ItemListViewModel> autoWarehouse(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Warehouse.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Warehouse_Id.Contains(data.key)
                                                || c.Warehouse_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Warehouse_Name, c.Warehouse_Index, c.Warehouse_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.Warehouse_Index,
                            id = item.Warehouse_Id,
                            name = item.Warehouse_Name,
                            key = item.Warehouse_Id + " - " + item.Warehouse_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoSku
        public List<ItemListViewModel> autoSku(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var owP = context.MS_ProductOwner.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key2))
                    {
                        owP = owP.Where(c => c.Owner_Index == new Guid(data.key2));
                    }

                    var queryO = owP.ToList();

                    var query = context.MS_Product.Where(c => c.IsActive == 1 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Product_Id.Contains(data.key));
                    }
                    if (data.key3 == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key3))
                    {
                        query = query.Where(c => c.Product_Id.Contains(data.key3));
                    }

                    query = query.Where(c => queryO.Select(s => s.Product_Index).Contains(c.Product_Index));

                    var result = query.Select(c => new { c.Product_Index, c.Product_Id, c.Product_Name, c.Ref_No1, c.Ref_No2, c.IsLot, c.UDF_1, c.IsMfgDate, c.IsExpDate}).Distinct().Take(10).ToList();

                    var items = new List<ItemListViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Product_Index,
                            id = item.Product_Id,
                            name = item.Product_Name,
                            value1 = item.Ref_No1,
                            value2 = item.Ref_No2,
                            value3 = item.IsLot.ToString(),
                            value4 = item.IsMfgDate.ToString(),
                            value7 = item.IsExpDate.ToString(),
                            value6 = item.UDF_1
                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion




        #region autoRuleConditionOperationField
        public List<ItemListViewModel> autoRuleConditionOperationField(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.sy_RuleConditionField.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.RuleConditionField_Name.Contains(data.key));

                    }

                    var result = query.Select(c => new { c.RuleConditionField_Index, c.RuleConditionField_Name }).Distinct().Take(10).ToList();

                    var items = new List<ItemListViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.RuleConditionField_Index,
                            name = item.RuleConditionField_Name

                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region autoRuleConditionOperation
        public List<ItemListViewModel> autoRuleConditionOperation(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.sy_RuleConditionOperation.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.RuleConditionOperation.Contains(data.key));

                    }

                    var result = query.Select(c => new { c.RuleConditionOperation }).Distinct().Take(10).ToList();

                    var items = new List<ItemListViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {

                            name = item.RuleConditionOperation

                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region autoProduct
        public List<ItemListViewModel> autoProduct(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var owP = context.MS_ProductOwner.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key2))
                    {
                        owP = owP.Where(c => c.Owner_Index == new Guid(data.key2));
                    }

                    var queryO = owP.ToList();

                    var query = context.MS_Product.Where(c => c.IsActive == 1 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Product_Name.Contains(data.key));

                    }
                    else if (!string.IsNullOrEmpty(data.key3))
                    {
                        query = query.Where(c => c.Product_Name.Contains(data.key3));
                    }

                    query = query.Where(c => queryO.Select(s => s.Product_Index).Contains(c.Product_Index));



                    var result = query.Select(c => new { c.Product_Index, c.Product_Id, c.Product_Name, c.Ref_No1, c.Ref_No2, c.IsLot, c.UDF_1 }).Distinct().Take(10).ToList();

                    var items = new List<ItemListViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Product_Index,
                            id = item.Product_Id,
                            name = item.Product_Name,
                            value1 = item.Ref_No1,
                            value2 = item.Ref_No2,
                            value3 = item.IsLot.ToString(),
                            value6 = item.UDF_1
                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoVehicleType
        public List<ItemListViewModel> autoVehicleType(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_VehicleType.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.VehicleType_Name.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.VehicleType_Index, c.VehicleType_Id, c.VehicleType_Name }).Distinct().Take(10).ToList();


                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.VehicleType_Index,
                            id = item.VehicleType_Id,
                            name = item.VehicleType_Name,

                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoDockDoor
        public List<ItemListViewModel> autoDockDoor(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_DockDoor.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.DockDoor_Name.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.DockDoor_Index, c.DockDoor_Id, c.DockDoor_Name }).Distinct().Take(10).ToList();


                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.DockDoor_Index,
                            id = item.DockDoor_Id,
                            name = item.DockDoor_Name,

                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoContainerType
        public List<ItemListViewModel> autoContainerType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_ContainerType.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ContainerType_Name.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ContainerType_Index, c.ContainerType_Id, c.ContainerType_Name }).Distinct().Take(10).ToList();


                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ContainerType_Index,
                            id = item.ContainerType_Id,
                            name = item.ContainerType_Name,

                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoWHOwner
        public List<ItemListViewModel> autoWHOwner(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_WHOwner.AsQueryable();
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.WHOwner_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.WHOwner_Index, c.WHOwner_Id, c.WHOwner_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.WHOwner_Index,
                            id = item.WHOwner_Id,
                            name = item.WHOwner_Name,

                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoSoldTo
        public List<ItemListViewModel> autoSoldTo(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {


                    var query = context.MS_SoldTo.AsQueryable();
                    if (data.key == "-")
                    {

                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        //query = query.Where(c => c.SoldTo_Name.Contains(data.key));
                        query = query.Where(c => c.SoldTo_Id.Contains(data.key)
                                               || c.SoldTo_Name.Contains(data.key));
                    }
                    else if (!string.IsNullOrEmpty(data.key2))
                    {
                        query = query.Where(c => c.SoldTo_Id.Contains(data.key2));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.SoldTo_Index, c.SoldTo_Id, c.SoldTo_Name, c.Contact_Person, c.SoldTo_Address }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.SoldTo_Index,
                            id = item.SoldTo_Id,
                            name = item.SoldTo_Name,
                            value1 = item.Contact_Person,
                            address = item.SoldTo_Address
                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoOwnerType
        public List<ItemListViewModel> autoOwnerType(ItemListViewModel data)
        {
            try
            {

                var query = db.MS_OwnerType.AsQueryable();

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.OwnerType_Name.Contains(data.key));
                }

                var items = new List<ItemListViewModel>();

                var result = query.Select(c => new { c.OwnerType_Name, c.OwnerType_Index, c.OwnerType_Id }).Distinct().Take(10).ToList();

                foreach (var item in result)
                {
                    var resultItem = new ItemListViewModel
                    {
                        index = item.OwnerType_Index,
                        id = item.OwnerType_Id,
                        name = item.OwnerType_Name
                    };

                    items.Add(resultItem);
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoVendorType
        public List<ItemListViewModel> autoVendorType(ItemListViewModel data)
        {
            try
            {

                var query = db.MS_VendorType.AsQueryable();

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.VendorType_Name.Contains(data.key));
                }

                var items = new List<ItemListViewModel>();

                var result = query.Select(c => new { c.VendorType_Name, c.VendorType_Index, c.VendorType_Id }).Distinct().Take(10).ToList();

                foreach (var item in result)
                {
                    var resultItem = new ItemListViewModel
                    {
                        index = item.VendorType_Index,
                        id = item.VendorType_Id,
                        name = item.VendorType_Name
                    };

                    items.Add(resultItem);
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoAddressCountry
        public List<ItemListViewModel> autoAddressCountry(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_AddressCountry.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Country_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Country_Name, c.Country_Index, c.Country_Id }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            id = item.Country_Id,
                            name = item.Country_Name,
                            index = item.Country_Index
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoAddressDistrict
        public List<ItemListViewModel> autoAddressDistrict(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_AddressDistrict.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.District_Name.Contains(data.key));
                    }
                    if (!string.IsNullOrEmpty(data.key2))
                    {
                        query = query.Where(c => c.Country_Index == Guid.Parse(data.key2));
                    }
                    if (!string.IsNullOrEmpty(data.key3))
                    {
                        query = query.Where(c => c.Province_Index == Guid.Parse(data.key3));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.District_Name, c.District_Index, c.District_Id, c.Country_Index, c.Province_Index }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.District_Index,
                            id = item.District_Id,
                            name = item.District_Name
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoAddressPostcode
        public List<ItemListViewModel> autoAddressPostcode(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_AddressPostcode.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Postcode_Name.Contains(data.key));
                    }
                    if (!string.IsNullOrEmpty(data.key2))
                    {
                        query = query.Where(c => c.Country_Index == Guid.Parse(data.key2));
                    }
                    if (!string.IsNullOrEmpty(data.key3))
                    {
                        query = query.Where(c => c.Province_Index == Guid.Parse(data.key3));
                    }
                    if (!string.IsNullOrEmpty(data.key4))
                    {
                        query = query.Where(c => c.District_Index == Guid.Parse(data.key4));
                    }
                    if (!string.IsNullOrEmpty(data.key5))
                    {
                        query = query.Where(c => c.SubDistrict_Index == Guid.Parse(data.key5));
                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Postcode_Index, c.Postcode_Id, c.Postcode_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Postcode_Index,
                            id = item.Postcode_Id,
                            name = item.Postcode_Name
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AutoAddressProvince
        public List<ItemListViewModel> autoAddressProvince(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_AddressProvince.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Province_Name.Contains(data.key));
                    }
                    if (!string.IsNullOrEmpty(data.key2))
                    {
                        query = query.Where(c => c.Country_Index == Guid.Parse(data.key2));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Province_Name, c.Province_Index, c.Province_Id, c.Country_Index }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.Province_Index,
                            id = item.Province_Id,
                            name = item.Province_Name
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoAddressSubDistrict
        public List<ItemListViewModel> autoAddressSubDistrict(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_AddressSubDistrict.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.SubDistrict_Name.Contains(data.key));
                    }
                    if (!string.IsNullOrEmpty(data.key2))
                    {
                        query = query.Where(c => c.Country_Index == Guid.Parse(data.key2));
                    }
                    if (!string.IsNullOrEmpty(data.key3))
                    {
                        query = query.Where(c => c.Province_Index == Guid.Parse(data.key3));
                    }
                    if (!string.IsNullOrEmpty(data.key4))
                    {
                        query = query.Where(c => c.District_Index == Guid.Parse(data.key4));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.SubDistrict_Name, c.SubDistrict_Index, c.SubDistrict_Id, c.Country_Index, c.Province_Index, c.District_Index }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.SubDistrict_Index,
                            id = item.SubDistrict_Id,
                            name = item.SubDistrict_Name
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AutoSoldToType
        public List<ItemListViewModel> AutoSoldToType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_SoldToType.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.SoldToType_Id.Contains(data.key)
                                                || c.SoldToType_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.SoldToType_Name, c.SoldToType_Index, c.SoldToType_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.SoldToType_Index,
                            id = item.SoldToType_Id,
                            name = item.SoldToType_Id + " - " + item.SoldToType_Name,
                            key = item.SoldToType_Id + " - " + item.SoldToType_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoShipToType
        public List<ItemListViewModel> autoShipToType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_ShipToType.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ShipToType_Id.Contains(data.key)
                                                || c.ShipToType_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.ShipToType_Name, c.ShipToType_Index, c.ShipToType_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.ShipToType_Index,
                            id = item.ShipToType_Id,
                            name = item.ShipToType_Id + " - " + item.ShipToType_Name,
                            key = item.ShipToType_Id + " - " + item.ShipToType_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoShipTo
        public List<ItemListViewModel> autoShipTo(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_ShipTo.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ShipTo_Id.Contains(data.key)
                                                || c.ShipTo_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ShipTo_Index, c.ShipTo_Id, c.ShipTo_Name, c.Contact_Person, c.ShipTo_Address, c.ShipToType_Index,c.Ref_No3 }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ShipTo_Index,
                            id = item.ShipTo_Id,
                            name = item.ShipTo_Name,
                            address = item.ShipTo_Address,
                            value1 = item.Contact_Person,
                            value2 = item.ShipToType_Index.ToString(),
                            value3 = item.Ref_No3
                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoRoom
        public List<ItemListViewModel> autoRoom(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Room.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Room_Id.Contains(data.key)
                                                || c.Room_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Room_Name, c.Room_Index, c.Room_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.Room_Index,
                            id = item.Room_Id,
                            name = item.Room_Id + " - " + item.Room_Name,
                            key = item.Room_Id + " - " + item.Room_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region autoLocationAisle
        public List<ItemListViewModel> autoLocationAisle(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_LocationAisle.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.LocationLock_Id.Contains(data.key)
                                                || c.LocationLock_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.LocationLock_Name, c.LocationAisle_Index, c.LocationLock_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.LocationAisle_Index,
                            id = item.LocationLock_Id,
                            name = item.LocationLock_Name,
                            key = item.LocationLock_Id + " - " + item.LocationLock_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region autoLocation
        public List<ItemListViewModel> autoLocation(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Location.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Location_Id.Contains(data.key)
                                                || c.Location_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Location_Name, c.Location_Index, c.Location_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.Location_Index,
                            id = item.Location_Id,
                            name = item.Location_Name,
                            key = item.Location_Id + " - " + item.Location_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoLocationType
        public List<ItemListViewModel> autoLocationType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_LocationType.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.LocationType_Id.Contains(data.key)
                                                || c.LocationType_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.LocationType_Name, c.LocationType_Index, c.LocationType_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.LocationType_Index,
                            id = item.LocationType_Id,
                            name = item.LocationType_Name,
                            key = item.LocationType_Id + " - " + item.LocationType_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoWHOwnerType
        public List<ItemListViewModel> autoWHOwnerType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_WHOwnerType.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.WHOwnerType_Name.Contains(data.key));
                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.WHOwnerType_Index, c.WHOwnerType_Id, c.WHOwnerType_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.WHOwnerType_Index,
                            id = item.WHOwnerType_Id,
                            name = item.WHOwnerType_Name,
                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoProductCategory
        public List<ItemListViewModel> autoProductCategory(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_ProductCategory.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductCategory_Name.Contains(data.key));
                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ProductCategory_Index, c.ProductCategory_Id, c.ProductCategory_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProductCategory_Index,
                            id = item.ProductCategory_Id,
                            name = item.ProductCategory_Name,
                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoProductType
        public List<ItemListViewModel> autoProductType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_ProductType.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        if (data.key != "-")
                        {
                            query = query.Where(c => c.ProductType_Name.Contains(data.key));
                        }
                    }
                    if (!string.IsNullOrEmpty(data.key2))
                    {
                        query = query.Where(c => c.ProductCategory_Index == Guid.Parse(data.key2));
                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ProductType_Index, c.ProductType_Id, c.ProductType_Name, c.ProductCategory_Index }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProductType_Index,
                            id = item.ProductType_Id,
                            name = item.ProductType_Name,
                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoProductSubType
        public List<ItemListViewModel> autoProductSubType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_ProductSubType.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductSubType_Name.Contains(data.key));
                    }
                    if (!string.IsNullOrEmpty(data.key2))
                    {
                        query = query.Where(c => c.ProductType_Index == Guid.Parse(data.key2));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ProductSubType_Index, c.ProductSubType_Id, c.ProductSubType_Name, c.ProductType_Index }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProductSubType_Index,
                            id = item.ProductSubType_Id,
                            name = item.ProductSubType_Name,
                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoProductOwner
        public List<ItemListViewModel> autoProductOwner(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_ProductOwner.Where(c => c.ProductOwner_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductOwner_Id,
                        key = s.ProductOwner_Id
                    }).Distinct();

                    var query2 = db.View_ProductOwner.Where(c => c.Owner_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Owner_Name,
                        key = s.Owner_Name
                    }).Distinct();

                    var query3 = db.View_ProductOwner.Where(c => c.Product_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Product_Name,
                        key = s.Product_Name

                    }).Distinct();

                    var query = query1.Union(query2).Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoProductConversion
        public List<ItemListViewModel> autoProductConversion(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_ProductConversion.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductConversion_Name.Contains(data.key));

                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ProductConversion_Index, c.ProductConversion_Id, c.ProductConversion_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProductConversion_Index,
                            id = item.ProductConversion_Id,
                            name = item.ProductConversion_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


        #region autoProductConversionAssembly
        public List<ItemListViewModel> autoProductConversionAssembly(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_ProductConversion.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductConversion_Name.Contains(data.key));

                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ProductConversion_Name, c.ProductConversion_Index, c.ProductConversion_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            id = item.ProductConversion_Id,
                            name = item.ProductConversion_Name,
                            index = item.ProductConversion_Index
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region autoProductId
        public List<ItemListViewModel> autoProductId(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_Product.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key.ToString()) && data.key != "-")
                    {
                        query = query.Where(c => c.Product_Id.Contains(data.key));
                    }

                    var result = query.Select(c => new { c.Product_Index, c.Product_Id, c.Product_Name }).Distinct().Take(10).ToList();

                    var items = new List<ItemListViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Product_Index,
                            id = item.Product_Name,
                            name = item.Product_Id
                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoOwnerId
        public List<ItemListViewModel> autoOwnerId(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_Owner.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Owner_Id.Contains(data.key));
                    }

                    var result = query.Select(c => new { c.Owner_Index, c.Owner_Id, c.Owner_Name, c.Ref_No1, c.Ref_No2, c.Ref_No3, c.Owner_Address, c.Owner_TaxID }).Distinct().Take(10).ToList();

                    var items = new List<ItemListViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Owner_Index,
                            id = item.Owner_Name,
                            name = item.Owner_Id,
                            value1 = item.Ref_No1,
                            value2 = item.Ref_No2,
                            value3 = item.Ref_No3,
                            value4 = item.Owner_Address,
                            value7 = item.Owner_TaxID,
                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoProductConversionId
        public List<ItemListViewModel> autoProductConversionId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_ProductConversion.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductConversion_Id.Contains(data.key));
                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ProductConversion_Index, c.ProductConversion_Id, c.ProductConversion_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProductConversion_Index,
                            id = item.ProductConversion_Name,
                            name = item.ProductConversion_Id,
                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoSearchProductConvertionBarcodeOwner
        public List<ItemListViewModel> autoSearchProductConvertionBarcodeOwner(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_ProductConversionBarcodeV2.Where(c => c.ProductConversionBarcode_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductConversionBarcode_Id,
                        key = s.ProductConversionBarcode_Id
                    }).Distinct();

                    var query2 = db.View_ProductConversionBarcodeV2.Where(c => c.ProductConversionBarcode.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductConversionBarcode,
                        key = s.ProductConversionBarcode
                    }).Distinct();

                    var query3 = db.View_ProductConversionBarcodeV2.Where(c => c.Owner_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Owner_Name,
                        key = s.Owner_Name

                    }).Distinct();

                    var query4 = db.View_ProductConversionBarcodeV2.Where(c => c.Product_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Product_Name,
                        key = s.Product_Name

                    }).Distinct();

                    var query5 = db.View_ProductConversionBarcodeV2.Where(c => c.ProductConversion_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductConversion_Name,
                        key = s.ProductConversion_Name

                    }).Distinct();

                    var query6 = db.View_ProductConversionBarcodeV2.Where(c => c.Product_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Product_Id,
                        key = s.Product_Id

                    }).Distinct();

                    var query = query1.Union(query2).Union(query3).Union(query4).Union(query5).Union(query6);

                    var arr = new List<ItemListViewModel>();

                    var queryList = query.OrderBy(c => c.name).ToList();
                    var i = 0;
                    foreach (var dataList in queryList)
                    {
                        if (arr.Count == 0)
                        {
                            arr.Add(dataList);
                        }
                        if (arr[i].name != dataList.name)
                        {
                            arr.Add(dataList);
                            i++;
                        }
                    }
                    items = arr.Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoProductCategoryId
        public List<ItemListViewModel> autoProductCategoryId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_ProductCategory.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductCategory_Id.Contains(data.key));
                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ProductCategory_Index, c.ProductCategory_Id, c.ProductCategory_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProductCategory_Index,
                            id = item.ProductCategory_Name,
                            name = item.ProductCategory_Id,
                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoProductTypeId
        public List<ItemListViewModel> autoProductTypeId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_ProductType.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductType_Id.Contains(data.key));
                    }
                    if (!string.IsNullOrEmpty(data.key2))
                    {
                        query = query.Where(c => c.ProductCategory_Index == Guid.Parse(data.key2));
                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ProductType_Index, c.ProductType_Id, c.ProductType_Name, c.ProductCategory_Index }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProductType_Index,
                            id = item.ProductType_Name,
                            name = item.ProductType_Id,
                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoProductSubTypeId
        public List<ItemListViewModel> autoProductSubTypeId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_ProductSubType.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductSubType_Id.Contains(data.key));
                    }
                    if (!string.IsNullOrEmpty(data.key2))
                    {
                        query = query.Where(c => c.ProductType_Index == Guid.Parse(data.key2));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ProductSubType_Index, c.ProductSubType_Id, c.ProductSubType_Name, c.ProductType_Index }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProductSubType_Index,
                            id = item.ProductSubType_Name,
                            name = item.ProductSubType_Id,
                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoSearchProduct
        public List<ItemListViewModel> autoSearchProduct(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_ProductDetailV2.Where(c => c.Product_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Product_Id,
                        key = s.Product_Id
                    }).Distinct();

                    var query2 = db.View_ProductDetailV2.Where(c => c.Product_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Product_Name,
                        key = s.Product_Name
                    }).Distinct();

                    var query3 = db.View_ProductDetailV2.Where(c => c.ProductType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductType_Name,
                        key = s.ProductType_Name
                    }).Distinct();


                    var query4 = db.View_ProductDetailV2.Where(c => c.ProductSubType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductSubType_Name,
                        key = s.ProductSubType_Name
                    }).Distinct();

                    var query5 = db.View_ProductDetailV2.Where(c => c.ProductConversion_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductConversion_Name,
                        key = s.ProductConversion_Name
                    }).Distinct();


                    var query6 = db.View_ProductDetailV2.Where(c => c.ProductCategory_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductCategory_Name,
                        key = s.ProductCategory_Name
                    }).Distinct();

                    var query7 = db.View_ProductDetailV2.Where(c => c.Ref_No1.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Ref_No1,
                        key = s.Ref_No1
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3).Union(query4).Union(query5).Union(query6).Union(query7);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchProductType
        public List<ItemListViewModel> autoSearchProductType(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_ProductType.Where(c => c.ProductType_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductType_Id,
                        key = s.ProductType_Id
                    }).Distinct();

                    var query2 = db.View_ProductType.Where(c => c.ProductType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductType_Name,
                        key = s.ProductType_Name
                    }).Distinct();

                    var query3 = db.View_ProductType.Where(c => c.ProductCategory_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductCategory_Name,
                        key = s.ProductCategory_Name
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchProductCategory
        public List<ItemListViewModel> autoSearchProductCategory(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_ProductCategory.Where(c => c.ProductCategory_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductCategory_Id,
                        key = s.ProductCategory_Id
                    }).Distinct();

                    var query2 = db.View_ProductType.Where(c => c.ProductCategory_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductCategory_Name,
                        key = s.ProductCategory_Name
                    }).Distinct();

                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchProductSubType
        public List<ItemListViewModel> autoSearchProductSubType(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_ProductSubType.Where(c => c.ProductSubType_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductSubType_Id,
                        key = s.ProductSubType_Id
                    }).Distinct();

                    var query2 = db.View_ProductSubType.Where(c => c.ProductSubType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductSubType_Name,
                        key = s.ProductSubType_Name
                    }).Distinct();

                    var query3 = db.View_ProductSubType.Where(c => c.ProductType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductType_Name,
                        key = s.ProductType_Name
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchProductConversion
        public List<ItemListViewModel> autoSearchProductConversion(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_ProductConversion.Where(c => c.ProductConversion_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductConversion_Id,
                        key = s.ProductConversion_Id
                    }).Distinct();

                    var query2 = db.View_ProductConversion.Where(c => c.ProductConversion_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductConversion_Name,
                        key = s.ProductConversion_Name
                    }).Distinct();

                    var query3 = db.View_ProductConversion.Where(c => c.Product_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Product_Name,
                        key = s.Product_Name
                    }).Distinct();

                    var query4 = db.View_ProductConversion.Where(c => c.Product_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Product_Id,
                        key = s.Product_Id
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3).Union(query4);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchVendor
        public List<ItemListViewModel> autoSearchVendor(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_Vendor.Where(c => c.Vendor_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Vendor_Id,
                        key = s.Vendor_Id
                    }).Distinct();

                    var query2 = db.MS_Vendor.Where(c => c.Vendor_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Vendor_Name,
                        key = s.Vendor_Name
                    }).Distinct();

                    var query3 = db.MS_Vendor.Where(c => c.VendorType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.VendorType_Name,
                        key = s.VendorType_Name
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchVendorType
        public List<ItemListViewModel> autoSearchVendorType(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_VendorType.Where(c => c.VendorType_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.VendorType_Id,
                        key = s.VendorType_Id
                    }).Distinct();

                    var query2 = db.MS_VendorType.Where(c => c.VendorType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.VendorType_Name,
                        key = s.VendorType_Name
                    }).Distinct();

                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchRoom
        public List<ItemListViewModel> autoSearchRoom(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_Room.Where(c => c.Room_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Room_Id,
                        key = s.Room_Id
                    }).Distinct();

                    var query2 = db.View_Room.Where(c => c.Room_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Room_Name,
                        key = s.Room_Name
                    }).Distinct();

                    var query3 = db.View_Room.Where(c => c.Warehouse_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Warehouse_Name,
                        key = s.Warehouse_Name
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchWareHouse
        public List<ItemListViewModel> autoSearchWareHouse(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_Warehouse.Where(c => c.Warehouse_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Warehouse_Id,
                        key = s.Warehouse_Id
                    }).Distinct();

                    var query2 = db.MS_Warehouse.Where(c => c.Warehouse_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Warehouse_Name,
                        key = s.Warehouse_Name
                    }).Distinct();

                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchFacility
        public List<ItemListViewModel> autoSearchFacility(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_Facility.Where(c => c.Facility_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Facility_Id,
                        key = s.Facility_Id
                    }).Distinct();

                    var query2 = db.MS_Facility.Where(c => c.Facility_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Facility_Name,
                        key = s.Facility_Name
                    }).Distinct();

                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchWHOwner
        public List<ItemListViewModel> autoSearchWHOwner(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_WHOwner.Where(c => c.WHOwner_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.WHOwner_Id,
                        key = s.WHOwner_Id
                    }).Distinct();

                    var query2 = db.View_WHOwner.Where(c => c.WHOwner_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.WHOwner_Name,
                        key = s.WHOwner_Name
                    }).Distinct();

                    var query3 = db.View_WHOwner.Where(c => c.WHOwnerType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.WHOwnerType_Name,
                        key = s.WHOwnerType_Name
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchWHOwnerType
        public List<ItemListViewModel> autoSearchWHOwnerType(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_WHOwner.Where(c => c.WHOwnerType_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.WHOwnerType_Id,
                        key = s.WHOwnerType_Id
                    }).Distinct();

                    var query2 = db.View_WHOwner.Where(c => c.WHOwnerType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.WHOwnerType_Name,
                        key = s.WHOwnerType_Name
                    }).Distinct();

                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchOwner
        public List<ItemListViewModel> autoSearchOwner(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_Owner.Where(c => c.Owner_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Owner_Id,
                        key = s.Owner_Id
                    }).Distinct();

                    var query2 = db.MS_Owner.Where(c => c.Owner_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Owner_Name,
                        key = s.Owner_Name
                    }).Distinct();


                    var query3 = db.MS_Owner.Where(c => c.OwnerType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.OwnerType_Name,
                        key = s.OwnerType_Name
                    }).Distinct();

                    var query4 = db.MS_Owner.Where(c => c.Ref_No2.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Ref_No2,
                        key = s.Ref_No2
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3).Union(query4);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchOwnerType
        public List<ItemListViewModel> autoSearchOwnerType(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_OwnerType.Where(c => c.OwnerType_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.OwnerType_Id,
                        key = s.OwnerType_Id
                    }).Distinct();

                    var query2 = db.MS_OwnerType.Where(c => c.OwnerType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.OwnerType_Name,
                        key = s.OwnerType_Name
                    }).Distinct();

                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchOwnerSoldTo
        public List<ItemListViewModel> autoSearchOwnerSoldTo(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_OwnerSoldTo.Where(c => c.OwnerSoldTo_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.OwnerSoldTo_Id,
                        key = s.OwnerSoldTo_Id
                    }).Distinct();

                    var query2 = db.View_OwnerSoldTo.Where(c => c.Owner_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Owner_Id,
                        key = s.Owner_Id
                    }).Distinct();

                    var query3 = db.View_OwnerSoldTo.Where(c => c.Owner_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Owner_Name,
                        key = s.Owner_Name
                    }).Distinct();

                    var query4 = db.View_OwnerSoldTo.Where(c => c.SoldTo_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.SoldTo_Id,
                        key = s.SoldTo_Id
                    }).Distinct();

                    var query5 = db.View_OwnerSoldTo.Where(c => c.SoldTo_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.SoldTo_Name,
                        key = s.SoldTo_Name
                    }).Distinct();


                    var query = query1.Union(query2).Union(query3).Union(query4).Union(query5);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSoldToId
        public List<ItemListViewModel> autoSoldToId(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_SoldTo.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key.ToString()))
                    {
                        query = query.Where(c => c.SoldTo_Id.Contains(data.key));
                    }

                    var result = query.Select(c => new { c.SoldTo_Index, c.SoldTo_Id, c.SoldTo_Name }).Distinct().Take(10).ToList();

                    var items = new List<ItemListViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.SoldTo_Index,
                            id = item.SoldTo_Name,
                            name = item.SoldTo_Id
                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoSearchZone
        public List<ItemListViewModel> autoSearchZone(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_Zone.Where(c => c.Zone_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Zone_Id,
                        key = s.Zone_Id
                    }).Distinct();

                    var query2 = db.MS_Zone.Where(c => c.Zone_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Zone_Name,
                        key = s.Zone_Name
                    }).Distinct();

                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchZoneLocation
        public List<ItemListViewModel> autoSearchZoneLocation(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_ZoneLocation.Where(c => c.ZoneLocation_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ZoneLocation_Id,
                        key = s.ZoneLocation_Id
                    }).Distinct();

                    var query2 = db.View_ZoneLocation.Where(c => c.Zone_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Zone_Id,
                        key = s.Zone_Id
                    }).Distinct();

                    var query3 = db.View_ZoneLocation.Where(c => c.Zone_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Zone_Name,
                        key = s.Zone_Name
                    }).Distinct();

                    var query4 = db.View_ZoneLocation.Where(c => c.Location_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Location_Id,
                        key = s.Location_Id
                    }).Distinct();

                    var query5 = db.View_ZoneLocation.Where(c => c.Location_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Location_Name,
                        key = s.Location_Name
                    }).Distinct();
                    var query = query1.Union(query2).Union(query3).Union(query4).Union(query5);
                    items = query.Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoZone
        public List<ItemListViewModel> autoZone(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {


                    var query = context.MS_Zone.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Zone_Name.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Zone_Index, c.Zone_Id, c.Zone_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Zone_Index,
                            id = item.Zone_Id,
                            name = item.Zone_Name,

                        };

                        items.Add(resultItem);
                    }

                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoZoneId
        public List<ItemListViewModel> autoZoneId(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {


                    var query = context.MS_Zone.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Zone_Id.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Zone_Index, c.Zone_Id, c.Zone_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Zone_Index,
                            id = item.Zone_Name,
                            name = item.Zone_Id,

                        };

                        items.Add(resultItem);
                    }

                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoLocationId
        public List<ItemListViewModel> autoLocationId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_Location.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Location_Id.Contains(data.key));
                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Location_Index, c.Location_Id, c.Location_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Location_Index,
                            id = item.Location_Name,
                            name = item.Location_Id,
                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoSearchFacilityType

        public List<ItemListViewModel> autoSearchFacilityType(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_FacilityType.Where(c => c.FacilityType_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.FacilityType_Id,
                        key = s.FacilityType_Id
                    }).Distinct();

                    var query2 = db.MS_FacilityType.Where(c => c.FacilityType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.FacilityType_Name,
                        key = s.FacilityType_Name
                    }).Distinct();

                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoVendorTypeAndVendorTypeId
        public List<ItemListViewModel> autoVendorTypeAndVendorTypeId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_VendorType.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.VendorType_Id.Contains(data.key)
                                                || c.VendorType_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.VendorType_Index, c.VendorType_Id, c.VendorType_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.VendorType_Index,
                            id = item.VendorType_Id,
                            name = item.VendorType_Id + " - " + item.VendorType_Name,
                            key = item.VendorType_Id + " - " + item.VendorType_Name,
                            value1 = item.VendorType_Name
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoOwnerTypeAndOwnerTypeId
        public List<ItemListViewModel> autoOwnerTypeAndOwnerTypeId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_OwnerType.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.OwnerType_Id.Contains(data.key)
                                                || c.OwnerType_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.OwnerType_Index, c.OwnerType_Id, c.OwnerType_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.OwnerType_Index,
                            id = item.OwnerType_Id,
                            name = item.OwnerType_Id + " - " + item.OwnerType_Name,
                            value1 = item.OwnerType_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoOwnerAndOwnerId
        public List<ItemListViewModel> autoOwnerAndOwnerId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Owner.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Owner_Id.Contains(data.key)
                                                || c.Owner_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Owner_Index, c.Owner_Id, c.Owner_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Owner_Index,
                            id = item.Owner_Id,
                            name = item.Owner_Id + " - " + item.Owner_Name,
                            key = item.Owner_Id + " - " + item.Owner_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoVendorAndVendorId
        public List<ItemListViewModel> autoVendorAndVendorId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Vendor.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Vendor_Id.Contains(data.key)
                                                || c.Vendor_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Vendor_Index, c.Vendor_Id, c.Vendor_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Vendor_Index,
                            id = item.Vendor_Id,
                            name = item.Vendor_Id + " - " + item.Vendor_Name,
                            key = item.Vendor_Id + " - " + item.Vendor_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoSoldToAndSoldToId
        public List<ItemListViewModel> autoSoldToAndSoldToId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_SoldTo.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.SoldTo_Id.Contains(data.key)
                                                || c.SoldTo_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.SoldTo_Index, c.SoldTo_Id, c.SoldTo_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.SoldTo_Index,
                            id = item.SoldTo_Id,
                            name = item.SoldTo_Id + " - " + item.SoldTo_Name,
                            key = item.SoldTo_Id + " - " + item.SoldTo_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoProductCategoryAndProductCategoryId
        public List<ItemListViewModel> autoProductCategoryAndProductCategoryId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_ProductCategory.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductCategory_Id.Contains(data.key)
                                                || c.ProductCategory_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.ProductCategory_Index, c.ProductCategory_Id, c.ProductCategory_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProductCategory_Index,
                            id = item.ProductCategory_Id,
                            name = item.ProductCategory_Id + " - " + item.ProductCategory_Name,
                            key = item.ProductCategory_Id + " - " + item.ProductCategory_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoProductTypeAndProductTypeId
        public List<ItemListViewModel> autoProductTypeAndProductTypeId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_ProductType.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductType_Id.Contains(data.key)
                                                || c.ProductType_Name.Contains(data.key));

                    }

                    var configwhere = new AppSettingConfig().GetUrl("configwhereCategory");
                    if (configwhere == "0")
                    {
                        if (!string.IsNullOrEmpty(data.key2))
                        {
                            query = query.Where(c => c.ProductCategory_Index == Guid.Parse(data.key2));
                        }
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.ProductType_Index, c.ProductType_Id, c.ProductType_Name, c.ProductCategory_Index }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProductType_Index,
                            id = item.ProductType_Id,
                            name = item.ProductType_Id + " - " + item.ProductType_Name,
                            key = item.ProductType_Id + " - " + item.ProductType_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoProductSubTypeAndProductSubTypeId
        public List<ItemListViewModel> autoProductSubTypeAndProductSubTypeId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_ProductSubType.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductSubType_Id.Contains(data.key)
                                                || c.ProductSubType_Name.Contains(data.key));

                    }
                    if (!string.IsNullOrEmpty(data.key2))
                    {
                        query = query.Where(c => c.ProductType_Index == Guid.Parse(data.key2));
                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.ProductSubType_Index, c.ProductSubType_Id, c.ProductSubType_Name, c.ProductType_Index }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProductSubType_Index,
                            id = item.ProductSubType_Id,
                            name = item.ProductSubType_Id + " - " + item.ProductSubType_Name,
                            key = item.ProductSubType_Id + " - " + item.ProductSubType_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoProductAndProductId
        public List<ItemListViewModel> autoProductAndProductId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Product.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Product_Id.Contains(data.key)
                                                || c.Product_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Product_Index, c.Product_Id, c.Product_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Product_Index,
                            id = item.Product_Id,
                            name = item.Product_Id + " - " + item.Product_Name,
                            key = item.Product_Id + " - " + item.Product_Name,
                            value1 = item.Product_Name,

                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoProductConversionAndProductConversionId
        public List<ItemListViewModel> autoProductConversionAndProductConversionId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_ProductConversion.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductConversion_Id.Contains(data.key)
                                                || c.ProductConversion_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.ProductConversion_Index, c.ProductConversion_Id, c.ProductConversion_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProductConversion_Index,
                            id = item.ProductConversion_Id,
                            name = item.ProductConversion_Id + " - " + item.ProductConversion_Name,
                            key = item.ProductConversion_Id + " - " + item.ProductConversion_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoWarehouseAndWarehouseId
        public List<ItemListViewModel> autoWarehouseAndWarehouseId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Warehouse.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Warehouse_Id.Contains(data.key)
                                                || c.Warehouse_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Warehouse_Index, c.Warehouse_Id, c.Warehouse_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Warehouse_Index,
                            id = item.Warehouse_Id,
                            name = item.Warehouse_Id + " - " + item.Warehouse_Name,
                            key = item.Warehouse_Id + " - " + item.Warehouse_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoZoneAndZoneId
        public List<ItemListViewModel> autoZoneAndZoneId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Zone.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Zone_Id.Contains(data.key)
                                                || c.Zone_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Zone_Index, c.Zone_Id, c.Zone_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Zone_Index,
                            id = item.Zone_Id,
                            name = item.Zone_Id + " - " + item.Zone_Name,
                            key = item.Zone_Id + " - " + item.Zone_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoLocationAndLocationId
        public List<ItemListViewModel> autoLocationAndLocationId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Location.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Location_Id.Contains(data.key)
                                                || c.Location_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Location_Index, c.Location_Id, c.Location_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Location_Index,
                            id = item.Location_Id,
                            name = item.Location_Id + " - " + item.Location_Name,
                            key = item.Location_Id + " - " + item.Location_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoWHOwnerTypeAndWHOwnerTypeId
        public List<ItemListViewModel> autoWHOwnerTypeAndWHOwnerTypeId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_WHOwnerType.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.WHOwnerType_Id.Contains(data.key)
                                                || c.WHOwnerType_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.WHOwnerType_Index, c.WHOwnerType_Id, c.WHOwnerType_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.WHOwnerType_Index,
                            id = item.WHOwnerType_Id,
                            name = item.WHOwnerType_Id + " - " + item.WHOwnerType_Name,
                            key = item.WHOwnerType_Id + " - " + item.WHOwnerType_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoFacilityTypeAndFacilityTypeId
        public List<ItemListViewModel> autoFacilityTypeAndFacilityTypeId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_FacilityType.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.FacilityType_Id.Contains(data.key)
                                                || c.FacilityType_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.FacilityType_Index, c.FacilityType_Id, c.FacilityType_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.FacilityType_Index,
                            id = item.FacilityType_Id,
                            name = item.FacilityType_Id + " - " + item.FacilityType_Name,
                            key = item.FacilityType_Id + " - " + item.FacilityType_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoSearchRuleConditionField

        public List<ItemListViewModel> autoSearchRuleConditionField(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_RuleConditionField.Where(c => c.RuleConditionField_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleConditionField_Name,
                        key = s.RuleConditionField_Name
                    }).Distinct();

                    var query2 = db.View_RuleConditionField.Where(c => c.Process_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Process_Id,
                        key = s.Process_Id
                    }).Distinct();

                    var query3 = db.View_RuleConditionField.Where(c => c.Process_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Process_Name,
                        key = s.Process_Name
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchRuleConditionOperation

        public List<ItemListViewModel> autoSearchRuleConditionOperation(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_RuleConditionOperation.Where(c => c.RuleConditionField_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleConditionField_Name,
                        key = s.RuleConditionField_Name
                    }).Distinct();

                    var query2 = db.View_RuleConditionOperation.Where(c => c.RuleConditionOperationType.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleConditionOperationType,
                        key = s.RuleConditionOperationType
                    }).Distinct();

                    var query3 = db.View_RuleConditionOperation.Where(c => c.RuleConditionOperation.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleConditionOperation,
                        key = s.RuleConditionOperation
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoProcessAndProcessId
        public List<ItemListViewModel> autoProcessAndProcessId(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.sy_Process.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Process_Id.Contains(data.key)
                                                || c.Process_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Process_Index, c.Process_Id, c.Process_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Process_Index,
                            id = item.Process_Id,
                            name = item.Process_Id + " - " + item.Process_Name,
                            key = item.Process_Id + " - " + item.Process_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoSearchProcess

        public List<ItemListViewModel> autoSearchProcess(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.sy_Process.Where(c => c.Process_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Process_Id,
                        key = s.Process_Id
                    }).Distinct();

                    var query2 = db.sy_Process.Where(c => c.Process_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Process_Name,
                        key = s.Process_Name
                    }).Distinct();

                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchZonePutaway

        public List<ItemListViewModel> autoSearchZonePutaway(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_Zoneputaway.Where(c => c.Zoneputaway_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Zoneputaway_Id,
                        key = s.Zoneputaway_Id
                    }).Distinct();

                    var query2 = db.MS_Zoneputaway.Where(c => c.Zoneputaway_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Zoneputaway_Name,
                        key = s.Zoneputaway_Name
                    }).Distinct();

                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchRule
        public List<ItemListViewModel> autoSearchRule(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_Rule.Where(c => c.Rule_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Rule_Name,
                        key = s.Rule_Name
                    }).Distinct();

                    var query2 = db.View_Rule.Where(c => c.Rule_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Rule_Id,
                        key = s.Rule_Id
                    }).Distinct();

                    var query3 = db.View_Rule.Where(c => c.Process_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Process_Id,
                        key = s.Process_Id
                    }).Distinct();

                    var query4 = db.View_Rule.Where(c => c.Process_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Process_Name,
                        key = s.Process_Name
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3).Union(query4);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchWave
        public List<ItemListViewModel> autoSearchWave(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_Wave.Where(c => c.Wave_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Wave_Id,
                        key = s.Wave_Id
                    }).Distinct();

                    var query2 = db.MS_Wave.Where(c => c.Wave_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Wave_Name,
                        key = s.Wave_Name
                    }).Distinct();

                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchLocationZoneputaway

        public List<ItemListViewModel> autoSearchLocationZoneputaway(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_LocationZoneputaway.Where(c => c.LocationZoneputaway_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.LocationZoneputaway_Id,
                        key = s.LocationZoneputaway_Id
                    }).Distinct();

                    var query2 = db.View_LocationZoneputaway.Where(c => c.Zoneputaway_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Zoneputaway_Name,
                        key = s.Zoneputaway_Name
                    }).Distinct();

                    var query3 = db.View_LocationZoneputaway.Where(c => c.Location_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Location_Name,
                        key = s.Location_Name
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoZoneputaway
        public List<ItemListViewModel> autoZoneputaway(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Zoneputaway.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Zoneputaway_Id.Contains(data.key)
                                                || c.Zoneputaway_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Zoneputaway_Index, c.Zoneputaway_Id, c.Zoneputaway_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.Zoneputaway_Index,
                            id = item.Zoneputaway_Id,
                            name = item.Zoneputaway_Id + " - " + item.Zoneputaway_Name,
                            key = item.Zoneputaway_Id + " - " + item.Zoneputaway_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoSearchRuleputawayConditionField
        public List<ItemListViewModel> autoSearchRuleputawayConditionField(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_RuleputawayConditionField.Where(c => c.RuleputawayConditionField_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleputawayConditionField_Id,
                        key = s.RuleputawayConditionField_Id
                    }).Distinct();

                    var query2 = db.MS_RuleputawayConditionField.Where(c => c.RuleputawayConditionField_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleputawayConditionField_Name,
                        key = s.RuleputawayConditionField_Name
                    }).Distinct();

                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchRuleputawayCondition
        public List<ItemListViewModel> autoSearchRuleputawayCondition(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {

                    var query1 = db.View_RuleputawayCondition.Where(c => c.RuleputawayCondition_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleputawayCondition_Id,
                        key = s.RuleputawayCondition_Id
                    }).Distinct();

                    var query2 = db.View_RuleputawayCondition.Where(c => c.RuleputawayCondition_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleputawayCondition_Name,
                        key = s.RuleputawayCondition_Name
                    }).Distinct();

                    var query = query1.Union(query2);


                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoRuleputawayConditionField
        public List<ItemListViewModel> autoRuleputawayConditionField(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_RuleputawayConditionField.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.RuleputawayConditionField_Id.Contains(data.key)
                                                || c.RuleputawayConditionField_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.RuleputawayConditionField_Index, c.RuleputawayConditionField_Id, c.RuleputawayConditionField_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.RuleputawayConditionField_Index,
                            id = item.RuleputawayConditionField_Id,
                            name = item.RuleputawayConditionField_Id + " - " + item.RuleputawayConditionField_Name,
                            key = item.RuleputawayConditionField_Id + " - " + item.RuleputawayConditionField_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoItemStatus
        public List<ItemListViewModel> autoItemStatus(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_ItemStatus.AsQueryable();

                    if (data.key == "-")
                    {

                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ItemStatus_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ItemStatus_Index, c.ItemStatus_Id, c.ItemStatus_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ItemStatus_Index,
                            id = item.ItemStatus_Id,
                            name = item.ItemStatus_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoRuleputawayCondition
        public List<ItemListViewModel> autoRuleputawayCondition(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_RuleputawayCondition.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.RuleputawayCondition_Id.Contains(data.key)
                                                || c.RuleputawayCondition_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.RuleputawayCondition_Index, c.RuleputawayCondition_Id, c.RuleputawayCondition_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.RuleputawayCondition_Index,
                            id = item.RuleputawayCondition_Id,
                            name = item.RuleputawayCondition_Id + " - " + item.RuleputawayCondition_Name,
                            key = item.RuleputawayCondition_Id + " - " + item.RuleputawayCondition_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoDocumentTypeItemStatusSearch
        public List<ItemListViewModel> autoDocumentTypeItemStatusSearch(ItemListViewModel data)


        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {


                    var query1 = db.MS_DocumentType.Where(c => c.DocumentType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.DocumentType_Name,
                        key = s.DocumentType_Name
                    }).Distinct();

                    var query2 = db.MS_ItemStatus.Where(c => c.ItemStatus_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ItemStatus_Name,
                        key = s.ItemStatus_Name
                    }).Distinct();

                    var query = query1.Union(query2).Union(query2);


                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }

        #endregion

        #region autoSearchRuleputaway
        public List<ItemListViewModel> autoSearchRuleputaway(ItemListViewModel data)

        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {

                    var query1 = db.MS_Ruleputaway.Where(c => c.Ruleputaway_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Ruleputaway_Id,
                        key = s.Ruleputaway_Id
                    }).Distinct();

                    var query2 = db.MS_Ruleputaway.Where(c => c.Ruleputaway_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Ruleputaway_Name,
                        key = s.Ruleputaway_Name
                    }).Distinct();
                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchUserGroup
        public List<ItemListViewModel> autoSearchUserGroup(ItemListViewModel data)

        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {

                    var query1 = db.MS_UserGroup.Where(c => c.UserGroup_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.UserGroup_Id,
                        key = s.UserGroup_Id
                    }).Distinct();

                    var query2 = db.MS_UserGroup.Where(c => c.UserGroup_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.UserGroup_Name,
                        key = s.UserGroup_Name
                    }).Distinct();
                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion


        #region autoSearchUserGroupMenu
        public List<ItemListViewModel> autoSearchUserGroupMenu(ItemListViewModel data)

        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {

                    var query1 = db.MS_UserGroupMenu.Where(c => c.UserGroupMenu_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.UserGroupMenu_Id,
                        key = s.UserGroupMenu_Id
                    }).Distinct();

                    var query2 = db.MS_UserGroup.Where(c => c.UserGroup_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.UserGroup_Name,
                        key = s.UserGroup_Name
                    }).Distinct();

                    var query3 = db.sy_Menu.Where(c => c.Menu_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Menu_Name,
                        key = s.Menu_Name
                    }).Distinct();
                    var query = query1.Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchUserGroupZone
        public List<ItemListViewModel> autoSearchUserGroupZone(ItemListViewModel data)

        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {

                    var query1 = db.MS_UserGroupZone.Where(c => c.UserGroupZone_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.UserGroupZone_Id,
                        key = s.UserGroupZone_Id
                    }).Distinct();

                    var query2 = db.MS_UserGroup.Where(c => c.UserGroup_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.UserGroup_Name,
                        key = s.UserGroup_Name
                    }).Distinct();

                    var query3 = db.MS_Zone.Where(c => c.Zone_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Zone_Name,
                        key = s.Zone_Name
                    }).Distinct();
                    var query = query1.Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoMovementType
        public List<ItemListViewModel> autoMovementType(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {


                    var query = context.ms_MovementType.AsQueryable();
                    if (data.key == "-")
                    {

                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.MovementType_Name.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.MovementType_Index, c.MovementType_Id, c.MovementType_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.MovementType_Index,
                            id = item.MovementType_Id,
                            name = item.MovementType_Name,

                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoUserName
        public List<ItemListViewModel> autoUserName(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_User.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.User_Name.Contains(data.key) || c.Last_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.User_Index, c.User_Id, c.User_Name, c.First_Name, c.Last_Name, c.Position_Code, c.Position_Name, c.Title }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {

                            index = item.User_Index,
                            name = item.Title + " " + item.First_Name + " " + item.Last_Name,
                            id = item.User_Id,
                            value1 = item.First_Name,
                            value2 = item.Last_Name,
                            value3 = item.User_Name,
                            value4 = item.Position_Code,
                            value5 = item.User_Index,
                            value6 = item.User_Id,
                            value7 = item.Position_Name
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region AutoProductBarcodeConversion
        public List<ItemListViewModel> autoProductBarcodeConversion(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    //var query = context.MS_ProductConversion.AsQueryable();
                    var query = context.MS_ProductConversion.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductConversion_Name.Contains(data.key));
                    }
                    if (!string.IsNullOrEmpty(data.key4))
                    {
                        query = query.Where(c => c.Product_Index == Guid.Parse(data.key4));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ProductConversion_Name, c.ProductConversion_Index, c.ProductConversion_Id, c.Product_Index }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.ProductConversion_Index,
                            id = item.ProductConversion_Id,
                            name = item.ProductConversion_Id + " - " + item.ProductConversion_Name
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region autoCostCenter
        public List<ItemListViewModel> autoCostCenter(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {


                    var query = context.ms_CostCenter.AsQueryable();
                    if (data.key == "-")
                    {

                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.CostCenter_Id.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.CostCenter_Id , c.CostCenter_Index , c.CostCenter_Name}).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.CostCenter_Index,
                            id = item.CostCenter_Id,
                            name = item.CostCenter_Name


                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region autoSearchDocumentType
        public List<ItemListViewModel> autoSearchDocumentType(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {

                    var query1 = db.MS_DocumentType.Where(c => c.DocumentType_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.DocumentType_Id,
                        key = s.DocumentType_Id
                    }).Distinct();

                    var query2 = db.MS_DocumentType.Where(c => c.DocumentType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.DocumentType_Name,
                        key = s.DocumentType_Name
                    }).Distinct();
                    var query = query1.Union(query2);
                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }

        #endregion
        #region autoSearchUser
        public List<ItemListViewModel> autoSearchUser(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_User.Where(c => c.User_Name.Contains(data.key) && c.IsActive == 1).Select(s => new ItemListViewModel
                    {
                        name = s.User_Name,
                        key = s.User_Name
                    }).Distinct();

                    var query2 = db.View_User.Where(c => c.User_Id.Contains(data.key) && c.IsActive == 1).Select(s => new ItemListViewModel
                    {
                        name = s.User_Id,
                        key = s.User_Id
                    }).Distinct();

                    var query3 = db.View_User.Where(c => c.UserGroup_Name.Contains(data.key) && c.IsActive == 1).Select(s => new ItemListViewModel
                    {
                        name = s.UserGroup_Name,
                        key = s.UserGroup_Name
                    }).Distinct();

                    var query4 = db.View_User.Where(c => c.First_Name.Contains(data.key) || c.Last_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.First_Name + " " + s.Last_Name ,
                        key = s.First_Name + " " + s.Last_Name
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3).Union(query4);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion


        #region autoSearchRuleCondition
        public List<ItemListViewModel> autoSearchRuleCondition(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_RuleCondition.Where(c => c.RuleCondition_Param.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleCondition_Param,
                        key = s.RuleCondition_Param
                    }).Distinct();

                    var query2 = db.View_RuleCondition.Where(c => c.Rule_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Rule_Id,
                        key = s.Rule_Id
                    }).Distinct();

                    var query3 = db.View_RuleCondition.Where(c => c.Rule_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Rule_Name,
                        key = s.Rule_Name
                    }).Distinct();

                    var query4 = db.View_RuleCondition.Where(c => c.RuleConditionField_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleConditionField_Name,
                        key = s.RuleConditionField_Name
                    }).Distinct();

                    var query5 = db.View_RuleCondition.Where(c => c.RuleConditionOperationType.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleConditionOperationType,
                        key = s.RuleConditionOperationType
                    }).Distinct();

                    var query = query1.Union(query2).Union(query3).Union(query4).Union(query5);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoRule
        public List<ItemListViewModel> autoRule(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Rule.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Rule_Id.Contains(data.key)
                                                    || c.Rule_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Rule_Index, c.Rule_Id, c.Rule_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.Rule_Index,
                            id = item.Rule_Id,
                            name = item.Rule_Id + " - " + item.Rule_Name,
                            key = item.Rule_Id + " - " + item.Rule_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoSearchProductConversionV2
        public List<ItemListViewModel> autoSearchProductConversionV2(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var queryPC = db.MS_ProductConversion.Where(c => c.Product_Index.ToString() == data.key4);

                    var query1 = queryPC.Where(c => c.ProductConversion_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductConversion_Id,
                        key = s.ProductConversion_Id,
                    }).Distinct();

                    var query2 = queryPC.Where(c => c.ProductConversion_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductConversion_Name,
                        key = s.ProductConversion_Name
                    }).Distinct();

                    var query = query1.Union(query2);



                    items = query.OrderBy(c => c.name).Take(10).ToList();
                    foreach (ItemListViewModel item in items)
                    {

                        item.key4 = data.key4;

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region autoSearchOwnerV2
        public List<ItemListViewModel> autoSearchOwnerV2(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var queryPO = db.MS_ProductOwner.Where(c => c.Product_Index.ToString() == data.key5);

                    var query1 = queryPO.Where(c => c.Owner_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Owner_Id,
                        key = s.Owner_Id
                    }).Distinct();

                    var query2 = queryPO.Where(c => c.Owner_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Owner_Name,
                        key = s.Owner_Name
                    }).Distinct();


                    var query = query1.Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                    foreach (ItemListViewModel item in items)
                    {

                        item.key5 = data.key5;

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion



        #region autoProductAssembly
        public List<ItemListViewModel> autoProductAssembly(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_Product.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Product_Id.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Product_Index, c.Product_Id, c.Product_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Product_Index,
                            id = item.Product_Id,
                            name = item.Product_Id,
                            value1 = item.Product_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region autoRoomWH
        public List<ItemListViewModel> autoRoomWH(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Room.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0 && c.Warehouse_Index == data.value5);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Room_Id.Contains(data.key)
                                                || c.Room_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Room_Index, c.Room_Id, c.Room_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Room_Index,
                            id = item.Room_Id,
                            name = item.Room_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region autoServiceCharge
        public List<ItemListViewModel> autoServiceCharge(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.View_ServiceCharge.Where(c => c.IsActive == 1 && c.IsDelete == 0);
                    if (data.key == "-")
                    {

                    }
                    if (data.index != new Guid("00000000-0000-0000-0000-000000000000".ToString()) && data.Sub_index != new Guid("00000000-0000-0000-0000-000000000000".ToString()))
                    {
                        query = query.Where(c => c.DEFAULT_Process_Index == data.index && c.Owner_Index == data.Sub_index);
                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ServiceCharge_Id.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.ServiceCharge_Index, c.ServiceCharge_Id, c.ServiceCharge_Name, c.Rate, c.Minimumrate }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ServiceCharge_Index,
                            id = item.ServiceCharge_Id,
                            name = item.ServiceCharge_Name,
                            key5 = item.Rate.ToString(),
                            key4 = item.Minimumrate.ToString()
                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoCostCenterFull_Name_Id
        public List<ItemListViewModel> autoCostCenterFull_Name_Id(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {


                    var query = context.ms_CostCenter.AsQueryable();
                    query = query.Where(c => c.IsActive == 1 && c.IsDelete == 0);
                    if (data.key != "-")
                    {
                        query = query.Where(c => c.CostCenter_Id.Contains(data.key) || c.CostCenter_Name.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.CostCenter_Index,
                            id = item.CostCenter_Id,
                            name = item.CostCenter_Id + "-" + item.CostCenter_Name,
                            value1 = item.CostCenter_Name


                        };

                        items.Add(resultItem);
                    }



                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoLocationAndLocationIdPickFace
        public List<ItemListViewModel> autoLocationAndLocationIdPickFace(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Location.Where(c => (c.IsActive == 1 || c.IsActive == 0) && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Location_Id.Contains(data.key)
                                                || c.Location_Name.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Location_Index, c.Location_Id, c.Location_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Location_Index,
                            id = item.Location_Id,
                            name = item.Location_Id + " - " + item.Location_Name,
                            key = item.Location_Id + " - " + item.Location_Name,
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoSearchProductConversionV3
        public List<ItemListViewModel> autoSearchProductConversionV3(ItemListViewModel data)
        {
            try
            {

                {
                    var query = db.MS_ProductConversion.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductConversion_Name.Contains(data.key));

                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ProductConversion_Name }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        { 
                            name = item.ProductConversion_Name
                        };
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoSloc
        public List<ItemListViewModel> autoSloc(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.ms_Storage_Loc.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.StorageLoc_Id.Contains(data.key)
                                                || c.StorageLoc_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.StorageLoc_Id, c.StorageLoc_Name, }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            id = item.StorageLoc_Id,
                            name = item.StorageLoc_Name,

                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AutoSearchDocument
        public List<ItemListViewModel> AutoSearchDocument(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.View_AutoSearchDocument.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key.ToString()) && data.key != "-")
                    {
                        query = query.Where(c => c.DOC_LINK.Contains(data.key));
                    }
                    


                    var result = query.Select(c => new { c.DOC_LINK }).Distinct().Take(10).ToList();

                    var items = new List<ItemListViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            key = item.DOC_LINK,
                            id = item.DOC_LINK,
                            name = item.DOC_LINK,
                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
  
    }
}

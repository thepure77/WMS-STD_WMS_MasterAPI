using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.Product;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


using Newtonsoft.Json;

namespace MasterDataBusiness
{
    public class ConfigSKUService
    {
        #region ConfigSKUService
        private MasterDataDbContext db;

        public ConfigSKUService()
        {
            db = new MasterDataDbContext();
        }

        public ConfigSKUService(MasterDataDbContext db)
        {
            this.db = db;
        }
        #endregion

        #region filter
        public ConfigSKUViewModel filter()
        {
            ConfigSKUViewModel result = new ConfigSKUViewModel();
            try
            {
                List<ms_Config_zone> config_Zones = db.ms_Config_zone.Where(c=> c.IsActive == 1 && c.IsDelete == 0).ToList();
                if (config_Zones.Count <= 0)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = "ไม่พบการจัดโซน กรุณาเพิ่มโซน หรือติดต่อ Admin";
                    return result;
                }
                //var locationZoneputaways = db.View_LocationZoneputaway.GroupBy(c => new { c.Zoneputaway_Id, c.Zoneputaway_Index }).Select(c => new { c.Key.Zoneputaway_Id, c.Key.Zoneputaway_Index, location_empty = c.Count() }).ToList();

                foreach (var item in config_Zones)
                {
                    Zone_type zone = new Zone_type
                    {
                        Config_zone_Index = item.Config_zone_Index,
                        Config_zone_Id = item.Config_zone_Id,
                        Config_zone_Name = item.Config_zone_Name,
                        Config_zone_SecondName = item.Config_zone_SecondName,
                        Remark = item.Remark,
                        Ref_No1 = item.Ref_No1,
                        Ref_No2 = item.Ref_No2,
                        Ref_No3 = item.Ref_No3,
                        Ref_No4 = item.Ref_No4,
                        Ref_No5 = item.Ref_No5,
                        UDF_1 = item.UDF_1,
                        UDF_2 = item.UDF_2,
                        UDF_3 = item.UDF_3,
                        UDF_4 = item.UDF_4,
                        UDF_5 = item.UDF_5,
                        IsActive = item.IsActive,
                        IsDelete = item.IsDelete,
                        IsSystem = item.IsSystem,
                        Status_Id = item.Status_Id,
                        Create_By = item.Create_By,
                        Create_Date = item.Create_Date,
                        Cancel_By = item.Cancel_By,
                        Cancel_Date = item.Cancel_Date,
                        Update_By = item.Update_By,
                        Update_Date = item.Update_Date

                    };
                    result.ZonetypeList.Add(zone);
                }

                return result;
            }

            catch (Exception ex)
            {
                result.ResultIsUse = false;
                result.ResultMsg = ex.Message;
                return result;
            }
        }
        #endregion

        #region filter
        public ConfigSKUViewModel filter_Zonetype(SearchConfigSKUViewModel data)
        {
            ConfigSKUViewModel result = new ConfigSKUViewModel();
            try
            {
                ms_Config_zone config_Zones = db.ms_Config_zone.FirstOrDefault(c => c.IsActive == 1 && c.IsDelete == 0 && c.Config_zone_Index == data.Config_zone_Index);
                if (config_Zones == null)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = "ไม่พบการจัดโซนที่ค้นหา กรุณาเลือกโซนใหม่ หรือติดต่อ Admin";
                    return result;
                }

                List<View_LocationZoneputaway> locationZoneputaways = db.View_LocationZoneputaway.ToList();
                List<View_LocationZoneputaway> Ref_No1 = new List<View_LocationZoneputaway>();
                List<View_LocationZoneputaway> Ref_No2 = new List<View_LocationZoneputaway>();
                List<View_LocationZoneputaway> Ref_No3 = new List<View_LocationZoneputaway>();
                List<View_LocationZoneputaway> Ref_No4 = new List<View_LocationZoneputaway>();
                List<View_LocationZoneputaway> Ref_No5 = new List<View_LocationZoneputaway>();

                if (!string.IsNullOrEmpty(config_Zones.Ref_No1))
                {
                    Ref_No1 = locationZoneputaways.Where(c => c.Zoneputaway_Id.StartsWith(config_Zones.Ref_No1)).ToList();
                }
                if (!string.IsNullOrEmpty(config_Zones.Ref_No2))
                {
                    Ref_No2 = locationZoneputaways.Where(c => c.Zoneputaway_Id.StartsWith(config_Zones.Ref_No2)).ToList();
                }
                if (!string.IsNullOrEmpty(config_Zones.Ref_No3))
                {
                    Ref_No3 = locationZoneputaways.Where(c => c.Zoneputaway_Id.StartsWith(config_Zones.Ref_No3)).ToList();
                }
                if (!string.IsNullOrEmpty(config_Zones.Ref_No4))
                {
                    Ref_No4 = locationZoneputaways.Where(c => c.Zoneputaway_Id.StartsWith(config_Zones.Ref_No4)).ToList();
                }
                if (!string.IsNullOrEmpty(config_Zones.Ref_No5))
                {
                    Ref_No5 = locationZoneputaways.Where(c => c.Zoneputaway_Id.StartsWith(config_Zones.Ref_No5)).ToList();
                }
                List<View_LocationZoneputaway> result_union = Ref_No1.Union(Ref_No2).Union(Ref_No3).Union(Ref_No4).Union(Ref_No5).ToList();

                var Zone_model = result_union.GroupBy(c => new { c.Zoneputaway_Id, c.Zoneputaway_Index }).Select(c => new { c.Key.Zoneputaway_Id, c.Key.Zoneputaway_Index, location_empty = c.Count() }).ToList();

                foreach (var item in Zone_model)
                {
                    Zone_type zone = new Zone_type
                    {
            

                    };
                    result.ZonetypeList.Add(zone);
                }

                return result;
            }

            catch (Exception ex)
            {
                result.ResultIsUse = false;
                result.ResultMsg = ex.Message;
                return result;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(ProductViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            String Product_Id = "";


            try
            {

                var productOld = db.MS_Product.Find(data.product_Index);
                var productConversionOld = db.MS_ProductConversion.Find(data.productConversion_Index);

                var authUser = db.sy_Config.Where(c => c.Config_Key == "Config_User_Update_Product");
                if (authUser.Count() > 0)
                {
                    var splitUser = authUser.FirstOrDefault().Config_Value.Split(',');
                    var user = splitUser.Contains(data.create_By); //Check User update in config
                    if (user != true)
                    {
                        return "Fail_User";
                    }
                }
                

                if (productOld == null)
                {
                    if (!string.IsNullOrEmpty(data.product_Id))
                    {
                        var query = db.MS_Product.FirstOrDefault(c => c.Product_Id == data.product_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.product_Id))
                    {
                        data.product_Id = "Product_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_Product.FirstOrDefault(c => c.Product_Id == data.product_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Product_Id == data.product_Id)
                                {
                                    data.product_Id = "Product_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.product_Id = "Product_Id".genAutonumber();
                    data.productConversion_Id = "ProductConversion_Id".genAutonumber();

                    MS_Product Model = new MS_Product();
                    MS_ProductConversion Model2 = new MS_ProductConversion();

                    Model.Product_Index = Guid.NewGuid();
                    Model.Product_Id = data.product_Id;
                    Model.Product_Name = data.product_Name;
                    Model.Product_SecondName = data.product_SecondName;
                    Model.Product_ThirdName = data.product_ThirdName;
                    Model.ProductCategory_Index = data.productCategory_Index;
                    Model.ProductType_Index = data.productType_Index;
                    Model.ProductSubType_Index = data.productSubType_Index;
                    Model.ProductItemLife_Y = data.productItemLife_Y;
                    Model.ProductItemLife_M = data.productItemLife_M;
                    Model.ProductItemLife_D = data.productItemLife_D;
                    Model.ProductImage_Path = data.productImage_Path;
                    Model.ProductShelfLife_D = data.ProductShelfLife_D;
                    Model.IsLot = Convert.ToInt32(data.isLot);
                    Model.IsExpDate = Convert.ToInt32(data.isExpDate);
                    Model.IsMfgDate = Convert.ToInt32(data.isMfgDate);
                    Model.IsCatchWeight = Convert.ToInt32(data.isCatchWeight);
                    Model.IsPack = 0;
                    Model.IsSerial = Convert.ToInt32(data.isSerial);
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Ref_No1 = data.ref_No1;
                    Model.Ref_No2 = data.ref_No2;
                    Model.Ref_No3 = data.ref_No3;
                    Model.Ref_No4 = data.ref_No4;
                    Model.Ref_No5 = data.ref_No5;
                    Model.Remark = data.remark;
                    Model.UDF_1 = data.udf_1;
                    Model.UDF_2 = null;
                    Model.UDF_3 = null;
                    Model.UDF_4 = null;
                    Model.UDF_5 = null;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;
                    Model.ProductConversion_Index = Guid.NewGuid();
                    Model.ProductConversion_Id = data.productConversion_Id;
                    Model.ProductConversion_Name = data.productConversion_Name;
                    Model.Qty_Per_Tag = data.qty_Per_Tag;

                    Model2.ProductConversion_Index = Model.ProductConversion_Index;
                    Model2.ProductConversion_Id = Model.ProductConversion_Id;
                    Model2.ProductConversion_Name = Model.ProductConversion_Name;
                    Model2.Product_Index = Model.Product_Index;
                    Model2.ProductConversion_Volume = 0;
                    Model2.ProductConversion_Ratio = 1;
                    Model2.ProductConversion_VolumeRatio = 1;
                    Model2.ProductConversion_Weight = 0;
                    Model2.ProductConversion_Width = 0;
                    Model2.ProductConversion_Length = 0;
                    Model2.ProductConversion_Height = 0;
                    Model2.IsActive = Convert.ToInt32(data.isActive);
                    Model2.IsDelete = 0;
                    Model2.IsSystem = 0;
                    Model2.Status_Id = 0;
                    Model2.Create_By = data.create_By;
                    Model2.Create_Date = DateTime.Now;
                    Product_Id = Model.Product_Id;

                    db.MS_Product.Add(Model);
                    db.MS_ProductConversion.Add(Model2);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.product_Id))
                    {
                        if (productOld.Product_Id != "")
                        {
                            data.product_Id = productOld.Product_Id;
                        }
                    }
                    else
                    {
                        if (productOld.Product_Id != data.product_Id)
                        {
                            var query = db.MS_Product.FirstOrDefault(c => c.Product_Id == data.product_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.product_Id = productOld.Product_Id;
                        }
                    }
                    productOld.Product_Id = data.product_Id;
                    productOld.Product_Name = data.product_Name;
                    productOld.Product_SecondName = data.product_SecondName;
                    productOld.Product_ThirdName = data.product_ThirdName;
                    productOld.ProductCategory_Index = data.productCategory_Index;
                    productOld.ProductType_Index = data.productType_Index;
                    productOld.ProductSubType_Index = data.productSubType_Index;
                    productOld.ProductItemLife_Y = data.productItemLife_Y;
                    productOld.ProductItemLife_M = data.productItemLife_M;
                    productOld.ProductItemLife_D = data.productItemLife_D;
                    productOld.ProductShelfLife_D = data.ProductShelfLife_D;
                    productOld.ProductImage_Path = data.productImage_Path;
                    productOld.IsLot = Convert.ToInt32(data.isLot);
                    productOld.IsExpDate = Convert.ToInt32(data.isExpDate);
                    productOld.IsMfgDate = Convert.ToInt32(data.isMfgDate);
                    productOld.IsSerial = Convert.ToInt32(data.isSerial);
                    productOld.IsCatchWeight = Convert.ToInt32(data.isCatchWeight);
                    productOld.IsActive = Convert.ToInt32(data.isActive);
                    productOld.Ref_No1 = data.ref_No1;
                    productOld.Ref_No2 = data.ref_No2;
                    productOld.Ref_No3 = data.ref_No3;
                    productOld.Ref_No4 = data.ref_No4;
                    productOld.Ref_No5 = data.ref_No5;
                    productOld.Remark = data.remark;
                    productOld.UDF_1 = data.udf_1;
                    productOld.UDF_2 = null;
                    productOld.UDF_3 = null;
                    productOld.UDF_4 = null;
                    productOld.UDF_5 = null;
                    productOld.Update_By = data.create_By;
                    productOld.Update_Date = DateTime.Now;

                    productOld.ProductConversion_Name = data.productConversion_Name;
                    productOld.Qty_Per_Tag = data.qty_Per_Tag;
                    productConversionOld.ProductConversion_Name = data.productConversion_Name;

                    Product_Id = productOld.Product_Id;
                }

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SaveProduct", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                //return Product_Id;
                return "Done";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
    }
}

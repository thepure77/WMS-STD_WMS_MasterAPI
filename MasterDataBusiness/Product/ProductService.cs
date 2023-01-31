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
using System.Data.SqlClient;
using System.Linq;
using static MasterDataBusiness.ViewModels.ProductViewModel;

using Newtonsoft.Json;

namespace MasterDataBusiness
{
    public class ProductService
    {
        private MasterDataDbContext db;

        public ProductService()
        {
            db = new MasterDataDbContext();
        }

        public ProductService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public actionResultProductViewModel FilterInClause(string jsonData)
        {
            try
            {
                SearchProductInClauseViewModel model = JsonConvert.DeserializeObject<SearchProductInClauseViewModel>(jsonData);

                var query = db.View_ProductDetailV2.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if ((model?.List_Product_Index?.Count ?? 0) > 0)
                {
                    query = query.Where(c => model.List_Product_Index.Contains(c.Product_Index));
                }

                if ((model?.List_Product_Id?.Count ?? 0) > 0)
                {
                    query = query.Where(c => model.List_Product_Id.Contains(c.Product_Id));
                }

                if ((model?.List_Product_Ref?.Count ?? 0) > 0)
                {
                    query = query.Where(c => model.List_Product_Ref.Contains(c.Ref_No1));
                }

                if ((model?.List_Product_Ref2?.Count ?? 0) > 0)
                {
                    query = query.Where(c => model.List_Product_Ref2.Contains(c.Ref_No2));
                }

                if ((model?.List_ProductType_Index?.Count ?? 0) > 0)
                {
                    query = query.Where(c => model.List_ProductType_Index.Contains(c.ProductType_Index));
                }

                var Item = new List<View_ProductDetailV2>();
                var TotalRow = new List<View_ProductDetailV2>();

                TotalRow = query.ToList();

                if (model.CurrentPage != 0 && model.PerPage != 0)
                {
                    query = query.Skip(((model.CurrentPage - 1) * model.PerPage));
                }

                if (model.PerPage != 0)
                {
                    query = query.Take(model.PerPage);

                }

                Item = query.OrderBy(o => o.Product_Id).ToList();

                var result = new List<SearchProductViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductViewModel()
                    {
                        product_Index = item.Product_Index,
                        product_Id = item.Product_Id,
                        product_Name = item.Product_Name,
                        product_SecondName = item.Product_SecondName,
                        product_ThirdName = item.Product_ThirdName,
                        productCategory_Index = item.ProductCategory_Index,
                        productCategory_Id = item.ProductCategory_Id,
                        productCategory_Name = item.ProductCategory_Name,
                        productType_Index = item.ProductType_Index,
                        productType_Id = item.ProductType_Id,
                        productType_Name = item.ProductType_Name,
                        productSubType_Index = item.ProductSubType_Index,
                        productSubType_Id = item.ProductSubType_Id,
                        productSubType_Name = item.ProductSubType_Name,
                        productConversion_Index = item.ProductConversion_Index,
                        productConversion_Id = item.ProductConversion_Id,
                        productConversion_Name = item.ProductConversion_Name,
                        productItemLife_Y = item.ProductItemLife_Y,
                        productItemLife_M = item.ProductItemLife_M,
                        productItemLife_D = item.ProductItemLife_D,
                        productImage_Path = item.ProductImage_Path,
                        isActive = item.IsActive,

                        ref_1 = item.Ref_No1,
                        ref_2 = item.Ref_No2
                    };

                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultProductViewModel = new actionResultProductViewModel();
                actionResultProductViewModel.itemsProduct = result.ToList();
                actionResultProductViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage, PerPage = model.PerPage };

                return actionResultProductViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region filterProduct
        public actionResultProductViewModel filter(SearchProductViewModel data)
        {
            try
            {
                var query = db.View_ProductDetailV2.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Product_Id.Contains(data.key)
                                        || c.Product_Name.Contains(data.key)
                                         || c.ProductType_Name.Contains(data.key)
                                          || c.ProductSubType_Name.Contains(data.key)
                                           || c.ProductConversion_Name.Contains(data.key)
                                           || c.Ref_No1.Contains(data.key)
                                        || c.ProductCategory_Name.Contains(data.key));

                }

                var Item = new List<View_ProductDetailV2>();
                var TotalRow = new List<View_ProductDetailV2>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Product_Id).ToList();

                var result = new List<SearchProductViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductViewModel();

                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.product_SecondName = item.Product_SecondName;
                    resultItem.product_ThirdName = item.Product_ThirdName;
                    resultItem.productCategory_Index = item.ProductCategory_Index;
                    resultItem.productCategory_Id = item.ProductCategory_Id;
                    resultItem.productCategory_Name = item.ProductCategory_Name;
                    resultItem.productType_Index = item.ProductType_Index;
                    resultItem.productType_Id = item.ProductType_Id;
                    resultItem.productType_Name = item.ProductType_Name;
                    resultItem.productSubType_Index = item.ProductSubType_Index;
                    resultItem.productSubType_Id = item.ProductSubType_Id;
                    resultItem.productSubType_Name = item.ProductSubType_Name;
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.productItemLife_Y = item.ProductItemLife_Y;
                    resultItem.productItemLife_M = item.ProductItemLife_M;
                    resultItem.productItemLife_D = item.ProductItemLife_D;
                    resultItem.productImage_Path = item.ProductImage_Path;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.remark = item.Remark;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.SALE_ProductConversion_Name = item.SALE_ProductConversion_Name;
                    resultItem.IN_ProductConversion_Name = item.IN_ProductConversion_Name;
                    resultItem.DocumentFile_Url = item.DocumentFile_Url;
                    resultItem.isActive = item.IsActive;
                    resultItem.ti_Hi = (item.Ti == "0" || item.Ti == "" && item.Hi == "0" || item.Hi == "") ? "": item.Ti + "*" + item.Hi;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultProductViewModel = new actionResultProductViewModel();
                actionResultProductViewModel.itemsProduct = result.ToList();
                actionResultProductViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultProductViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
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
                    Model2.Product_Id = Model.Product_Id;
                    Model2.Product_Name = Model.Product_Name;
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

        #region find
        public ProductViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_ProductDetailV2.Where(c => c.Product_Index == id).FirstOrDefault();

                var result = new ProductViewModel();


                result.product_Index = queryResult.Product_Index;
                result.product_Id = queryResult.Product_Id;
                result.product_Name = queryResult.Product_Name;
                result.product_SecondName = queryResult.Product_SecondName;
                result.product_ThirdName = queryResult.Product_ThirdName;
                result.productCategory_Index = queryResult.ProductCategory_Index;
                result.productCategory_Id = queryResult.ProductCategory_Id;
                result.productCategory_Name = queryResult.ProductCategory_Name;
                result.productConversion_Index = queryResult.ProductConversion_Index;
                result.productConversion_Id = queryResult.ProductConversion_Id;
                result.productConversion_Name = queryResult.ProductConversion_Name;
                result.productType_Index = queryResult.ProductType_Index;
                result.productType_Id = queryResult.ProductType_Id;
                result.productType_Name = queryResult.ProductType_Name;
                result.productSubType_Index = queryResult.ProductSubType_Index;
                result.productSubType_Id = queryResult.ProductSubType_Id;
                result.productSubType_Name = queryResult.ProductSubType_Name;
                result.productItemLife_Y = queryResult.ProductItemLife_Y;
                result.productItemLife_M = queryResult.ProductItemLife_M;
                result.productItemLife_D = queryResult.ProductItemLife_D;
                result.ProductShelfLife_D = queryResult.ProductShelfLife_D;
                result.productImage_Path = queryResult.ProductImage_Path;
                result.ref_No1 = queryResult.Ref_No1;
                result.ref_No2 = queryResult.Ref_No2;
                result.ref_No3 = queryResult.Ref_No3;
                result.ref_No4 = queryResult.Ref_No4;
                result.ref_No5 = queryResult.Ref_No5;
                result.remark = queryResult.Remark;
                result.udf_1 = queryResult.UDF_1;
                result.udf_2 = queryResult.UDF_2;
                result.udf_3 = queryResult.UDF_3;
                result.udf_4 = queryResult.UDF_4;
                result.udf_5 = queryResult.UDF_5;
                result.isLot = queryResult.IsLot;
                result.isExpDate = queryResult.IsExpDate;
                result.isMfgDate = queryResult.IsMfgDate;
                result.isSerial = queryResult.IsSerial;
                result.key = queryResult.ProductCategory_Id + " - " + queryResult.ProductCategory_Name;
                result.key2 = queryResult.ProductType_Id + " - " + queryResult.ProductType_Name;
                result.key3 = queryResult.ProductSubType_Id + " - " + queryResult.ProductSubType_Name;
                result.isActive = queryResult.IsActive;
                result.qty_Per_Tag = queryResult.Qty_Per_Tag;
                result.businessUnit_Index = queryResult.BusinessUnit_Index;
                result.businessUnit_Id = queryResult.BusinessUnit_Id;
                result.businessUnit_Name = queryResult.BusinessUnit_Name;
                result.fireClass_Index = queryResult.FireClass_Index;
                result.fireClass_Id = queryResult.FireClass_Id;
                result.fireClass_Name = queryResult.FireClass_Name;
                result.masterType_Index = queryResult.MasterType_Index;
                result.masterType_Id = queryResult.MasterType_Id;
                result.masterType_Name = queryResult.MasterType_Name;
                result.materialClass_Index = queryResult.MaterialClass_Index;
                result.materialClass_Id = queryResult.MaterialClass_Id;
                result.materialClass_Name = queryResult.MaterialClass_Name;
                result.movingCondition_Index = queryResult.MovingCondition_Index;
                result.movingCondition_Id = queryResult.MovingCondition_Id;
                result.movingCondition_Name = queryResult.MovingCondition_Name;
                result.productHierarchy5_Index = queryResult.ProductHierarchy5_Index;
                result.productHierarchy5_Id = queryResult.ProductHierarchy5_Id;
                result.productHierarchy5_Name = queryResult.ProductHierarchy5_Name;
                result.tempCondition_Index = queryResult.TempCondition_Index;
                result.tempCondition_Id = queryResult.TempCondition_Id;
                result.tempCondition_Name = queryResult.TempCondition_Name;

                result.type_Product_Index = queryResult.Type_Production_Index;
                result.type_Product_Id = queryResult.Type_Production_Id;
                result.type_Product_Name = queryResult.Type_Production_Name;

                result.isSAP = queryResult.IsSAP;
                result.shelfLeft_alert = queryResult.ShelfLeft_alert;
                result.isPending = queryResult.IsPending;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDelete
        public Boolean getDelete(ProductViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Product = db.MS_Product.Find(data.product_Index);

                if (Product != null)
                {
                    Product.IsActive = 0;
                    Product.IsDelete = 1;
                    Product.Cancel_By = data.create_By;
                    Product.Cancel_Date = DateTime.Now;

                    #region ProductConversion
                    var ProductConversion = db.MS_ProductConversion.Where(c => c.Product_Index == data.product_Index).ToList();

                    if (ProductConversion != null)
                    {
                        foreach (var item in ProductConversion)
                        {
                            var delete = db.MS_ProductConversion.Find(item.ProductConversion_Index);

                            delete.IsActive = 0;
                            delete.IsDelete = 1;
                            delete.Cancel_By = data.create_By;
                            delete.Cancel_Date = DateTime.Now;
                        }
                    }

                    #endregion

                    #region ProductConversionBarcode

                    var ProductConversionBarcode = db.MS_ProductConversionBarcode.Where(c => c.Product_Index == data.product_Index).ToList();

                    if (ProductConversionBarcode != null)
                    {
                        foreach (var item in ProductConversionBarcode)
                        {
                            var delete = db.MS_ProductConversionBarcode.Find(item.ProductConversionBarcode_Index);

                            delete.IsActive = 0;
                            delete.IsDelete = 1;
                            delete.Cancel_By = data.create_By;
                            delete.Cancel_Date = DateTime.Now;
                        }
                    }
                    #endregion



                    #region ProductOnwer
                    var ProductOnwer = db.MS_ProductOwner.Where(c => c.Product_Index == data.product_Index).ToList();

                    if (ProductOnwer != null)
                    {
                        foreach (var item in ProductOnwer)
                        {
                            var delete = db.MS_ProductOwner.Find(item.ProductOwner_Index);

                            delete.IsActive = 0;
                            delete.IsDelete = 1;
                            delete.Cancel_By = data.create_By;
                            delete.Cancel_Date = DateTime.Now;
                        }
                    }
                    #endregion

                    var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("DeleteProduct" +
                            "", msglog);
                        transaction.Rollback();
                        throw exy;
                    }

                }


                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        //public actionResultProductViewModel Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            //var queryResult = context.MS_Product.FromSql("sp_GetProduct").Where( c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            string pstring = "";

        //            var strwheres = new SqlParameter("@strwhere", pstring);
        //            var PageNumber = new SqlParameter("@PageNumber", 1);
        //            var RowspPage = new SqlParameter("@RowspPage", 10000);

        //            var countx = context.View_ProductPopup.FromSql("sp_GetView_ProductPopup @strwhere , @PageNumber , @RowspPage ", strwheres, PageNumber, RowspPage).ToList();

        //            var count = countx.Count();

        //            var strwhere1 = new SqlParameter("@strwhere", pstring);
        //            var PageNumber1 = new SqlParameter("@PageNumber", 1);
        //            var RowspPage1 = new SqlParameter("@RowspPage", 30);
        //            //var queryResultTotal = context.MS_Product.FromSql("sp_GetProductByPagination @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber, RowspPage).ToList();
        //            var queryResultTotal = context.View_ProductPopup.FromSql("sp_GetView_ProductPopup @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).ToList();


        //            var result = new List<ProductViewModel>();
        //            foreach (var item in queryResultTotal)
        //            {
        //                var resultItem = new ProductViewModel();



        //                resultItem.product_Id = item.Product_Id;
        //                resultItem.product_Index = item.Product_Index;
        //                resultItem.product_Name = item.Product_Name;
        //                resultItem.product_SecondName = item.Product_SecondName;
        //                resultItem.product_ThirdName = item.Product_ThirdName;
        //                resultItem.isLot = item.IsLot;
        //                resultItem.isExpDate = item.IsExpDate;
        //                resultItem.isMfgDate = item.IsMfgDate;
        //                resultItem.isCatchWeight = item.IsCatchWeight;
        //                if (item.ProductCategory_Index != null)
        //                {
        //                    var itemList = context.MS_ProductCategory.FromSql("sp_GetProductCategory").Where(c => c.ProductCategory_Index == item.ProductCategory_Index).FirstOrDefault();
        //                    resultItem.productCategory_Index = itemList.ProductCategory_Index;
        //                    resultItem.productCategory_Name = itemList.ProductCategory_Name;
        //                }
        //                if (item.ProductCategory_Index != null)
        //                {
        //                    var itemList = context.MS_ProductSubType.FromSql("sp_GetProductSubType").Where(c => c.ProductSubType_Index == item.ProductSubType_Index).FirstOrDefault();
        //                    resultItem.productSubType_Index = itemList.ProductSubType_Index;
        //                    resultItem.productSubType_Name = itemList.ProductSubType_Name;
        //                }
        //                if (item.ProductCategory_Index != null)
        //                {
        //                    var itemList = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.ProductType_Index == item.ProductType_Index).FirstOrDefault();
        //                    resultItem.productType_Index = itemList.ProductType_Index;
        //                    resultItem.productType_Name = itemList.ProductType_Name;
        //                }
        //                if (item.ProductCategory_Index != null)
        //                {
        //                    //var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.productConversion_Index == item.productConversion_Index).FirstOrDefault();
        //                    resultItem.productConversion_Index = item.ProductConversion_Index;
        //                    resultItem.productConversion_Id = item.ProductConversion_Id;
        //                    resultItem.productConversion_Name = item.ProductConversion_Name;
        //                    resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
        //                    resultItem.productConversion_Weight = item.ProductConversion_Weight;
        //                    resultItem.productConversion_Width = item.ProductConversion_Width;
        //                    resultItem.productConversion_Length = item.ProductConversion_Length;
        //                    resultItem.productConversion_Height = item.ProductConversion_Height;
        //                    resultItem.productConversion_VolumeRatio = item.ProductConversion_VolumeRatio;
        //                    resultItem.productConversion_Volume = item.ProductConversion_Volume;
        //                }
        //                resultItem.productItemLife_Y = item.ProductItemLife_Y;
        //                resultItem.productItemLife_M = item.ProductItemLife_M;
        //                resultItem.productItemLife_D = item.ProductItemLife_D;
        //                resultItem.productImage_Path = item.ProductImage_Path;
        //                resultItem.isActive = item.IsActive;
        //                resultItem.isDelete = item.IsDelete;
        //                resultItem.create_Date = item.Create_Date.GetValueOrDefault();
        //                resultItem.create_By = item.Create_By;
        //                resultItem.update_Date = item.Update_Date.GetValueOrDefault();
        //                resultItem.update_By = item.Update_By;
        //                resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.cancel_By = item.Cancel_By;
        //                result.Add(resultItem);
        //            }

        //            var actionResultProduct = new actionResultProductViewModel();
        //            actionResultProduct.itemsProduct = result.ToList();
        //            actionResultProduct.pagination = new Pagination() { TotalRow = count, CurrentPage = 1, NumPerPage = 30 };

        //            return actionResultProduct;

        //            //return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ProductViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == id).ToList();

        //            var result = new List<ProductViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProductViewModel();
        //                resultItem.product_Id = item.Product_Id;
        //                resultItem.product_Index = item.Product_Index;
        //                resultItem.product_Name = item.Product_Name;
        //                resultItem.product_SecondName = item.Product_SecondName;
        //                resultItem.isLot = item.IsLot;
        //                resultItem.isExpDate = item.IsExpDate;
        //                resultItem.isMfgDate = item.IsMfgDate;
        //                resultItem.isCatchWeight = item.IsCatchWeight;

        //                if (item.ProductCategory_Index != null)
        //                {
        //                    var itemList = context.MS_ProductCategory.FromSql("sp_GetProductCategory").Where(c => c.ProductCategory_Index == item.ProductCategory_Index).FirstOrDefault();
        //                    resultItem.productCategory_Index = itemList.ProductCategory_Index;
        //                    resultItem.productCategory_Name = itemList.ProductCategory_Name;
        //                }
        //                if (item.ProductSubType_Index != null)
        //                {
        //                    var itemList = context.MS_ProductSubType.FromSql("sp_GetProductSubType").Where(c => c.ProductSubType_Index == item.ProductSubType_Index).FirstOrDefault();
        //                    resultItem.productSubType_Index = itemList.ProductSubType_Index;
        //                    resultItem.productSubType_Name = itemList.ProductSubType_Name;
        //                }
        //                if (item.ProductType_Index != null)
        //                {
        //                    var itemList = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.ProductType_Index == item.ProductType_Index).FirstOrDefault();
        //                    resultItem.productType_Index = itemList.ProductType_Index;
        //                    resultItem.productType_Name = itemList.ProductType_Name;
        //                }
        //                if (item.ProductConversion_Index != null)
        //                {
        //                    var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.ProductConversion_Index == item.ProductConversion_Index).FirstOrDefault();
        //                    resultItem.productConversion_Id = itemList.ProductConversion_Id;
        //                    resultItem.productConversion_Index = itemList.ProductConversion_Index;
        //                    resultItem.productConversion_Name = itemList.ProductConversion_Name;
        //                }
        //                else
        //                {
        //                    var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                    resultItem.productConversion_Id = itemList.ProductConversion_Id;
        //                    resultItem.productConversion_Index = itemList.ProductConversion_Index;
        //                    resultItem.productConversion_Name = itemList.ProductConversion_Name;
        //                }

        //                resultItem.product_ThirdName = item.Product_ThirdName;
        //                resultItem.productItemLife_Y = item.ProductItemLife_Y;
        //                resultItem.productItemLife_M = item.ProductItemLife_M;
        //                resultItem.productItemLife_D = item.ProductItemLife_D;
        //                resultItem.productImage_Path = item.ProductImage_Path;
        //                resultItem.isActive = item.IsActive;
        //                resultItem.isDelete = item.IsDelete;
        //                resultItem.create_Date = item.Create_Date.GetValueOrDefault();
        //                resultItem.create_By = item.Create_By;
        //                resultItem.update_Date = item.Update_Date.GetValueOrDefault();
        //                resultItem.update_By = item.Update_By;
        //                resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.cancel_By = item.Cancel_By;
        //                result.Add(resultItem);
        //            }

        //            if (queryResult.Count == 0)
        //            {
        //                var findItem = context.MS_ProductOwner.FromSql("sp_GetProductOwner").Where(c => c.Owner_Index == id).ToList();
        //                if (findItem != null)
        //                {
        //                    foreach (var item in findItem.OrderByDescending(c => c.Product_Index))
        //                    {
        //                        var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();

        //                        var itemLists = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.Product_Index == itemList.Product_Index).FirstOrDefault();

        //                        if (itemList != null)
        //                        {
        //                            var resultItem = new ProductViewModel();
        //                            resultItem.product_Id = itemList.Product_Id;
        //                            resultItem.product_Index = itemList.Product_Index;
        //                            resultItem.product_Name = itemList.Product_Name;
        //                            resultItem.productConversion_Index = itemList.ProductConversion_Index;
        //                            resultItem.productConversion_Name = itemLists.ProductConversion_Name;
        //                            resultItem.productConversion_Id = itemList.ProductConversion_Id;
        //                            resultItem.productConversion_Ratio = itemLists.ProductConversion_Ratio;
        //                            resultItem.productConversion_Width = itemLists.ProductConversion_Weight;
        //                            resultItem.productConversion_Width = itemLists.ProductConversion_Width;
        //                            resultItem.productConversion_Length = itemLists.ProductConversion_Length;
        //                            resultItem.productConversion_Height = itemLists.ProductConversion_Height;
        //                            resultItem.productConversion_VolumeRatio = itemLists.ProductConversion_VolumeRatio;
        //                            resultItem.productConversion_Volume = itemLists.ProductConversion_Volume;
        //                            resultItem.product_SecondName = itemList.Product_SecondName;
        //                            resultItem.product_ThirdName = itemList.Product_ThirdName;
        //                            resultItem.productItemLife_Y = itemList.ProductItemLife_Y;
        //                            resultItem.productItemLife_M = itemList.ProductItemLife_M;
        //                            resultItem.productItemLife_D = itemList.ProductItemLife_D;
        //                            resultItem.productImage_Path = itemList.ProductImage_Path;
        //                            resultItem.isActive = itemList.IsActive;
        //                            resultItem.isDelete = itemList.IsDelete;
        //                            resultItem.create_Date = item.Create_Date.GetValueOrDefault();
        //                            resultItem.create_By = item.Create_By;
        //                            resultItem.update_Date = item.Update_Date.GetValueOrDefault();
        //                            resultItem.update_By = item.Update_By;
        //                            resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
        //                            resultItem.cancel_By = item.Cancel_By;
        //                            result.Add(resultItem);
        //                        }



        //                    }
        //                }

        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public String SaveChanges(ProductViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.product_Index.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.product_Index = Guid.NewGuid();
        //            }
        //            if (data.productConversion_Index.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.productConversion_Index = Guid.NewGuid();
        //            }
        //            if (data.product_Id == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "product_Id");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.product_Id = resultParameter.Value.ToString();
        //            }
        //            if (data.productConversion_Id == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "productConversion_Id");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.productConversion_Id = resultParameter.Value.ToString();
        //            }
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int isSystem = 0;
        //            int statusId = 0;

        //            int isPack = 0;
        //            int isSerial = 0;
        //            var Product_Index = new SqlParameter("Product_Index", data.product_Index);
        //            var Product_Id = new SqlParameter("Product_Id", data.product_Id);
        //            var Product_Name = new SqlParameter("Product_Name", data.product_Name);
        //            var Product_SecondName = new SqlParameter("Product_SecondName", data.product_SecondName);
        //            var Product_ThirdName = new SqlParameter("Product_ThirdName", data.product_ThirdName);
        //            var ProductCategory_Index = new SqlParameter("ProductCategory_Index", data.productCategory_Index);
        //            var productConversion_Index = new SqlParameter("productConversion_Index", data.productConversion_Index);
        //            var ProductConversion_Id = new SqlParameter("ProductConversion_Id", data.productConversion_Id);
        //            var ProductConversion_Name = new SqlParameter("ProductConversion_Name", data.productConversion_Name);
        //            var ProductType_Index = new SqlParameter("ProductType_Index", data.productType_Index);
        //            var ProductSubType_Index = new SqlParameter("ProductSubType_Index", data.productSubType_Index);
        //            var ProductItemLife_Y = new SqlParameter("ProductItemLife_Y", data.productItemLife_Y);
        //            var ProductItemLife_M = new SqlParameter("ProductItemLife_M", data.productItemLife_M);
        //            var ProductItemLife_D = new SqlParameter("ProductItemLife_D", data.productItemLife_D);
        //            var ProductImage_Path = new SqlParameter("ProductImage_Path", data.productImage_Path);
        //            var IsExpDate = new SqlParameter("IsExpDate", data.isExpDate);
        //            if (data.isExpDate != null)
        //            {
        //                IsExpDate.SqlValue = data.isExpDate;
        //            }
        //            else
        //            {
        //                IsExpDate.SqlValue = 0;
        //            }
        //            var IsLot = new SqlParameter("IsLot", data.isLot);
        //            var IsMfgDate = new SqlParameter("IsMfgDate", data.isMfgDate);
        //            var IsCatchWeight = new SqlParameter("IsCatchWeight", data.isCatchWeight);
        //            var IsPack = new SqlParameter("IsPack", isPack);
        //            var IsSerial = new SqlParameter("IsSerial", isSerial);
        //            var IsActive = new SqlParameter("IsActive", isactive);
        //            var IsDelete = new SqlParameter("IsDelete", isdelete);
        //            var IsSystem = new SqlParameter("IsSystem", isSystem);
        //            var Status_Id = new SqlParameter("Status_Id", statusId);
        //            var Create_By = new SqlParameter("Create_By", "");
        //            var Create_Date = new SqlParameter("Create_Date", DateTime.Now);
        //            var Update_By = new SqlParameter("Update_By", "");
        //            var Update_Date = new SqlParameter("Update_Date", DateTime.Now);
        //            var Cancel_By = new SqlParameter("Cancel_By", "");
        //            var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now);
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_Product  @Product_Index,@Product_Id,@Product_Name,@Product_SecondName,@Product_ThirdName,@ProductCategory_Index,@productConversion_Index,@ProductConversion_Id,@ProductConversion_Name,@ProductType_Index,@ProductSubType_Index,@ProductItemLife_Y,@ProductItemLife_M,@ProductItemLife_D,@ProductImage_Path,@IsLot,@IsExpDate,@IsMfgDate,@IsCatchWeight,@IsPack,@IsSerial,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Product_Index, Product_Id, Product_Name, Product_SecondName, Product_ThirdName, ProductCategory_Index, productConversion_Index, ProductConversion_Id, ProductConversion_Name, ProductType_Index, ProductSubType_Index, ProductItemLife_Y, ProductItemLife_M, ProductItemLife_D, ProductImage_Path, IsLot, IsExpDate, IsMfgDate, IsCatchWeight, IsPack, IsSerial, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ProductViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == id).ToList();

        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<ProductViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var Product_Index = new SqlParameter("Product_Index", item.Product_Index);
        //                var Product_Id = new SqlParameter("Product_Id", item.Product_Id);
        //                var Product_Name = new SqlParameter("Product_Name", item.Product_Name);
        //                var Product_SecondName = new SqlParameter("Product_SecondName", item.Product_SecondName);
        //                var Product_ThirdName = new SqlParameter("Product_ThirdName", item.Product_ThirdName);
        //                var ProductCategory_Index = new SqlParameter("ProductCategory_Index", item.ProductCategory_Index);
        //                var productConversion_Index = new SqlParameter("productConversion_Index", item.ProductConversion_Index);
        //                var ProductConversion_Id = new SqlParameter("ProductConversion_Id", item.ProductConversion_Id);
        //                var ProductConversion_Name = new SqlParameter("ProductConversion_Name", item.ProductConversion_Name);
        //                var ProductType_Index = new SqlParameter("ProductType_Index", item.ProductType_Index);
        //                var ProductSubType_Index = new SqlParameter("ProductSubType_Index", item.ProductSubType_Index);
        //                var ProductItemLife_Y = new SqlParameter("ProductItemLife_Y", item.ProductItemLife_Y);
        //                var ProductItemLife_M = new SqlParameter("ProductItemLife_M", item.ProductItemLife_M);
        //                var ProductItemLife_D = new SqlParameter("ProductItemLife_D", item.ProductItemLife_D);
        //                var ProductImage_Path = new SqlParameter("ProductImage_Path", item.ProductImage_Path);
        //                var IsLot = new SqlParameter("IsLot", item.IsLot);
        //                var IsExpDate = new SqlParameter("IsExpDate", item.IsExpDate);
        //                var IsMfgDate = new SqlParameter("IsMfgDate", item.IsMfgDate);
        //                var IsCatchWeight = new SqlParameter("IsCatchWeight", item.IsCatchWeight);
        //                var IsPack = new SqlParameter("IsPack", item.IsPack);
        //                var IsSerial = new SqlParameter("IsSerial", item.IsSerial);
        //                var IsActive = new SqlParameter("IsActive", isactive);
        //                var IsDelete = new SqlParameter("IsDelete", isdelete);
        //                var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
        //                var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
        //                var Create_By = new SqlParameter("Create_By", "");
        //                var Create_Date = new SqlParameter("Create_Date", DateTime.Now);
        //                var Update_By = new SqlParameter("Update_By", "");
        //                var Update_Date = new SqlParameter("Update_Date", DateTime.Now);
        //                var Cancel_By = new SqlParameter("Cancel_By", "");
        //                var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now);
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_Product  @Product_Index,@Product_Id,@Product_Name,@Product_SecondName,@Product_ThirdName,@ProductCategory_Index,@productConversion_Index,@ProductConversion_Id,@ProductConversion_Name,@ProductType_Index,@ProductSubType_Index,@ProductItemLife_Y,@ProductItemLife_M,@ProductItemLife_D,@ProductImage_Path,@IsLot,@IsExpDate,@IsMfgDate,@IsCatchWeight,@IsPack,@IsSerial,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Product_Index, Product_Id, Product_Name, Product_SecondName, Product_ThirdName, ProductCategory_Index, productConversion_Index, ProductConversion_Id, ProductConversion_Name, ProductType_Index, ProductSubType_Index, ProductItemLife_Y, ProductItemLife_M, ProductItemLife_D, ProductImage_Path, IsLot, IsExpDate, IsMfgDate, IsCatchWeight, IsPack, IsSerial, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //                context.SaveChanges();
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public actionResultProductViewModel search(ProductViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {



        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<ProductViewModel>();

        //            if (data.product_Id != "" && data.product_Id != null)
        //            {
        //                pwhereFilter = " And Product_Id like N'%" + data.product_Id + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter = "";
        //            }

        //            if (data.product_Name != "" && data.product_Name != null)
        //            {
        //                pwhereFilter += " And Product_Name like N'%" + data.product_Name + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }

        //            if (data.CurrentPage == 0)
        //            {
        //                data.CurrentPage = 1;
        //            }
        //            if (data.NumPerPage == 0)
        //            {
        //                data.NumPerPage = 30;
        //            }
        //            var strwheres = new SqlParameter("@strwhere", pwhereFilter);
        //            var PageNumber = new SqlParameter("@PageNumber", 1);
        //            var RowspPage = new SqlParameter("@RowspPage", 10000);

        //            var queryResultTotal = context.View_ProductPopup.FromSql("sp_GetView_ProductPopup @strwhere , @PageNumber , @RowspPage ", strwheres, PageNumber, RowspPage).ToList();


        //            if (queryResultTotal.Count == 0)
        //            {
        //                pwhereFilter = " And Product_SecondName like N'%" + data.product_Name + "%'";

        //            }

        //            var strwhere1 = new SqlParameter("@strwhere", pwhereFilter);
        //            var PageNumber1 = new SqlParameter("@PageNumber", data.CurrentPage);
        //            var RowspPage1 = new SqlParameter("@RowspPage", data.PerPage);
        //            var querys = context.View_ProductPopup.FromSql("sp_GetView_ProductPopup @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).ToList();



        //            if (data.product_Id != "" && data.product_Id != null || data.product_Name != "" && data.product_Name != null)
        //            {

        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_Product.FromSql("sp_GetProduct @strwhere ", strwhere).ToList();
        //                foreach (var item in querys)
        //                {
        //                    var resultItem = new ProductViewModel();

        //                    resultItem.product_Id = item.Product_Id;
        //                    resultItem.product_Index = item.Product_Index;
        //                    resultItem.product_Name = item.Product_Name;
        //                    resultItem.product_SecondName = item.Product_SecondName;
        //                    resultItem.product_ThirdName = item.Product_ThirdName;
        //                    resultItem.isLot = item.IsLot;
        //                    resultItem.isExpDate = item.IsExpDate;
        //                    resultItem.isMfgDate = item.IsMfgDate;
        //                    resultItem.isCatchWeight = item.IsCatchWeight;
        //                    if (item.ProductCategory_Index != null)
        //                    {
        //                        var itemList = context.MS_ProductCategory.FromSql("sp_GetProductCategory").Where(c => c.ProductCategory_Index == item.ProductCategory_Index).FirstOrDefault();
        //                        resultItem.productCategory_Index = itemList.ProductCategory_Index;
        //                        resultItem.productCategory_Name = itemList.ProductCategory_Name;
        //                    }
        //                    if (item.ProductCategory_Index != null)
        //                    {
        //                        var itemList = context.MS_ProductSubType.FromSql("sp_GetProductSubType").Where(c => c.ProductSubType_Index == item.ProductSubType_Index).FirstOrDefault();
        //                        resultItem.productSubType_Index = itemList.ProductSubType_Index;
        //                        resultItem.productSubType_Name = itemList.ProductSubType_Name;
        //                    }
        //                    if (item.ProductCategory_Index != null)
        //                    {
        //                        var itemList = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.ProductType_Index == item.ProductType_Index).FirstOrDefault();
        //                        resultItem.productType_Index = itemList.ProductType_Index;
        //                        resultItem.productType_Name = itemList.ProductType_Name;
        //                    }
        //                    if (item.ProductCategory_Index != null)
        //                    {
        //                        //var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.productConversion_Index == item.productConversion_Index).FirstOrDefault();
        //                        resultItem.productConversion_Index = item.ProductConversion_Index;
        //                        resultItem.productConversion_Id = item.ProductConversion_Id;
        //                        resultItem.productConversion_Name = item.ProductConversion_Name;
        //                        resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
        //                        resultItem.productConversion_Width = item.ProductConversion_Weight;
        //                        resultItem.productConversion_Width = item.ProductConversion_Width;
        //                        resultItem.productConversion_Length = item.ProductConversion_Length;
        //                        resultItem.productConversion_Height = item.ProductConversion_Height;
        //                        resultItem.productConversion_VolumeRatio = item.ProductConversion_VolumeRatio;
        //                        resultItem.productConversion_Volume = item.ProductConversion_Volume;
        //                    }
        //                    resultItem.productItemLife_Y = item.ProductItemLife_Y;
        //                    resultItem.productItemLife_M = item.ProductItemLife_M;
        //                    resultItem.productItemLife_D = item.ProductItemLife_D;
        //                    resultItem.productImage_Path = item.ProductImage_Path;
        //                    resultItem.isActive = item.IsActive;
        //                    resultItem.isDelete = item.IsDelete;
        //                    resultItem.create_Date = item.Create_Date.GetValueOrDefault();
        //                    resultItem.create_By = item.Create_By;
        //                    resultItem.update_Date = item.Update_Date.GetValueOrDefault();
        //                    resultItem.update_By = item.Update_By;
        //                    resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
        //                    resultItem.cancel_By = item.Cancel_By;
        //                    result.Add(resultItem);
        //                }
        //            }

        //            else
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And ProductType_Name like N'%" + data.productType_Name + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_ProductType.FromSql("sp_GetProductType @strwhere ", pstrwhere1).ToList();
        //                foreach (var item in querys)
        //                {
        //                    var resultItem = new ProductViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.ProductType_Index == ItemList.ProductType_Index)
        //                        {
        //                            resultItem.product_Id = item.Product_Id;
        //                            resultItem.product_Index = item.Product_Index;
        //                            resultItem.product_Name = item.Product_Name;
        //                            resultItem.product_SecondName = item.Product_SecondName;
        //                            resultItem.product_ThirdName = item.Product_ThirdName;
        //                            resultItem.isLot = item.IsLot;
        //                            resultItem.isExpDate = item.IsExpDate;
        //                            resultItem.isMfgDate = item.IsMfgDate;
        //                            resultItem.isCatchWeight = item.IsCatchWeight;
        //                            if (item.ProductCategory_Index != null)
        //                            {
        //                                var itemList = context.MS_ProductCategory.FromSql("sp_GetProductCategory").Where(c => c.ProductCategory_Index == item.ProductCategory_Index).FirstOrDefault();
        //                                resultItem.productCategory_Index = itemList.ProductCategory_Index;
        //                                resultItem.productCategory_Name = itemList.ProductCategory_Name;
        //                            }
        //                            if (item.ProductCategory_Index != null)
        //                            {
        //                                var itemList = context.MS_ProductSubType.FromSql("sp_GetProductSubType").Where(c => c.ProductSubType_Index == item.ProductSubType_Index).FirstOrDefault();
        //                                resultItem.productSubType_Index = itemList.ProductSubType_Index;
        //                                resultItem.productSubType_Name = itemList.ProductSubType_Name;
        //                            }
        //                            if (item.ProductCategory_Index != null)
        //                            {
        //                                var itemList = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.ProductType_Index == item.ProductType_Index).FirstOrDefault();
        //                                resultItem.productType_Index = itemList.ProductType_Index;
        //                                resultItem.productType_Name = itemList.ProductType_Name;
        //                            }
        //                            if (item.ProductCategory_Index != null)
        //                            {
        //                                //var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.productConversion_Index == item.productConversion_Index).FirstOrDefault();
        //                                resultItem.productConversion_Index = item.ProductConversion_Index;
        //                                resultItem.productConversion_Id = item.ProductConversion_Id;
        //                                resultItem.productConversion_Name = item.ProductConversion_Name;
        //                                resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
        //                                resultItem.productConversion_Width = item.ProductConversion_Weight;
        //                                resultItem.productConversion_Width = item.ProductConversion_Width;
        //                                resultItem.productConversion_Length = item.ProductConversion_Length;
        //                                resultItem.productConversion_Height = item.ProductConversion_Height;
        //                                resultItem.productConversion_VolumeRatio = item.ProductConversion_VolumeRatio;
        //                                resultItem.productConversion_Volume = item.ProductConversion_Volume;
        //                            }
        //                            resultItem.productItemLife_Y = item.ProductItemLife_Y;
        //                            resultItem.productItemLife_M = item.ProductItemLife_M;
        //                            resultItem.productItemLife_D = item.ProductItemLife_D;
        //                            resultItem.productImage_Path = item.ProductImage_Path;
        //                            resultItem.isActive = item.IsActive;
        //                            resultItem.isDelete = item.IsDelete;
        //                            resultItem.create_Date = item.Create_Date.GetValueOrDefault();
        //                            resultItem.create_By = item.Create_By;
        //                            resultItem.update_Date = item.Update_Date.GetValueOrDefault();
        //                            resultItem.update_By = item.Update_By;
        //                            resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
        //                            resultItem.cancel_By = item.Cancel_By;
        //                            result.Add(resultItem);
        //                        }

        //                    }
        //                }
        //            }

        //            //if (data.product_Id == "" && data.product_Name == "" && data.productType_Name == "")
        //            //{
        //            //    result = this.Filter();
        //            //}

        //            var count = queryResultTotal.Count;
        //            var actionResultProduct = new actionResultProductViewModel();
        //            actionResultProduct.itemsProduct = result.ToList();
        //            actionResultProduct.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, NumPerPage = data.NumPerPage };

        //            return actionResultProduct;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ItemListViewModel> autoSearch(ItemListViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {
        //            var query = new List<ItemListViewModel>();

        //            //Product Name
        //            if (data.chk == 1)
        //            {
        //                var query2 = context.MS_Product.Where(c => c.Product_Name.Contains(data.key)).Select(s => new ItemListViewModel
        //                {
        //                    name = s.Product_Name,
        //                    id = s.Product_Id,
        //                    index = s.Product_Index
        //                }).Distinct();

        //                var query3 = context.MS_Product.Where(c => c.Product_SecondName.Contains(data.key)).Select(s => new ItemListViewModel
        //                {
        //                    name = s.Product_SecondName,
        //                    id = s.Product_Id,
        //                    index = s.Product_Index
        //                }).Distinct();

        //                query = query2.Union(query3).Take(10).ToList();
        //            }
        //            //Product Id
        //            else if (data.chk == 2)
        //            {
        //                var query1 = context.MS_Product.Where(c => c.Product_Id.Contains(data.key)).Select(s => new ItemListViewModel
        //                {
        //                    name = s.Product_Id,
        //                    id = s.Product_Name,
        //                    index = s.Product_Index
        //                }).Distinct();

        //                query = query1.Take(10).ToList();

        //            }
        //            var items = new List<ItemListViewModel>();

        //            foreach (var item in query)
        //            {
        //                var resultItem = new ItemListViewModel
        //                {
        //                    index = item.index,
        //                    id = item.id,
        //                    name = item.name

        //                };

        //                items.Add(resultItem);
        //            }
        //            return items;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public List<ProductDetailViewModel> productDetail(ProductDetailViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.View_ProductDetail.AsQueryable();

                    if (!string.IsNullOrEmpty(data.productConversionBarcode))
                    {
                        query = query.Where(c => c.ProductConversionBarcode.Contains(data.productConversionBarcode));
                    }
                    if (!string.IsNullOrEmpty(data.owner_Index.ToString()) && data.owner_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Owner_Index == data.owner_Index);
                    }
                    if (!string.IsNullOrEmpty(data.product_Index.ToString()) && data.product_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Product_Index == data.product_Index);
                    }


                    var items = new List<ProductDetailViewModel>();

                    var result = query.ToList();



                    foreach (var item in result)
                    {
                        var SuggestLocation = context.sy_SuggestPutawayByProduct.Where(c => c.Product_Index == item.Product_Index).Distinct().Select(x => x.Location_Name).FirstOrDefault();

                        var resultItem = new ProductDetailViewModel
                        {
                            productConversionBarcode_Index = item.ProductConversionBarcode_Index,
                            owner_Index = item.Owner_Index,
                            owner_Id = item.Owner_Id,
                            owner_Name = item.Owner_Name,
                            productCategory_Index = item.ProductCategory_Index,
                            productCategory_Id = item.ProductCategory_Id,
                            productCategory_Name = item.ProductCategory_Name,
                            productType_Index = item.ProductType_Index,
                            productType_Id = item.ProductType_Id,
                            productType_Name = item.ProductType_Name,
                            productSubType_Index = item.ProductSubType_Index,
                            productSubType_Id = item.ProductSubType_Id,
                            productSubType_Name = item.ProductSubType_Name,
                            product_Index = item.Product_Index,
                            product_Id = item.Product_Id,
                            product_Name = item.Product_Name,
                            product_SecondName = item.Product_SecondName,
                            product_ThirdName = item.Product_ThirdName,
                            productConversion_Index = item.ProductConversion_Index,
                            productConversion_Id = item.ProductConversion_Id,
                            productConversion_Name = item.ProductConversion_Name,
                            productConversion_Ratio = item.ProductConversion_Ratio,
                            productConversion_Weight = item.ProductConversion_Weight,
                            productConversion_Width = item.ProductConversion_Width,
                            productConversion_Length = item.ProductConversion_Length,
                            productConversion_Height = item.ProductConversion_Height,
                            productConversion_VolumeRatio = item.ProductConversion_VolumeRatio,
                            productConversion_Volume = item.ProductConversion_Volume,
                            productConversionBarcode_Id = item.ProductConversionBarcode_Id,
                            productConversionBarcode = item.ProductConversionBarcode,
                            productItemLife_D = item.ProductItemLife_D,
                            productItemLife_M = item.ProductItemLife_M,
                            productItemLife_Y = item.ProductItemLife_Y,
                            isExpDate = item.IsExpDate,
                            baseProductConversion = item.BaseProductConversion,
                            isMfgDate = item.IsMfgDate,
                            isLot = item.IsLot,
                            isCatchWeight = item.IsCatchWeight,
                            suggestLocation = SuggestLocation,
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

        public List<ProductViewModel> Product(ProductViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Product.AsQueryable();

                    if (!string.IsNullOrEmpty(data.product_Index.ToString()) && data.product_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Product_Index == data.product_Index);
                    }

                    if (!string.IsNullOrEmpty(data.product_Id))
                    {
                        query = query.Where(c => c.Product_Id == data.product_Id);
                    }

                    var items = new List<ProductViewModel>();

                    var result = query.ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ProductViewModel();

                        resultItem.product_SecondName = item.Product_SecondName;
                        resultItem.product_ThirdName = item.Product_ThirdName;

                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Index = item.Product_Index;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_SecondName = item.Product_SecondName;
                        resultItem.product_ThirdName = item.Product_ThirdName;
                        resultItem.isLot = item.IsLot;
                        resultItem.isExpDate = item.IsExpDate;
                        resultItem.isMfgDate = item.IsMfgDate;
                        resultItem.isCatchWeight = item.IsCatchWeight;
                        resultItem.productCategory_Index = item.ProductCategory_Index;
                        resultItem.productSubType_Index = item.ProductSubType_Index;
                        resultItem.productType_Index = item.ProductType_Index;
                        resultItem.productConversion_Index = item.ProductConversion_Index;
                        resultItem.productConversion_Id = item.ProductConversion_Id;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.productItemLife_Y = item.ProductItemLife_Y;
                        resultItem.productItemLife_M = item.ProductItemLife_M;
                        resultItem.productItemLife_D = item.ProductItemLife_D;
                        resultItem.productImage_Path = item.ProductImage_Path;

                        resultItem.ref_No1 = item.Ref_No1;
                        resultItem.ref_No2 = item.Ref_No2;
                        resultItem.ref_No3 = item.Ref_No3;
                        resultItem.ref_No4 = item.Ref_No4;
                        resultItem.ref_No5 = item.Ref_No5;
                        resultItem.remark = item.Remark;
                        resultItem.udf_1 = item.UDF_1;
                        resultItem.udf_2 = item.UDF_2;
                        resultItem.udf_3 = item.UDF_3;
                        resultItem.udf_4 = item.UDF_4;
                        resultItem.udf_5 = item.UDF_5;


                        resultItem.isActive = item.IsActive;
                        resultItem.isDelete = item.IsDelete;
                        resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                        resultItem.create_By = item.Create_By;
                        resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                        resultItem.update_By = item.Update_By;
                        resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                        resultItem.cancel_By = item.Cancel_By;
                        resultItem.qty_Per_Tag = item.Qty_Per_Tag;
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

        public List<ProductViewModel> GetProduct(ProductViewModel data)
        {
            var olog = new logtxt();
            String msglog = "";
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Product.AsQueryable();

                    if (data.listProductViewModel != null)
                    {
                        if (data.listProductViewModel.Count > 0)
                        {
                            query = query.Where(c => data.listProductViewModel.Select(s => s.product_Index).Contains(c.Product_Index));
                        }
                    }

                    if (!string.IsNullOrEmpty(data.product_Index.ToString()) && data.product_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Product_Index == data.product_Index);
                    }

                    var items = new List<ProductViewModel>();

                    var result = query.ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ProductViewModel();

                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Index = item.Product_Index;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_SecondName = item.Product_SecondName;
                        resultItem.product_ThirdName = item.Product_ThirdName;
                        resultItem.isLot = item.IsLot;
                        resultItem.isExpDate = item.IsExpDate;
                        resultItem.isMfgDate = item.IsMfgDate;
                        resultItem.isCatchWeight = item.IsCatchWeight;
                        resultItem.productCategory_Index = item.ProductCategory_Index;
                        resultItem.productSubType_Index = item.ProductSubType_Index;
                        resultItem.productType_Index = item.ProductType_Index;
                        resultItem.productConversion_Index = item.ProductConversion_Index;
                        resultItem.productConversion_Id = item.ProductConversion_Id;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.productItemLife_Y = item.ProductItemLife_Y;
                        resultItem.productItemLife_M = item.ProductItemLife_M;
                        resultItem.productItemLife_D = item.ProductItemLife_D;
                        resultItem.ProductShelfLife_D = item.ProductShelfLife_D;
                        resultItem.productImage_Path = item.ProductImage_Path;

                        resultItem.ref_No1 = item.Ref_No1;
                        resultItem.ref_No2 = item.Ref_No2;
                        resultItem.ref_No3 = item.Ref_No3;
                        resultItem.ref_No4 = item.Ref_No4;
                        resultItem.ref_No5 = item.Ref_No5;
                        resultItem.remark = item.Remark;
                        resultItem.udf_1 = item.UDF_1;
                        resultItem.udf_2 = item.UDF_2;
                        resultItem.udf_3 = item.UDF_3;
                        resultItem.udf_4 = item.UDF_4;
                        resultItem.udf_5 = item.UDF_5;


                        resultItem.isActive = item.IsActive;
                        resultItem.isDelete = item.IsDelete;
                        resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                        resultItem.create_By = item.Create_By;
                        resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                        resultItem.update_By = item.Update_By;
                        resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                        resultItem.cancel_By = item.Cancel_By;
                        resultItem.qty_Per_Tag = item.Qty_Per_Tag;
                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                olog.logging("GetProduct", "ex Rollback " + ex.Message.ToString());
                throw ex;
            }
        }

        //public List<ProductViewModel> GetProductOfType(ProductViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var query = context.MS_Product.AsQueryable();


        //            if (!string.IsNullOrEmpty(data.productType_Index.ToString()) && data.productType_Index.ToString() != "00000000-0000-0000-0000-000000000000")
        //            {
        //                query = query.Where(c => c.ProductType_Index == data.productType_Index);
        //            }

        //            var items = new List<ProductViewModel>();

        //            var result = query.ToList();

        //            foreach (var item in result)
        //            {
        //                var resultItem = new ProductViewModel();

        //                resultItem.product_Id = item.Product_Id;
        //                resultItem.product_Index = item.Product_Index;
        //                resultItem.product_Name = item.Product_Name;
        //                resultItem.product_SecondName = item.Product_SecondName;
        //                resultItem.product_ThirdName = item.Product_ThirdName;
        //                resultItem.isLot = item.IsLot;
        //                resultItem.isExpDate = item.IsExpDate;
        //                resultItem.isMfgDate = item.IsMfgDate;
        //                resultItem.isCatchWeight = item.IsCatchWeight;
        //                resultItem.productCategory_Index = item.ProductCategory_Index;
        //                resultItem.productSubType_Index = item.ProductSubType_Index;
        //                resultItem.productType_Index = item.ProductType_Index;
        //                resultItem.productConversion_Index = item.ProductConversion_Index;
        //                resultItem.productConversion_Id = item.ProductConversion_Id;
        //                resultItem.productConversion_Name = item.ProductConversion_Name;
        //                resultItem.productItemLife_Y = item.ProductItemLife_Y;
        //                resultItem.productItemLife_M = item.ProductItemLife_M;
        //                resultItem.productItemLife_D = item.ProductItemLife_D;
        //                resultItem.productImage_Path = item.ProductImage_Path;
        //                resultItem.isActive = item.IsActive;
        //                resultItem.isDelete = item.IsDelete;
        //                resultItem.create_Date = item.Create_Date.GetValueOrDefault();
        //                resultItem.create_By = item.Create_By;
        //                resultItem.update_Date = item.Update_Date.GetValueOrDefault();
        //                resultItem.update_By = item.Update_By;
        //                resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.cancel_By = item.Cancel_By;
        //                items.Add(resultItem);
        //            }
        //            return items;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<ProductDetailViewModel> ConfigViewProductDetail(ProductDetailViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {


                    var items = new List<ProductDetailViewModel>();

                    if (!string.IsNullOrEmpty(data.productConversionBarcode))
                    {
                        var query = db.View_ProductDetail.Where(c => c.ProductConversionBarcode == data.productConversionBarcode).ToList();


                        foreach (var item in query)
                        {
                            var resultItem = new ProductDetailViewModel();

                            resultItem.product_Index = item.Product_Index;
                            resultItem.product_Id = item.Product_Id;
                            resultItem.product_Name = item.Product_Name;
                            resultItem.productConversion_Index = item.ProductConversion_Index;
                            resultItem.productConversion_Id = item.ProductConversion_Id;
                            resultItem.productConversion_Name = item.ProductConversion_Name;

                            items.Add(resultItem);
                        }

                    }

                    if (!string.IsNullOrEmpty(data.productType_Index.ToString()) && data.productType_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        var query = db.View_ProductDetail.Where(c => c.ProductType_Index == data.productType_Index).ToList();


                        foreach (var item in query)
                        {
                            var resultItem = new ProductDetailViewModel();

                            resultItem.product_Index = item.Product_Index;
                            resultItem.product_Id = item.Product_Id;
                            resultItem.product_Name = item.Product_Name;
                            items.Add(resultItem);
                        }

                    }


                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<BarcodeViewModel> ConfigBarcode(BarcodeViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {


                    var items = new List<BarcodeViewModel>();

                    if (!string.IsNullOrEmpty(data.productConversionBarcode))
                    {
                        var query = db.View_Barcode.Where(c => c.ProductConversionBarcode == data.productConversionBarcode).ToList();


                        foreach (var item in query)
                        {
                            var resultItem = new BarcodeViewModel();

                            resultItem.product_Index = item.Product_Index;
                            resultItem.product_Id = item.Product_Id;
                            resultItem.product_Name = item.Product_Name;
                            resultItem.product_SecondName = item.Product_SecondName;
                            resultItem.product_ThirdName = item.Product_ThirdName;
                            resultItem.Ref_No2 = item.Ref_No2;
                            resultItem.Ref_No3 = item.Ref_No3;
                            resultItem.productConversion_Width = item.ProductConversion_Width;
                            resultItem.productConversion_Length = item.ProductConversion_Length;
                            resultItem.productConversion_Height = item.ProductConversion_Height;
                            resultItem.productConversionBarcode = item.ProductConversionBarcode;
                            resultItem.ProductItemLife_D = item.ProductItemLife_D;
                            resultItem.ProductItemLife_Y = item.ProductItemLife_Y;
                            resultItem.ProductItemLife_M = item.ProductItemLife_M;
                            resultItem.volume_Index = item.Volume_Index;
                            resultItem.volume_Id = item.Volume_Id;
                            resultItem.volume_Name = item.Volume_Name;
                            resultItem.volume_Ratio = item.Volume_Ratio;
                            resultItem.isLot = item.IsLot;
                            resultItem.isMfgDate = item.IsMfgDate;
                            resultItem.isExpDate = item.IsExpDate;
                            resultItem.isSerial = item.IsSerial;
                            resultItem.productConversion_Index = item.ProductConversion_Index;
                            resultItem.productConversion_Id = item.ProductConversion_Id;
                            resultItem.productConversion_Name = item.ProductConversion_Name;
                            resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
                            resultItem.tihi = item.TIHI;
                            resultItem.qty_Per_Tag = item.qty_Per_Tag;

                            items.Add(resultItem);
                        }

                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Export Excel
        public actionResultExportViewModel Export(ProductExportViewModel data)
        {
            try
            {
                var query = db.View_CheckDimensionAllPrdouct.AsQueryable();


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Product_Id.Contains(data.key)
                                        || c.Product_Name.Contains(data.key));

                }

                var Item = new List<View_CheckDimensionAllPrdouct>();
                var TotalRow = new List<View_CheckDimensionAllPrdouct>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.Product_Id).ToList();

                var result = new List<ProductExportViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new ProductExportViewModel();

                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.businessUnit_Name = item.BusinessUnit_Name; 
                    resultItem.tempCondition_Name = item.TempCondition_Name; 
                    resultItem.shelfLeft_alert = item.ShelfLeft_alert.ToString();
                    resultItem.bu_Unit = item.BU_UNIT;
                    resultItem.sale_Unit = item.SALE_UNIT;
                    resultItem.in_Unit = item.IN_UNIT;
                    resultItem.unit = item.UNIT;
                    resultItem.ratio = item.Ratio;
                    resultItem.weight = item.Weight;
                    resultItem.grsWeight = item.GrsWeight;
                    resultItem.w = item.W;
                    resultItem.l = item.L;
                    resultItem.h = item.H;
                    resultItem.ti = item.TI;
                    resultItem.hi = item.HI;
                    resultItem.isPiecePick = item.IsPiecePcik;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultExportViewModel = new actionResultExportViewModel();
                actionResultExportViewModel.itemsProduct = result.ToList();

                return actionResultExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChangesV2(ProductViewModel data)
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

                //check type product
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
                    Model.BusinessUnit_Index = data.businessUnit_Index;
                    Model.BusinessUnit_Id = data.businessUnit_Id;
                    Model.BusinessUnit_Name = data.businessUnit_Name;
                    Model.FireClass_Index = data.fireClass_Index;
                    Model.FireClass_Id = data.fireClass_Id;
                    Model.FireClass_Name = data.fireClass_Name;
                    Model.MasterType_Index = data.masterType_Index;
                    Model.MasterType_Id = data.masterType_Id;
                    Model.MasterType_Name = data.masterType_Name;
                    Model.MaterialClass_Index = data.materialClass_Index;
                    Model.MaterialClass_Id = data.materialClass_Id;
                    Model.MaterialClass_Name = data.materialClass_Name;
                    Model.MovingCondition_Index = data.movingCondition_Index;
                    Model.MovingCondition_Id = data.movingCondition_Id;
                    Model.MovingCondition_Name = data.movingCondition_Name;
                    Model.ProductHierarchy5_Index = data.productHierarchy5_Index;
                    Model.ProductHierarchy5_Id = data.productHierarchy5_Id;
                    Model.ProductHierarchy5_Name = data.productHierarchy5_Name;
                    Model.TempCondition_Index = data.tempCondition_Index;
                    Model.TempCondition_Id = data.tempCondition_Id;
                    Model.TempCondition_Name = data.tempCondition_Name;
                    Model.IsSAP = Convert.ToInt32(data.isSAP);
                    Model.ShelfLeft_alert = data.shelfLeft_alert;
                    Model.IsPending = data.isPending;

                    //new Column
                    Model.Type_Production_Index = data.type_Product_Index;
                    Model.Type_Production_Id = data.type_Product_Id;
                    Model.Type_Production_Name = data.type_Product_Name;

                    Model2.ProductConversion_Index = Model.ProductConversion_Index;
                    Model2.ProductConversion_Id = Model.ProductConversion_Id;
                    Model2.ProductConversion_Name = Model.ProductConversion_Name;
                    Model2.Product_Index = Model.Product_Index;
                    Model2.Product_Id = Model.Product_Id;
                    Model2.Product_Name = Model.Product_Name;
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
                    productOld.BusinessUnit_Index = data.businessUnit_Index;
                    productOld.BusinessUnit_Id = data.businessUnit_Id;
                    productOld.BusinessUnit_Name = data.businessUnit_Name;
                    productOld.FireClass_Index = data.fireClass_Index;
                    productOld.FireClass_Id = data.fireClass_Id;
                    productOld.FireClass_Name = data.fireClass_Name;
                    productOld.MasterType_Index = data.masterType_Index;
                    productOld.MasterType_Id = data.masterType_Id;
                    productOld.MasterType_Name = data.masterType_Name;
                    productOld.MaterialClass_Index = data.materialClass_Index;
                    productOld.MaterialClass_Id = data.materialClass_Id;
                    productOld.MaterialClass_Name = data.materialClass_Name;
                    productOld.MovingCondition_Index = data.movingCondition_Index;
                    productOld.MovingCondition_Id = data.movingCondition_Id;
                    productOld.MovingCondition_Name = data.movingCondition_Name;
                    productOld.ProductHierarchy5_Index = data.productHierarchy5_Index;
                    productOld.ProductHierarchy5_Id = data.productHierarchy5_Id;
                    productOld.ProductHierarchy5_Name = data.productHierarchy5_Name;
                    productOld.TempCondition_Index = data.tempCondition_Index;
                    productOld.TempCondition_Id = data.tempCondition_Id;
                    productOld.TempCondition_Name = data.tempCondition_Name;
                    productOld.IsSAP = Convert.ToInt32(data.isSAP);
                    productOld.ShelfLeft_alert = data.shelfLeft_alert;
                    productOld.IsPending = data.isPending;

                    //new Column
                    productOld.Type_Production_Index = data.type_Product_Index;
                    productOld.Type_Production_Id = data.type_Product_Id;
                    productOld.Type_Production_Name = data.type_Product_Name;

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

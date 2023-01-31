using Comone.Utils;
using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MasterDataBusiness
{
    public class ProductAssemblyService
    {
        private MasterDataDbContext db;

        public ProductAssemblyService()
        {
            db = new MasterDataDbContext();
        }

        public ProductAssemblyService(MasterDataDbContext db)
        {
            this.db = db;
        }
        #region filterProductAssembly
        public actionResultProductAssemblyViewModel filter(SearchProductAssemblyViewModel data)
        {
            try
            {
                var query = db.ms_ProductBom.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Product_Id.Contains(data.key)
                                         || c.Product_Name.Contains(data.key)
                                         || c.ProductConversion_Name.Contains(data.key));
                }

                var Item = new List<ms_ProductBom>();
                var TotalRow = new List<ms_ProductBom>();

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

                var result = new List<SearchProductAssemblyViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductAssemblyViewModel();

                    resultItem.productBOM_Index = item.ProductBOM_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
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
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultProductAssemblyViewModel = new actionResultProductAssemblyViewModel();
                actionResultProductAssemblyViewModel.itemsProductAssembly = result.ToList();
                actionResultProductAssemblyViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultProductAssemblyViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(ProductAssemblyViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ProductAssemblyOld = db.ms_ProductBom.Find(data.productBOM_Index);

                if (ProductAssemblyOld == null)
                {
                    if (!string.IsNullOrEmpty(data.product_Id))
                    {
                        var query = db.ms_ProductBom.FirstOrDefault(c => c.Product_Id == data.product_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }

                    if (string.IsNullOrEmpty(data.productBOM_No))
                    {
                        data.productBOM_No = "ProductBOM_No".genAutonumber();
                    }

                    ms_ProductBom Model = new ms_ProductBom();

                    Model.ProductBOM_Index = Guid.NewGuid();
                    Model.Product_Id = data.product_Id;
                    Model.ProductBOM_No = data.productBOM_No;
                    Model.ProductBOM_Type = data.productBOM_Type;
                    if (data.productBOM_Type == "Promotion")
                    {
                        Model.Promotion_Date = data.promotion_Date.toDate();
                        Model.Promotion_Date_To = data.promotion_Date_To.toDate();
                    }
                    else {
                        Model.Promotion_Date = null;
                        Model.Promotion_Date_To = null;
                    }
                    Model.Product_Name = data.product_Name;
                    Model.Product_Index = data.product_Index;
                    Model.ProductConversion_Id = data.product_Id;
                    Model.ProductConversion_Index = data.productConversion_Index;
                    Model.ProductConversion_Name = data.productConversion_Name;
                    Model.ProductConversion_Ratio = data.ratio;
                    Model.Ref_No1 = data.ref_No1;
                    Model.Ref_No2 = data.ref_No2;
                    Model.Ref_No3 = data.ref_No3;
                    Model.Ref_No4 = data.ref_No4;
                    Model.Ref_No5 = data.ref_No5;
                    Model.Remark = data.remark;
                    Model.UDF_1 = null;
                    Model.UDF_2 = null;
                    Model.UDF_3 = null;
                    Model.UDF_4 = null;
                    Model.UDF_5 = null;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.ms_ProductBom.Add(Model);

                    foreach (var item in data.listProductAssemblyItemViewModel)
                    {
                        ms_ProductBOMItem resultItem = new ms_ProductBOMItem();

                        resultItem.ProductBOMItem_Index = Guid.NewGuid();
                        resultItem.ProductBOM_Index = Model.ProductBOM_Index;
                        resultItem.Product_Index = item.product_Index;
                        resultItem.Product_Id = item.product_Id;
                        resultItem.Product_Name = item.product_Name;
                        resultItem.ProductBOM_No = Model.ProductBOM_No;
                        resultItem.ProductConversion_Id = data.productConversion_Id;
                        resultItem.ProductConversion_Name = item.productConversion_Name;
                        resultItem.ProductConversion_Index = item.productConversion_Index;
                        resultItem.Qty = item.qty;
                        resultItem.Ratio = item.ratio;
                        resultItem.TotalQty = item.qty * item.ratio;
                        resultItem.Ref_No1 = item.ref_No1;
                        resultItem.Ref_No2 = item.ref_No2;
                        resultItem.Ref_No3 = item.ref_No3;
                        resultItem.Ref_No4 = item.ref_No4;
                        resultItem.Ref_No5 = item.ref_No5;
                        resultItem.Remark = item.remark;
                        resultItem.UDF_1 = null;
                        resultItem.UDF_2 = null;
                        resultItem.UDF_3 = null;
                        resultItem.UDF_4 = null;
                        resultItem.UDF_5 = null;
                        resultItem.IsActive = 1;
                        resultItem.IsDelete = 0;
                        resultItem.IsSystem = 0;
                        resultItem.Status_Id = 0;
                        resultItem.Create_By = data.create_By;
                        resultItem.Create_Date = DateTime.Now;
                        resultItem.ProductConversion_Weight_Index = item.productConversion_Weight_Index;
                        resultItem.ProductConversion_Volume_Index = item.productConversion_Volume_Index;
                        db.ms_ProductBOMItem.Add(resultItem);

                    }
                }
                else
                {
                    ProductAssemblyOld.Product_Index = data.product_Index;
                    ProductAssemblyOld.Product_Id = data.product_Id;
                    ProductAssemblyOld.Product_Name = data.product_Name;
                    ProductAssemblyOld.ProductBOM_Type = data.productBOM_Type;
                    if (data.productBOM_Type == "Promotion")
                    {
                        ProductAssemblyOld.Promotion_Date = data.promotion_Date.toDate();
                        ProductAssemblyOld.Promotion_Date_To = data.promotion_Date_To.toDate();
                    }
                    else
                    {
                        ProductAssemblyOld.Promotion_Date = null;
                        ProductAssemblyOld.Promotion_Date_To = null;
                    }
                    ProductAssemblyOld.ProductBOM_No = data.productBOM_No;
                    ProductAssemblyOld.ProductConversion_Index = data.productConversion_Index;
                    ProductAssemblyOld.ProductConversion_Id = data.productConversion_Id;
                    ProductAssemblyOld.ProductConversion_Name = data.productConversion_Name;
                    ProductAssemblyOld.ProductConversion_Ratio = data.ratio;
                    ProductAssemblyOld.Ref_No1 = data.ref_No1;
                    ProductAssemblyOld.Ref_No2 = data.ref_No2;
                    ProductAssemblyOld.Ref_No3 = data.ref_No3;
                    ProductAssemblyOld.Ref_No4 = data.ref_No4;
                    ProductAssemblyOld.Ref_No5 = data.ref_No5;
                    ProductAssemblyOld.Remark = data.remark;
                    ProductAssemblyOld.UDF_1 = null;
                    ProductAssemblyOld.UDF_2 = null;
                    ProductAssemblyOld.UDF_3 = null;
                    ProductAssemblyOld.UDF_4 = null;
                    ProductAssemblyOld.UDF_5 = null;
                    ProductAssemblyOld.IsActive = Convert.ToInt32(data.isActive);
                    ProductAssemblyOld.Update_By = data.create_By;
                    ProductAssemblyOld.Update_Date = DateTime.Now;

                    foreach (var item in data.listProductAssemblyItemViewModel)
                    {
                        var ProductAssemblyItemOld = db.ms_ProductBOMItem.Find(item.productBOMItem_Index);
                        if (ProductAssemblyItemOld != null)
                        {
                            ProductAssemblyItemOld.Product_Index = item.product_Index;
                            ProductAssemblyItemOld.Product_Id = item.product_Id;
                            ProductAssemblyItemOld.Product_Name = item.product_Name;
                            ProductAssemblyItemOld.ProductConversion_Id = item.productConversion_Id;
                            ProductAssemblyItemOld.ProductConversion_Name = item.productConversion_Name;
                            ProductAssemblyItemOld.ProductConversion_Index = item.productConversion_Index;
                            ProductAssemblyItemOld.Qty = item.qty;
                            ProductAssemblyItemOld.Ratio = item.ratio;
                            ProductAssemblyItemOld.TotalQty = item.qty * item.ratio;
                            ProductAssemblyItemOld.Ref_No1 = item.ref_No1;
                            ProductAssemblyItemOld.Ref_No2 = item.ref_No2;
                            ProductAssemblyItemOld.Ref_No3 = item.ref_No3;
                            ProductAssemblyItemOld.Ref_No4 = item.ref_No4;
                            ProductAssemblyItemOld.Ref_No5 = item.ref_No5;
                            ProductAssemblyItemOld.Remark = item.remark;
                            ProductAssemblyItemOld.UDF_1 = null;
                            ProductAssemblyItemOld.UDF_2 = null;
                            ProductAssemblyItemOld.UDF_3 = null;
                            ProductAssemblyItemOld.UDF_4 = null;
                            ProductAssemblyItemOld.UDF_5 = null;
                            ProductAssemblyItemOld.Update_By = data.create_By;
                            ProductAssemblyItemOld.IsActive = Convert.ToInt32(item.isActive);
                            ProductAssemblyItemOld.IsDelete = Convert.ToInt32(item.isDelete);
                            ProductAssemblyItemOld.Update_Date = DateTime.Now;
                            ProductAssemblyItemOld.ProductConversion_Weight_Index = item.productConversion_Weight_Index;
                            ProductAssemblyItemOld.ProductConversion_Volume_Index = item.productConversion_Volume_Index;
                        }
                        else
                        {
                            ms_ProductBOMItem resultItem = new ms_ProductBOMItem();

                            resultItem.ProductBOMItem_Index = Guid.NewGuid();
                            resultItem.ProductBOM_Index = ProductAssemblyOld.ProductBOM_Index;
                            resultItem.ProductBOM_No = ProductAssemblyOld.ProductBOM_No;
                            resultItem.Product_Index = item.product_Index;
                            resultItem.Product_Id = item.product_Id;
                            resultItem.Product_Name = item.product_Name;
                            resultItem.ProductConversion_Id = item.productConversion_Id;
                            resultItem.ProductConversion_Name = item.productConversion_Name;
                            resultItem.ProductConversion_Index = item.productConversion_Index;
                            resultItem.Qty = item.qty;
                            resultItem.Ratio = item.ratio;
                            resultItem.TotalQty = item.qty * item.ratio;
                            resultItem.Ref_No1 = item.ref_No1;
                            resultItem.Ref_No2 = item.ref_No2;
                            resultItem.Ref_No3 = item.ref_No3;
                            resultItem.Ref_No4 = item.ref_No4;
                            resultItem.Ref_No5 = item.ref_No5;
                            resultItem.Remark = item.remark;
                            resultItem.UDF_1 = null;
                            resultItem.UDF_2 = null;
                            resultItem.UDF_3 = null;
                            resultItem.UDF_4 = null;
                            resultItem.UDF_5 = null;
                            resultItem.IsActive = 1;
                            resultItem.IsDelete = 0;
                            resultItem.IsSystem = 0;
                            resultItem.Status_Id = 0;
                            resultItem.Create_By = data.create_By;
                            resultItem.Create_Date = DateTime.Now;
                            resultItem.ProductConversion_Weight_Index = item.productConversion_Weight_Index;
                            resultItem.ProductConversion_Volume_Index = item.productConversion_Volume_Index;
                            db.ms_ProductBOMItem.Add(resultItem);
                        }
                    }
                    var deleteItem = db.ms_ProductBOMItem.Where(c => !data.listProductAssemblyItemViewModel.Select(s => s.productBOMItem_Index).Contains(c.ProductBOMItem_Index)
                                           && c.ProductBOM_Index == ProductAssemblyOld.ProductBOM_Index).ToList();

                    foreach (var c in deleteItem)
                    {
                        var deleteProductAssemblyItem = db.ms_ProductBOMItem.Find(c.ProductBOMItem_Index);

                        deleteProductAssemblyItem.IsActive = 0;
                        deleteProductAssemblyItem.IsDelete = 1;
                        deleteProductAssemblyItem.Update_By = data.update_By;
                        deleteProductAssemblyItem.Update_Date = DateTime.Now;

                    }
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
                    olog.logging("SaveProductAssembly", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return "Done"; ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region find
        public ProductAssemblyViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_ProductBom.Where(c => c.ProductBOM_Index == id).FirstOrDefault();

                var result = new ProductAssemblyViewModel();


                result.productBOM_Index = queryResult.ProductBOM_Index;
                result.productBOM_No = queryResult.ProductBOM_No;
                result.productBOM_Type = queryResult.ProductBOM_Type;
                result.promotion_Date = queryResult.Promotion_Date == null ? null : queryResult.Promotion_Date.toString();
                result.promotion_Date_To = queryResult.Promotion_Date_To == null ? null :  queryResult.Promotion_Date_To.toString();
                result.product_Index = queryResult.Product_Index;
                result.product_Id = queryResult.Product_Id;
                result.product_Name = queryResult.Product_Name;
                result.productConversion_Index = queryResult.ProductConversion_Index;
                result.productConversion_Id = queryResult.ProductConversion_Id;
                result.productConversion_Name = queryResult.ProductConversion_Name;
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
                result.key = queryResult.Product_Id;
                result.isActive = queryResult.IsActive;

                var queryResultItem = db.ms_ProductBOMItem.AsQueryable();
                queryResultItem = queryResultItem.Where(c => c.ProductBOM_Index == id && c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                result.listProductAssemblyItemViewModel = new List<ProductAssemblyItemViewModel>();

                foreach (var item in queryResultItem)
                {
                    var resultItem = new ProductAssemblyItemViewModel();

                    //var queryProductAssemblyItem = db.ms_ProductBOMItem.FirstOrDefault(c => c.ProductBOMItem_Index == item.ProductBOMItem_Index);
                    resultItem.productBOMItem_Index = item.ProductBOMItem_Index;
                    resultItem.productBOM_Index = item.ProductBOM_Index;
                    resultItem.productBOM_No = item.ProductBOM_No;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.qty = item.Qty;
                    resultItem.ratio = item.Ratio;
                    resultItem.totalQty = item.TotalQty;
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
                    resultItem.key = item.Product_Id;
                    resultItem.isActive = item.IsActive;
                    resultItem.productConversion_Weight_Index = item.ProductConversion_Weight_Index;
                    resultItem.productConversion_Volume_Index = item.ProductConversion_Volume_Index;
                    //resultItem.product_Id = queryProductAssemblyItem.Product_Id;
                    //resultItem.product_Name = queryProductAssemblyItem.Product_Name;
                    //resultItem.productConversion_Name = queryProductAssemblyItem.ProductConversion_Name;
                    result.listProductAssemblyItemViewModel.Add(resultItem);
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDelete
        public Boolean getDelete(ProductAssemblyViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var ProductBom = db.ms_ProductBom.Find(data.productBOM_Index);

                if (ProductBom != null)
                {
                    ProductBom.IsActive = 0;
                    ProductBom.IsDelete = 1;


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
                        olog.logging("DeleteProductAssembly", msglog);
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

        #region autoSearchProductAssembly

        public List<ItemListViewModel> autoSearchProductAssembly(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.ms_ProductBom.Where(c => c.Product_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Product_Id,
                        key = s.Product_Id
                    }).Distinct();

                    var query2 = db.ms_ProductBom.Where(c => c.Product_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Product_Name,
                        key = s.Product_Name
                    }).Distinct();
                    var query3 = db.ms_ProductBom.Where(c => c.ProductConversion_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ProductConversion_Name,
                        key = s.ProductConversion_Name
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
    }
}


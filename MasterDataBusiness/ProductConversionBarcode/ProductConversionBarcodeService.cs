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
using static MasterDataBusiness.ViewModels.ProductConversionBarcodePopupViewModel;

namespace MasterDataBusiness
{
    public class ProductConversionBarcodeService
    {
        private MasterDataDbContext db;

        public ProductConversionBarcodeService()
        {
            db = new MasterDataDbContext();
        }

        public ProductConversionBarcodeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterProductConversionBarcode
        public actionResultProductConversionBarcodeViewModel filter(SearchProductConversionBarcodeViewModel data)
        {
            try
            {
                var query = db.View_ProductConversionBarcodeV2.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (data.changeSet != "1")
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(cc => cc.ProductConversionBarcode_Id.Contains(data.key)
                                            || cc.ProductConversionBarcode.Contains(data.key)
                                            || cc.Product_Id.Contains(data.key)
                                            || cc.Product_Name.Contains(data.key)
                                            || cc.Owner_Name.Contains(data.key)
                                            || cc.ProductConversion_Name.Contains(data.key));

                    }

                    if (!string.IsNullOrEmpty(data.productConversionBarcode))
                    {
                        query = query.Where(c => c.ProductConversionBarcode == data.productConversionBarcode);
                    }

                    if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                    {
                        query = query.Where(c => c.Product_Index == data.product_Index);
                    }

                    if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                    {
                        var dateStart = data.create_date.toBetweenDate();
                        var dateEnd = data.create_date_to.toBetweenDate();
                        query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(cc => cc.ProductConversionBarcode_Id.Contains(data.key)
                                            || cc.ProductConversionBarcode.Contains(data.key)
                                            || cc.Product_Id.Contains(data.key)
                                            || cc.Product_Name.Contains(data.key)
                                            || cc.Owner_Name.Contains(data.key)
                                            || cc.ProductConversion_Name.Contains(data.key));

                    }

                    if (!string.IsNullOrEmpty(data.productConversionBarcode))
                    {
                        query = query.Where(c => c.ProductConversionBarcode == data.productConversionBarcode);
                    }

                    if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                    {
                        query = query.Where(c => c.Product_Index == data.product_Index);
                    }
                }
                

                var Item = new List<View_ProductConversionBarcodeV2>();
                var TotalRow = new List<View_ProductConversionBarcodeV2>();

                TotalRow = query.ToList();
                
                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);
                }
                
                Item = query.OrderBy(o => o.ProductConversionBarcode_Id).ToList();

                var result = new List<SearchProductConversionBarcodeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductConversionBarcodeViewModel();

                    resultItem.productConversionBarcode_Index = item.ProductConversionBarcode_Index;
                    resultItem.productConversionBarcode_Id = item.ProductConversionBarcode_Id;
                    resultItem.productConversionBarcode = item.ProductConversionBarcode;
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.businessUnit_Index = item.BusinessUnit_Index;
                    resultItem.businessUnit_Id = item.BusinessUnit_Id;
                    resultItem.businessUnit_Name = item.BusinessUnit_Name;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.remark = item.Remark;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultProductConversionBarcodeViewModel = new actionResultProductConversionBarcodeViewModel();
                actionResultProductConversionBarcodeViewModel.itemsProductConversionBarcode = result.ToList();
                actionResultProductConversionBarcodeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultProductConversionBarcodeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(ProductConversionBarcodeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ProductConversionBarcodeOld = db.MS_ProductConversionBarcode.Find(data.productConversionBarcode_Index);

                if (ProductConversionBarcodeOld == null)
                {
                    
                    //if (data.product_Index != null)
                    //{
                    //    var query = db.MS_ProductConversionBarcode.FirstOrDefault(c => c.Product_Index == data.product_Index && c.IsActive == 1);
                    //    if (query != null)
                    //    {
                    //        return "FailProductIndex";
                    //    }
                    //}
                    //if (data.productConversion_Index != null)
                    //{
                    //    var query = db.MS_ProductConversionBarcode.FirstOrDefault(c => c.ProductConversion_Index == data.productConversion_Index && c.IsActive == 1);
                    //    if (query != null)
                    //    {
                    //        return "FailProductConversionIndex";
                    //    }
                    //}

                    if (string.IsNullOrEmpty(data.productConversionBarcode_Id))
                    {
                        data.productConversionBarcode_Id = "ProductConversionBarcode_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_ProductConversionBarcode.FirstOrDefault(c => c.ProductConversionBarcode_Id == data.productConversionBarcode_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ProductConversionBarcode_Id == data.productConversionBarcode_Id)
                                {
                                    data.productConversionBarcode_Id = "ProductConversionBarcode_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(data.productConversionBarcode_Id))
                    {
                        var query = db.MS_ProductConversionBarcode.FirstOrDefault(c => c.ProductConversionBarcode_Id == data.productConversionBarcode_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (!string.IsNullOrEmpty(data.productConversionBarcode) && data.product_Index != null && data.productConversion_Index != null)
                    {
                        var query = db.MS_ProductConversionBarcode.FirstOrDefault(c => c.ProductConversionBarcode == data.productConversionBarcode && c.Product_Index == data.product_Index && c.ProductConversion_Index == data.productConversion_Index && c.IsActive == 1);
                        if (query != null)
                        {
                            return "FailProductConversionBarcode";
                        }
                    }
                    if (!string.IsNullOrEmpty(data.productConversionBarcode) && data.product_Index != null)
                    {
                        var query = db.MS_ProductConversionBarcode.FirstOrDefault(c => c.ProductConversionBarcode == data.productConversionBarcode && c.Product_Index == data.product_Index && c.IsActive == 1);
                        if (query != null)
                        {
                            return "FailProductConversionBarcode";
                        }
                    }
                    MS_ProductConversionBarcode Model = new MS_ProductConversionBarcode();

                    Model.ProductConversionBarcode_Index = Guid.NewGuid();
                    Model.ProductConversionBarcode_Id = data.productConversionBarcode_Id;
                    Model.ProductConversionBarcode = data.productConversionBarcode;
                    Model.Product_Index = data.product_Index;
                    Model.Product_Id = data.product_Id;
                    Model.Product_Name = data.product_Name;
                    Model.ProductConversion_Index = data.productConversion_Index;
                    Model.ProductConversion_Id = data.productConversion_Id;
                    Model.ProductConversion_Name = data.productConversion_Name;
                    Model.Owner_Index = data.owner_Index;
                    Model.Owner_Id = data.owner_Id;
                    Model.Owner_Name = data.owner_Name;
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

                    db.MS_ProductConversionBarcode.Add(Model);
                }
                else
                {
                    
                    if (string.IsNullOrEmpty(data.productConversionBarcode_Id))
                    {
                        if (ProductConversionBarcodeOld.ProductConversionBarcode_Id != "")
                        {
                            data.productConversionBarcode_Id = ProductConversionBarcodeOld.ProductConversionBarcode_Id;
                        }
                    }
                    else
                    {
                        if (ProductConversionBarcodeOld.ProductConversionBarcode_Id != data.productConversionBarcode_Id)
                        {
                            var query = db.MS_ProductConversionBarcode.FirstOrDefault(c => c.ProductConversionBarcode_Id == data.productConversionBarcode_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.productConversionBarcode_Id = ProductConversionBarcodeOld.ProductConversionBarcode_Id;
                        }
                    }
                    var checkNotchange = db.MS_ProductConversionBarcode.FirstOrDefault(c => c.ProductConversionBarcode_Id == data.productConversionBarcode_Id && c.ProductConversionBarcode == data.productConversionBarcode && c.Product_Index == data.product_Index && c.IsActive == 1);
                    if (checkNotchange != null)
                    {
                        return "Done";
                    }
                    var checkProduct = db.MS_ProductConversionBarcode.FirstOrDefault(c => c.ProductConversionBarcode == data.productConversionBarcode && c.Product_Index == data.product_Index && c.IsActive == 1);
                    if (checkProduct != null)
                    {
                        var checkId = db.MS_ProductConversionBarcode.FirstOrDefault(c => c.ProductConversionBarcode_Id == data.productConversionBarcode_Id && c.IsActive == 1);
                        if (checkId != null) { return "FailProductConversionBarcode"; }
                       
                    }
                    //if (string.IsNullOrEmpty(data.productConversionBarcode))
                    //{
                    //    if (ProductConversionBarcodeOld.ProductConversionBarcode != "")
                    //    {
                    //        data.productConversionBarcode = ProductConversionBarcodeOld.ProductConversionBarcode;
                    //    }
                    //}else
                    //    if(ProductConversionBarcodeOld.ProductConversionBarcode != data.productConversionBarcode)
                    //{
                    //    var query = db.MS_ProductConversionBarcode.FirstOrDefault(c => c.ProductConversionBarcode == data.productConversionBarcode && c.Product_Index == data.product_Index && c.ProductConversion_Index == data.productConversion_Index && c.IsActive == 1);
                    //    if (query != null)
                    //    {
                    //        return "FailProductConversionBarcode";
                    //    }
                    //}
                    //else
                    //{
                    //    data.productConversionBarcode = ProductConversionBarcodeOld.ProductConversionBarcode;
                    //}
                    ProductConversionBarcodeOld.ProductConversionBarcode_Id = data.productConversionBarcode_Id;
                    ProductConversionBarcodeOld.ProductConversionBarcode = data.productConversionBarcode;
                    ProductConversionBarcodeOld.ProductConversion_Index = data.productConversion_Index;
                    ProductConversionBarcodeOld.ProductConversion_Id = data.productConversion_Id;
                    ProductConversionBarcodeOld.ProductConversion_Name = data.productConversion_Name;
                    ProductConversionBarcodeOld.Owner_Index = data.owner_Index;
                    ProductConversionBarcodeOld.Owner_Id = data.owner_Id;
                    ProductConversionBarcodeOld.Owner_Name = data.owner_Name;
                    ProductConversionBarcodeOld.Product_Index = data.product_Index;
                    ProductConversionBarcodeOld.Product_Id = data.product_Id;
                    ProductConversionBarcodeOld.Product_Name = data.product_Name;
                    ProductConversionBarcodeOld.Ref_No1 = data.ref_No1;
                    ProductConversionBarcodeOld.Ref_No2 = data.ref_No2;
                    ProductConversionBarcodeOld.Ref_No3 = data.ref_No3;
                    ProductConversionBarcodeOld.Ref_No4 = data.ref_No4;
                    ProductConversionBarcodeOld.Ref_No5 = data.ref_No5;
                    ProductConversionBarcodeOld.Remark = data.remark;
                    ProductConversionBarcodeOld.UDF_1 = null;
                    ProductConversionBarcodeOld.UDF_2 = null;
                    ProductConversionBarcodeOld.UDF_3 = null;
                    ProductConversionBarcodeOld.UDF_4 = null;
                    ProductConversionBarcodeOld.UDF_5 = null;
                    ProductConversionBarcodeOld.IsActive = Convert.ToInt32(data.isActive);
                    ProductConversionBarcodeOld.Update_By = data.create_By;
                    ProductConversionBarcodeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveProductConversionBarcode", msglog);
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
        public ProductConversionBarcodeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_ProductConversionBarcodeV2.Where(c => c.ProductConversionBarcode_Index == id).FirstOrDefault();

                var result = new ProductConversionBarcodeViewModel();

                result.productConversionBarcode_Index = queryResult.ProductConversionBarcode_Index;
                result.productConversionBarcode_Id = queryResult.ProductConversionBarcode_Id;
                result.productConversionBarcode = queryResult.ProductConversionBarcode;
                result.productConversion_Index = queryResult.ProductConversion_Index;
                result.productConversion_Id = queryResult.ProductConversion_Id;
                result.productConversion_Name = queryResult.ProductConversion_Name;
                result.product_Index = queryResult.Product_Index;
                result.product_Id = queryResult.Product_Id;
                result.product_Name = queryResult.Product_Name;
                result.owner_Index = queryResult.Owner_Index;
                result.owner_Id = queryResult.Owner_Id;
                result.owner_Name = queryResult.Owner_Name;
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
                result.key = queryResult.Owner_Id + " - " + queryResult.Owner_Name;
                result.key2 = queryResult.Product_Id + " - " + queryResult.Product_Name;
                result.key3 = queryResult.ProductConversion_Id + " - " + queryResult.ProductConversion_Name;
                result.isActive = queryResult.IsActive;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDelete
        public Boolean getDelete(ProductConversionBarcodeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Owner = db.MS_ProductConversionBarcode.Find(data.productConversionBarcode_Index);

                if (Owner != null)
                {
                    Owner.IsActive = 0;
                    Owner.IsDelete = 1;


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
                        olog.logging("DeleteProductConversionBarcode" +
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

        #region PackGetBarcode
        public List<ProductConversionBarcodeViewModel> getPackBarcode(ProductConversionBarcodeViewModel data)
        {
            try
            {
                var query = db.View_ProductConversionBarcodeV2.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                var Item = new List<View_ProductConversionBarcodeV2>();

                var result = new List<ProductConversionBarcodeViewModel>();

                Item = query.ToList();

                foreach (var item in Item)
                {
                    var resultItem = new ProductConversionBarcodeViewModel();

                    resultItem.productConversionBarcode_Index = item.ProductConversionBarcode_Index;
                    resultItem.productConversionBarcode_Id = item.ProductConversionBarcode_Id;
                    resultItem.productConversionBarcode = item.ProductConversionBarcode;
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
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


                return result; 
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region filterPopupProductConversionBarcode
        //Filter
        public actionResultProductConversionBarcodePopupViewModel filterPopupProductConversionBarcode(ProductConversionBarcodePopupViewModel data)
        {
            try
            {
                var query = db.MS_ProductConversionBarcode.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (data.listProductConversionBarcodeViewModel != null)
                {
                    foreach (var item in data.listProductConversionBarcodeViewModel)
                    {
                        query = query.Where(q => q.ProductConversionBarcode_Index != item.productConversionBarcode_Index);
                    }

                }


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.ProductConversionBarcode_Id.Contains(data.key)
                                          || c.ProductConversionBarcode.Contains(data.key));
                }
                if (!string.IsNullOrEmpty(data.productConversionBarcode_Id))
                {
                    query = query.Where(c => c.ProductConversionBarcode_Id.Contains(data.productConversionBarcode_Id));
                }
                if (!string.IsNullOrEmpty(data.productConversionBarcode))
                {
                    query = query.Where(c => c.ProductConversionBarcode.Contains(data.productConversionBarcode));

                }

                var Item = new List<MS_ProductConversionBarcode>();
                var TotalRow = new List<MS_ProductConversionBarcode>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ProductConversionBarcode_Id).ToList();

                var result = new List<ProductConversionBarcodePopupViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new ProductConversionBarcodePopupViewModel();


                    resultItem.productConversionBarcode_Index = item.ProductConversionBarcode_Index;
                    resultItem.productConversionBarcode_Id = item.ProductConversionBarcode_Id;
                    resultItem.productConversionBarcode
 = item.ProductConversionBarcode;
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
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

                var actionResultProductConversionBarcodePopupViewModel = new actionResultProductConversionBarcodePopupViewModel();
                actionResultProductConversionBarcodePopupViewModel.itemsProductConversionBarcodePopup = result.ToList();
                actionResultProductConversionBarcodePopupViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultProductConversionBarcodePopupViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region filterProductConversionBarcode
        public ProductConversionBarcodeViewModel filterProductConversionBarcode(Guid id,Guid i, string data)
        {
            try
            {

                var queryResult = db.MS_ProductConversionBarcode.Where(c => c.ProductConversion_Index == id && c.Product_Index == i && c.IsActive == 1);
               
                    if ( data !="null")
                    {
                        queryResult = queryResult.Where(c => c.ProductConversionBarcode.Contains(data)
                                             || c.ProductConversionBarcode_Id.Contains(data)
                                             || c.Owner_Name.Contains(data)
                                             || c.Product_Name.Contains(data)
                                             || c.ProductConversion_Name.Contains(data));
                    }
               
                var result = new ProductConversionBarcodeViewModel();

                result.listProductConversionBarcodeItemViewModel = new List<ProductConversionBarcodeItemViewModel>();

                foreach (var item in queryResult)
                {
                    var resultItem = new ProductConversionBarcodeItemViewModel();
                    resultItem.productConversionBarcode_Index = item.ProductConversionBarcode_Index;
                    resultItem.productConversionBarcode_Id = item.ProductConversionBarcode_Id;
                    resultItem.productConversionBarcode = item.ProductConversionBarcode;
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.remark = item.Remark;
        


                    result.listProductConversionBarcodeItemViewModel.Add(resultItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        
        #region dropdown
        public List<ProductConversionBarcodeViewModel> productConversionBarcodeDropdown(ProductConversionBarcodeViewModel data)
        {
            try
            {
                var result = new List<ProductConversionBarcodeViewModel>();

                var query = db.MS_ProductConversionBarcode.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.ProductConversionBarcode_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new ProductConversionBarcodeViewModel();
                    resultItem.productConversionBarcode_Index = item.ProductConversionBarcode_Index;
                    resultItem.productConversionBarcode_Id = item.ProductConversionBarcode_Id;
                    resultItem.productConversionBarcode = item.ProductConversionBarcode;
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
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

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region export
        public actionResultProductConversionBarcodeViewModel export(SearchProductConversionBarcodeViewModel data)
        {
            try
            {
                var query = db.View_ProductConversionBarcodeV2.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (data.changeSet != "1")
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(cc => cc.ProductConversionBarcode_Id.Contains(data.key)
                                            || cc.ProductConversionBarcode.Contains(data.key)
                                            || cc.Product_Id.Contains(data.key)
                                            || cc.Product_Name.Contains(data.key)
                                            || cc.Owner_Name.Contains(data.key)
                                            || cc.ProductConversion_Name.Contains(data.key));

                    }

                    if (!string.IsNullOrEmpty(data.productConversionBarcode))
                    {
                        query = query.Where(c => c.ProductConversionBarcode == data.productConversionBarcode);
                    }

                    if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                    {
                        query = query.Where(c => c.Product_Index == data.product_Index);
                    }

                    if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                    {
                        var dateStart = data.create_date.toBetweenDate();
                        var dateEnd = data.create_date_to.toBetweenDate();
                        query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(cc => cc.ProductConversionBarcode_Id.Contains(data.key)
                                            || cc.ProductConversionBarcode.Contains(data.key)
                                            || cc.Product_Id.Contains(data.key)
                                            || cc.Product_Name.Contains(data.key)
                                            || cc.Owner_Name.Contains(data.key)
                                            || cc.ProductConversion_Name.Contains(data.key));

                    }

                    if (!string.IsNullOrEmpty(data.productConversionBarcode))
                    {
                        query = query.Where(c => c.ProductConversionBarcode == data.productConversionBarcode);
                    }

                    if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                    {
                        query = query.Where(c => c.Product_Index == data.product_Index);
                    }
                }

                var Item = new List<View_ProductConversionBarcodeV2>();
                var TotalRow = new List<View_ProductConversionBarcodeV2>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.ProductConversionBarcode_Id).ToList();

                var result = new List<SearchProductConversionBarcodeViewModel>();
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new SearchProductConversionBarcodeViewModel();
                    resultItem.rowNum = num + 1;
                    resultItem.productConversionBarcode_Index = item.ProductConversionBarcode_Index;
                    resultItem.productConversionBarcode_Id = item.ProductConversionBarcode_Id;
                    resultItem.productConversionBarcode = item.ProductConversionBarcode;
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.businessUnit_Index = item.BusinessUnit_Index;
                    resultItem.businessUnit_Id = item.BusinessUnit_Id == null ? "" : item.BusinessUnit_Id;
                    resultItem.businessUnit_Name = item.BusinessUnit_Name == null ? "" : item.BusinessUnit_Name;
                    resultItem.ref_No1 = item.Ref_No1 == null ? "" : item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2 == null ? "" : item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3 == null ? "" : item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4 == null ? "" : item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5 == null ? "" : item.Ref_No5;
                    resultItem.remark = item.Remark == null ? "" : item.Remark;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var actionResultProductConversionBarcodeViewModel = new actionResultProductConversionBarcodeViewModel();
                actionResultProductConversionBarcodeViewModel.itemsProductConversionBarcode = result.ToList();

                return actionResultProductConversionBarcodeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //public actionResultProductConversionBarcodeViewModel Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            string pstring = "";
        //            var strwheres = new SqlParameter("@strwhere", pstring);
        //            var PageNumber = new SqlParameter("@PageNumber", 1);
        //            var RowspPage = new SqlParameter("@RowspPage", 10000);

        //            var countx = context.View_ProductConversionBarcode.FromSql("sp_GetView_ProductConversionBarcode @strwhere , @PageNumber , @RowspPage ", strwheres, PageNumber, RowspPage).ToList();

        //            var count = countx.Count();

        //            var strwhere1 = new SqlParameter("@strwhere", pstring);
        //            var PageNumber1 = new SqlParameter("@PageNumber", 1);
        //            var RowspPage1 = new SqlParameter("@RowspPage", 30);
        //            //var queryResultTotal = context.MS_Product.FromSql("sp_GetProductByPagination @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber, RowspPage).ToList();
        //            var queryResultTotal = context.View_ProductConversionBarcode.FromSql("sp_GetView_ProductConversionBarcode @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).ToList();

        //            //var queryResult = context.MS_ProductConversionBarcode.FromSql("sp_GetProductConversionBarcode").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<ProductConversionBarcodeViewModel>();
        //            foreach (var item in queryResultTotal)
        //            {
        //                var resultItem = new ProductConversionBarcodeViewModel();

        //                resultItem.ProductConversionBarcodeIndex = item.ProductConversionBarcode_Index;
        //                if (item.ProductConversion_Index != null)
        //                {
        //                    var newSqlWhere = " and ProductConversion_Index = '" + item.ProductConversion_Index + "'";
        //                    var strwhere = new SqlParameter("@strwhere", newSqlWhere);
        //                    var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere", strwhere).FirstOrDefault();
        //                    resultItem.ProductConversionIndex = itemList.ProductConversion_Index;
        //                    resultItem.ProductConversionName = itemList.ProductConversion_Name;
        //                }
        //                if (item.Product_Index != null)
        //                {
        //                    var newSqlWhere = " and Product_Index = '" + item.Product_Index + "'";
        //                    var strwhere = new SqlParameter("@strwhere", newSqlWhere);
        //                    var itemList = context.MS_Product.FromSql("sp_GetProduct @strwhere", strwhere).FirstOrDefault();
        //                    resultItem.ProductIndex = itemList.Product_Index;
        //                    resultItem.ProductName = itemList.Product_Name;
        //                }
        //                if (item.Owner_Index != null)
        //                {
        //                    var newSqlWhere = " and Owner_Index = '" + item.Owner_Index + "'";
        //                    var strwhere = new SqlParameter("@strwhere", newSqlWhere);
        //                    var itemList = context.MS_Owner.FromSql("sp_GetOwner @strwhere", strwhere).FirstOrDefault();
        //                    resultItem.OwnerIndex = itemList.Owner_Index;
        //                    resultItem.OwnerName = itemList.Owner_Name;
        //                }
        //                resultItem.ProductConversionBarcodeId = item.ProductConversionBarcode_Id;
        //                resultItem.ProductConversionBarcode = item.ProductConversionBarcode;
        //                resultItem.IsActive = item.IsActive;
        //                resultItem.IsDelete = item.IsDelete;
        //                resultItem.IsSystem = item.IsSystem;
        //                resultItem.StatusId = item.Status_Id;
        //                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                resultItem.CreateBy = item.Create_By;
        //                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                resultItem.UpdateBy = item.Update_By;
        //                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.CancelBy = item.Cancel_By;

        //                result.Add(resultItem);
        //            }


        //            var actionResultProductCB = new actionResultProductConversionBarcodeViewModel();
        //            actionResultProductCB.itemsProductConversionBarcode = result.ToList();
        //            actionResultProductCB.pagination = new Pagination() { TotalRow = count, CurrentPage = 1, NumPerPage = 30 };

        //            return actionResultProductCB;
        //            //return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public String SaveChanges(ProductConversionBarcodeViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.ProductConversionBarcodeIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.ProductConversionBarcodeIndex = Guid.NewGuid();
        //            }
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int isSystem = 0;
        //            int statusId = 0;
        //            var ProductConversionBarcode_Index = new SqlParameter("ProductConversionBarcode_Index", data.ProductConversionBarcodeIndex);
        //            var ProductConversion_Index = new SqlParameter("ProductConversion_Index", data.ProductConversionIndex);
        //            var Product_Index = new SqlParameter("Product_Index", data.ProductIndex);
        //            var Owner_Index = new SqlParameter("Owner_Index", data.OwnerIndex);
        //            if (data.ProductConversionBarcodeId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "ProductConversionBarcodeId");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.ProductConversionBarcodeId = resultParameter.Value.ToString();
        //            }
        //            var ProductConversionBarcode_Id = new SqlParameter("ProductConversionBarcode_Id", data.ProductConversionBarcodeId);
        //            var ProductConversionBarcode = new SqlParameter("ProductConversionBarcode", data.ProductConversionBarcode);
        //            var IsActive = new SqlParameter("IsActive", isactive);
        //            var IsDelete = new SqlParameter("IsDelete", isdelete);
        //            var IsSystem = new SqlParameter("IsSystem", isSystem);
        //            var Status_Id = new SqlParameter("Status_Id", statusId);
        //            var Create_By = new SqlParameter("Create_By", "");
        //            var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        //            var Update_By = new SqlParameter("Update_By", "");
        //            var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        //            var Cancel_By = new SqlParameter("Cancel_By", "");
        //            var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ProductConversionBarcode  @ProductConversionBarcode_Index,@ProductConversion_Index,@Product_Index,@Owner_Index,@ProductConversionBarcode_Id,@ProductConversionBarcode,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ProductConversionBarcode_Index, ProductConversion_Index, Product_Index, Owner_Index, ProductConversionBarcode_Id, ProductConversionBarcode, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ProductConversionBarcodeViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductConversionBarcode.FromSql("sp_GetProductConversionBarcode").ToList();
        //            queryResult.Where(c => c.ProductConversionBarcode_Index == id);

        //            var result = new List<ProductConversionBarcodeViewModel>();
        //            foreach (var item in queryResult.Where(c => c.ProductConversionBarcode_Index == id))
        //            {
        //                var resultItem = new ProductConversionBarcodeViewModel();
        //                resultItem.ProductConversionBarcodeIndex = item.ProductConversionBarcode_Index;
        //                if (item.ProductConversion_Index != null)
        //                {
        //                    var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.ProductConversion_Index == item.ProductConversion_Index).FirstOrDefault();
        //                    resultItem.ProductConversionIndex = itemList.ProductConversion_Index;
        //                    resultItem.ProductConversionName = itemList.ProductConversion_Name;
        //                }
        //                if (item.Product_Index != null)
        //                {
        //                    var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                    resultItem.ProductIndex = itemList.Product_Index;
        //                    resultItem.ProductName = itemList.Product_Name;
        //                }
        //                if (item.Owner_Index != null)
        //                {
        //                    var itemList = context.MS_Owner.FromSql("sp_GetOwner").Where(c => c.Owner_Index == item.Owner_Index).FirstOrDefault();
        //                    resultItem.OwnerIndex = itemList.Owner_Index;
        //                    resultItem.OwnerName = itemList.Owner_Name;
        //                }
        //                resultItem.ProductConversionBarcodeId = item.ProductConversionBarcode_Id;
        //                resultItem.ProductConversionBarcode = item.ProductConversionBarcode;
        //                resultItem.IsActive = item.IsActive;
        //                resultItem.IsDelete = item.IsDelete;
        //                resultItem.IsSystem = item.IsSystem;
        //                resultItem.StatusId = item.Status_Id;
        //                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                resultItem.CreateBy = item.Create_By;
        //                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                resultItem.UpdateBy = item.Update_By;
        //                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.CancelBy = item.Cancel_By;
        //                result.Add(resultItem);
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ProductConversionBarcodeViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductConversionBarcode.FromSql("sp_GetProductConversionBarcode").ToList();
        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<ProductConversionBarcodeViewModel>();
        //            foreach (var item in queryResult.Where(c => c.ProductConversionBarcode_Index == id))
        //            {
        //                var ProductConversionBarcode_Index = new SqlParameter("ProductConversionBarcode_Index", item.ProductConversionBarcode_Index);
        //                var ProductConversion_Index = new SqlParameter("ProductConversion_Index", item.ProductConversion_Index);
        //                var Product_Index = new SqlParameter("Product_Index", item.Product_Index);
        //                var Owner_Index = new SqlParameter("Owner_Index", item.Owner_Index);
        //                var ProductConversionBarcode_Id = new SqlParameter("ProductConversionBarcode_Id", item.ProductConversionBarcode_Id);
        //                var ProductConversionBarcode = new SqlParameter("ProductConversionBarcode", item.ProductConversionBarcode);
        //                var IsActive = new SqlParameter("IsActive", isactive);
        //                var IsDelete = new SqlParameter("IsDelete", isdelete);
        //                var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
        //                var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
        //                var Create_By = new SqlParameter("Create_By", "");
        //                var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        //                var Update_By = new SqlParameter("Update_By", "");
        //                var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        //                var Cancel_By = new SqlParameter("Cancel_By", "");
        //                var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ProductConversionBarcode  @ProductConversionBarcode_Index,@ProductConversion_Index,@Product_Index,@Owner_Index,@ProductConversionBarcode_Id,@ProductConversionBarcode,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ProductConversionBarcode_Index, ProductConversion_Index, Product_Index, Owner_Index, ProductConversionBarcode_Id, ProductConversionBarcode, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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
        //public actionResultProductConversionBarcodeViewModel search(ProductConversionBarcodeViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<ProductConversionBarcodeViewModel>();
        //            var queryResult = context.MS_ProductConversionBarcode.FromSql("sp_GetProductConversionBarcode").Where(c => c.IsActive == 1 && c.IsDelete == 0)
        //                                            .ToList();
        //            if (data.ProductConversionBarcodeId != "" && data.ProductConversionBarcodeId != null)
        //            {
        //                pwhereFilter = " And ProductConversionBarcode_Id = '" + data.ProductConversionBarcodeId + "'";
        //            }
        //            else
        //            {
        //                pwhereFilter = "";
        //            }

        //            if (data.ProductConversionBarcode != "" && data.ProductConversionBarcode != null)
        //            {
        //                pwhereFilter += " And ProductConversionBarcode like N'%" + data.ProductConversionBarcode + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }

        //            //if (data.ProductName != "" && data.ProductName != null)
        //            //{
        //            //    pwhereFilter += " And isActive = '" + 1 + "'";
        //            //    pwhereFilter += " And isDelete = '" + 0 + "'";
        //            //    pwhereFilter += " And Product_Name like N'%" + data.ProductName + "%'";
        //            //}

        //            //if (data.OwnerName != "" && data.OwnerName != null)
        //            //{
        //            //    pwhereFilter += " And isActive = '" + 1 + "'";
        //            //    pwhereFilter += " And isDelete = '" + 0 + "'";
        //            //    pwhereFilter += " And Owner_Name like N'%" + data.OwnerName + "%'";
        //            //}

        //            if (data.ProductConversionName != "" && data.ProductConversionName != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                pwhereFilter += " And ProductConversion_Name like N'%" + data.ProductConversionName + "%'";
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

        //            var queryResultTotal = context.View_ProductConversionBarcode.FromSql("sp_GetView_ProductConversionBarcode @strwhere , @PageNumber , @RowspPage ", strwheres, PageNumber, RowspPage).ToList();


        //            var strwhere1 = new SqlParameter("@strwhere", pwhereFilter);
        //            var PageNumber1 = new SqlParameter("@PageNumber", data.CurrentPage);
        //            var RowspPage1 = new SqlParameter("@RowspPage", data.PerPage);
        //            var querys = context.View_ProductConversionBarcode.FromSql("sp_GetView_ProductConversionBarcode @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).ToList();

        //            int count = 0;
        //            int newCount = 0;

        //            if (data.ProductConversionBarcodeId != "" && data.ProductConversionBarcodeId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.View_ProductConversionBarcode.FromSql("sp_GetView_ProductConversionBarcode @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new ProductConversionBarcodeViewModel();

        //                    resultItem.ProductConversionBarcodeIndex = item.ProductConversionBarcode_Index;
        //                    if (item.ProductConversion_Index != null)
        //                    {
        //                        var newSqlWhere = " and ProductConversion_Index = '" + item.ProductConversion_Index + "'";
        //                        var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                        var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere2", strwhere2).FirstOrDefault();
        //                        resultItem.ProductConversionIndex = itemList.ProductConversion_Index;
        //                        resultItem.ProductConversionName = itemList.ProductConversion_Name;
        //                    }
        //                    if (item.Product_Index != null)
        //                    {
        //                        var newSqlWhere = " and Product_Index = '" + item.Product_Index + "'";
        //                        var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                        var itemList = context.MS_Product.FromSql("sp_GetProduct @strwhere2", strwhere2).FirstOrDefault();
        //                        resultItem.ProductIndex = itemList.Product_Index;
        //                        resultItem.ProductName = itemList.Product_Name;
        //                    }
        //                    if (item.Owner_Index != null)
        //                    {
        //                        var newSqlWhere = " and Owner_Index = '" + item.Owner_Index + "'";
        //                        var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                        var itemList = context.MS_Owner.FromSql("sp_GetOwner @strwhere2", strwhere2).FirstOrDefault();
        //                        resultItem.OwnerIndex = itemList.Owner_Index;
        //                        resultItem.OwnerName = itemList.Owner_Name;
        //                    }
        //                    resultItem.ProductConversionBarcodeId = item.ProductConversionBarcode_Id;
        //                    resultItem.ProductConversionBarcode = item.ProductConversionBarcode;
        //                    resultItem.IsActive = item.IsActive;
        //                    resultItem.IsDelete = item.IsDelete;
        //                    resultItem.IsSystem = item.IsSystem;
        //                    resultItem.StatusId = item.Status_Id;
        //                    resultItem.IsActive = item.IsActive;
        //                    resultItem.IsDelete = item.IsDelete;
        //                    resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                    resultItem.CreateBy = item.Create_By;
        //                    resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                    resultItem.UpdateBy = item.Update_By;
        //                    resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                    resultItem.CancelBy = item.Cancel_By;

        //                    result.Add(resultItem);
        //                }
        //            }
        //            else if (data.ProductConversionBarcode != "" && data.ProductConversionBarcode != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.View_ProductConversionBarcode.FromSql("sp_GetView_ProductConversionBarcode @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new ProductConversionBarcodeViewModel();

        //                    resultItem.ProductConversionBarcodeIndex = item.ProductConversionBarcode_Index;
        //                    if (item.ProductConversion_Index != null)
        //                    {
        //                        var newSqlWhere = " and ProductConversion_Index = '" + item.ProductConversion_Index + "'";
        //                        var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                        var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere2", strwhere2).FirstOrDefault();
        //                        resultItem.ProductConversionIndex = itemList.ProductConversion_Index;
        //                        resultItem.ProductConversionName = itemList.ProductConversion_Name;
        //                    }
        //                    if (item.Product_Index != null)
        //                    {
        //                        var newSqlWhere = " and Product_Index = '" + item.Product_Index + "'";
        //                        var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                        var itemList = context.MS_Product.FromSql("sp_GetProduct @strwhere2", strwhere2).FirstOrDefault();
        //                        resultItem.ProductIndex = itemList.Product_Index;
        //                        resultItem.ProductName = itemList.Product_Name;
        //                    }
        //                    if (item.Owner_Index != null)
        //                    {
        //                        var newSqlWhere = " and Owner_Index = '" + item.Owner_Index + "'";
        //                        var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                        var itemList = context.MS_Owner.FromSql("sp_GetOwner @strwhere2", strwhere2).FirstOrDefault();
        //                        resultItem.OwnerIndex = itemList.Owner_Index;
        //                        resultItem.OwnerName = itemList.Owner_Name;
        //                    }
        //                    resultItem.ProductConversionBarcodeId = item.ProductConversionBarcode_Id;
        //                    resultItem.ProductConversionBarcode = item.ProductConversionBarcode;
        //                    resultItem.IsActive = item.IsActive;
        //                    resultItem.IsDelete = item.IsDelete;
        //                    resultItem.IsSystem = item.IsSystem;
        //                    resultItem.StatusId = item.Status_Id;
        //                    resultItem.IsActive = item.IsActive;
        //                    resultItem.IsDelete = item.IsDelete;
        //                    resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                    resultItem.CreateBy = item.Create_By;
        //                    resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                    resultItem.UpdateBy = item.Update_By;
        //                    resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                    resultItem.CancelBy = item.Cancel_By;

        //                    result.Add(resultItem);
        //                }
        //            }
        //            else if (data.ProductName != "" && data.ProductName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And Product_Name like N'%" + data.ProductName + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_Product.FromSql("sp_GetProduct @strwhere ", pstrwhere1).ToList();
        //                newCount = dataList.Count;


        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new ProductConversionBarcodeViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.Product_Index == ItemList.Product_Index)
        //                        {
        //                            resultItem.ProductConversionBarcodeIndex = item.ProductConversionBarcode_Index;
        //                            if (item.ProductConversion_Index != null)
        //                            {
        //                                var newSqlWhere = " and ProductConversion_Index = '" + item.ProductConversion_Index + "'";
        //                                var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                                var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere2", strwhere2).FirstOrDefault();
        //                                resultItem.ProductConversionIndex = itemList.ProductConversion_Index;
        //                                resultItem.ProductConversionName = itemList.ProductConversion_Name;
        //                            }

        //                            resultItem.ProductIndex = ItemList.Product_Index;
        //                            resultItem.ProductName = ItemList.Product_Name;

        //                            if (item.Owner_Index != null)
        //                            {
        //                                var newSqlWhere = " and Owner_Index = '" + item.Owner_Index + "'";
        //                                var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                                var itemList = context.MS_Owner.FromSql("sp_GetOwner @strwhere2", strwhere2).FirstOrDefault();
        //                                resultItem.OwnerIndex = itemList.Owner_Index;
        //                                resultItem.OwnerName = itemList.Owner_Name;
        //                            }
        //                            resultItem.ProductConversionBarcodeId = item.ProductConversionBarcode_Id;
        //                            resultItem.ProductConversionBarcode = item.ProductConversionBarcode;
        //                            resultItem.IsActive = item.IsActive;
        //                            resultItem.IsDelete = item.IsDelete;
        //                            resultItem.IsSystem = item.IsSystem;
        //                            resultItem.StatusId = item.Status_Id;
        //                            resultItem.IsActive = item.IsActive;
        //                            resultItem.IsDelete = item.IsDelete;
        //                            resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                            resultItem.CreateBy = item.Create_By;
        //                            resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                            resultItem.UpdateBy = item.Update_By;
        //                            resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                            resultItem.CancelBy = item.Cancel_By;

        //                            result.Add(resultItem);
        //                        }

        //                    }

        //                }
        //            }
        //            else if (data.OwnerName != "" && data.OwnerName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike += " And Owner_Name like N'%" + data.OwnerName + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_Owner.FromSql("sp_GetOwner @strwhere ", pstrwhere1).ToList();
        //                foreach (var ItemList in dataList)
        //                {
        //                    pwhereLike = " And Owner_Index = '" + ItemList.Owner_Index + "'";
        //                    pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);

        //                    var _query = context.View_ProductConversionBarcode.FromSql("sp_GetView_ProductConversionBarcode @strwhere ", pstrwhere1).ToList();
        //                    var resultItem = new ProductConversionBarcodeViewModel();
        //                    foreach (var item in _query)
        //                    {

        //                        resultItem.ProductConversionBarcodeIndex = item.ProductConversionBarcode_Index;
        //                        if (item.ProductConversion_Index != null)
        //                        {
        //                            var newSqlWhere = " and ProductConversion_Index = '" + item.ProductConversion_Index + "'";
        //                            var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                            var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere2", strwhere2).FirstOrDefault();
        //                            resultItem.ProductConversionIndex = itemList.ProductConversion_Index;
        //                            resultItem.ProductConversionName = itemList.ProductConversion_Name;
        //                        }
        //                        if (item.Product_Index != null)
        //                        {
        //                            var newSqlWhere = " and Product_Index = '" + item.Product_Index + "'";
        //                            var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                            var itemList = context.MS_Product.FromSql("sp_GetProduct @strwhere2", strwhere2).FirstOrDefault();
        //                            resultItem.ProductIndex = itemList.Product_Index;
        //                            resultItem.ProductName = itemList.Product_Name;
        //                        }
        //                        resultItem.OwnerIndex = ItemList.Owner_Index;
        //                        resultItem.OwnerName = ItemList.Owner_Name;

        //                        resultItem.ProductConversionBarcodeId = item.ProductConversionBarcode_Id;
        //                        resultItem.ProductConversionBarcode = item.ProductConversionBarcode;
        //                        resultItem.IsActive = item.IsActive;
        //                        resultItem.IsDelete = item.IsDelete;
        //                        resultItem.IsSystem = item.IsSystem;
        //                        resultItem.StatusId = item.Status_Id;
        //                        resultItem.IsActive = item.IsActive;
        //                        resultItem.IsDelete = item.IsDelete;
        //                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                        resultItem.CreateBy = item.Create_By;
        //                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                        resultItem.UpdateBy = item.Update_By;
        //                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                        resultItem.CancelBy = item.Cancel_By;

        //                        result.Add(resultItem);
        //                    }

        //                }
        //            }
        //            else if (data.ProductConversionName != "" && data.ProductConversionName != null)
        //            {
        //                //pwhereLike += " And isActive = '" + 1 + "'";
        //                //pwhereLike += " And isDelete = '" + 0 + "'";
        //                //pwhereLike += " And ProductConversion_Name like N'%" + data.ProductConversionName + "%'";
        //                //var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                //var dataList = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere ", pstrwhere1).ToList();
        //                //var dataList = context.View_ProductConversionBarcode.FromSql("sp_GetView_ProductConversionBarcode @strwhere ", pstrwhere1).ToList();
        //                foreach (var item in querys)
        //                {
        //                    //pwhereLike = " And ProductConversion_Index = '" + item.ProductConversion_Index + "'";
        //                    //pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);

        //                    var resultItem = new ProductConversionBarcodeViewModel();
        //                    resultItem.ProductConversionBarcodeIndex = item.ProductConversionBarcode_Index;
        //                    resultItem.ProductConversionIndex = item.ProductConversion_Index;
        //                    resultItem.ProductConversionName = item.ProductConversion_Name;

        //                    if (item.Product_Index != null)
        //                    {
        //                        var newSqlWhere = " and Product_Index = '" + item.Product_Index + "'";
        //                        var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                        var itemList = context.MS_Product.FromSql("sp_GetProduct @strwhere2", strwhere2).FirstOrDefault();
        //                        resultItem.ProductIndex = itemList.Product_Index;
        //                        resultItem.ProductName = itemList.Product_Name;
        //                    }
        //                    if (item.Owner_Index != null)
        //                    {
        //                        var newSqlWhere = " and Owner_Index = '" + item.Owner_Index + "'";
        //                        var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                        var itemList = context.MS_Owner.FromSql("sp_GetOwner @strwhere2", strwhere2).FirstOrDefault();
        //                        resultItem.OwnerIndex = itemList.Owner_Index;
        //                        resultItem.OwnerName = itemList.Owner_Name;
        //                    }
        //                    resultItem.ProductConversionBarcodeId = item.ProductConversionBarcode_Id;
        //                    resultItem.ProductConversionBarcode = item.ProductConversionBarcode;
        //                    resultItem.IsActive = item.IsActive;
        //                    resultItem.IsDelete = item.IsDelete;
        //                    resultItem.IsSystem = item.IsSystem;
        //                    resultItem.StatusId = item.Status_Id;
        //                    resultItem.IsActive = item.IsActive;
        //                    resultItem.IsDelete = item.IsDelete;
        //                    resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                    resultItem.CreateBy = item.Create_By;
        //                    resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                    resultItem.UpdateBy = item.Update_By;
        //                    resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                    resultItem.CancelBy = item.Cancel_By;

        //                    result.Add(resultItem);

        //                }
        //            }

        //            else
        //            {
        //                if (result.Count == 0)
        //                {

        //                    foreach (var item in querys)
        //                    {
        //                        var resultItem = new ProductConversionBarcodeViewModel();
        //                        resultItem.ProductConversionBarcodeIndex = item.ProductConversionBarcode_Index;
        //                        if (item.ProductConversion_Index != null)
        //                        {
        //                            var newSqlWhere = " and ProductConversion_Index = '" + item.ProductConversion_Index + "'";
        //                            var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                            var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere2", strwhere2).FirstOrDefault();
        //                            resultItem.ProductConversionIndex = itemList.ProductConversion_Index;
        //                            resultItem.ProductConversionName = itemList.ProductConversion_Name;
        //                        }
        //                        if (item.Product_Index != null)
        //                        {
        //                            var newSqlWhere = " and Product_Index = '" + item.Product_Index + "'";
        //                            var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                            var itemList = context.MS_Product.FromSql("sp_GetProduct @strwhere2", strwhere2).FirstOrDefault();
        //                            resultItem.ProductIndex = itemList.Product_Index;
        //                            resultItem.ProductName = itemList.Product_Name;
        //                        }
        //                        if (item.Owner_Index != null)
        //                        {
        //                            var newSqlWhere = " and Owner_Index = '" + item.Owner_Index + "'";
        //                            var strwhere2 = new SqlParameter("@strwhere2", newSqlWhere);
        //                            var itemList = context.MS_Owner.FromSql("sp_GetOwner @strwhere2", strwhere2).FirstOrDefault();
        //                            resultItem.OwnerIndex = itemList.Owner_Index;
        //                            resultItem.OwnerName = itemList.Owner_Name;
        //                        }
        //                        resultItem.ProductConversionBarcodeId = item.ProductConversionBarcode_Id;
        //                        resultItem.ProductConversionBarcode = item.ProductConversionBarcode;
        //                        resultItem.IsActive = item.IsActive;
        //                        resultItem.IsDelete = item.IsDelete;
        //                        resultItem.IsSystem = item.IsSystem;
        //                        resultItem.StatusId = item.Status_Id;
        //                        resultItem.IsActive = item.IsActive;
        //                        resultItem.IsDelete = item.IsDelete;
        //                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                        resultItem.CreateBy = item.Create_By;
        //                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                        resultItem.UpdateBy = item.Update_By;
        //                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                        resultItem.CancelBy = item.Cancel_By;

        //                        result.Add(resultItem);


        //                    }


        //                }
        //            }


        //            if (newCount != 0)
        //            {
        //                count = newCount;
        //            }
        //            else
        //            {
        //                count = queryResultTotal.Count;
        //            }
        //            var actionResultProductCB = new actionResultProductConversionBarcodeViewModel();
        //            actionResultProductCB.itemsProductConversionBarcode = result.ToList();
        //            actionResultProductCB.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, NumPerPage = data.NumPerPage };

        //            return actionResultProductCB;



        //            //return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ScanBarcodeViewModel> ScanBarcode(ScanBarcodeViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {

        //            var result = new List<ScanBarcodeViewModel>();
        //            var SqlWhere = "";

        //            SqlWhere += " and ProductConversionBarcode = '" + data.ProductConversionBarcode + "'" + " and Product_Index = '" + data.ProductIndex + "'";

        //            var strwhere = new SqlParameter("@strwhere", SqlWhere);
        //            var findConversion = context.MS_ProductConversionBarcode.FromSql("sp_GetProductConversionBarcode @strwhere", strwhere).FirstOrDefault();


        //            var SqlWhere1 = "";
        //            if (findConversion != null)
        //            {
        //                SqlWhere1 += " and ProductConversion_Index = '" + findConversion.ProductConversion_Index + "'";
        //            }
        //            else
        //            {
        //                SqlWhere1 = " and ProductConversion_Name = '" + "00000000000000" + "'";
        //            }

        //            var strwhere1 = new SqlParameter("@strwhere1", SqlWhere1);
        //            var chkConversion = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere1", strwhere1).FirstOrDefault();


        //            //------------------------------------------ Check Product ที่ซ้ำกัน ------------------------------------------//
        //            var SqlWhere2 = " and Product_Id = N'" + data.ProductId + "' ";
        //            var strwhere2 = new SqlParameter("@strwhere2", SqlWhere2);
        //            var chkDupilcateProduct = context.MS_Product.FromSql("sp_GetProduct @strwhere2", strwhere2).ToList();


        //            if (chkDupilcateProduct.Count > 1)
        //            {
        //                var SqlWhere3 = "";
        //                var dataResult = chkDupilcateProduct.Where(c => c.ProductConversion_Name == data.ProductConversionName && c.ProductConversion_Index == data.ProductConversionIndex).FirstOrDefault();

        //                if (data.ProductConversionName != null && data.ProductConversionIndex != null)
        //                {
        //                    SqlWhere3 = " and Product_Index = '" + dataResult.Product_Index + "'" + " and ProductConversion_Index = '" + dataResult.ProductConversion_Index + "'";
        //                }
        //                var strwhere3 = new SqlParameter("@strwhere3", SqlWhere3);

        //                //------------------------------------------ หาเลข Barcode ตัวที่ถูกใช้จริงจากตัว Product ที่ซ้ำกัน ------------------------------------------//
        //                var queryfindBarcode = context.MS_ProductConversionBarcode.FromSql("sp_GetProductConversionBarcode @strwhere3", strwhere3).FirstOrDefault();
        //                if (queryfindBarcode.ProductConversionBarcode == data.ProductConversionBarcode)
        //                {
        //                    if (dataResult != null)
        //                    {
        //                        var resultItem = new ScanBarcodeViewModel();
        //                        resultItem.ProductId = dataResult.Product_Id;
        //                        resultItem.ProductIndex = dataResult.Product_Index;
        //                        resultItem.ProductName = dataResult.Product_Name;
        //                        resultItem.ProductConversionRatio = chkConversion.ProductConversion_Ratio;
        //                        resultItem.ProductConversionName = chkConversion.ProductConversion_Name;
        //                        if (queryfindBarcode != null)
        //                        {
        //                            resultItem.ProductConversionBarcodeIndex = queryfindBarcode.ProductConversionBarcode_Index;
        //                            resultItem.ProductConversionBarcodeId = queryfindBarcode.ProductConversionBarcode_Id;
        //                            resultItem.ProductConversionIndex = queryfindBarcode.ProductConversion_Index;
        //                            resultItem.ProductConversionBarcode = queryfindBarcode.ProductConversionBarcode;
        //                        }

        //                        resultItem.IsActive = dataResult.IsActive;
        //                        resultItem.IsDelete = dataResult.IsDelete;
        //                        resultItem.IsSystem = dataResult.IsSystem;
        //                        resultItem.StatusId = dataResult.Status_Id;
        //                        resultItem.CreateDate = dataResult.Create_Date.GetValueOrDefault();
        //                        resultItem.CreateBy = dataResult.Create_By;
        //                        resultItem.UpdateDate = dataResult.Update_Date.GetValueOrDefault();
        //                        resultItem.UpdateBy = dataResult.Update_By;
        //                        resultItem.CancelDate = dataResult.Cancel_Date.GetValueOrDefault();
        //                        resultItem.CancelBy = dataResult.Cancel_By;
        //                        result.Add(resultItem);

        //                    }
        //                }


        //            }
        //            else
        //            {
        //                //------------------------------------------ ถ้า Barcode ที่ถูกสแกนไม่ใช้หน่วยเดียวกัน  ------------------------------------------//
        //                var queryResult = context.MS_Product.FromSql("sp_GetProduct @strwhere2", strwhere2).FirstOrDefault();
        //                if (queryResult != null)
        //                {
        //                    var SqlWhere4 = " and ProductConversion_Name = N'" + data.ProductConversionName + "'" + " and Product_Index = '" + queryResult.Product_Index + "' ";

        //                    var strwhere4 = new SqlParameter("@strwhere4", SqlWhere4);
        //                    var queryfindConvertion = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere4", strwhere4).FirstOrDefault();
        //                    //var queryfindConvertion = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.ProductConversion_Name == data.ProductConversionName && c.Product_Index == queryResult.Product_Index).FirstOrDefault();

        //                    if (queryfindConvertion != null)
        //                    {
        //                        //------------------------------------------ หาเลข Barcode ตัวที่ถูกใช้จริงจากตัว Product ที่ซ้ำกัน ------------------------------------------//
        //                        var SqlWhere5 = "";
        //                        if (data.ProductConversionName != null && data.ProductConversionIndex != null)
        //                        {
        //                            SqlWhere5 = " and Product_Index = '" + queryfindConvertion.Product_Index + "'" + " and ProductConversion_Index = '" + queryfindConvertion.ProductConversion_Index + "'";
        //                        }
        //                        var strwhere5 = new SqlParameter("@strwhere5", SqlWhere5);

        //                        var queryfindBarcode = context.MS_ProductConversionBarcode.FromSql("sp_GetProductConversionBarcode @strwhere5", strwhere5).FirstOrDefault();
        //                        if (queryfindBarcode.ProductConversionBarcode == data.ProductConversionBarcode)
        //                        {

        //                            var resultItem = new ScanBarcodeViewModel();
        //                            resultItem.ProductId = queryResult.Product_Id;
        //                            resultItem.ProductIndex = queryResult.Product_Index;
        //                            resultItem.ProductName = queryResult.Product_Name;
        //                            resultItem.ProductConversionBarcodeIndex = queryfindBarcode.ProductConversionBarcode_Index;
        //                            resultItem.ProductConversionBarcodeId = queryfindBarcode.ProductConversionBarcode_Id;
        //                            resultItem.ProductConversionIndex = queryfindBarcode.ProductConversion_Index;
        //                            resultItem.ProductConversionBarcode = queryfindBarcode.ProductConversionBarcode;
        //                            resultItem.ProductConversionRatio = chkConversion.ProductConversion_Ratio;
        //                            resultItem.ProductConversionName = chkConversion.ProductConversion_Name;
        //                            resultItem.IsActive = queryResult.IsActive;
        //                            resultItem.IsDelete = queryResult.IsDelete;
        //                            resultItem.IsSystem = queryResult.IsSystem;
        //                            resultItem.StatusId = queryResult.Status_Id;
        //                            resultItem.CreateDate = queryResult.Create_Date.GetValueOrDefault();
        //                            resultItem.CreateBy = queryResult.Create_By;
        //                            resultItem.UpdateDate = queryResult.Update_Date.GetValueOrDefault();
        //                            resultItem.UpdateBy = queryResult.Update_By;
        //                            resultItem.CancelDate = queryResult.Cancel_Date.GetValueOrDefault();
        //                            resultItem.CancelBy = queryResult.Cancel_By;
        //                            result.Add(resultItem);

        //                        }
        //                        else
        //                        {
        //                            if (chkConversion != null)
        //                            {
        //                                if (findConversion.ProductConversionBarcode == data.ProductConversionBarcode)
        //                                {
        //                                    var resultItem = new ScanBarcodeViewModel();
        //                                    resultItem.ProductId = queryResult.Product_Id;
        //                                    resultItem.ProductIndex = queryResult.Product_Index;
        //                                    resultItem.ProductName = queryResult.Product_Name;
        //                                    resultItem.ProductConversionRatio = chkConversion.ProductConversion_Ratio;
        //                                    resultItem.ProductConversionName = chkConversion.ProductConversion_Name;
        //                                    if (findConversion != null)
        //                                    {
        //                                        resultItem.ProductConversionBarcodeIndex = findConversion.ProductConversionBarcode_Index;
        //                                        resultItem.ProductConversionBarcodeId = findConversion.ProductConversionBarcode_Id;
        //                                        resultItem.ProductConversionIndex = findConversion.ProductConversion_Index;
        //                                        resultItem.ProductConversionBarcode = findConversion.ProductConversionBarcode;
        //                                    }

        //                                    resultItem.IsActive = queryResult.IsActive;
        //                                    resultItem.IsDelete = queryResult.IsDelete;
        //                                    resultItem.IsSystem = queryResult.IsSystem;
        //                                    resultItem.StatusId = queryResult.Status_Id;
        //                                    resultItem.CreateDate = queryResult.Create_Date.GetValueOrDefault();
        //                                    resultItem.CreateBy = queryResult.Create_By;
        //                                    resultItem.UpdateDate = queryResult.Update_Date.GetValueOrDefault();
        //                                    resultItem.UpdateBy = queryResult.Update_By;
        //                                    resultItem.CancelDate = queryResult.Cancel_Date.GetValueOrDefault();
        //                                    resultItem.CancelBy = queryResult.Cancel_By;
        //                                    result.Add(resultItem);
        //                                }

        //                            }
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

        //public List<ScanBarcodeViewModel> ScanProductCVBarcode(string ProductConversionBarcode)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var result = new List<ScanBarcodeViewModel>();
        //            var resultItem = new ScanBarcodeViewModel();
        //            var findProductIndex = context.MS_ProductConversionBarcode.FromSql("sp_GetProductConversionBarcode").Where(c => c.ProductConversionBarcode == ProductConversionBarcode).FirstOrDefault();
        //            if (findProductIndex != null)
        //            {
        //                var queryResult = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == findProductIndex.Product_Index).ToList();
        //                if (queryResult.Count > 0)
        //                {
        //                    foreach (var item in queryResult)
        //                    {
        //                        resultItem.ProductId = item.Product_Id;
        //                        resultItem.ProductIndex = item.Product_Index;
        //                        resultItem.ProductName = item.Product_Name;
        //                        resultItem.ProductConversionBarcodeIndex = findProductIndex.ProductConversionBarcode_Index;
        //                        resultItem.ProductConversionBarcodeId = findProductIndex.ProductConversionBarcode_Id;
        //                        resultItem.ProductConversionIndex = findProductIndex.ProductConversion_Index;
        //                        resultItem.ProductConversionBarcode = findProductIndex.ProductConversionBarcode;
        //                        resultItem.ProductConversionName = item.ProductConversion_Name;
        //                        resultItem.IsActive = item.IsActive;
        //                        resultItem.IsDelete = item.IsDelete;
        //                        resultItem.IsSystem = item.IsSystem;
        //                        resultItem.StatusId = item.Status_Id;
        //                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                        resultItem.CreateBy = item.Create_By;
        //                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                        resultItem.UpdateBy = item.Update_By;
        //                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                        resultItem.CancelBy = item.Cancel_By;
        //                        result.Add(resultItem);

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
    }
}

using Comone.Utils;
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

namespace MasterDataBusiness
{
    public class ProductCategoryService
    {
        private MasterDataDbContext db;

        public ProductCategoryService()
        {
            db = new MasterDataDbContext();
        }

        public ProductCategoryService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterProduct
        public actionResultProductCategoryViewModel filter(SearchProductCategoryViewModel data)
        {
            try
            {
                var query = db.MS_ProductCategory.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if(data.changeSet != "1")
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductCategory_Id.Contains(data.key)
                                             || c.ProductCategory_Name.Contains(data.key));
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
                        query = query.Where(c => c.ProductCategory_Id.Contains(data.key)
                                             || c.ProductCategory_Name.Contains(data.key));
                    }
                }
               

                var Item = new List<MS_ProductCategory>();
                var TotalRow = new List<MS_ProductCategory>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ProductCategory_Id).ToList();

                var result = new List<SearchProductCategoryViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductCategoryViewModel();

                    resultItem.productCategory_Index = item.ProductCategory_Index;
                    resultItem.productCategory_Id = item.ProductCategory_Id;
                    resultItem.productCategory_Name = item.ProductCategory_Name;
                    resultItem.productCategory_SecondName = item.ProductCategory_SecondName;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.remark = item.Remark;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultProductCategoryViewModel = new actionResultProductCategoryViewModel();
                actionResultProductCategoryViewModel.itemsProductCategory = result.ToList();
                actionResultProductCategoryViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultProductCategoryViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(ProductCategoryViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var productCategoryOld = db.MS_ProductCategory.Find(data.productCategory_Index);

                if (productCategoryOld == null)
                {
                    if (!string.IsNullOrEmpty(data.productCategory_Id))
                    {
                        var query = db.MS_ProductCategory.FirstOrDefault(c => c.ProductCategory_Id == data.productCategory_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.productCategory_Id))
                    {
                        data.productCategory_Id = "ProductCategory_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_ProductCategory.FirstOrDefault(c => c.ProductCategory_Id == data.productCategory_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ProductCategory_Id == data.productCategory_Id)
                                {
                                    data.productCategory_Id = "ProductCategory_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_ProductCategory Model = new MS_ProductCategory();

                    Model.ProductCategory_Index = Guid.NewGuid();
                    Model.ProductCategory_Id = data.productCategory_Id;
                    Model.ProductCategory_Name = data.productCategory_Name;
                    Model.ProductCategory_SecondName = data.productCategory_SecondName;
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

                    db.MS_ProductCategory.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.productCategory_Id))
                    {
                        if (productCategoryOld.ProductCategory_Id != "")
                        {
                            data.productCategory_Id = productCategoryOld.ProductCategory_Id;
                        }
                    }
                    else
                    {
                        if (productCategoryOld.ProductCategory_Id != data.productCategory_Id)
                        {
                            var query = db.MS_ProductCategory.FirstOrDefault(c => c.ProductCategory_Id == data.productCategory_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.productCategory_Id = productCategoryOld.ProductCategory_Id;
                        }
                    }

                    productCategoryOld.ProductCategory_Id = data.productCategory_Id;
                    productCategoryOld.ProductCategory_Name = data.productCategory_Name;
                    productCategoryOld.ProductCategory_SecondName = data.productCategory_SecondName;
                    productCategoryOld.Ref_No1 = data.ref_No1;
                    productCategoryOld.Ref_No2 = data.ref_No2;
                    productCategoryOld.Ref_No3 = data.ref_No3;
                    productCategoryOld.Ref_No4 = data.ref_No4;
                    productCategoryOld.Ref_No5 = data.ref_No5;
                    productCategoryOld.Remark = data.remark;
                    productCategoryOld.UDF_1 = null;
                    productCategoryOld.UDF_2 = null;
                    productCategoryOld.UDF_3 = null;
                    productCategoryOld.UDF_4 = null;
                    productCategoryOld.UDF_5 = null;
                    productCategoryOld.IsActive = Convert.ToInt32(data.isActive);
                    productCategoryOld.Update_By = data.create_By;
                    productCategoryOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveProductCategory", msglog);
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
        public ProductCategoryViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_ProductCategory.Where(c => c.ProductCategory_Index == id).FirstOrDefault();

                var result = new ProductCategoryViewModel();


                result.productCategory_Index = queryResult.ProductCategory_Index;
                result.productCategory_Id = queryResult.ProductCategory_Id;
                result.productCategory_Name = queryResult.ProductCategory_Name;
                result.productCategory_SecondName = queryResult.ProductCategory_SecondName;
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
        public Boolean getDelete(ProductCategoryViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Product = db.MS_ProductCategory.Find(data.productCategory_Index);

                if (Product != null)
                {
                    Product.IsActive = 0;
                    Product.IsDelete = 1;


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
                        olog.logging("DeleteProductCategory" +
                            "" +
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


        #region export
        public actionResultProductCategoryViewModel export(SearchProductCategoryViewModel data)
        {
            try
            {
                var query = db.MS_ProductCategory.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (data.changeSet != "1")
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductCategory_Id.Contains(data.key)
                                             || c.ProductCategory_Name.Contains(data.key));
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
                        query = query.Where(c => c.ProductCategory_Id.Contains(data.key)
                                             || c.ProductCategory_Name.Contains(data.key));
                    }
                }

                var Item = new List<MS_ProductCategory>();
                var TotalRow = new List<MS_ProductCategory>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.ProductCategory_Id).ToList();

                var result = new List<SearchProductCategoryViewModel>();
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new SearchProductCategoryViewModel();
                    resultItem.rowNum = num + 1;
                    resultItem.productCategory_Index = item.ProductCategory_Index;
                    resultItem.productCategory_Id = item.ProductCategory_Id;
                    resultItem.productCategory_Name = item.ProductCategory_Name;
                    resultItem.productCategory_SecondName = item.ProductCategory_SecondName;
                    resultItem.ref_No1 = item.Ref_No1 == null ? "" : item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2 == null ? "" : item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3 == null ? "" : item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4 == null ? "" : item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5 == null ? "" : item.Ref_No5;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.remark = item.Remark == null ? "" : item.Remark;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var actionResultProductCategoryViewModel = new actionResultProductCategoryViewModel();
                actionResultProductCategoryViewModel.itemsProductCategory = result.ToList();

                return actionResultProductCategoryViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //public List<ProductCategoryViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductCategory.FromSql("sp_GetProductCategory").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<ProductCategoryViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProductCategoryViewModel();

        //                resultItem.ProductCategoryId = item.ProductCategory_Id;
        //                resultItem.ProductCategoryIndex = item.ProductCategory_Index;
        //                resultItem.ProductCategoryName = item.ProductCategory_Name;
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
        //public List<ProductCategoryViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductCategory.FromSql("sp_GetProductCategory").Where(c => c.ProductCategory_Index == id).ToList();
        //            var result = new List<ProductCategoryViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProductCategoryViewModel();
        //                resultItem.ProductCategoryId = item.ProductCategory_Id;
        //                resultItem.ProductCategoryIndex = item.ProductCategory_Index;
        //                resultItem.ProductCategoryName = item.ProductCategory_Name;
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
        //public List<ProductCategoryViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductCategory.FromSql("sp_GetProductCategory").Where(c => c.ProductCategory_Index == id).ToList();

        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<ProductCategoryViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var ProductCategory_Index = new SqlParameter("ProductCategory_Index", item.ProductCategory_Index);
        //                var ProductCategory_Id = new SqlParameter("ProductCategory_Id", item.ProductCategory_Id);
        //                var ProductCategory_Name = new SqlParameter("ProductCategory_Name", item.ProductCategory_Name);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ProductCategory  @ProductCategory_Index,@ProductCategory_Id,@ProductCategory_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ProductCategory_Index, ProductCategory_Id, ProductCategory_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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
        //public String SaveChanges(ProductCategoryViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int issystem = 0;
        //            int statusid = 0;

        //            if (data.ProductCategoryIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.ProductCategoryIndex = Guid.NewGuid();
        //            }
        //            if (data.ProductCategoryId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "ProductCategoryID");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.ProductCategoryId = resultParameter.Value.ToString();
        //            }

        //            var ProductCategory_Index = new SqlParameter("ProductCategory_Index", data.ProductCategoryIndex);
        //            var ProductCategory_Id = new SqlParameter("ProductCategory_Id", data.ProductCategoryId);
        //            var ProductCategory_Name = new SqlParameter("ProductCategory_Name", data.ProductCategoryName);
        //            var IsActive = new SqlParameter("IsActive", isactive);
        //            var IsDelete = new SqlParameter("IsDelete", isdelete);
        //            var IsSystem = new SqlParameter("IsSystem", issystem);
        //            var Status_Id = new SqlParameter("Status_Id", statusid);
        //            var Create_By = new SqlParameter("Create_By", "");
        //            var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        //            var Update_By = new SqlParameter("Update_By", "");
        //            var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        //            var Cancel_By = new SqlParameter("Cancel_By", "");
        //            var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ProductCategory  @ProductCategory_Index,@ProductCategory_Id,@ProductCategory_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ProductCategory_Index, ProductCategory_Id, ProductCategory_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ProductCategoryViewModel> search(ProductCategoryViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<ProductCategoryViewModel>();
        //            if (data.ProductCategoryId != "" && data.ProductCategoryId != null)
        //            {
        //                pwhereFilter = " And ProductCategory_Id like N'%" + data.ProductCategoryId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter = "";
        //            }

        //            if (data.ProductCategoryName != "" && data.ProductCategoryName != null)
        //            {
        //                pwhereFilter += " And ProductCategory_Name like N'%" + data.ProductCategoryName + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }


        //            if (data.ProductCategoryId != "" && data.ProductCategoryId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_ProductCategory.FromSql("sp_GetProductCategory @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new ProductCategoryViewModel();

        //                    resultItem.ProductCategoryId = item.ProductCategory_Id;
        //                    resultItem.ProductCategoryIndex = item.ProductCategory_Index;
        //                    resultItem.ProductCategoryName = item.ProductCategory_Name;
        //                    resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                    resultItem.CreateBy = item.Create_By;
        //                    resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                    resultItem.UpdateBy = item.Update_By;
        //                    resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                    resultItem.CancelBy = item.Cancel_By;

        //                    result.Add(resultItem);
        //                }
        //            }
        //            else if (data.ProductCategoryName != "" && data.ProductCategoryName != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_ProductCategory.FromSql("sp_GetProductCategory @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new ProductCategoryViewModel();

        //                    resultItem.ProductCategoryId = item.ProductCategory_Id;
        //                    resultItem.ProductCategoryIndex = item.ProductCategory_Index;
        //                    resultItem.ProductCategoryName = item.ProductCategory_Name;
        //                    resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                    resultItem.CreateBy = item.Create_By;
        //                    resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                    resultItem.UpdateBy = item.Update_By;
        //                    resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                    resultItem.CancelBy = item.Cancel_By;

        //                    result.Add(resultItem);
        //                }
        //            }

        //            if (data.ProductCategoryId == "" && data.ProductCategoryName == "")
        //            {
        //                result = this.Filter();
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<View_ProductCategoryViewModel> getViewProductCategory(ProductViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.View_ProductCategory.AsQueryable();

                    //query = query.Where(c => c.Product_Index == Guid.Parse("741C05f9-A03A-4343-BD73-0003D5F38963"));
                    if (!string.IsNullOrEmpty(data.product_Index.ToString()) && data.product_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Product_Index == data.product_Index);
                    }

                    var items = new List<View_ProductCategoryViewModel>();

                    var result = query.ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new View_ProductCategoryViewModel();

                        resultItem.productCategory_Index = item.ProductCategory_Index;
                        resultItem.productCategory_Id = item.ProductCategory_Id;
                        resultItem.productCategory_Name = item.ProductCategory_Name;
                        resultItem.productCategory_IsActive = item.ProductCategory_IsActive;
                        resultItem.productCategory_IsDelete = item.ProductCategory_IsDelete;
                        resultItem.product_Index = item.Product_Index;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_SecondName = item.Product_SecondName;
                        resultItem.product_ThirdName = item.Product_ThirdName;
                        resultItem.productConversion_Index = item.ProductConversion_Index;
                        resultItem.productConversion_Id = item.ProductConversion_Id;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.productType_Index = item.ProductType_Index;
                        resultItem.productSubType_Index = item.ProductSubType_Index;
                        resultItem.productItemLife_Y = item.ProductItemLife_Y;
                        resultItem.productItemLife_M = item.ProductItemLife_M;
                        resultItem.productItemLife_D = item.ProductItemLife_D;
                        resultItem.productImage_Path = item.ProductImage_Path;
                        resultItem.isLot = item.IsLot;
                        resultItem.isExpDate = item.IsExpDate;
                        resultItem.isMfgDate = item.IsMfgDate;
                        resultItem.isCatchWeight = item.IsCatchWeight;
                        resultItem.isPack = item.IsPack;
                        resultItem.isSerial = item.IsSerial;
                        resultItem.product_IsActive = item.Product_IsActive;
                        resultItem.product_IsDelete = item.Product_IsDelete;

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
    }
}

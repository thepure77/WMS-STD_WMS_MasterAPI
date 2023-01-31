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
    public class ProductSubTypeService
    {
        private MasterDataDbContext db;

        public ProductSubTypeService()
        {
            db = new MasterDataDbContext();
        }

        public ProductSubTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterProductSubType
        public actionResultProductSubTypeViewModel filter(SearchProductSubTypeViewModel data)
        {
            try
            {
                var query = db.View_ProductSubType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.ProductSubType_Id.Contains(data.key)
                                         || c.ProductSubType_Name.Contains(data.key)
                                         || c.ProductType_Name.Contains(data.key));
                }

                var Item = new List<View_ProductSubType>();
                var TotalRow = new List<View_ProductSubType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ProductSubType_Id).ToList();

                var result = new List<SearchProductSubTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductSubTypeViewModel();

                    resultItem.productSubType_Index = item.ProductSubType_Index;
                    resultItem.productSubType_Id = item.ProductSubType_Id;
                    resultItem.productSubType_Name = item.ProductSubType_Name;
                    resultItem.productType_Index = item.ProductType_Index;
                    resultItem.productType_Name = item.ProductType_Name;
                    resultItem.productSubType_SecondName = item.ProductSubType_SecondName;
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

                var actionResultProductSubTypeViewModel = new actionResultProductSubTypeViewModel();
                actionResultProductSubTypeViewModel.itemsProductSubType = result.ToList();
                actionResultProductSubTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultProductSubTypeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(ProductSubTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ProductSubTypeOld = db.MS_ProductSubType.Find(data.productSubType_Index);

                if (ProductSubTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.productSubType_Id))
                    {
                        var query = db.MS_ProductSubType.FirstOrDefault(c => c.ProductSubType_Id == data.productSubType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.productSubType_Id))
                    {
                        data.productSubType_Id = "ProductSubType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_ProductSubType.FirstOrDefault(c => c.ProductSubType_Id == data.productSubType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ProductSubType_Id == data.productSubType_Id)
                                {
                                    data.productSubType_Id = "ProductSubType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_ProductSubType Model = new MS_ProductSubType();


                    Model.ProductSubType_Index = Guid.NewGuid();
                    Model.ProductSubType_Id = data.productSubType_Id;
                    Model.ProductSubType_Name = data.productSubType_Name;
                    Model.ProductSubType_SecondName = data.productSubType_SecondName;
                    Model.ProductType_Index = data.productType_Index;
                    Model.ProductType_Id = data.productType_Id;
                    Model.ProductType_Name = data.productType_Name;
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

                    db.MS_ProductSubType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.productSubType_Id))
                    {
                        if (ProductSubTypeOld.ProductSubType_Id != "")
                        {
                            data.productSubType_Id = ProductSubTypeOld.ProductSubType_Id;
                        }
                    }
                    else
                    {
                        if (ProductSubTypeOld.ProductSubType_Id != data.productSubType_Id)
                        {
                            var query = db.MS_ProductSubType.FirstOrDefault(c => c.ProductSubType_Id == data.productSubType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.productSubType_Id = ProductSubTypeOld.ProductSubType_Id;
                        }
                    }

                    ProductSubTypeOld.ProductSubType_Id = data.productSubType_Id;
                    ProductSubTypeOld.ProductSubType_Name = data.productSubType_Name;
                    ProductSubTypeOld.ProductSubType_SecondName = data.productSubType_SecondName;
                    ProductSubTypeOld.ProductType_Index = data.productType_Index;
                    ProductSubTypeOld.ProductType_Id = data.productType_Id;
                    ProductSubTypeOld.ProductType_Name = data.productType_Name;
                    ProductSubTypeOld.Ref_No1 = data.ref_No1;
                    ProductSubTypeOld.Ref_No2 = data.ref_No2;
                    ProductSubTypeOld.Ref_No3 = data.ref_No3;
                    ProductSubTypeOld.Ref_No4 = data.ref_No4;
                    ProductSubTypeOld.Ref_No5 = data.ref_No5;
                    ProductSubTypeOld.Remark = data.remark;
                    ProductSubTypeOld.UDF_1 = null;
                    ProductSubTypeOld.UDF_2 = null;
                    ProductSubTypeOld.UDF_3 = null;
                    ProductSubTypeOld.UDF_4 = null;
                    ProductSubTypeOld.UDF_5 = null;
                    ProductSubTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    ProductSubTypeOld.Update_By = data.create_By;
                    ProductSubTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveProductSubType", msglog);
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
        public ProductSubTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_ProductSubType.Where(c => c.ProductSubType_Index == id).FirstOrDefault();

                var result = new ProductSubTypeViewModel();

                result.productSubType_Index = queryResult.ProductSubType_Index;
                result.productSubType_Id = queryResult.ProductSubType_Id;
                result.productSubType_Name = queryResult.ProductSubType_Name;
                result.productSubType_SecondName = queryResult.ProductSubType_SecondName;
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
                result.productType_Index = queryResult.ProductType_Index;
                result.productType_Name = queryResult.ProductType_Name;
                result.key = queryResult.ProductType_Id + " - " + queryResult.ProductType_Name;
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
        public Boolean getDelete(ProductSubTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Room = db.MS_ProductSubType.Find(data.productSubType_Index);

                if (Room != null)
                {
                    Room.IsActive = 0;
                    Room.IsDelete = 1;


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
                        olog.logging("DeleteProductSubType", msglog);
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

        //public List<ProductSubTypeViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductSubType.FromSql("sp_GetProductSubType").Where(c => c.IsActive == 1 && c.IsDelete == 0)
        //                                                                .ToList();

        //            var result = new List<ProductSubTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProductSubTypeViewModel();

        //                resultItem.ProductSubTypeId = item.ProductSubType_Id;
        //                resultItem.ProductSubTypeName = item.ProductSubType_Name;
        //                resultItem.ProductSubTypeIndex = item.ProductSubType_Index;
        //                if (item.ProductType_Index != null)
        //                {
        //                    var itemList = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.ProductType_Index == item.ProductType_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.ProductTypeIndex = itemList.ProductType_Index;
        //                        resultItem.ProductTypeName = itemList.ProductType_Name;
        //                    }

        //                }

        //                resultItem.IsActive = item.IsActive;
        //                resultItem.IsDelete = item.IsDelete;
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
        //public List<ProductSubTypeViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductSubType.FromSql("sp_GetProductSubType").Where(c => c.ProductSubType_Index == id).ToList();
        //            var result = new List<ProductSubTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProductSubTypeViewModel();
        //                resultItem.ProductSubTypeId = item.ProductSubType_Id;
        //                resultItem.ProductSubTypeName = item.ProductSubType_Name;
        //                resultItem.ProductSubTypeIndex = item.ProductSubType_Index;
        //                if (item.ProductType_Index != null)
        //                {
        //                    var itemList = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.ProductType_Index == item.ProductType_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.ProductTypeIndex = itemList.ProductType_Index;
        //                        resultItem.ProductTypeName = itemList.ProductType_Name;
        //                    }

        //                }

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
        //            if (queryResult.Count == 0)
        //            {
        //                var findItem = context.MS_ProductSubType.FromSql("sp_GetProductSubType").Where(c => c.ProductType_Index == id).ToList();
        //                if (findItem != null)
        //                {
        //                    foreach (var item in findItem.OrderByDescending(c => c.ProductType_Index))
        //                    {
        //                        var itemList = context.MS_ProductSubType.FromSql("sp_GetProductSubType").Where(c => c.ProductType_Index == item.ProductType_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            var resultItem = new ProductSubTypeViewModel();
        //                            resultItem.ProductSubTypeId = itemList.ProductSubType_Id;
        //                            resultItem.ProductSubTypeIndex = itemList.ProductSubType_Index;
        //                            resultItem.ProductSubTypeName = itemList.ProductSubType_Name;
        //                            resultItem.ProductTypeIndex = itemList.ProductType_Index;
        //                            resultItem.IsActive = itemList.IsActive;
        //                            resultItem.IsDelete = itemList.IsDelete;
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

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ProductSubTypeViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductSubType.FromSql("sp_GetProductSubType").Where(c => c.ProductType_Index == id).ToList();
        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<ProductSubTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var ProductSubType_Index = new SqlParameter("ProductSubType_Index", item.ProductSubType_Index);
        //                var ProductSubType_Id = new SqlParameter("ProductSubType_Id", item.ProductSubType_Id);
        //                var ProductSubType_Name = new SqlParameter("ProductSubType_Name", item.ProductSubType_Name);
        //                var ProductType_Index = new SqlParameter("ProductType_Index", item.ProductType_Index);
        //                var IsActive = new SqlParameter("IsActive", isactive);
        //                var IsDelete = new SqlParameter("IsDelete", isdelete);
        //                var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
        //                var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
        //                var Create_By = new SqlParameter("Create_By", item.Create_By);
        //                var Create_Date = new SqlParameter("Create_Date", item.Create_Date);
        //                var Update_By = new SqlParameter("Update_By", item.Update_By);
        //                var Update_Date = new SqlParameter("Update_Date", item.Update_Date);
        //                var Cancel_By = new SqlParameter("Cancel_By", item.Cancel_By);
        //                var Cancel_Date = new SqlParameter("Cancel_Date", item.Cancel_Date);
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ProductSubType  @ProductSubType_Index,@ProductSubType_Id,@ProductSubType_Name,@ProductType_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ProductSubType_Index, ProductSubType_Id, ProductSubType_Name, ProductType_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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
        //public String SaveChanges(ProductSubTypeViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.ProductSubTypeIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.ProductSubTypeIndex = Guid.NewGuid();
        //            }
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int isSystem = 0;
        //            int statusId = 0;
        //            if (data.ProductSubTypeId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "ProductSubTypeID");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.ProductSubTypeId = resultParameter.Value.ToString();
        //            }

        //            var ProductSubType_Index = new SqlParameter("ProductSubType_Index", data.ProductSubTypeIndex);
        //            var ProductSubType_Id = new SqlParameter("ProductSubType_Id", data.ProductSubTypeId);
        //            var ProductSubType_Name = new SqlParameter("ProductSubType_Name", data.ProductSubTypeName);
        //            var ProductType_Index = new SqlParameter("ProductType_Index", data.ProductTypeIndex);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ProductSubType  @ProductSubType_Index,@ProductSubType_Id,@ProductSubType_Name,@ProductType_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ProductSubType_Index, ProductSubType_Id, ProductSubType_Name, ProductType_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ProductSubTypeViewModel> search(ProductSubTypeViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<ProductSubTypeViewModel>();
        //            var queryResult = context.MS_ProductSubType.FromSql("sp_GetProductSubType").Where(c => c.IsActive == 1 && c.IsDelete == 0)
        //                                            .ToList();
        //            if (data.ProductSubTypeId != "" && data.ProductSubTypeId != null)
        //            {
        //                pwhereFilter = " And ProductSubType_Id like N'%" + data.ProductSubTypeId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter = "";
        //            }

        //            if (data.ProductSubTypeName != "" && data.ProductSubTypeName != null)
        //            {
        //                pwhereFilter += " And ProductSubType_Name like N'%" + data.ProductSubTypeName + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }


        //            if (data.ProductSubTypeId != "" && data.ProductSubTypeId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_ProductSubType.FromSql("sp_GetProductSubType @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new ProductSubTypeViewModel();

        //                    resultItem.ProductSubTypeId = item.ProductSubType_Id;
        //                    resultItem.ProductSubTypeName = item.ProductSubType_Name;
        //                    resultItem.ProductSubTypeIndex = item.ProductSubType_Index;
        //                    if (item.ProductType_Index != null)
        //                    {
        //                        var itemList = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.ProductType_Index == item.ProductType_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            resultItem.ProductTypeIndex = itemList.ProductType_Index;
        //                            resultItem.ProductTypeName = itemList.ProductType_Name;
        //                        }

        //                    }

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
        //            else if (data.ProductSubTypeName != "" && data.ProductSubTypeName != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_ProductSubType.FromSql("sp_GetProductSubType @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new ProductSubTypeViewModel();

        //                    resultItem.ProductSubTypeId = item.ProductSubType_Id;
        //                    resultItem.ProductSubTypeName = item.ProductSubType_Name;
        //                    resultItem.ProductSubTypeIndex = item.ProductSubType_Index;
        //                    if (item.ProductType_Index != null)
        //                    {
        //                        var itemList = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.ProductType_Index == item.ProductType_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            resultItem.ProductTypeIndex = itemList.ProductType_Index;
        //                            resultItem.ProductTypeName = itemList.ProductType_Name;
        //                        }

        //                    }

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
        //            else if (data.ProductTypeName != "" && data.ProductTypeName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And ProductType_Name like N'%" + data.ProductTypeName + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_ProductType.FromSql("sp_GetProductType @strwhere ", pstrwhere1).ToList();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new ProductSubTypeViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.ProductType_Index == ItemList.ProductType_Index)
        //                        {
        //                            resultItem.ProductSubTypeId = item.ProductSubType_Id;
        //                            resultItem.ProductSubTypeName = item.ProductSubType_Name;
        //                            resultItem.ProductSubTypeIndex = item.ProductSubType_Index;
        //                            if (item.ProductType_Index != null)
        //                            {
        //                                var itemList = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.ProductType_Index == item.ProductType_Index).FirstOrDefault();
        //                                if (itemList != null)
        //                                {
        //                                    resultItem.ProductTypeIndex = itemList.ProductType_Index;
        //                                    resultItem.ProductTypeName = itemList.ProductType_Name;
        //                                }

        //                            }

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

        //            if (data.ProductSubTypeId == "" && data.ProductSubTypeName == "" && data.ProductTypeName == "")
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
    }
}

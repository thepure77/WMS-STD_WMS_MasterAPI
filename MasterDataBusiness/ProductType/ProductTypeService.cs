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
    public class ProductTypeService
    {
        private MasterDataDbContext db;

        public ProductTypeService()
        {
            db = new MasterDataDbContext();
        }

        public ProductTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterProductType
        public actionResultProductTypeViewModel filter(SearchProductTypeViewModel data)
        {
            try
            {
                var query = db.View_ProductType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.ProductType_Id.Contains(data.key)
                                         || c.ProductType_Name.Contains(data.key)
                                         || c.ProductCategory_Name.Contains(data.key));
                }

                var Item = new List<View_ProductType>();
                var TotalRow = new List<View_ProductType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ProductType_Id).ToList();

                var result = new List<SearchProductTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductTypeViewModel();

                    resultItem.productType_Index = item.ProductType_Index;
                    resultItem.productType_Id = item.ProductType_Id;
                    resultItem.productType_Name = item.ProductType_Name;
                    resultItem.productType_SecondName = item.ProductType_SecondName;
                    resultItem.productCategory_Index = item.ProductCategory_Index;
                    resultItem.productCategory_Name = item.ProductCategory_Name;
                    resultItem.productCategory_Id = item.ProductCategory_Id;
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

                var actionResultProductTypeViewModel = new actionResultProductTypeViewModel();
                actionResultProductTypeViewModel.itemsProductType = result.ToList();
                actionResultProductTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultProductTypeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(ProductTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var productTypeOld = db.MS_ProductType.Find(data.productType_Index);

                if (productTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.productType_Id))
                    {
                        var query = db.MS_ProductType.FirstOrDefault(c => c.ProductType_Id == data.productType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.productType_Id))
                    {
                        data.productType_Id = "ProductType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_ProductType.FirstOrDefault(c => c.ProductType_Id == data.productType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ProductType_Id == data.productType_Id)
                                {
                                    data.productType_Id = "ProductType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    //data.productType_Id = "ProductType_Id".genAutonumber();

                    MS_ProductType Model = new MS_ProductType();


                    Model.ProductType_Index = Guid.NewGuid();
                    Model.ProductType_Id = data.productType_Id;
                    Model.ProductType_Name = data.productType_Name;
                    Model.ProductType_SecondName = data.productType_SecondName;
                    Model.ProductCategory_Index = data.productCategory_Index;
                    Model.ProductCategory_Id = data.productCategory_Id;
                    Model.ProductCategory_Name = data.productCategory_Name;
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

                    db.MS_ProductType.Add(Model);
                }
                else 
                {
                    if (string.IsNullOrEmpty(data.productType_Id))
                    {
                        if (productTypeOld.ProductType_Id != "")
                        {
                            data.productType_Id = productTypeOld.ProductType_Id;
                        }
                    }
                    else
                    {
                        if (productTypeOld.ProductType_Id != data.productType_Id)
                        {
                            var query = db.MS_ProductType.FirstOrDefault(c => c.ProductType_Id == data.productType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.productType_Id = productTypeOld.ProductType_Id;
                        }
                    }
                    productTypeOld.ProductType_Id = data.productType_Id;
                    productTypeOld.ProductType_Name = data.productType_Name;
                    productTypeOld.ProductType_SecondName = data.productType_SecondName;
                    productTypeOld.ProductCategory_Index = data.productCategory_Index;
                    productTypeOld.ProductCategory_Id = data.productCategory_Id;
                    productTypeOld.ProductCategory_Name = data.productCategory_Name;
                    productTypeOld.Ref_No1 = data.ref_No1;
                    productTypeOld.Ref_No2 = data.ref_No2;
                    productTypeOld.Ref_No3 = data.ref_No3;
                    productTypeOld.Ref_No4 = data.ref_No4;
                    productTypeOld.Ref_No5 = data.ref_No5;
                    productTypeOld.Remark = data.remark;
                    productTypeOld.UDF_1 = null;
                    productTypeOld.UDF_2 = null;
                    productTypeOld.UDF_3 = null;
                    productTypeOld.UDF_4 = null;
                    productTypeOld.UDF_5 = null;
                    productTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    productTypeOld.Update_By = data.create_By;
                    productTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveProductType", msglog);
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
        public ProductTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_ProductType.Where(c => c.ProductType_Index == id).FirstOrDefault();

                var result = new ProductTypeViewModel();

                result.productType_Index = queryResult.ProductType_Index;
                result.productType_Id = queryResult.ProductType_Id;
                result.productType_Name = queryResult.ProductType_Name;
                result.productType_SecondName = queryResult.ProductType_SecondName;
                result.productCategory_Index = queryResult.ProductCategory_Index;
                result.productCategory_Id = queryResult.ProductCategory_Id;
                result.productCategory_Name = queryResult.ProductCategory_Name;
                result.key = queryResult.ProductCategory_Id + " - " + queryResult.ProductCategory_Name;
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
        public Boolean getDelete(ProductTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Room = db.MS_ProductType.Find(data.productType_Index);

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
                        olog.logging("DeleteProductType", msglog);
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

        //public List<ProductTypeViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<ProductTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProductTypeViewModel();

        //                resultItem.ProductTypeId = item.ProductType_Id;
        //                resultItem.ProductTypeName = item.ProductType_Name;
        //                if (item.ProductCategory_Index != null)
        //                {
        //                    var itemList = context.MS_ProductCategory.FromSql("sp_GetProductCategory").Where(c => c.ProductCategory_Index == item.ProductCategory_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.ProductCategoryIndex = itemList.ProductCategory_Index;
        //                        resultItem.ProductCategoryName = itemList.ProductCategory_Name;
        //                    }

        //                }

        //                resultItem.ProductTypeIndex = item.ProductType_Index;
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
        //public List<ProductTypeViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.ProductType_Index == id).ToList();

        //            var result = new List<ProductTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProductTypeViewModel();
        //                resultItem.ProductTypeId = item.ProductType_Id;
        //                resultItem.ProductTypeName = item.ProductType_Name;
        //                if (item.ProductCategory_Index != null)
        //                {
        //                    var itemList = context.MS_ProductCategory.FromSql("sp_GetProductCategory").Where(c => c.ProductCategory_Index == item.ProductCategory_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.ProductCategoryIndex = itemList.ProductCategory_Index;
        //                        resultItem.ProductCategoryName = itemList.ProductCategory_Name;
        //                    }
        //                }
        //                resultItem.ProductTypeIndex = item.ProductType_Index;
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
        //                var findItem = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.ProductCategory_Index == id).ToList();
        //                if (findItem != null)
        //                {
        //                    foreach (var item in findItem.OrderByDescending(c => c.ProductType_Index))
        //                    {
        //                        var resultItem = new ProductTypeViewModel();
        //                        resultItem.ProductTypeId = item.ProductType_Id;
        //                        resultItem.ProductTypeName = item.ProductType_Name;
        //                        resultItem.ProductTypeIndex = item.ProductType_Index;
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

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ProductTypeViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.ProductType_Index == id).ToList();
        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<ProductTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var ProductType_Index = new SqlParameter("ProductType_Index", item.ProductType_Index);
        //                var ProductType_Id = new SqlParameter("ProductType_Id", item.ProductType_Id);
        //                var ProductType_Name = new SqlParameter("ProductType_Name", item.ProductType_Name);
        //                var ProductCategory_Index = new SqlParameter("ProductCategory_Index", item.ProductCategory_Index);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ProductType  @ProductType_Index,@ProductType_Id,@ProductType_Name,@ProductCategory_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ProductType_Index, ProductType_Id, ProductType_Name, ProductCategory_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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
        //public String SaveChanges(ProductTypeViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int issystem = 0;
        //            int statusid = 0;


        //            if (data.ProductTypeIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.ProductTypeIndex = Guid.NewGuid();
        //            }
        //            if (data.ProductTypeId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "ProductTypeID");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.ProductTypeId = resultParameter.Value.ToString();
        //            }

        //            var ProductType_Index = new SqlParameter("ProductType_Index", data.ProductTypeIndex);
        //            var ProductType_Id = new SqlParameter("ProductType_Id", data.ProductTypeId);
        //            var ProductType_Name = new SqlParameter("ProductType_Name", data.ProductTypeName);
        //            var ProductCategory_Index = new SqlParameter("ProductCategory_Index", data.ProductCategoryIndex);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ProductType  @ProductType_Index,@ProductType_Id,@ProductType_Name,@ProductCategory_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ProductType_Index, ProductType_Id, ProductType_Name, ProductCategory_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ProductTypeViewModel> search(ProductTypeViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhere = "";
        //            string pwhereLike = "";
        //            var result = new List<ProductTypeViewModel>();
        //            var queryResult = context.MS_ProductType.FromSql("sp_GetProductType").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
        //            if (data.ProductTypeId != "" && data.ProductTypeId != null)
        //            {
        //                pwhere = " And ProductType_Id like N'%" + data.ProductTypeId + "%'";
        //            }
        //            else
        //            {
        //                pwhere += " ";
        //            }

        //            if (data.ProductTypeName != "" && data.ProductTypeName != null)
        //            {
        //                pwhere += " And ProductType_Name like N'%" + data.ProductTypeName + "%'";
        //            }
        //            else
        //            {
        //                pwhere += " ";
        //            }

        //            if (data.ProductTypeId != "" && data.ProductTypeId != null || data.ProductTypeName != "" && data.ProductTypeName != null)
        //            {
        //                pwhere += " And isActive = '" + 1 + "'";
        //                pwhere += " And isDelete = '" + 0 + "'";
        //                var pstrwhere = new SqlParameter("@strwhere", pwhere);
        //                var query = context.MS_ProductType.FromSql("sp_GetProductType @strwhere ", pstrwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new ProductTypeViewModel();
        //                    resultItem.ProductTypeId = item.ProductType_Id;
        //                    resultItem.ProductTypeName = item.ProductType_Name;
        //                    if (item.ProductCategory_Index != null)
        //                    {
        //                        var itemList = context.MS_ProductCategory.FromSql("sp_GetProductCategory").Where(c => c.ProductCategory_Index == item.ProductCategory_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            resultItem.ProductCategoryIndex = itemList.ProductCategory_Index;
        //                            resultItem.ProductCategoryName = itemList.ProductCategory_Name;
        //                        }
        //                    }
        //                    resultItem.ProductTypeIndex = item.ProductType_Index;
        //                    resultItem.IsActive = item.IsActive;
        //                    resultItem.IsDelete = item.IsDelete;
        //                    resultItem.IsSystem = item.IsSystem;
        //                    resultItem.StatusId = item.Status_Id;
        //                    result.Add(resultItem);
        //                }
        //            }
        //            else if (data.ProductTypeId == "" && data.ProductTypeName == "")
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

        public List<ProductTypeViewModel> ProductTypeFilter(ProductTypeViewModel model)
        {
            try
            {
                var items = new List<ProductTypeViewModel>();
                var result = db.MS_ProductType.ToList();


                foreach (var item in result)
                {
                    var resultItem = new ProductTypeViewModel
                    {
                        productType_Index = item.ProductType_Index,
                        productType_Id = item.ProductType_Id,
                        productType_Name = item.ProductType_Name,
                        productCategory_Index = item.ProductCategory_Index
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

        public List<View_BinbalanceProductTypeViewModel> configProductType(View_BinbalanceProductTypeViewModel model)
        {
            try
            {
                var items = new List<View_BinbalanceProductTypeViewModel>();

                var query = db.View_BinbalanceProductType.AsQueryable();



                var result = query.ToList();


                foreach (var item in result)
                {
                    var resultItem = new View_BinbalanceProductTypeViewModel
                    {
                        productType_Index = item.ProductType_Index,
                        productType_Id = item.ProductType_Id,
                        productType_Name = item.ProductType_Name,
                        product_Index = item.Product_Index,
                        product_Id = item.Product_Id,
                        product_Name = item.Product_Name
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
    }
}

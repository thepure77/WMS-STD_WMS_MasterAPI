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

using Newtonsoft.Json;
using Comone.Utils;

namespace MasterDataBusiness
{
    public class ProductOwnerService
    {
        private MasterDataDbContext db;

        public ProductOwnerService()
        {
            db = new MasterDataDbContext();
        }

        public ProductOwnerService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterProductOwner
        public actionResultProductOwnerViewModel filter(SearchProductOwnerViewModel data)
        {
            try
            {
                var query = db.View_ProductOwner.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (data.changeSet != "1")
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductOwner_Id.Contains(data.key)
                                            || c.Owner_Name.Contains(data.key)
                                            || c.Product_Name.Contains(data.key));

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
                        query = query.Where(c => c.ProductOwner_Id.Contains(data.key)
                                            || c.Owner_Name.Contains(data.key)
                                            || c.Product_Name.Contains(data.key));

                    }
                }
               

                var Item = new List<View_ProductOwner>();
                var TotalRow = new List<View_ProductOwner>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ProductOwner_Id).ToList();

                var result = new List<SearchProductOwnerViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductOwnerViewModel();

                    resultItem.productOwner_Index = item.ProductOwner_Index;
                    resultItem.productOwner_Id = item.ProductOwner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultProductOwnerViewModel = new actionResultProductOwnerViewModel();
                actionResultProductOwnerViewModel.itemsProductOwner = result.ToList();
                actionResultProductOwnerViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultProductOwnerViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public actionResultProductOwnerViewModel FilterInClause(string jsonData)
        {
            try
            {
                SearchProductOwnerInClauseViewModel data = JsonConvert.DeserializeObject<SearchProductOwnerInClauseViewModel>(jsonData);

                var query = db.View_ProductOwner.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if ((data?.data?.Count ?? 0) > 0)
                {
                    query = query.Where(c => data.data.Any(w => w.Product_Index == c.Product_Index && w.Owner_Index == c.Owner_Index) );
                }

                var Item = new List<View_ProductOwner>();
                var TotalRow = new List<View_ProductOwner>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ProductOwner_Id).ToList();

                var result = new List<SearchProductOwnerViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductOwnerViewModel();

                    resultItem.productOwner_Index = item.ProductOwner_Index;
                    resultItem.productOwner_Id = item.ProductOwner_Id;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultProductOwnerViewModel = new actionResultProductOwnerViewModel();
                actionResultProductOwnerViewModel.itemsProductOwner = result.ToList();
                actionResultProductOwnerViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = null };

                return actionResultProductOwnerViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(ProductOwnerViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ProductOwnerOld = db.MS_ProductOwner.Find(data.productOwner_Index);

                if (ProductOwnerOld == null)
                {
                    if (!string.IsNullOrEmpty(data.productOwner_Id))
                    {
                        var query = db.MS_ProductOwner.FirstOrDefault(c => c.ProductOwner_Id == data.productOwner_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.productOwner_Id))
                    {
                        data.productOwner_Id = "ProductOwner_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_ProductOwner.FirstOrDefault(c => c.ProductOwner_Id == data.productOwner_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ProductOwner_Id == data.productOwner_Id)
                                {
                                    data.productOwner_Id = "ProductOwner_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_ProductOwner Model = new MS_ProductOwner();

                    Model.ProductOwner_Index = Guid.NewGuid();
                    Model.ProductOwner_Id = data.productOwner_Id;
                    Model.Owner_Index = data.owner_Index;
                    Model.Product_Index = data.product_Index;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_ProductOwner.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.productOwner_Id))
                    {
                        if (ProductOwnerOld.ProductOwner_Id != "")
                        {
                            data.productOwner_Id = ProductOwnerOld.ProductOwner_Id;
                        }
                    }
                    else
                    {
                        if (ProductOwnerOld.ProductOwner_Id != data.productOwner_Id)
                        {
                            var query = db.MS_ProductOwner.FirstOrDefault(c => c.ProductOwner_Id == data.productOwner_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.productOwner_Id = ProductOwnerOld.ProductOwner_Id;
                        }
                    }

                    ProductOwnerOld.ProductOwner_Id = data.productOwner_Id;
                    ProductOwnerOld.Owner_Index = data.owner_Index;
                    ProductOwnerOld.Product_Index = data.product_Index;
                    ProductOwnerOld.IsActive = Convert.ToInt32(data.isActive);
                    ProductOwnerOld.Update_By = data.create_By;
                    ProductOwnerOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveProductOwner", msglog);
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
        public ProductOwnerViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_ProductOwner.Where(c => c.ProductOwner_Index == id).FirstOrDefault();

                var result = new ProductOwnerViewModel();


                result.productOwner_Index = queryResult.ProductOwner_Index;
                result.productOwner_Id = queryResult.ProductOwner_Id;
                result.owner_Index = queryResult.Owner_Index;
                result.owner_Name = queryResult.Owner_Name;
                result.product_Index = queryResult.Product_Index;
                result.product_Name = queryResult.Product_Name;
                result.key = queryResult.Product_Id + " - " + queryResult.Product_Name;
                result.key2 = queryResult.Owner_Id + " - " + queryResult.Owner_Name;
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
        public Boolean getDelete(ProductOwnerViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Owner = db.MS_ProductOwner.Find(data.productOwner_Index);

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
                        olog.logging("DeleteProductOwner" +
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

        #region filterV2
        public actionResultProductOwnerViewModel filterV2(SearchProductOwnerViewModel data)
        {
            try
            {
                var query = db.MS_ProductOwner.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                {
                    query = query.Where(c => c.Product_Index == data.product_Index);
                }

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    query = query.Where(c => c.Product_Id == data.product_Id);
                }


                if (!string.IsNullOrEmpty(data.value2))
                {
                    query = query.Where(c => c.Owner_Id.Contains(data.value2)
                                         || c.Owner_Name.Contains(data.value2));
                }

                var Item = new List<MS_ProductOwner>();
                var TotalRow = new List<MS_ProductOwner>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ProductOwner_Id).ToList();

                var result = new List<SearchProductOwnerViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductOwnerViewModel();

                    resultItem.productOwner_Index = item.ProductOwner_Index;
                    resultItem.productOwner_Id = item.ProductOwner_Id;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.owner_Id = item.Owner_Id;
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

                var actionResultProductOwnerViewModel = new actionResultProductOwnerViewModel();
                actionResultProductOwnerViewModel.itemsProductOwner = result.ToList();
                actionResultProductOwnerViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultProductOwnerViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveProductOwnerList
        public String SaveProductOwnerList(ProductOwnerViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                foreach (var item in data.listProductOwnerViewModel)
                {
                    MS_ProductOwner Model = new MS_ProductOwner();

                    Model.ProductOwner_Index = Guid.NewGuid();

                    data.productOwner_Id = "ProductOwner_Id".genAutonumber();
                    int i = 1;
                    while (i > 0)
                    {
                        var query = db.MS_SoldToShipTo.FirstOrDefault(c => c.SoldToShipTo_Id == data.productOwner_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            if (query.SoldToShipTo_Id == data.productOwner_Id)
                            {
                                data.productOwner_Id = "ProductOwner_Id".genAutonumber();
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    Model.ProductOwner_Id = data.productOwner_Id;
                    Model.Product_Index = data.product_Index;
                    Model.Product_Id = data.product_Id;
                    Model.Product_Name = data.product_Name;
                    Model.Owner_Index = item.owner_Index;
                    Model.Owner_Id = item.owner_Id;
                    Model.Owner_Name = item.owner_Name;
                    Model.Ref_No1 = data.ref_No1;
                    Model.Ref_No2 = data.ref_No2;
                    Model.Ref_No3 = data.ref_No3;
                    Model.Ref_No4 = data.ref_No4;
                    Model.Ref_No5 = data.ref_No5;
                    Model.Remark = data.remark;
                    Model.UDF_1 = data.udf_1;
                    Model.UDF_2 = data.udf_2;
                    Model.UDF_3 = data.udf_3;
                    Model.UDF_4 = data.udf_4;
                    Model.UDF_5 = data.udf_5;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_ProductOwner.Add(Model);
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
                    olog.logging("SaveProductOwner", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return "Done";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region export
        public actionResultProductOwnerViewModel export(SearchProductOwnerViewModel data)
        {
            try
            {
                var query = db.View_ProductOwner.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (data.changeSet != "1")
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductOwner_Id.Contains(data.key)
                                            || c.Owner_Name.Contains(data.key)
                                            || c.Product_Name.Contains(data.key));

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
                        query = query.Where(c => c.ProductOwner_Id.Contains(data.key)
                                            || c.Owner_Name.Contains(data.key)
                                            || c.Product_Name.Contains(data.key));

                    }
                }

                var Item = new List<View_ProductOwner>();
                var TotalRow = new List<View_ProductOwner>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.ProductOwner_Id).ToList();

                var result = new List<SearchProductOwnerViewModel>();
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new SearchProductOwnerViewModel();
                    resultItem.rowNum = num + 1;
                    resultItem.productOwner_Index = item.ProductOwner_Index;
                    resultItem.productOwner_Id = item.ProductOwner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var actionResultProductOwnerViewModel = new actionResultProductOwnerViewModel();
                actionResultProductOwnerViewModel.itemsProductOwner = result.ToList();

                return actionResultProductOwnerViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        //public actionResultProductOwnerViewModel Filter(ProductOwnerViewModel data)
        //{
        //    try
        //    {

        //        var res = new actionResultProductOwnerViewModel();
        //        var count = 0;
        //        var pwhereFilter = "";
        //        using (var context = new MasterDataDbContext())
        //        {
        //            pwhereFilter += " And ms_ProductOwner.isActive = '" + 1 + "'";
        //            pwhereFilter += " And ms_ProductOwner.isDelete = '" + 0 + "'";

        //            if (!string.IsNullOrEmpty(data.ProductOwnerId))
        //                pwhereFilter = " And ms_ProductOwner.ProductOwner_Id like N'%" + data.ProductOwnerId + "%'";

        //            if (!string.IsNullOrEmpty(data.ProductOwnerName))
        //                pwhereFilter = " And ms_ProductOwner.Product_Name like N'%" + data.ProductName + "%'";

        //            var strwheres = new SqlParameter("@strwhere", pwhereFilter);
        //            var PageNumber = new SqlParameter("@PageNumber", 1);
        //            var RowspPage = new SqlParameter("@RowspPage", 10000);

        //            count = context.MS_ProductOwnerPaging.FromSql("sp_GetProductOwnerPaging @strwhere , @PageNumber , @RowspPage ", strwheres, PageNumber, RowspPage).Count();

        //            var strwhere1 = new SqlParameter("@strwhere", pwhereFilter);
        //            var PageNumber1 = new SqlParameter("@PageNumber", data.CurrentPage);
        //            var RowspPage1 = new SqlParameter("@RowspPage", data.PerPage);
        //            var queryResultTotal = context.MS_ProductOwnerPaging.FromSql("sp_GetProductOwnerPaging @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).ToList();

        //            var result = new List<ProductOwnerViewModel>();
        //            foreach (var item in queryResultTotal)
        //            {
        //                var resultItem = new ProductOwnerViewModel();

        //              resultItem.ProductIndex = item.Product_Index;
        //                resultItem.ProductName = item.product_name;
        //                resultItem.OwnerIndex = item.Owner_Index;
        //                resultItem.OwnerName = item.owner_name;

        //                resultItem.ProductOwnerIndex = item.ProductOwner_Index;
        //                resultItem.ProductOwnerId = item.ProductOwner_Id;
        //                resultItem.IsActive = item.IsActive;
        //                resultItem.IsDelete = item.IsDelete;
        //                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                resultItem.CreateBy = item.Create_By;
        //                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                resultItem.UpdateBy = item.Update_By;
        //                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                result.Add(resultItem);
        //            }

        //            res.items = result;
        //            res.pagination = new Pagination { PerPage = data.PerPage, NumPerPage = data.PerPage, CurrentPage = data.CurrentPage, TotalRow = count };

        //            return res;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ProductOwnerViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductOwner.FromSql("sp_GetProductOwner").Where(c => c.ProductOwner_Index == id).ToList();
        //            var result = new List<ProductOwnerViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProductOwnerViewModel();
        //                resultItem.ProductOwnerIndex = item.ProductOwner_Index;
        //                resultItem.ProductOwnerId = item.ProductOwner_Id;
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

        //public String SaveChanges(ProductOwnerViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.ProductOwnerIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.ProductOwnerIndex = Guid.NewGuid();
        //            }
        //            if (data.ProductOwnerId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "ProductOwnerId");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.ProductOwnerId = resultParameter.Value.ToString();
        //            }
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int isSystem = 0;
        //            int statusId = 0;
        //            var ProductOwner_Index = new SqlParameter("ProductOwner_Index", data.ProductOwnerIndex);
        //            var ProductOwner_Id = new SqlParameter("ProductOwner_Id", data.ProductOwnerId);
        //            var Owner_Index = new SqlParameter("Owner_Index", data.OwnerIndex);
        //            var Product_Index = new SqlParameter("Product_Index", data.ProductIndex);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ProductOwner  @ProductOwner_Index,@ProductOwner_Id,@Owner_Index,@Product_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ProductOwner_Index, ProductOwner_Id, Owner_Index, Product_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ProductOwnerViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductOwner.FromSql("sp_GetProductOwner").Where(c => c.ProductOwner_Index == id).ToList();
        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<ProductOwnerViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var ProductOwner_Index = new SqlParameter("ProductOwner_Index", item.ProductOwner_Index);
        //                var ProductOwner_Id = new SqlParameter("ProductOwner_Index", item.ProductOwner_Id);
        //                var Owner_Index = new SqlParameter("Owner_Index", item.Owner_Index);
        //                var Product_Index = new SqlParameter("Product_Index", item.Product_Index);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ProductOwner  @ProductOwner_Index,@ProductOwner_Id,@Owner_Index,@Product_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ProductOwner_Index, ProductOwner_Id, Owner_Index, Product_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        //public actionResultProductOwnerViewModel search(ProductOwnerViewModel data)
        //{
        //    try
        //    {
        //        var res = new actionResultProductOwnerViewModel();
        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<ProductOwnerViewModel>();
        //            var queryResult = context.MS_ProductOwner.FromSql("sp_GetProductOwner").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
        //            if (data.ProductOwnerId != "" && data.ProductOwnerId != null)
        //            {
        //                pwhereFilter = " And ProductOwner_Id like N'%" + data.ProductOwnerId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter = "";
        //            }

        //            if (data.ProductOwnerId != "" && data.ProductOwnerId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_ProductOwner.FromSql("sp_GetProductOwner @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new ProductOwnerViewModel();

        //                    resultItem.ProductOwnerIndex = item.ProductOwner_Index;
        //                    resultItem.ProductOwnerId = item.ProductOwner_Id;
        //                    if (item.Product_Index != null)
        //                    {
        //                        var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                        resultItem.ProductIndex = itemList.Product_Index;
        //                        resultItem.ProductName = itemList.Product_Name;
        //                    }

        //                    if (item.Owner_Index != null)
        //                    {
        //                        var itemList = context.MS_Owner.FromSql("sp_GetOwner").Where(c => c.Owner_Index == item.Owner_Index).FirstOrDefault();
        //                        resultItem.OwnerIndex = itemList.Owner_Index;
        //                        resultItem.OwnerName = itemList.Owner_Name;
        //                    }
        //                    resultItem.IsActive = item.IsActive;
        //                    resultItem.IsDelete = item.IsDelete;
        //                    resultItem.IsSystem = item.IsSystem;
        //                    resultItem.StatusId = item.Status_Id;
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
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new ProductOwnerViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.Product_Index == ItemList.Product_Index)
        //                        {
        //                            resultItem.ProductOwnerIndex = item.ProductOwner_Index;
        //                            resultItem.ProductOwnerId = item.ProductOwner_Id;
        //                            if (item.Product_Index != null)
        //                            {
        //                                var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                                resultItem.ProductIndex = itemList.Product_Index;
        //                                resultItem.ProductName = itemList.Product_Name;
        //                            }

        //                            if (item.Owner_Index != null)
        //                            {
        //                                var itemList = context.MS_Owner.FromSql("sp_GetOwner").Where(c => c.Owner_Index == item.Owner_Index).FirstOrDefault();
        //                                resultItem.OwnerIndex = itemList.Owner_Index;
        //                                resultItem.OwnerName = itemList.Owner_Name;
        //                            }
        //                            resultItem.IsActive = item.IsActive;
        //                            resultItem.IsDelete = item.IsDelete;
        //                            resultItem.IsSystem = item.IsSystem;
        //                            resultItem.StatusId = item.Status_Id;
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
        //                pwhereLike = " And Owner_Name like N'%" + data.OwnerName + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_Owner.FromSql("sp_GetOwner @strwhere ", pstrwhere1).ToList();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new ProductOwnerViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.Owner_Index == ItemList.Owner_Index)
        //                        {
        //                            resultItem.ProductOwnerIndex = item.ProductOwner_Index;
        //                            resultItem.ProductOwnerId = item.ProductOwner_Id;
        //                            if (item.Product_Index != null)
        //                            {
        //                                var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                                resultItem.ProductIndex = itemList.Product_Index;
        //                                resultItem.ProductName = itemList.Product_Name;
        //                            }

        //                            resultItem.OwnerIndex = ItemList.Owner_Index;
        //                            resultItem.OwnerName = ItemList.Owner_Name;
        //                            resultItem.IsActive = item.IsActive;
        //                            resultItem.IsDelete = item.IsDelete;
        //                            resultItem.IsSystem = item.IsSystem;
        //                            resultItem.StatusId = item.Status_Id;
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

        //            if (data.ProductOwnerId == "" && data.ProductName == "" && data.OwnerName == "")
        //            {
        //                res = this.Filter(new ProductOwnerViewModel());
        //            }

        //            return res;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ProductViewModel> productPopup(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            //   string strwhere = " Vendor_Index IN ( ";

        //          //  var queryResult = context.MS_ProductOwner.FromSql("sp_GetProductOwner").Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Owner_Index == id).ToList();

        //            var result = new List<ProductViewModel>();


        //            //View_ProductPopup
        //            //var queryResult2 = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            //var queryResult3 = queryResult2.Where(x => queryResult.Select(a => a.Product_Index).Contains(x.Product_Index)).OrderBy(c => c.Product_Id);
        //            // Product_Index in ( Select Product_Index from ms_ProductOwner where Owner_Index  = '' )

        //            string pstring = " and Product_Index in ( Select Product_Index from ms_ProductOwner where Owner_Index  = N'" + id.ToString() + "' )";

        //            var strwhere = new SqlParameter("@strwhere", pstring);

        //            var queryResult3 = context.View_ProductPopup.FromSql("sp_GetView_ProductPopup @strwhere", strwhere).ToList();


        //            foreach (var item in queryResult3)
        //            {

        //                var resultItem = new ProductViewModel();

        //                resultItem.product_Index = item.Product_Index;
        //                resultItem.productConversion_Height = item.ProductConversion_Height;
        //                resultItem.productConversion_Length = item.ProductConversion_Length;
        //                resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
        //                resultItem.productConversion_Volume = item.ProductConversion_Volume;
        //                resultItem.productConversion_Weight = item.ProductConversion_Weight;
        //                resultItem.productConversion_Width = item.ProductConversion_Width;
        //                resultItem.productConversion_VolumeRatio = item.ProductConversion_VolumeRatio;


        //                //if (item.Product_Index != null)
        //                //{
        //                //    var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                //    resultItem.product_Index = itemList.Product_Index;
        //                //    resultItem.productConversion_Height = itemList.ProductConversion_Height;
        //                //    resultItem.productConversion_Length = itemList.ProductConversion_Length;
        //                //    resultItem.productConversion_Ratio = itemList.ProductConversion_Ratio;
        //                //    resultItem.productConversion_Volume = itemList.ProductConversion_Volume;
        //                //    resultItem.productConversion_Weight = itemList.ProductConversion_Weight;
        //                //    resultItem.productConversion_Width = itemList.ProductConversion_Width;
        //                //    resultItem.productConversion_VolumeRatio = itemList.ProductConversion_VolumeRatio;
        //                //}
        //                resultItem.product_Id = item.Product_Id;
        //                resultItem.product_Name = item.Product_Name;
        //                resultItem.product_SecondName = item.Product_SecondName;
        //                resultItem.product_ThirdName = item.Product_ThirdName;
        //                resultItem.productConversion_Index = item.ProductConversion_Index;
        //                resultItem.productConversion_Id = item.ProductConversion_Id;
        //                resultItem.productConversion_Name = item.ProductConversion_Name;                        
        //                resultItem.isActive = item.IsActive;
        //                resultItem.isDelete = item.IsDelete;
        //                resultItem.isSystem = item.IsSystem;
        //                resultItem.status_Id = item.Status_Id;
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

        //public actionResultProductViewModel FilterProduct(ProductViewModel model)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())

        //        {
        //            string pwhere1 = "";
        //            if (model.product_Name != null && model.product_Name != "")
        //            {
        //                pwhere1 += " And Product_Name like N'%" + model.product_Name + "%'";
        //            }
        //            else
        //            {
        //                pwhere1 += " ";
        //            }
        //            if(model.product_Id != null && model.product_Id != "")
        //            {
        //                pwhere1 += " And Product_Id = '" + model.product_Id + "'";
        //            }
        //            else
        //            {
        //                pwhere1 += " ";
        //            }


        //            if (model.owner_Index != null && model.owner_Index.ToString() != "00000000-0000-0000-0000-000000000000")
        //            {
        //                pwhere1 += "  and Product_Index in ( Select Product_Index from ms_ProductOwner where Owner_Index  = N'" + model.owner_Index.ToString() + "' ) ";
        //            }

        //            var strwhere1 = new SqlParameter("@strwhere", pwhere1);
        //            // var strwhere = new SqlParameter("@strwhere", DBNull.Value);
        //            var PageNumber1 = new SqlParameter("@PageNumber", 1);
        //            var RowspPage1 = new SqlParameter("@RowspPage", 10000);
        //            var queryResultTotal = context.MS_Product.FromSql("sp_GetView_ProductPopup @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            string pwhere = "";
        //            if (model.product_Name != null && model.product_Name != "")
        //            {
        //                if (queryResultTotal.Count > 0)
        //                {
        //                    pwhere = " And Product_Name like N'%" + model.product_Name + "%'";
        //                }
        //                else
        //                {
        //                    pwhere = " And Product_SecondName like N'%" + model.product_Name + "%'";
        //                }

        //            }
        //            else
        //            {
        //                pwhere = " ";
        //            }

        //            if (model.product_Id != null && model.product_Id != "")
        //            {
        //                pwhere += " And Product_Id = '" + model.product_Id + "'";
        //            }
        //            else
        //            {
        //                pwhere += " ";
        //            }


        //            if (model.owner_Index != null && model.owner_Index.ToString() != "00000000-0000-0000-0000-000000000000")
        //            {
        //                pwhere += "  and Product_Index in ( Select Product_Index from ms_ProductOwner where Owner_Index  = N'" + model.owner_Index.ToString() + "' ) ";
        //            }

        //            var strwhere = new SqlParameter("@strwhere", pwhere);
        //            // var strwhere = new SqlParameter("@strwhere", DBNull.Value);
        //            var PageNumber = new SqlParameter("@PageNumber", model.CurrentPage);
        //            var RowspPage = new SqlParameter("@RowspPage", model.NumPerPage);

        //            var queryResult = context.View_ProductPopup.FromSql("sp_GetView_ProductPopup @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber, RowspPage).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<ProductViewModel>();
        //            foreach (var item in queryResult)
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
        //                    var ProductCategory = new SqlParameter("@strwhere", " and ProductCategory_Index = '" + item.ProductCategory_Index + "'");
        //                    var itemList = context.MS_ProductCategory.FromSql("sp_GetProductCategory @strwhere", ProductCategory).FirstOrDefault();
        //                    resultItem.productCategory_Index = itemList.ProductCategory_Index;
        //                    resultItem.productCategory_Name = itemList.ProductCategory_Name;
        //                }
        //                if (item.ProductCategory_Index != null)
        //                {
        //                    var ProductSubType = new SqlParameter("@strwhere", " and ProductSubType_Index = '" + item.ProductSubType_Index + "'");
        //                    var itemList = context.MS_ProductSubType.FromSql("sp_GetProductSubType @strwhere", ProductSubType).FirstOrDefault();
        //                    resultItem.productSubType_Index = itemList.ProductSubType_Index;
        //                    resultItem.productSubType_Name = itemList.ProductSubType_Name;
        //                }
        //                if (item.ProductCategory_Index != null)
        //                {
        //                    var ProductType = new SqlParameter("@strwhere", " and ProductType_Index = '" + item.ProductType_Index + "'");
        //                    var itemList = context.MS_ProductType.FromSql("sp_GetProductType").FirstOrDefault();
        //                    resultItem.productType_Index = itemList.ProductType_Index;
        //                    resultItem.productType_Name = itemList.ProductType_Name;
        //                }
        //                //if (item.ProductCategory_Index != null)
        //                //{
        //                //    var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.ProductConversion_Index == item.ProductConversion_Index).FirstOrDefault();
        //                //    resultItem.ProductConversionIndex = itemList.ProductConversion_Index;
        //                //    resultItem.ProductConversionId = itemList.ProductConversion_Id;
        //                //    resultItem.ProductConversionName = itemList.ProductConversion_Name;
        //                //    resultItem.productConversion_Ratio = itemList.ProductConversion_Ratio;
        //                //    resultItem.productConversion_Weight = itemList.ProductConversion_Weight;
        //                //    resultItem.productConversion_Width = itemList.ProductConversion_Width;
        //                //    resultItem.productConversion_Length = itemList.ProductConversion_Length;
        //                //    resultItem.productConversion_Height = itemList.ProductConversion_Height;
        //                //    resultItem.productConversion_VolumeRatio = itemList.ProductConversion_VolumeRatio;
        //                //    resultItem.productConversion_Volume = itemList.ProductConversion_Volume;
        //                //}
        //                resultItem.productConversion_Index = item.ProductConversion_Index;
        //                resultItem.productConversion_Id = item.ProductConversion_Id;
        //                resultItem.productConversion_Name = item.ProductConversion_Name;
        //                resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
        //                resultItem.productConversion_Weight = item.ProductConversion_Weight;
        //                resultItem.productConversion_Width = item.ProductConversion_Width;
        //                resultItem.productConversion_Length = item.ProductConversion_Length;
        //                resultItem.productConversion_Height = item.ProductConversion_Height;
        //                resultItem.productConversion_VolumeRatio = item.ProductConversion_VolumeRatio;
        //                resultItem.productConversion_Volume = item.ProductConversion_Volume;

        //                resultItem.productItemLife_Y = item.ProductItemLife_Y;
        //                resultItem.productItemLife_M = item.ProductItemLife_M;
        //                resultItem.productItemLife_D = item.ProductItemLife_D;
        //                resultItem.productImage_Path = item.ProductImage_Path;
        //                resultItem.create_Date = item.Create_Date.GetValueOrDefault();
        //                resultItem.create_By = item.Create_By;
        //                resultItem.update_Date = item.Update_Date.GetValueOrDefault();
        //                resultItem.update_By = item.Update_By;
        //                resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.cancel_By = item.Cancel_By;

        //                result.Add(resultItem);
        //            }

        //            var count = queryResultTotal.Count;
        //            var actionResultProduct = new ProductViewModel.actionResultProductViewModel();
        //            actionResultProduct.itemsProduct = result.ToList();
        //            actionResultProduct.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage };

        //            //return actionResultVender;

        //            return actionResultProduct;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ProductViewModel> productPopupSearch(ProductOwnerViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var result = new List<ProductViewModel>();

        //            if (data.Chk == "1")
        //            {
        //                var query = context.MS_ProductOwner.FromSql("sp_GetProductOwner").Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Owner_Index == data.OwnerIndex).AsQueryable();

        //                if (!string.IsNullOrEmpty(data.ProductId))
        //                    query = query.Where(c => c.MS_Product.Product_Id == data.ProductId);

        //                var queryResult = query.ToList();                        

        //                var queryResult2 = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //                var queryResult3 = queryResult2.Where(x => queryResult.Select(a => a.Product_Index).Contains(x.Product_Index)).OrderBy(c => c.Product_Id);


        //                foreach (var item in queryResult3)
        //                {

        //                    var resultItem = new ProductViewModel();

        //                    if (item.Product_Index != null)
        //                    {
        //                        var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                        resultItem.product_Index = itemList.Product_Index;
        //                        resultItem.productConversion_Height = itemList.ProductConversion_Height;
        //                        resultItem.productConversion_Length = itemList.ProductConversion_Length;
        //                        resultItem.productConversion_Ratio = itemList.ProductConversion_Ratio;
        //                        resultItem.productConversion_Volume = itemList.ProductConversion_Volume;
        //                        resultItem.productConversion_Weight = itemList.ProductConversion_Weight;
        //                        resultItem.productConversion_Width = itemList.ProductConversion_Width;
        //                        resultItem.productConversion_VolumeRatio = itemList.ProductConversion_VolumeRatio;
        //                    }
        //                    resultItem.product_Id = item.Product_Id;
        //                    resultItem.product_Name = item.Product_Name;
        //                    resultItem.product_SecondName = item.Product_SecondName;
        //                    resultItem.product_ThirdName = item.Product_ThirdName;
        //                    resultItem.productConversion_Index = item.ProductConversion_Index;
        //                    resultItem.productConversion_Id = item.ProductConversion_Id;
        //                    resultItem.productConversion_Name = item.ProductConversion_Name;                            
        //                    resultItem.isActive = item.IsActive;
        //                    resultItem.isDelete = item.IsDelete;
        //                    resultItem.isSystem = item.IsSystem;
        //                    resultItem.status_Id = item.Status_Id;
        //                    result.Add(resultItem);


        //                }
        //            }
        //            else
        //            {
        //                var query = context.MS_ProductOwner.FromSql("sp_GetProductOwner").Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Owner_Index == data.OwnerIndex).AsQueryable();

        //                if (!string.IsNullOrEmpty(data.ProductId))
        //                    query = query.Where(c => c.MS_Product.Product_Id.Contains(data.ProductId));

        //                if (!string.IsNullOrEmpty(data.ProductName))
        //                    query = query.Where(c => c.MS_Product.Product_Name.ToUpper().Contains(data.ProductName.ToUpper()));

        //                var queryResult = query.ToList();


        //                var queryResult2 = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //                var queryResult3 = queryResult2.Where(x => queryResult.Select(a => a.Product_Index).Contains(x.Product_Index)).OrderBy(c => c.Product_Id);


        //                foreach (var item in queryResult3)
        //                {

        //                    var resultItem = new ProductViewModel();

        //                    if (item.Product_Index != null)
        //                    {
        //                        var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                        resultItem.product_Index = itemList.Product_Index;
        //                        resultItem.productConversion_Height = itemList.ProductConversion_Height;
        //                        resultItem.productConversion_Length = itemList.ProductConversion_Length;
        //                        resultItem.productConversion_Ratio = itemList.ProductConversion_Ratio;
        //                        resultItem.productConversion_Volume = itemList.ProductConversion_Volume;
        //                        resultItem.productConversion_Weight = itemList.ProductConversion_Weight;
        //                        resultItem.productConversion_Width = itemList.ProductConversion_Width;
        //                        resultItem.productConversion_VolumeRatio = itemList.ProductConversion_VolumeRatio;
        //                    }
        //                    resultItem.product_Id = item.Product_Id;
        //                    resultItem.product_Name = item.Product_Name;
        //                    resultItem.product_SecondName = item.Product_SecondName;
        //                    resultItem.product_ThirdName = item.Product_ThirdName;
        //                    resultItem.productConversion_Index = item.ProductConversion_Index;
        //                    resultItem.productConversion_Id = item.ProductConversion_Id;
        //                    resultItem.productConversion_Name = item.ProductConversion_Name;
        //                    resultItem.isActive = item.IsActive;
        //                    resultItem.isDelete = item.IsDelete;
        //                    resultItem.isSystem = item.IsSystem;
        //                    resultItem.status_Id = item.Status_Id;
        //                    result.Add(resultItem);


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

        //public actionResultProductViewModel FilterPopupProduct(ProductViewModel model)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())

        //        {
        //            string pwhere1 = "";
        //            if (model.product_Name != null && model.product_Name != "")
        //            {
        //                pwhere1 += " And Product_Name like N'%" + model.product_Name + "%'";
        //            }
        //            else
        //            {
        //                pwhere1 += " ";
        //            }
        //            if (model.product_Id != null && model.product_Id != "")
        //            {
        //                pwhere1 += " And Product_Id = '" + model.product_Id + "'";
        //            }
        //            else
        //            {
        //                pwhere1 += " ";
        //            }


        //            if (model.owner_Index != null && model.owner_Index.ToString() != "00000000-0000-0000-0000-000000000000")
        //            {
        //                pwhere1 += "  and Owner_Index = '" + model.owner_Index + "'";
        //            }

        //            var strwhere1 = new SqlParameter("@strwhere", pwhere1);
        //            // var strwhere = new SqlParameter("@strwhere", DBNull.Value);
        //            var PageNumber1 = new SqlParameter("@PageNumber", 1);
        //            var RowspPage1 = new SqlParameter("@RowspPage", 10000);
        //            var queryResultTotal = context.View_PopupProduct.FromSql("sp_Get_ViewPopupProduct @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).ToList();

        //            string pwhere = "";
        //            if (model.product_Name != null && model.product_Name != "")
        //            {
        //                if (queryResultTotal.Count > 0)
        //                {
        //                    pwhere = " And Product_Name like N'%" + model.product_Name + "%'";
        //                }
        //                else
        //                {
        //                    pwhere = " And Product_SecondName like N'%" + model.product_Name + "%'";
        //                }

        //            }
        //            else
        //            {
        //                pwhere = " ";
        //            }

        //            if (model.product_Id != null && model.product_Id != "")
        //            {
        //                pwhere += " And Product_Id = '" + model.product_Id + "'";
        //            }
        //            else
        //            {
        //                pwhere += " ";
        //            }


        //            if (model.owner_Index != null && model.owner_Index.ToString() != "00000000-0000-0000-0000-000000000000")
        //            {
        //                pwhere += "  and Owner_Index = '" + model.owner_Index + "'";
        //            }

        //            var strwhere = new SqlParameter("@strwhere", pwhere);
        //            // var strwhere = new SqlParameter("@strwhere", DBNull.Value);
        //            var PageNumber = new SqlParameter("@PageNumber", model.CurrentPage);
        //            var RowspPage = new SqlParameter("@RowspPage", model.NumPerPage);

        //            var queryResult = context.View_PopupProduct.FromSql("sp_Get_ViewPopupProduct @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber, RowspPage).ToList();

        //            var result = new List<ProductViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProductViewModel();
        //                resultItem.product_Index = item.Product_Index;
        //                resultItem.product_Id = item.Product_Id;
        //                resultItem.product_Index = item.Product_Index;
        //                resultItem.product_Name = item.Product_SecondName;
        //                resultItem.product_SecondName = item.Product_SecondName;
        //                resultItem.product_ThirdName = item.Product_ThirdName;



        //                result.Add(resultItem);
        //            }

        //            var count = queryResultTotal.Count;
        //            var actionResultProduct = new ProductViewModel.actionResultProductViewModel();
        //            actionResultProduct.itemsProduct = result.ToList();
        //            actionResultProduct.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage };

        //            //return actionResultVender;

        //            return actionResultProduct;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ProductViewModel> PopupSearch(ProductViewModel model)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())

        //        {

        //            var query = context.MS_Product.AsQueryable();

        //            if (!string.IsNullOrEmpty(model.product_Id))
        //            {
        //                query = query.Where(c => c.Product_Id.Contains(model.product_Id));
        //            }
        //            if (!string.IsNullOrEmpty(model.product_Name))
        //            {
        //                query = query.Where(c => c.Product_SecondName.Contains(model.product_SecondName));
        //            }
        //            //if (!string.IsNullOrEmpty(model.owner_Index.ToString()))
        //            //{
        //            //    query = query.Where(c => c.Owner_Index == model.owner_Index);
        //            //}

        //            var queryResult = query.Select(c => new {
        //                c.Product_Index,
        //                c.Product_Id,
        //                c.Product_Name,
        //                c.Product_SecondName,
        //                c.Product_ThirdName,
        //                c.ProductConversion_Id,
        //                c.ProductConversion_Index,
        //                c.ProductConversion_Name}).Distinct().Take(10).ToList();

        //            var result = new List<ProductViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProductViewModel();
        //                resultItem.product_Index = item.Product_Index;
        //                resultItem.product_Id = item.Product_Id;
        //                resultItem.product_Name = item.Product_Name;
        //                resultItem.product_SecondName = item.Product_SecondName;
        //                resultItem.product_ThirdName = item.Product_ThirdName;
        //                resultItem.productConversion_Id = item.ProductConversion_Id;
        //                resultItem.productConversion_Index = item.ProductConversion_Index;
        //                resultItem.productConversion_Name = item.ProductConversion_Name;


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

        //public List<View_GetProductOwnerViewModel> PopupSearchV2(View_GetProductOwnerViewModel model)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())

        //        {

        //            var query = context.View_GetProductOwner.AsQueryable();

        //            if (!string.IsNullOrEmpty(model.product_Id))
        //            {
        //                query = query.Where(c => c.Product_Id.Contains(model.product_Id));
        //            }
        //            if (!string.IsNullOrEmpty(model.product_Name))
        //            {
        //                query = query.Where(c => c.Product_SecondName.Contains(model.product_SecondName));
        //            }
        //            //if (!string.IsNullOrEmpty(model.owner_Index.ToString()))
        //            //{
        //            //    query = query.Where(c => c.Owner_Index == model.owner_Index);
        //            //}

        //            var queryResult = query.Select(c => new {
        //                c.Product_Index,
        //                c.Product_Id,
        //                c.Product_Name,
        //                c.Product_SecondName,
        //                c.Product_ThirdName,
        //                c.ProductConversion_Id,
        //                c.ProductConversion_Index,
        //                c.ProductConversion_Name,
        //                c.ProductConversion_Ratio,
        //                c.ProductConversion_Volume,
        //                c.ProductConversion_VolumeRatio,
        //                c.ProductConversion_Weight,
        //                c.ProductConversion_Width,
        //                c.ProductConversion_Height,
        //                c.ProductConversion_Length
        //            }).Distinct().ToList();

        //            var result = new List<View_GetProductOwnerViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new View_GetProductOwnerViewModel();
        //                resultItem.product_Index = item.Product_Index;
        //                resultItem.product_Id = item.Product_Id;
        //                resultItem.product_Name = item.Product_Name;
        //                resultItem.product_SecondName = item.Product_SecondName;
        //                resultItem.product_ThirdName = item.Product_ThirdName;
        //                resultItem.productConversion_Id = item.ProductConversion_Id;
        //                resultItem.productConversion_Index = item.ProductConversion_Index;
        //                resultItem.productConversion_Name = item.ProductConversion_Name;
        //                resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
        //                resultItem.productConversion_Volume = item.ProductConversion_Volume;
        //                resultItem.productConversion_Height = item.ProductConversion_Height;
        //                resultItem.productConversion_Length = item.ProductConversion_Length;
        //                resultItem.productConversion_VolumeRatio = item.ProductConversion_VolumeRatio;
        //                resultItem.productConversion_Weight = item.ProductConversion_Weight;
        //                resultItem.productConversion_Width = item.ProductConversion_Width;
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

    }
}

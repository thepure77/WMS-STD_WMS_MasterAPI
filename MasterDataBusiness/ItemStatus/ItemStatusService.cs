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
    public class ItemStatusService
    {
        #region BeforeCodeItemStatus
        //public List<ItemStatusViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ItemStatus.FromSql("sp_GetItemStatus").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<ItemStatusViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ItemStatusViewModel();
        //                resultItem.ItemStatusIndex = item.ItemStatus_Index;
        //                resultItem.ItemStatusId = item.ItemStatus_Id;
        //                resultItem.ItemStatusName = item.ItemStatus_Name;
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
        //public List<ItemStatusViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ItemStatus.FromSql("sp_GetItemStatus").Where(c => c.ItemStatus_Index == id).ToList();

        //            var result = new List<ItemStatusViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ItemStatusViewModel();
        //                resultItem.ItemStatusIndex = item.ItemStatus_Index;
        //                resultItem.ItemStatusId = item.ItemStatus_Id;
        //                resultItem.ItemStatusName = item.ItemStatus_Name;
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
        //public String SaveChanges(ItemStatusViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.ItemStatusIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.ItemStatusIndex = Guid.NewGuid();
        //            }
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int issystem = 0;
        //            int statusid = 0;
        //            if (data.ItemStatusId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "ItemStatusId");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.ItemStatusId = resultParameter.Value.ToString();
        //            }
        //            var ItemStatus_Id = new SqlParameter("ItemStatus_Id", data.ItemStatusId);
        //            var ItemStatus_Index = new SqlParameter("ItemStatus_Index", data.ItemStatusIndex);                    
        //            var ItemStatus_Name = new SqlParameter("ItemStatus_Name", data.ItemStatusName);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ItemStatus  @ItemStatus_Index,@ItemStatus_Id,@ItemStatus_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ItemStatus_Index, ItemStatus_Id, ItemStatus_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ItemStatusViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ItemStatus.FromSql("sp_GetItemStatus").Where(c => c.ItemStatus_Index == id).ToList();
        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<ItemStatusViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var ItemStatus_Id = new SqlParameter("ItemStatus_Id", item.ItemStatus_Id);
        //                var ItemStatus_Index = new SqlParameter("ItemStatus_Index", item.ItemStatus_Index);
        //                var ItemStatus_Name = new SqlParameter("ItemStatus_Name", item.ItemStatus_Name);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ItemStatus  @ItemStatus_Index,@ItemStatus_Id,@ItemStatus_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ItemStatus_Index, ItemStatus_Id, ItemStatus_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        ////public List<ItemStatusViewModel> search(ItemStatusViewModel data)
        ////{
        ////    try
        ////    {

        ////        using (var context = new MasterDataDbContext())
        ////        {
        ////            var result = new List<ItemStatusViewModel>();

        ////            string pwhere1 = "";
        ////            if (data.ItemStatusId != null && data.ItemStatusId != "")
        ////            {
        ////                pwhere1 += " And ItemStatus_Id like N'%" + data.ItemStatusId + "%'";
        ////            }
        ////            else
        ////            {
        ////                pwhere1 += " ";
        ////            }
        ////            if (data.ItemStatusName != null && data.ItemStatusName != "")
        ////            {
        ////                pwhere1 += " And ItemStatus_Name = '" + data.ItemStatusName + "'";
        ////            }
        ////            else
        ////            {
        ////                pwhere1 += " ";
        ////            }

        ////            var query = context.MS_ItemStatus.FromSql("sp_GetSearchItemStatus @pId ,@pName ", id, name).Where( c => c.IsActive == 1 && c.IsDelete == 0).ToList();
        ////            foreach (var item in query)
        ////            {
        ////                var resultItem = new ItemStatusViewModel();
        ////                resultItem.ItemStatusIndex = item.ItemStatus_Index;
        ////                resultItem.ItemStatusId = item.ItemStatus_Id;
        ////                resultItem.ItemStatusName = item.ItemStatus_Name;
        ////                result.Add(resultItem);
        ////            }


        ////            return result;
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}
        //public List<ItemStatusDocViewModel> search(ItemStatusDocViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {

        //            var result = new List<ItemStatusDocViewModel>();

        //            var query = context.MS_ItemStatus.AsQueryable();

        //            if (!string.IsNullOrEmpty(data.itemStatus_Index.ToString()))
        //                query = query.Where(c => c.ItemStatus_Index == data.itemStatus_Index);

        //            if (!string.IsNullOrEmpty(data.itemStatus_Id))
        //                query = query.Where(c => c.ItemStatus_Id == data.itemStatus_Id);

        //            if (!string.IsNullOrEmpty(data.itemStatus_Name))
        //                query = query.Where(c => c.ItemStatus_Name == data.itemStatus_Name);

        //            var queryResult = query.OrderBy(o => o.ItemStatus_Name).ToList();

        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ItemStatusDocViewModel();
        //                resultItem.itemStatus_Index = item.ItemStatus_Index;
        //                resultItem.itemStatus_Id = item.ItemStatus_Id;
        //                resultItem.itemStatus_Name = item.ItemStatus_Name;
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

        public List<ItemStatusDocViewModel> itemStatusfilter(ItemStatusDocViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var result = new List<ItemStatusDocViewModel>();

                    var query = context.MS_ItemStatus.AsQueryable();



                    var queryResult = query.OrderBy(o => o.ItemStatus_Name).ToList();

                    foreach (var item in queryResult)
                    {
                        var resultItem = new ItemStatusDocViewModel();
                        resultItem.itemStatus_Index = item.ItemStatus_Index;
                        resultItem.itemStatus_Id = item.ItemStatus_Id;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.stck_Type = item.Stck_Type;

                        result.Add(resultItem);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindItemStatus

        public ItemStatusViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_ItemStatus.Where(c => c.ItemStatus_Index == id).FirstOrDefault();

                var result = new ItemStatusViewModel();


                result.itemStatus_Index = queryResult.ItemStatus_Index;
                result.itemStatus_Id = queryResult.ItemStatus_Id;
                result.itemStatus_Name = queryResult.ItemStatus_Name;
                result.isActive = queryResult.IsActive;


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region FilterItemStatus
        //Filter
        private MasterDataDbContext db;

        public ItemStatusService()
        {
            db = new MasterDataDbContext();
        }

        public ItemStatusService(MasterDataDbContext db)
        {
            this.db = db;
        }


        
        public actionResultItemStatusViewModel filter(SearchItemStatusViewModel data)
        {
            try
            {
                var query = db.MS_ItemStatus.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.ItemStatus_Id.Contains(data.key)
                                        || c.ItemStatus_Name.Contains(data.key));


                }
                if (!string.IsNullOrEmpty(data.createdateitemstatus_date) && !string.IsNullOrEmpty(data.createdateitemstatus_date_to))
                {
                    var dateStart = data.createdateitemstatus_date.toBetweenDate();
                    var dateEnd = data.createdateitemstatus_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }

                var Item = new List<MS_ItemStatus>();
                var TotalRow = new List<MS_ItemStatus>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ItemStatus_Id).ToList();

                var result = new List<SearchItemStatusViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchItemStatusViewModel();

                    resultItem.itemStatus_Index = item.ItemStatus_Index;
                    resultItem.itemStatus_Id = item.ItemStatus_Id;
                    resultItem.itemStatus_Name = item.ItemStatus_Name;
                    resultItem.create_By = item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultItemStatusViewModel = new actionResultItemStatusViewModel();
                actionResultItemStatusViewModel.itemsItemStatus = result.ToList();
                actionResultItemStatusViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultItemStatusViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region GetDelete
        public Boolean getDelete(ItemStatusViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var itemStatus = db.MS_ItemStatus.Find(data.itemStatus_Index);

                if (itemStatus != null)
                {
                    itemStatus.IsActive = 0;
                    itemStatus.IsDelete = 1;


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
                        olog.logging("DeleteItemStatus", msglog);
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

        #region SaveChanges

        public String SaveChanges(ItemStatusViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ItemStatusOld = db.MS_ItemStatus.Find(data.itemStatus_Index);

                if (ItemStatusOld == null)
                {

                    if (!string.IsNullOrEmpty(data.itemStatus_Id))
                    {
                        var query = db.MS_ItemStatus.FirstOrDefault(c => c.ItemStatus_Id == data.itemStatus_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.itemStatus_Id))
                    {
                        data.itemStatus_Id = "ItemStatus_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_ItemStatus.FirstOrDefault(c => c.ItemStatus_Id == data.itemStatus_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ItemStatus_Id == data.itemStatus_Id)
                                {
                                    data.itemStatus_Id = "ItemStatus_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_ItemStatus Model = new MS_ItemStatus();

                    Model.ItemStatus_Index = Guid.NewGuid();
                    Model.ItemStatus_Id = data.itemStatus_Id;
                    Model.ItemStatus_Name = data.itemStatus_Name;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_ItemStatus.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.itemStatus_Id))
                    {
                        if (ItemStatusOld.ItemStatus_Id != "")
                        {
                            data.itemStatus_Id = ItemStatusOld.ItemStatus_Id;
                        }
                    }
                    else
                    {
                        if (ItemStatusOld.ItemStatus_Id != data.itemStatus_Id)
                        {
                            var query = db.MS_ItemStatus.FirstOrDefault(c => c.ItemStatus_Id == data.itemStatus_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.itemStatus_Id = ItemStatusOld.ItemStatus_Id;
                        }
                    }

                    ItemStatusOld.ItemStatus_Id = data.itemStatus_Id;
                    ItemStatusOld.ItemStatus_Name = data.itemStatus_Name;
                    ItemStatusOld.IsActive = Convert.ToInt32(data.isActive);
                    ItemStatusOld.Update_By = data.update_By;
                    ItemStatusOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveItemStatus", msglog);
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

        //#region SearchItemStatus


        //public List<ItemListViewModel> autoSearchItemStatus(ItemListViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())

        //        {
        //            var query = context.MS_ItemStatus.AsQueryable();

        //            if (!string.IsNullOrEmpty(data.key))
        //            {
        //                query = query.Where(c => c.ItemStatus_Name.Contains(data.key));
        //            }


        //            var items = new List<ItemListViewModel>();

        //            var result = query.Select(c => new { c.ItemStatus_Index, c.ItemStatus_Id, c.ItemStatus_Name }).Distinct().Take(10).ToList();

        //            foreach (var item in result)
        //            {
        //                var resultItem = new ItemListViewModel
        //                {
        //                    index = item.ItemStatus_Index,
        //                    id = item.ItemStatus_Id,
        //                    name = item.ItemStatus_Name,
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
        //#endregion

        #region autoSearchItemStatusFilter
        public List<ItemListViewModel> autoSearchItemStatusFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_ItemStatus.Where(c => c.ItemStatus_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ItemStatus_Name,
                        key = s.ItemStatus_Name
                    }).Distinct();

                    var query2 = db.MS_ItemStatus.Where(c => c.ItemStatus_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ItemStatus_Id,
                        key = s.ItemStatus_Id
                    }).Distinct();



                    var query = query1.Union(query2).Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {

            }

            return items;
        }

        #endregion

        #region Export Excel
        public actionResultItemStatusViewModel Export(SearchItemStatusViewModel data)
        {
            try
            {
                var query = db.MS_ItemStatus.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.ItemStatus_Id.Contains(data.key)
                                        || c.ItemStatus_Name.Contains(data.key));


                }
                if (!string.IsNullOrEmpty(data.createdateitemstatus_date) && !string.IsNullOrEmpty(data.createdateitemstatus_date_to))
                {
                    var dateStart = data.createdateitemstatus_date.toBetweenDate();
                    var dateEnd = data.createdateitemstatus_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }

                var Item = new List<MS_ItemStatus>();
                var TotalRow = new List<MS_ItemStatus>();

                TotalRow = query.ToList();


                Item = query.OrderBy(o => o.ItemStatus_Id).ToList();

                var result = new List<SearchItemStatusViewModel>();
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new SearchItemStatusViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.itemStatus_Index = item.ItemStatus_Index;
                    resultItem.itemStatus_Id = item.ItemStatus_Id;
                    resultItem.itemStatus_Name = item.ItemStatus_Name;
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    

                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var actionResultItemStatusViewModel = new actionResultItemStatusViewModel();
                actionResultItemStatusViewModel.itemsItemStatus = result.ToList();
                actionResultItemStatusViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultItemStatusViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public List<ItemStatusDocViewModel> ConfigItemStatusDoc(ItemStatusDocViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {


                    var items = new List<ItemStatusDocViewModel>();

                        var query = db.MS_ItemStatus.ToList();


                        foreach (var item in query)
                        {
                            var resultItem = new ItemStatusDocViewModel();

                            resultItem.itemStatus_Index = item.ItemStatus_Index;
                            resultItem.itemStatus_Id = item.ItemStatus_Id;
                            resultItem.itemStatus_Name = item.ItemStatus_Name;

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

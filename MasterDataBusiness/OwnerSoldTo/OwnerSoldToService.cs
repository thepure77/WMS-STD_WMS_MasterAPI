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
    public class OwnerSoldToService
    {
        private MasterDataDbContext db;

        public OwnerSoldToService()
        {
            db = new MasterDataDbContext();
        }

        public OwnerSoldToService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterOwnerSoldTo
        public actionResultOwnerSoldToViewModel filter(SearchOwnerSoldToViewModel data)
        {
            try
            {
                var query = db.View_OwnerSoldTo.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.OwnerSoldTo_Id.Contains(data.key)
                                         || c.Owner_Id.Contains(data.key)
                                         || c.Owner_Name.Contains(data.key)
                                         || c.SoldTo_Id.Contains(data.key)
                                         || c.SoldTo_Name.Contains(data.key));
                }

                var Item = new List<View_OwnerSoldTo>();
                var TotalRow = new List<View_OwnerSoldTo>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.OwnerSoldTo_Id).ToList();

                var result = new List<SearchOwnerSoldToViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchOwnerSoldToViewModel();

                    resultItem.ownerSoldTo_Index = item.OwnerSoldTo_Index;
                    resultItem.ownerSoldTo_Id = item.OwnerSoldTo_Id;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.soldTo_Index = item.SoldTo_Index;
                    resultItem.soldTo_Id = item.SoldTo_Id;
                    resultItem.soldTo_Name = item.SoldTo_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultOwnerSoldToViewModel = new actionResultOwnerSoldToViewModel();
                actionResultOwnerSoldToViewModel.itemsOwnerSoldTo = result.ToList();
                actionResultOwnerSoldToViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultOwnerSoldToViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(OwnerSoldToViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var OwnerSoldToOld = db.MS_OwnerSoldTo.Find(data.ownerSoldTo_Index);

                if (OwnerSoldToOld == null)
                {
                    if (!string.IsNullOrEmpty(data.ownerSoldTo_Id))
                    {
                        var query = db.MS_OwnerSoldTo.FirstOrDefault(c => c.OwnerSoldTo_Id == data.ownerSoldTo_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.ownerSoldTo_Id))
                    {
                        data.ownerSoldTo_Id = "OwnerSoldTo_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_OwnerSoldTo.FirstOrDefault(c => c.OwnerSoldTo_Id == data.ownerSoldTo_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.OwnerSoldTo_Id == data.ownerSoldTo_Id)
                                {
                                    data.ownerSoldTo_Id = "OwnerSoldTo_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    //MS_OwnerSoldTo Model = new MS_OwnerSoldTo();

                    //Model.OwnerSoldTo_Index = Guid.NewGuid();
                    //Model.OwnerSoldTo_Id = data.ownerSoldTo_Id;
                    //Model.Owner_Index = data.owner_Index;
                    //Model.SoldTo_Index = data.soldTo_Index;
                    //Model.IsActive = Convert.ToInt32(data.isActive);
                    //Model.IsDelete = 0;
                    //Model.IsSystem = 0;
                    //Model.Status_Id = 0;
                    //Model.Create_By = data.create_By;
                    //Model.Create_Date = DateTime.Now;

                    foreach (var item in data.listOwnerSoldToItemViewModel)
                    {
                        MS_OwnerSoldTo resultItem = new MS_OwnerSoldTo();

                        resultItem.OwnerSoldTo_Index = Guid.NewGuid();
                        resultItem.Owner_Index = data.owner_Index;
                        resultItem.SoldTo_Index = item.soldTo_Index;
                        resultItem.OwnerSoldTo_Id = data.ownerSoldTo_Id;
                        resultItem.IsActive = 1;
                        resultItem.IsDelete = 0;
                        resultItem.IsSystem = 0;
                        resultItem.Status_Id = 0;
                        resultItem.Create_By = data.create_By;
                        resultItem.Create_Date = DateTime.Now;
                        db.MS_OwnerSoldTo.Add(resultItem);

                    }

                    //db.MS_OwnerSoldTo.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.ownerSoldTo_Id))
                    {
                        if (OwnerSoldToOld.OwnerSoldTo_Id != "")
                        {
                            data.ownerSoldTo_Id = OwnerSoldToOld.OwnerSoldTo_Id;
                        }
                    }
                    else
                    {
                        if (OwnerSoldToOld.OwnerSoldTo_Id != data.ownerSoldTo_Id)
                        {
                            var query = db.MS_OwnerSoldTo.FirstOrDefault(c => c.OwnerSoldTo_Id == data.ownerSoldTo_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.ownerSoldTo_Id = OwnerSoldToOld.OwnerSoldTo_Id;
                        }
                    }

                    OwnerSoldToOld.OwnerSoldTo_Id = data.ownerSoldTo_Id;
                    OwnerSoldToOld.Owner_Index = data.owner_Index;
                    OwnerSoldToOld.SoldTo_Index = data.soldTo_Index;
                    OwnerSoldToOld.IsActive = Convert.ToInt32(data.isActive);
                    OwnerSoldToOld.Update_By = data.create_By;
                    OwnerSoldToOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveOwnerSoldTo", msglog);
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
        public OwnerSoldToViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_OwnerSoldTo.Where(c => c.OwnerSoldTo_Index == id).FirstOrDefault();

                var result = new OwnerSoldToViewModel();


                result.ownerSoldTo_Index = queryResult.OwnerSoldTo_Index;
                result.ownerSoldTo_Id = queryResult.OwnerSoldTo_Id;
                result.owner_Index = queryResult.Owner_Index;
                result.owner_Id = queryResult.Owner_Id;
                result.owner_Name = queryResult.Owner_Name;
                result.soldTo_Index = queryResult.SoldTo_Index;
                result.soldTo_Id = queryResult.SoldTo_Id;
                result.soldTo_Name = queryResult.SoldTo_Name;
                result.key = queryResult.Owner_Id + " - " + queryResult.Owner_Name;
                result.key2 = queryResult.SoldTo_Id + " - " + queryResult.SoldTo_Name;
                result.isActive = queryResult.IsActive;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region findOwnerSoldTo
        public OwnerSoldToViewModel findOwnerSoldTo(Guid id)
        {
            try
            {

                var queryResult = db.View_OwnerSoldTo.Where(c => c.Owner_Index == id && c.IsActive ==1).ToList();
 
              
                var result = new OwnerSoldToViewModel();

                result.listOwnerSoldToItemViewModel = new List<OwnerSoldToItemViewModel>();

                foreach (var item in queryResult)
                {
                    var resultItem = new OwnerSoldToItemViewModel();
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.ownerSoldTo_Index = item.OwnerSoldTo_Index;
                    resultItem.soldTo_Index = item.SoldTo_Index;
                    resultItem.soldTo_Id = item.SoldTo_Id;
                    resultItem.soldTo_Name = item.SoldTo_Name;
                    

                    result.listOwnerSoldToItemViewModel.Add(resultItem);
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
        public Boolean getDelete(OwnerSoldToViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Owner = db.MS_OwnerSoldTo.Find(data.ownerSoldTo_Index);

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
                        olog.logging("DeleteOwnerSoldTo", msglog);
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

        //public List<OwnerSoldToViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_OwnerSoldTo.FromSql("sp_GetOwnerSoldTo").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<OwnerSoldToViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new OwnerSoldToViewModel();

        //                resultItem.OwnerSoldToIndex = item.OwnerSoldTo_Index;
        //                resultItem.OwnerSoldToId = item.OwnerSoldTo_Id;
        //                if (item.Owner_Index != null)
        //                {
        //                    var itemList = context.MS_Owner.FromSql("sp_GetOwner").Where(c => c.Owner_Index == item.Owner_Index).FirstOrDefault();
        //                    resultItem.OwnerIndex = itemList.Owner_Index;
        //                    resultItem.OwnerName = itemList.Owner_Name;
        //                }
        //                if (item.SoldTo_Index != null)
        //                {
        //                    var itemList = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.SoldTo_Index == item.SoldTo_Index).FirstOrDefault();
        //                    resultItem.SoldToIndex = itemList.SoldTo_Index;
        //                    resultItem.SoldToName = itemList.SoldTo_Name;
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

        //public String SaveChanges(OwnerSoldToViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.OwnerSoldToIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.OwnerSoldToIndex = Guid.NewGuid();
        //            }
        //            if (data.OwnerSoldToId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "OwnerSoldToId");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.OwnerSoldToId = resultParameter.Value.ToString();
        //            }
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int isSystem = 0;
        //            int statusId = 0;
        //            var OwnerSoldTo_Index = new SqlParameter("OwnerSoldTo_Index", data.OwnerSoldToIndex);
        //            var OwnerSoldTo_Id = new SqlParameter("OwnerSoldTo_Id", data.OwnerSoldToId);
        //            var Owner_Index = new SqlParameter("Owner_Index", data.OwnerIndex);
        //            var SoldTo_Index = new SqlParameter("SoldTo_Index", data.SoldToIndex);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_OwnerSoldTo  @OwnerSoldTo_Index,@OwnerSoldTo_Id,@Owner_Index,@SoldTo_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", OwnerSoldTo_Index, OwnerSoldTo_Id, Owner_Index, SoldTo_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<OwnerSoldToViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_OwnerSoldTo.FromSql("sp_GetOwnerSoldTo").Where(c => c.OwnerSoldTo_Index == id).ToList();

        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<OwnerSoldToViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var OwnerSoldTo_Index = new SqlParameter("OwnerSoldTo_Index", item.OwnerSoldTo_Index);
        //                var OwnerSoldTo_Id = new SqlParameter("OwnerSoldTo_Id", item.OwnerSoldTo_Id);
        //                var Owner_Index = new SqlParameter("Owner_Index", item.Owner_Index);
        //                var SoldTo_Index = new SqlParameter("SoldTo_Index", item.SoldTo_Index);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_OwnerSoldTo  @OwnerSoldTo_Index,@OwnerSoldTo_Id,@Owner_Index,@SoldTo_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", OwnerSoldTo_Index, OwnerSoldTo_Id, Owner_Index, SoldTo_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        //public List<OwnerSoldToViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_OwnerSoldTo.FromSql("sp_GetOwnerSoldTo").Where(c => c.OwnerSoldTo_Index == id).ToList();
        //            var result = new List<OwnerSoldToViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new OwnerSoldToViewModel();
        //                resultItem.OwnerSoldToIndex = item.OwnerSoldTo_Index;
        //                resultItem.OwnerSoldToId = item.OwnerSoldTo_Id;
        //                if (item.Owner_Index != null)
        //                {
        //                    var itemList = context.MS_Owner.FromSql("sp_GetOwner").Where(c => c.Owner_Index == item.Owner_Index).FirstOrDefault();
        //                    resultItem.OwnerIndex = itemList.Owner_Index;
        //                    resultItem.OwnerName = itemList.Owner_Name;
        //                }
        //                if (item.SoldTo_Index != null)
        //                {
        //                    var itemList = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.SoldTo_Index == item.SoldTo_Index).FirstOrDefault();
        //                    resultItem.SoldToIndex = itemList.SoldTo_Index;
        //                    resultItem.SoldToName = itemList.SoldTo_Name;
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
        //public List<OwnerSoldToViewModel> search(OwnerSoldToViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhere = "";
        //            string pwhereLike = "";
        //            var queryResult = context.MS_OwnerSoldTo.FromSql("sp_GetOwnerSoldTo").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
        //            var result = new List<OwnerSoldToViewModel>();

        //            if (data.OwnerSoldToId != null && data.OwnerSoldToId != "")
        //            {
        //                pwhere = " And OwnerSoldTo_Id like N'%" + data.OwnerSoldToId + "%'";
        //            }
        //            else
        //            {
        //                pwhere = " ";
        //            }

        //            if (data.OwnerSoldToId != null && data.OwnerSoldToId != "")
        //            {
        //                pwhere += " And isActive = '" + 1 + "'";
        //                pwhere += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhere);
        //                var query = context.MS_OwnerSoldTo.FromSql("sp_GetOwnerSoldTo @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new OwnerSoldToViewModel();
        //                    var strwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                    var dataList = context.MS_Owner.FromSql("sp_GetOwner @strwhere ", strwhere1).ToList();

        //                    resultItem.OwnerSoldToIndex = item.OwnerSoldTo_Index;
        //                    resultItem.OwnerSoldToId = item.OwnerSoldTo_Id;
        //                    if (item.Owner_Index != null)
        //                    {
        //                        var itemList = context.MS_Owner.FromSql("sp_GetOwner").Where(c => c.Owner_Index == item.Owner_Index).FirstOrDefault();
        //                        resultItem.OwnerIndex = itemList.Owner_Index;
        //                        resultItem.OwnerName = itemList.Owner_Name;
        //                    }
        //                    if (item.SoldTo_Index != null)
        //                    {
        //                        var itemList = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.SoldTo_Index == item.SoldTo_Index).FirstOrDefault();
        //                        resultItem.SoldToIndex = itemList.SoldTo_Index;
        //                        resultItem.SoldToName = itemList.SoldTo_Name;
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
        //            else if (data.SoldToName != "" && data.SoldToName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And SoldTo_Name like N'%" + data.SoldToName + "%'";
        //                var strwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_SoldTo.FromSql("sp_GetSoldTo @strwhere ", strwhere1).ToList();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new OwnerSoldToViewModel();

        //                    foreach (var soldTo in dataList)
        //                    {
        //                        if (item.SoldTo_Index == soldTo.SoldTo_Index)
        //                        {
        //                            resultItem.OwnerSoldToIndex = item.OwnerSoldTo_Index;
        //                            resultItem.OwnerSoldToId = item.OwnerSoldTo_Id;
        //                            if (item.Owner_Index != null)
        //                            {
        //                                var itemList = context.MS_Owner.FromSql("sp_GetOwner").Where(c => c.Owner_Index == item.Owner_Index).FirstOrDefault();
        //                                resultItem.OwnerIndex = itemList.Owner_Index;
        //                                resultItem.OwnerName = itemList.Owner_Name;
        //                            }
        //                            if (item.SoldTo_Index != null)
        //                            {
        //                                //var itemList = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.SoldTo_Index == item.SoldTo_Index).FirstOrDefault();
        //                                resultItem.SoldToIndex = soldTo.SoldTo_Index;
        //                                resultItem.SoldToName = soldTo.SoldTo_Name;
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

        //            else if (data.OwnerSoldToId != null && data.OwnerSoldToId != "" && data.SoldToName != "" && data.SoldToName != null)
        //            {
        //                pwhere += " And isActive = '" + 1 + "'";
        //                pwhere += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhere);
        //                var query = context.MS_OwnerSoldTo.FromSql("sp_GetOwnerSoldTo @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new OwnerSoldToViewModel();
        //                    var strwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                    var dataList = context.MS_Owner.FromSql("sp_GetOwner @strwhere ", strwhere1).ToList();

        //                    resultItem.OwnerSoldToIndex = item.OwnerSoldTo_Index;
        //                    resultItem.OwnerSoldToId = item.OwnerSoldTo_Id;
        //                    if (item.Owner_Index != null)
        //                    {
        //                        var itemList = context.MS_Owner.FromSql("sp_GetOwner").Where(c => c.Owner_Index == item.Owner_Index).FirstOrDefault();
        //                        resultItem.OwnerIndex = itemList.Owner_Index;
        //                        resultItem.OwnerName = itemList.Owner_Name;
        //                    }
        //                    if (item.SoldTo_Index != null)
        //                    {
        //                        var itemList = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.SoldTo_Index == item.SoldTo_Index).FirstOrDefault();
        //                        resultItem.SoldToIndex = itemList.SoldTo_Index;
        //                        resultItem.SoldToName = itemList.SoldTo_Name;
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
        //            else if (data.OwnerName != "" && data.OwnerName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And Owner_Name like N'%" + data.OwnerName + "%'";
        //                var strwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_Owner.FromSql("sp_GetOwner @strwhere ", strwhere1).ToList();

        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new OwnerSoldToViewModel();
        //                    foreach (var owner in dataList)
        //                    {
        //                        if (item.Owner_Index == owner.Owner_Index)
        //                        {
        //                            resultItem.OwnerSoldToIndex = item.OwnerSoldTo_Index;
        //                            resultItem.OwnerSoldToId = item.OwnerSoldTo_Id;
        //                            if (item.Owner_Index != null)
        //                            {
        //                                resultItem.OwnerIndex = owner.Owner_Index;
        //                                resultItem.OwnerName = owner.Owner_Name;
        //                            }
        //                            if (item.SoldTo_Index != null)
        //                            {
        //                                var itemList = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.SoldTo_Index == item.SoldTo_Index).FirstOrDefault();
        //                                resultItem.SoldToIndex = itemList.SoldTo_Index;
        //                                resultItem.SoldToName = itemList.SoldTo_Name;
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

        //            if (data.OwnerSoldToId == "" && data.OwnerName == "" && data.SoldToName == "")
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

        //public List<SoldToViewModel> SoldToPopup(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            //   string strwhere = " Vendor_Index IN ( ";

        //            var queryResult = context.MS_OwnerSoldTo.FromSql("sp_GetOwnerSoldTo").Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Owner_Index == id).ToList();

        //            var result = new List<SoldToViewModel>();


        //            var queryResult2 = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var queryResult3 = queryResult2.Where(x => queryResult.Select(a => a.SoldTo_Index).Contains(x.SoldTo_Index));


        //            foreach (var item in queryResult3)
        //            {

        //                var resultItem = new SoldToViewModel();

        //                resultItem.soldTo_Index = item.SoldTo_Index;
        //                resultItem.soldTo_Id = item.SoldTo_Id;
        //                resultItem.soldTo_Name = item.SoldTo_Name;
        //                resultItem.soldTo_Address = item.SoldTo_Address;
        //                resultItem.soldTo_Barcode = item.SoldTo_Barcode;
        //                resultItem.soldTo_Email = item.SoldTo_Barcode;
        //                resultItem.subDistrict_Index = item.SubDistrict_Index;
        //                resultItem.district_Index = item.District_Index;
        //                resultItem.province_Index = item.Province_Index;
        //                resultItem.country_Index = item.Country_Index;
        //                resultItem.postcode_Index = item.Postcode_Index;
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

    }
}

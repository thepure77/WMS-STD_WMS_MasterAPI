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
    public class UserGroupZoneService
    {
        #region Before Code
        //public List<UserGroupZoneViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_UserGroupZone.FromSql("sp_GetUserGroupZone").ToList();

        //            var result = new List<UserGroupZoneViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new UserGroupZoneViewModel();
        //                resultItem.UserGroupZoneIndex = item.UserGroupZone_Index;
        //                resultItem.UserGroupZoneId = item.UserGroupZone_Id;
        //                if (item.UserGroup_Index != null)
        //                {
        //                    var itemList = context.MS_UserGroup.FromSql("sp_GetUserGroup").Where(c => item.UserGroup_Index == c.UserGroup_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.UserGroupIndex = itemList.UserGroup_Index;
        //                        resultItem.UserGroupName = itemList.UserGroup_Name;
        //                    }

        //                }
        //                if (item.Zone_Index != null)
        //                {
        //                    var itemList = context.MS_Zone.FromSql("sp_GetZone").Where(c => item.Zone_Index == c.Zone_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.ZoneIndex = item.Zone_Index;
        //                        resultItem.ZoneName = itemList.Zone_Name;
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

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public String SaveChanges(UserGroupZoneViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.UserGroupZoneIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.UserGroupZoneIndex = Guid.NewGuid();
        //            }
        //            if (data.UserGroupZoneId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "UserGroupZoneId");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.UserGroupZoneId = resultParameter.Value.ToString();
        //            }
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int isSystem = 0;
        //            int statusId = 0;
        //            var UserGroupZone_Index = new SqlParameter("UserGroupZone_Index", data.UserGroupZoneIndex);
        //            var UserGroupZone_Id = new SqlParameter("UserGroupZone_Id", data.UserGroupZoneId);
        //            var UserGroup_Index = new SqlParameter("UserGroup_Index", data.UserGroupIndex);
        //            var Zone_Index = new SqlParameter("Zone_Index", data.ZoneIndex);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_UserGroupZone  @UserGroupZone_Index,@UserGroupZone_Id,@UserGroup_Index,@Zone_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", UserGroupZone_Index, UserGroupZone_Id, UserGroup_Index, Zone_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<UserGroupZoneViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_UserGroupZone.FromSql("sp_GetUserGroupZone").Where(c => c.UserGroupZone_Index == id).ToList();

        //            var result = new List<UserGroupZoneViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new UserGroupZoneViewModel();
        //                resultItem.UserGroupZoneIndex = item.UserGroupZone_Index;
        //                resultItem.UserGroupZoneId = item.UserGroupZone_Id;
        //                if (item.UserGroup_Index != null)
        //                {
        //                    var itemList = context.MS_UserGroup.FromSql("sp_GetUserGroup").Where(c => item.UserGroup_Index == c.UserGroup_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.UserGroupIndex = itemList.UserGroup_Index;
        //                        resultItem.UserGroupName = itemList.UserGroup_Name;
        //                    }

        //                }
        //                if (item.Zone_Index != null)
        //                {
        //                    var itemList = context.MS_Zone.FromSql("sp_GetZone").Where(c => item.Zone_Index == c.Zone_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.ZoneIndex = item.Zone_Index;
        //                        resultItem.ZoneName = itemList.Zone_Name;
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

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<UserGroupZoneViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_UserGroupZone.FromSql("sp_GetUserGroupZone").Where(c => c.UserGroupZone_Index == id).ToList();
        //            var result = new List<UserGroupZoneViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                int isactive = 0;
        //                int isdelete = 1;
        //                var UserGroupZone_Index = new SqlParameter("UserGroupZone_Index", item.UserGroupZone_Index);
        //                var UserGroupZone_Id = new SqlParameter("UserGroupZone_Id", item.UserGroupZone_Id);
        //                var UserGroup_Index = new SqlParameter("UserGroup_Index", item.UserGroup_Index);
        //                var Zone_Index = new SqlParameter("Zone_Index", item.Zone_Index);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_UserGroupZone  @UserGroupZone_Index,@UserGroupZone_Id,@UserGroup_Index,@Zone_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", UserGroupZone_Index, UserGroupZone_Id, UserGroup_Index, Zone_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        //public List<UserGroupZoneViewModel> search(UserGroupZoneViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<UserGroupZoneViewModel>();
        //            var queryResult = context.MS_UserGroupZone.FromSql("sp_GetUserGroupZone").Where(c => c.IsActive == 1 && c.IsDelete == 0)
        //                                            .ToList();
        //            if (data.UserGroupZoneId != "" && data.UserGroupZoneId != null)
        //            {
        //                pwhereFilter = " And UserGroupZone_Id like N'%" + data.UserGroupZoneId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }

        //            if (data.UserGroupZoneId != "" && data.UserGroupZoneId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_UserGroupZone.FromSql("sp_GetUserGroupZone @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new UserGroupZoneViewModel();
        //                    resultItem.UserGroupZoneIndex = item.UserGroupZone_Index;
        //                    resultItem.UserGroupZoneId = item.UserGroupZone_Id;
        //                    if (item.UserGroup_Index != null)
        //                    {
        //                        var itemList = context.MS_UserGroup.FromSql("sp_GetUserGroup").Where(c => item.UserGroup_Index == c.UserGroup_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            resultItem.UserGroupIndex = itemList.UserGroup_Index;
        //                            resultItem.UserGroupName = itemList.UserGroup_Name;
        //                        }

        //                    }
        //                    if (item.Zone_Index != null)
        //                    {
        //                        var itemList = context.MS_Zone.FromSql("sp_GetZone").Where(c => item.Zone_Index == c.Zone_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            resultItem.ZoneIndex = item.Zone_Index;
        //                            resultItem.ZoneName = itemList.Zone_Name;
        //                        }

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
        //            else if (data.UserGroupName != "" && data.UserGroupName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And UserGroup_Name like N'%" + data.UserGroupName + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_UserGroup.FromSql("sp_GetUserGroup @strwhere ", pstrwhere1).ToList();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new UserGroupZoneViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.UserGroup_Index == ItemList.UserGroup_Index)
        //                        {
        //                            resultItem.UserGroupZoneIndex = item.UserGroupZone_Index;
        //                            resultItem.UserGroupZoneId = item.UserGroupZone_Id;
        //                            if (item.UserGroup_Index != null)
        //                            {
        //                                resultItem.UserGroupIndex = ItemList.UserGroup_Index;
        //                                resultItem.UserGroupName = ItemList.UserGroup_Name;
        //                            }
        //                            if (item.Zone_Index != null)
        //                            {
        //                                var itemList = context.MS_Zone.FromSql("sp_GetZone").Where(c => item.Zone_Index == c.Zone_Index).FirstOrDefault();
        //                                if (itemList != null)
        //                                {
        //                                    resultItem.ZoneIndex = item.Zone_Index;
        //                                    resultItem.ZoneName = itemList.Zone_Name;
        //                                }
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
        //            else if (data.ZoneName != "" && data.ZoneName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And Zone_Name like N'%" + data.ZoneName + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_Zone.FromSql("sp_GetZone @strwhere ", pstrwhere1).ToList();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new UserGroupZoneViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.Zone_Index == ItemList.Zone_Index)
        //                        {
        //                            resultItem.UserGroupZoneIndex = item.UserGroupZone_Index;
        //                            resultItem.UserGroupZoneId = item.UserGroupZone_Id;
        //                            if (item.UserGroup_Index != null)
        //                            {
        //                                var itemList = context.MS_UserGroup.FromSql("sp_GetUserGroup").Where(c => item.UserGroup_Index == c.UserGroup_Index).FirstOrDefault();
        //                                if (itemList != null)
        //                                {
        //                                    resultItem.UserGroupIndex = itemList.UserGroup_Index;
        //                                    resultItem.UserGroupName = itemList.UserGroup_Name;
        //                                }
        //                            }

        //                            resultItem.ZoneIndex = ItemList.Zone_Index;
        //                            resultItem.ZoneName = ItemList.Zone_Name;
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
        //            if (data.ZoneName == "" && data.UserGroupName == "")
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
        #endregion

            private MasterDataDbContext db;

            public UserGroupZoneService()
            {
                db = new MasterDataDbContext();
            }

            public UserGroupZoneService(MasterDataDbContext db)
            {
                this.db = db;
            }

            #region filterUserGroupZone
            public actionResultUserGroupZoneViewModel filter(SearchUserGroupZoneViewModel data)
            {
                try
                {
                    var query = db.View_UserGroupZone.AsQueryable();
                    query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.UserGroupZone_Id.Contains(data.key)
                          || c.UserGroup_Name.Contains(data.key)
                          || c.Zone_Name.Contains(data.key));
                    }

                    var Item = new List<View_UserGroupZone>();
                    var TotalRow = new List<View_UserGroupZone>();

                    TotalRow = query.ToList();


                    if (data.CurrentPage != 0 && data.PerPage != 0)
                    {
                        query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                    }

                    if (data.PerPage != 0)
                    {
                        query = query.Take(data.PerPage);

                    }

                    Item = query.ToList();

                    var result = new List<SearchUserGroupZoneViewModel>();

                    foreach (var item in Item)
                    {
                        var resultItem = new SearchUserGroupZoneViewModel();

                        resultItem.userGroupZone_Index = item.UserGroupZone_Index;
                        resultItem.userGroupZone_Id = item.UserGroupZone_Id;
                        resultItem.userGroup_Index = item.UserGroup_Index;
                        resultItem.userGroup_Name = item.UserGroup_Name;
                        resultItem.userGroup_Id = item.UserGroup_Id;
                        resultItem.zone_Index = item.Zone_Index;
                        resultItem.zone_Id = item.Zone_Id;
                        resultItem.zone_Name = item.Zone_Name;
                        resultItem.isActive = item.IsActive;
                        result.Add(resultItem);
                    }

                    var count = TotalRow.Count;

                    var actionResultUserGroupZoneViewModel = new actionResultUserGroupZoneViewModel();
                    actionResultUserGroupZoneViewModel.itemsUserGroupZone = result.ToList();
                    actionResultUserGroupZoneViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                    return actionResultUserGroupZoneViewModel;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            #endregion



            #region SaveChanges
            public String SaveChanges(UserGroupZoneViewModel data)
            {
                String State = "Start";
                String msglog = "";
                var olog = new logtxt();

                try
                {

                    var UserGroupZoneOld = db.MS_UserGroupZone.Find(data.userGroupZone_Index);

                    if (UserGroupZoneOld == null)
                    {

                        data.userGroupZone_Id = "UserGroupZone_Id".genAutonumber();

                        MS_UserGroupZone Model = new MS_UserGroupZone();


                        Model.UserGroupZone_Index = Guid.NewGuid();
                        Model.UserGroupZone_Id = data.userGroupZone_Id;
                        Model.UserGroup_Index = data.userGroup_Index;
                        Model.Zone_Index = data.zone_Index;
                        Model.IsActive = Convert.ToInt32(data.isActive);
                        Model.IsDelete = 0;
                        Model.IsSystem = 0;
                        Model.Status_Id = 0;
                        Model.Create_By = data.create_By;
                        Model.Create_Date = DateTime.Now;

                        db.MS_UserGroupZone.Add(Model);
                    }
                    else
                    {
                        UserGroupZoneOld.UserGroup_Index = data.userGroup_Index;
                        UserGroupZoneOld.Zone_Index = data.zone_Index;
                        UserGroupZoneOld.IsActive = Convert.ToInt32(data.isActive);
                        UserGroupZoneOld.Update_By = data.create_By;
                        UserGroupZoneOld.Update_Date = DateTime.Now;
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
                        olog.logging("SaveUserGroupZone", msglog);
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
            public UserGroupZoneViewModel find(Guid id)
            {
                try
                {

                    var queryResult = db.View_UserGroupZone.Where(c => c.UserGroupZone_Index == id).FirstOrDefault();

                    var result = new UserGroupZoneViewModel();

                    result.userGroupZone_Index = queryResult.UserGroupZone_Index;
                    result.userGroupZone_Id = queryResult.UserGroupZone_Id;
                    result.userGroup_Index = queryResult.UserGroup_Index;
                    result.userGroup_Name = queryResult.UserGroup_Name;
                    result.zone_Index = queryResult.Zone_Index;
                    result.zone_Name = queryResult.Zone_Name;

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
            public Boolean getDelete(UserGroupZoneViewModel data)
            {
                String State = "Start";
                String msglog = "";
                var olog = new logtxt();

                try
                {
                    var UserGroupZone = db.MS_UserGroupZone.Find(data.userGroupZone_Index);

                    if (UserGroupZone != null)
                    {
                        UserGroupZone.IsActive = 0;
                        UserGroupZone.IsDelete = 1;


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
                            olog.logging("DeleteUserGroupZone", msglog);
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
    }
}

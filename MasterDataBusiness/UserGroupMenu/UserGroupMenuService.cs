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
    #region BeforeCode
    //public class UserGroupMenuService
    //{
    //    public List<UserGroupMenuViewModel> Filter()
    //    {
    //        try
    //        {
    //            using (var context = new MasterDataDbContext())
    //            {
    //                var queryResult = context.MS_UserGroupMenu.FromSql("sp_GetUserGroupMenu").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

    //                var result = new List<UserGroupMenuViewModel>();
    //                foreach (var item in queryResult)
    //                {
    //                    var resultItem = new UserGroupMenuViewModel();
    //                    resultItem.UserGroupMenuIndex = item.UserGroupMenu_Index;
    //                    resultItem.UserGroupMenuId = item.UserGroupMenu_Id;
    //                    if (item.UserGroup_Index != null)
    //                    {
    //                        var itemList = context.MS_UserGroup.FromSql("sp_GetUserGroup").Where(c => item.UserGroup_Index == c.UserGroup_Index).FirstOrDefault();
    //                        if (itemList != null)
    //                        {
    //                            resultItem.UserGroupIndex = itemList.UserGroup_Index;
    //                            resultItem.UserGroupName = itemList.UserGroup_Name;
    //                        }

    //                    }
    //                    if (item.Menu_Index != null)
    //                    {
    //                        var itemList = context.sy_Menu.FromSql("sy_Menu").Where(c => item.Menu_Index == c.Menu_Index).FirstOrDefault();
    //                        if (itemList != null)
    //                        {
    //                            resultItem.MenuIndex = item.Menu_Index;
    //                            resultItem.MenuName = itemList.Menu_Name;
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

    //                return result;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    public String SaveChanges(UserGroupMenuViewModel data)
    //    {
    //        try
    //        {
    //            using (var context = new MasterDataDbContext())
    //            {
    //                if (data.UserGroupMenuIndex.ToString() == "00000000-0000-0000-0000-000000000000")
    //                {
    //                    data.UserGroupMenuIndex = Guid.NewGuid();
    //                }
    //                int isactive = 1;
    //                int isdelete = 0;
    //                int isSystem = 0;
    //                int statusId = 0;
    //                var UserGroupMenu_Index = new SqlParameter("UserGroupMenu_Index", data.UserGroupMenuIndex);
    //                var UserGroupMenu_Id = new SqlParameter("UserGroupMenu_Id", data.UserGroupMenuId);
    //                var UserGroup_Index = new SqlParameter("UserGroup_Index", data.UserGroupIndex);
    //                var Menu_Index = new SqlParameter("Menu_Index", data.MenuIndex);
    //                var IsActive = new SqlParameter("IsActive", isactive);
    //                var IsDelete = new SqlParameter("IsDelete", isdelete);
    //                var IsSystem = new SqlParameter("IsSystem", isSystem);
    //                var Status_Id = new SqlParameter("Status_Id", statusId);
    //                var Create_By = new SqlParameter("Create_By", "");
    //                var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
    //                var Update_By = new SqlParameter("Update_By", "");
    //                var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
    //                var Cancel_By = new SqlParameter("Cancel_By", "");
    //                var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
    //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_UserGroupMenu  @UserGroupMenu_Index,@UserGroupMenu_Id,@UserGroup_Index,@Menu_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", UserGroupMenu_Index, UserGroupMenu_Id, UserGroup_Index, Menu_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
    //                return rowsAffected.ToString();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    public List<UserGroupMenuViewModel> getId(Guid id)
    //    {
    //        try
    //        {
    //            using (var context = new MasterDataDbContext())
    //            {
    //                var queryResult = context.MS_UserGroupMenu.FromSql("sp_GetUserGroupMenu").Where(c => c.UserGroupMenu_Index == id).ToList();

    //                var result = new List<UserGroupMenuViewModel>();
    //                foreach (var item in queryResult)
    //                {
    //                    var resultItem = new UserGroupMenuViewModel();
    //                    resultItem.UserGroupMenuIndex = item.UserGroupMenu_Index;
    //                    resultItem.UserGroupMenuId = item.UserGroupMenu_Id;
    //                    if (item.UserGroup_Index != null)
    //                    {
    //                        var itemList = context.MS_UserGroup.FromSql("sp_GetUserGroup").Where(c => item.UserGroup_Index == c.UserGroup_Index).FirstOrDefault();
    //                        if (itemList != null)
    //                        {
    //                            resultItem.UserGroupIndex = item.UserGroup_Index;
    //                            resultItem.UserGroupName = itemList.UserGroup_Name;
    //                        }

    //                    }
    //                    if (item.Menu_Index != null)
    //                    {
    //                        var itemList = context.sy_Menu.FromSql("sy_Menu").Where(c => item.Menu_Index == c.Menu_Index).FirstOrDefault();
    //                        if (itemList != null)
    //                        {
    //                            resultItem.MenuIndex = itemList.Menu_Index;
    //                            resultItem.MenuName = itemList.Menu_Name;
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
    //                }

    //                return result;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    public List<UserGroupMenuViewModel> getDelete(Guid id)
    //    {
    //        try
    //        {
    //            using (var context = new MasterDataDbContext())
    //            {
    //                var queryResult = context.MS_UserGroupMenu.FromSql("sp_GetUserGroupMenu").Where(c => c.UserGroupMenu_Index == id).ToList();
    //                var result = new List<UserGroupMenuViewModel>();
    //                foreach (var item in queryResult)
    //                {
    //                    int isactive = 0;
    //                    int isdelete = 1;
    //                    var UserGroupMenu_Index = new SqlParameter("UserGroupMenu_Index", item.UserGroupMenu_Index);
    //                    var UserGroupMenu_Id = new SqlParameter("UserGroupMenu_Id", item.UserGroupMenu_Id);
    //                    var UserGroup_Index = new SqlParameter("UserGroup_Index", item.UserGroup_Index);
    //                    var Menu_Index = new SqlParameter("Menu_Index", item.Menu_Index);
    //                    var IsActive = new SqlParameter("IsActive", isactive);
    //                    var IsDelete = new SqlParameter("IsDelete", isdelete);
    //                    var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
    //                    var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
    //                    var Create_By = new SqlParameter("Create_By", "");
    //                    var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
    //                    var Update_By = new SqlParameter("Update_By", "");
    //                    var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
    //                    var Cancel_By = new SqlParameter("Cancel_By", "");
    //                    var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
    //                    var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_UserGroupMenu  @UserGroupMenu_Index,@UserGroupMenu_Id,@UserGroup_Index,@Menu_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", UserGroupMenu_Index, UserGroupMenu_Id, UserGroup_Index, Menu_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
    //                    context.SaveChanges();
    //                }

    //                return result;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public List<UserGroupMenuViewModel> search(UserGroupMenuViewModel data)
    //    {
    //        try
    //        {

    //            using (var context = new MasterDataDbContext())
    //            {

    //                string pwhereFilter = "";
    //                string pwhereLike = "";
    //                var result = new List<UserGroupMenuViewModel>();
    //                var queryResult = context.MS_UserGroupMenu.FromSql("sp_GetUserGroupMenu").Where(c => c.IsActive == 1 && c.IsDelete == 0)
    //                                                .ToList();
    //                if (data.UserGroupMenuId != "" && data.UserGroupMenuId != null)
    //                {
    //                    pwhereFilter = " And UserGroupMenu_Id like N'%" + data.UserGroupMenuId + "%'";
    //                }
    //                else
    //                {
    //                    pwhereFilter += "";
    //                }

    //                if (data.UserGroupMenuId != "" && data.UserGroupMenuId != null)
    //                {
    //                    pwhereFilter += " And isActive = '" + 1 + "'";
    //                    pwhereFilter += " And isDelete = '" + 0 + "'";
    //                    var strwhere = new SqlParameter("@strwhere", pwhereFilter);
    //                    var query = context.MS_UserGroupMenu.FromSql("sp_GetUserGroupMenu @strwhere ", strwhere).ToList();
    //                    foreach (var item in query)
    //                    {
    //                        var resultItem = new UserGroupMenuViewModel();
    //                        resultItem.UserGroupMenuIndex = item.UserGroupMenu_Index;
    //                        resultItem.UserGroupMenuId = item.UserGroupMenu_Id;
    //                        if (item.UserGroup_Index != null)
    //                        {
    //                            var itemList = context.MS_UserGroup.FromSql("sp_GetUserGroup").Where(c => item.UserGroup_Index == c.UserGroup_Index).FirstOrDefault();
    //                            if (itemList != null)
    //                            {
    //                                resultItem.UserGroupIndex = item.UserGroup_Index;
    //                                resultItem.UserGroupName = itemList.UserGroup_Name;
    //                            }
    //                        }
    //                        if (item.Menu_Index != null)
    //                        {
    //                            var itemList = context.sy_Menu.FromSql("sy_Menu").Where(c => item.Menu_Index == c.Menu_Index).FirstOrDefault();
    //                            if (itemList != null)
    //                            {
    //                                resultItem.MenuIndex = item.Menu_Index;
    //                                resultItem.MenuName = itemList.Menu_Name;
    //                            }
    //                        }
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
    //                else if (data.UserGroupName != "" && data.UserGroupName != null)
    //                {
    //                    pwhereLike += " And isActive = '" + 1 + "'";
    //                    pwhereLike += " And isDelete = '" + 0 + "'";
    //                    pwhereLike = " And UserGroup_Name like N'%" + data.UserGroupName + "%'";
    //                    var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
    //                    var dataList = context.MS_UserGroup.FromSql("sp_GetUserGroup @strwhere ", pstrwhere1).ToList();
    //                    foreach (var item in queryResult)
    //                    {
    //                        var resultItem = new UserGroupMenuViewModel();
    //                        foreach (var ItemList in dataList)
    //                        {
    //                            if (item.UserGroup_Index == ItemList.UserGroup_Index)
    //                            {
    //                                resultItem.UserGroupMenuIndex = item.UserGroupMenu_Index;
    //                                resultItem.UserGroupMenuId = item.UserGroupMenu_Id;
    //                                resultItem.UserGroupIndex = ItemList.UserGroup_Index;
    //                                resultItem.UserGroupName = ItemList.UserGroup_Name;

    //                                if (item.Menu_Index != null)
    //                                {
    //                                    var itemList = context.sy_Menu.FromSql("sy_Menu").Where(c => item.Menu_Index == c.Menu_Index).FirstOrDefault();
    //                                    if (itemList != null)
    //                                    {
    //                                        resultItem.MenuIndex = item.Menu_Index;
    //                                        resultItem.MenuName = itemList.Menu_Name;
    //                                    }

    //                                }
    //                                resultItem.IsActive = item.IsActive;
    //                                resultItem.IsDelete = item.IsDelete;
    //                                resultItem.IsSystem = item.IsSystem;
    //                                resultItem.StatusId = item.Status_Id;
    //                                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
    //                                resultItem.CreateBy = item.Create_By;
    //                                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
    //                                resultItem.UpdateBy = item.Update_By;
    //                                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
    //                                resultItem.CancelBy = item.Cancel_By;
    //                                result.Add(resultItem);
    //                            }

    //                        }

    //                    }
    //                }
    //                else if (data.MenuName != "" && data.MenuName != null)
    //                {
    //                    pwhereLike += " And isActive = '" + 1 + "'";
    //                    pwhereLike += " And isDelete = '" + 0 + "'";
    //                    pwhereLike = " And Menu_Name like N'%" + data.MenuName + "%'";
    //                    var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
    //                    var dataList = context.sy_Menu.FromSql("sp_GetMenu @strwhere ", pstrwhere1).ToList();
    //                    foreach (var item in queryResult)
    //                    {
    //                        var resultItem = new UserGroupMenuViewModel();
    //                        foreach (var ItemList in dataList)
    //                        {
    //                            if (item.Menu_Index == ItemList.Menu_Index)
    //                            {
    //                                resultItem.UserGroupMenuIndex = item.UserGroupMenu_Index;
    //                                resultItem.UserGroupMenuId = item.UserGroupMenu_Id;
    //                                if (item.UserGroup_Index != null)
    //                                {
    //                                    var itemList = context.MS_UserGroup.FromSql("sp_GetUserGroup").Where(c => item.UserGroup_Index == c.UserGroup_Index).FirstOrDefault();
    //                                    if (itemList != null)
    //                                    {
    //                                        resultItem.UserGroupIndex = item.UserGroup_Index;
    //                                        resultItem.UserGroupName = itemList.UserGroup_Name;
    //                                    }

    //                                }

    //                                resultItem.MenuIndex = ItemList.Menu_Index;
    //                                resultItem.MenuName = ItemList.Menu_Name;
    //                                resultItem.IsActive = item.IsActive;
    //                                resultItem.IsDelete = item.IsDelete;
    //                                resultItem.IsSystem = item.IsSystem;
    //                                resultItem.StatusId = item.Status_Id;
    //                                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
    //                                resultItem.CreateBy = item.Create_By;
    //                                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
    //                                resultItem.UpdateBy = item.Update_By;
    //                                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
    //                                resultItem.CancelBy = item.Cancel_By;
    //                                result.Add(resultItem);
    //                            }

    //                        }

    //                    }
    //                }
    //                if (data.UserGroupMenuId == "" && data.MenuName == "" && data.UserGroupName == "")
    //                {
    //                    result = this.Filter();
    //                }

    //                return result;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //}
    #endregion
     public class UserGroupMenuService
    {
        private MasterDataDbContext db;

        public UserGroupMenuService()
        {
            db = new MasterDataDbContext();
        }

        public UserGroupMenuService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterUserGroupMenu
        public actionResultUserGroupMenuViewModel filter(SearchUserGroupMenuViewModel data)
        {
            try
            {
                var query = db.View_UserGroupMenu.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.UserGroupMenu_Id.Contains(data.key)
                      || c.UserGroup_Name.Contains(data.key)
                      || c.Menu_Name.Contains(data.key));
                }
                if (!string.IsNullOrEmpty(data.createdate_date) && !string.IsNullOrEmpty(data.createdate_date_to))
                {
                    var dateStart = data.createdate_date.toBetweenDate();
                    var dateEnd = data.createdate_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }

                var Item = new List<View_UserGroupMenu>();
                var TotalRow = new List<View_UserGroupMenu>();

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

                var result = new List<SearchUserGroupMenuViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchUserGroupMenuViewModel();

                    resultItem.userGroupMenu_Index = item.UserGroupMenu_Index;
                    resultItem.userGroupMenu_Id = item.UserGroupMenu_Id;
                    resultItem.userGroup_Index = item.UserGroup_Index;
                    resultItem.userGroup_Name = item.UserGroup_Name;
                    resultItem.userGroup_Id = item.UserGroup_Id;
                    resultItem.menu_Index = item.Menu_Index;
                    resultItem.menu_Name = item.Menu_Name;
                    resultItem.menu_Id = item.Menu_Id;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultUserGroupMenuViewModel = new actionResultUserGroupMenuViewModel();
                actionResultUserGroupMenuViewModel.itemsUserGroupMenu = result.ToList();
                actionResultUserGroupMenuViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultUserGroupMenuViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

       

        #region SaveChanges
        public String SaveChanges(UserGroupMenuViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var UserGroupMenuOld = db.MS_UserGroupMenu.Find(data.userGroupMenu_Index);

                if (UserGroupMenuOld == null)
                {

                    data.userGroupMenu_Id = "UserGroupMenu_Id".genAutonumber();

                    MS_UserGroupMenu Model = new MS_UserGroupMenu();


                    Model.UserGroupMenu_Index = Guid.NewGuid();
                    Model.UserGroupMenu_Id = data.userGroupMenu_Id;
                    Model.UserGroup_Index = data.userGroup_Index;
                    Model.Menu_Index = data.menu_Index;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_UserGroupMenu.Add(Model);
                }
                else
                {
                    UserGroupMenuOld.UserGroup_Index = data.userGroup_Index;
                    UserGroupMenuOld.Menu_Index = data.menu_Index;
                    UserGroupMenuOld.IsActive = Convert.ToInt32(data.isActive);
                    UserGroupMenuOld.Update_By = data.create_By;
                    UserGroupMenuOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveUserGroupMenu", msglog);
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
        public UserGroupMenuViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_UserGroupMenu.Where(c => c.UserGroupMenu_Index == id).FirstOrDefault();

                var result = new UserGroupMenuViewModel();

                result.userGroupMenu_Index = queryResult.UserGroupMenu_Index;
                result.userGroupMenu_Id = queryResult.UserGroupMenu_Id;
                result.userGroup_Index = queryResult.UserGroup_Index;
                result.userGroup_Name = queryResult.UserGroup_Name;
                result.menu_Index = queryResult.Menu_Index;
                result.menu_Name = queryResult.Menu_Name;
              
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
        public Boolean getDelete(UserGroupMenuViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var UserGroupMenu = db.MS_UserGroupMenu.Find(data.userGroupMenu_Index);

                if (UserGroupMenu != null)
                {
                    UserGroupMenu.IsActive = 0;
                    UserGroupMenu.IsDelete = 1;


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
                        olog.logging("DeleteUserGroupMenu", msglog);
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

        #region Export Excel
        public UserGroupMenuActionResultExportViewModel Export(UserGroupMenuExportViewModel data)
        {
            try
            {
                var query = db.View_UserGroupMenu.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.UserGroupMenu_Id.Contains(data.key)
                      || c.UserGroup_Name.Contains(data.key)
                      || c.Menu_Name.Contains(data.key));
                }

                if (!string.IsNullOrEmpty(data.createdate_date) && !string.IsNullOrEmpty(data.createdate_date_to))
                {
                    var dateStart = data.createdate_date.toBetweenDate();
                    var dateEnd = data.createdate_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }

                var Item = new List<View_UserGroupMenu>();
                var TotalRow = new List<View_UserGroupMenu>();

                TotalRow = query.ToList();
                Item = query.ToList();
                var result = new List<UserGroupMenuExportViewModel>();

                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new UserGroupMenuExportViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.userGroupMenu_Index = item.UserGroupMenu_Index;
                    resultItem.userGroupMenu_Id = item.UserGroupMenu_Id;
                    resultItem.userGroup_Index = item.UserGroup_Index;
                    resultItem.userGroup_Name = item.UserGroup_Name;
                    resultItem.userGroup_Id = item.UserGroup_Id;
                    resultItem.menu_Index = item.Menu_Index;
                    resultItem.menu_Name = item.Menu_Name;
                    resultItem.menu_Id = item.Menu_Id;
                    resultItem.isActive = item.IsActive;
                    resultItem.create_By = item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var UserGroupMenuActionResultExportViewModel = new UserGroupMenuActionResultExportViewModel();
                UserGroupMenuActionResultExportViewModel.itemsUserGroupMenu = result.ToList();

                return UserGroupMenuActionResultExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

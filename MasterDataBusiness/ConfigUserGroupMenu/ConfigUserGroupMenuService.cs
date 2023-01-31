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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MasterDataBusiness
{
    public class ConfigUserGroupMenuService
    {
        private MasterDataDbContext db;

        public ConfigUserGroupMenuService()
        {
            db = new MasterDataDbContext();
        }

        public ConfigUserGroupMenuService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterWave
        public actionResultConfigUserGroupMenuViewModel filter(SearchConfigUserGroupMenuViewModel data)
        {
            try
            {

                if (string.IsNullOrEmpty(data.userGroup_Index.ToString()))
                {
                    return new actionResultConfigUserGroupMenuViewModel();
                }
                var query = db.sy_Menu.Select(M => new ConfigUserGroupMenuViewModel
                {
                    userGroupMenu_Index = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    userGroupMenu_Id = null,
                    userGroup_Index = data.userGroup_Index,
                    isActive = false,
                    menu_Index = M.Menu_Index,
                    menuType_Index = M.MenuType_Index,
                    menu_Id = M.Menu_Id,
                    menuControl_Name = M.MenuControl_Name,
                    menu_Name = M.Menu_Name,
                    menu_SecondName = M.Menu_SecondName,
                    menu_ThirdName = M.Menu_ThirdName
                }).OrderBy(o => o.menu_Id.sParse<int>()).ToList();

                foreach (var q in query)
                {
                    var usergroupmenu = db.MS_UserGroupMenu.Where(c => c.UserGroup_Index == data.userGroup_Index && c.Menu_Index == q.menu_Index).ToList();
                    foreach (var u in usergroupmenu)
                    {
                        q.userGroupMenu_Index = u.UserGroupMenu_Index;
                        q.userGroupMenu_Id = u.UserGroupMenu_Id;
                        q.userGroup_Index = u.UserGroup_Index;
                        q.isActive = u.IsActive == 1 ? true : false;
                    }
                }

                var count = query.Count;

                var actionResultWaveViewModel = new actionResultConfigUserGroupMenuViewModel();
                actionResultWaveViewModel.items = query.OrderBy(o => o.menu_Id.sParse<int>()).ToList();
                actionResultWaveViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultWaveViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getUserGroupMenu
        public string confirm(actionResultConfigUserGroupMenuViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            int number = 1;
            try
            {

                var config = db.sy_Config.FirstOrDefault(c => c.Config_Key == "Config_User_Group_Menu");
                var usergroup = db.MS_User.FirstOrDefault(f => f.User_Name == data.username).UserGroup_Index.ToString();
                if (config.Config_Value.ToUpper() != usergroup.ToUpper())
                {
                    return "ผู้ใช้นี้ไม่มีสิทธิ์แก้ไขข้อมูล";
                }


                if (data.items.Count == 0)
                {
                    return "ไม่พบข้อมูล";
                }

                var lastId = db.MS_UserGroupMenu.OrderBy(o => o.UserGroupMenu_Id.sParse<int>()).Select(s => s.UserGroupMenu_Id).Last().sParse<int>();

                var joinMenu = (from menu in db.sy_Menu
                                join IsactiveMenu in data.items on menu.Menu_Index equals (IsactiveMenu.menu_Index ?? Guid.Empty) into im
                                from m in im.DefaultIfEmpty()
                                select new
                                {
                                    m = menu,
                                    im = m
                                }).OrderBy(o => o.m.Menu_Id.sParse<int>()).ToList();

                //foreach (var item in data.items)
                foreach (var item in joinMenu)
                {
                    //var query = db.MS_UserGroupMenu.FirstOrDefault(c => c.UserGroupMenu_Index == item.userGroupMenu_Index).IsActive = item.isActive ? 1 : 0;

                    //var query = db.MS_UserGroupMenu.FirstOrDefault(c => c.UserGroupMenu_Index == (item.im != null ? item.im.userGroupMenu_Index : Guid.Empty) && c.Menu_Index == item.m.Menu_Index);
                    var query = db.MS_UserGroupMenu.FirstOrDefault(c => c.UserGroup_Index == (item.im != null ? item.im.userGroup_Index : data.items.FirstOrDefault().userGroup_Index) && c.Menu_Index == item.m.Menu_Index);
                    //var query = db.MS_UserGroupMenu.FirstOrDefault(c => c.UserGroupMenu_Index == item.userGroupMenu_Index && c.Menu_Index == item.menu_Index);

                    if (query == null)
                    {
                        MS_UserGroupMenu m = new MS_UserGroupMenu();


                        m.UserGroupMenu_Index = Guid.NewGuid();
                        m.UserGroupMenu_Id = (lastId + number).ToString();
                        m.UserGroup_Index = (item.im != null ? item.im.userGroup_Index : data.items.FirstOrDefault().userGroup_Index);
                        m.Menu_Index = item.m.Menu_Index;
                        m.IsActive = (item.im != null ? item.im.isActive : false) ? 1 : 0;
                        //m.UserGroup_Index = item.userGroup_Index;
                        //m.Menu_Index = item.menu_Index;
                        //m.IsActive = item.isActive ? 1 : 0;
                        m.IsDelete = 0;
                        m.IsSystem = 0;
                        m.Status_Id = 0;
                        m.Create_By = data.username;
                        m.Create_Date = DateTime.Now;

                        number++;
                        db.MS_UserGroupMenu.Add(m);
                    }
                    else
                    {
                        query.IsActive = (item.im != null ? item.im.isActive : false) ? 1 : 0;
                        //query.IsActive = item.isActive ? 1 : 0;
                        query.Update_By = data.username;
                        query.Update_Date = DateTime.Now;
                    }
                }

                var transactionx = db.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SaveWave", msglog);
                    transactionx.Rollback();

                    throw exy;
                }


                return "บันทึกข้อมูลสำเร็จ";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region confirm
        public List<ConfigUserGroupViewModel> getUserGroupMenu(ConfigUserGroupMenuViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var query = db.MS_UserGroup.Where(c => c.IsActive == 1 && c.IsDelete == 0).Select(s => new ConfigUserGroupViewModel
                {
                    userGroup_Index = s.UserGroup_Index,
                    userGroup_Id = s.UserGroup_Id,
                    userGroup_Name = s.UserGroup_Name,
                    isActive = s.IsActive,
                    isDelete = s.IsDelete
                }).OrderBy(o => o.userGroup_Name).ToList();

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region filterConfigUserGroupMenu
        public actionResultConfigUserGroupMenuViewModel filterConfigUserGroupMenu(SearchConfigUserGroupMenuViewModel data)
        {
            try
            {

                if (string.IsNullOrEmpty(data.userGroup_Index.ToString()))
                {
                    return new actionResultConfigUserGroupMenuViewModel();
                }
                var query = db.sy_Menu.Where(c => c.IsActive == 1 && c.IsDelete == 0).Select(M => new ConfigUserGroupMenuViewModel
                {
                    userGroupMenu_Index = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    userGroupMenu_Id = null,
                    userGroup_Index = data.userGroup_Index,
                    isActive = false,
                    menu_Index = M.Menu_Index,
                    sub_Menu_Index = M.Sub_Menu_Index,
                    menuType_Index = M.MenuType_Index,
                    menu_Id = M.Menu_Id,
                    menuControl_Name = M.MenuControl_Name,
                    menu_Name = M.Menu_Name,
                    menu_SecondName = M.Menu_SecondName,
                    menu_ThirdName = M.Menu_ThirdName
                }).OrderBy(o => o.menu_Id.sParse<int>()).ToList();

                foreach (var q in query)
                {
                    var usergroupmenu = db.MS_UserGroupMenu.Where(c => c.UserGroup_Index == data.userGroup_Index && c.Menu_Index == q.menu_Index).ToList();
                    foreach (var u in usergroupmenu)
                    {
                        q.userGroupMenu_Index = u.UserGroupMenu_Index;
                        q.userGroupMenu_Id = u.UserGroupMenu_Id;
                        q.userGroup_Index = u.UserGroup_Index;
                        q.isActive = u.IsActive == 1 ? true : false;
                    }
                }

                var count = query.Count;

                var actionResultWaveViewModel = new actionResultConfigUserGroupMenuViewModel();
                actionResultWaveViewModel.items = query.OrderBy(o => o.menu_Id.sParse<int>()).ToList();
                actionResultWaveViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultWaveViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Export Excel
        public ConfigUserGroupMenuActionResultExportViewModel Export(ConfigUserGroupMenuExportViewModel data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.userGroup_Index.ToString()))
                {
                    return new ConfigUserGroupMenuActionResultExportViewModel();
                }
                
                var query = db.sy_Menu.Where(c => c.IsActive == 1 && c.IsDelete == 0).Select(M => new ConfigUserGroupMenuExportViewModel
                {
                    userGroupMenu_Index = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    userGroupMenu_Id = null,
                    userGroup_Index = data.userGroup_Index,
                    isActive = false,
                    menu_Index = M.Menu_Index,
                    sub_Menu_Index = M.Sub_Menu_Index,
                    menuType_Index = M.MenuType_Index,
                    menu_Id = M.Menu_Id,
                    menuControl_Name = M.MenuControl_Name,
                    menu_Name = M.Menu_Name,
                    menu_SecondName = M.Menu_SecondName,
                    menu_ThirdName = M.Menu_ThirdName,
                    create_By = M.Create_By == null ? " ": M.Create_By,
                    create_Date = M.Create_Date != null ? M.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
                    update_By = M.Update_By == null ? " " : M.Update_By,
                    update_Date = M.Update_Date != null ? M.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
                }).OrderBy(o => o.menu_Id.sParse<int>()).ToList();

                var result = new List<ConfigUserGroupMenuExportViewModel>();
                int num = 0;
                
                foreach (var q in query)
                {
                    var usergroupmenu = db.MS_UserGroupMenu.Where(c => c.UserGroup_Index == data.userGroup_Index && c.Menu_Index == q.menu_Index && c.IsActive == 1).ToList();
                    foreach (var u in usergroupmenu)
                    {
                        q.numBerOf = num + 1;
                        q.userGroupMenu_Index = u.UserGroupMenu_Index;
                        q.userGroupMenu_Id = u.UserGroupMenu_Id;
                        q.userGroup_Index = u.UserGroup_Index;
                        q.isActive = u.IsActive == 1 ? true : false;
                        num++;
                    }
                }
                query = query.Where(c => c.isActive == true).ToList();
                var count = query.Count;

                var actionResultWaveViewModel = new ConfigUserGroupMenuActionResultExportViewModel();
                actionResultWaveViewModel.itemsConfigUserGroupMenu = query.OrderBy(o => o.menu_Id.sParse<int>()).ToList();
                actionResultWaveViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultWaveViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

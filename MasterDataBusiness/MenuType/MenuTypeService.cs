using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MasterDataBusiness
{
    public class MenuTypeService
    {
        public List<MenuTypeViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.sy_MenuType.FromSql("sp_GetMenuType").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var result = new List<MenuTypeViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new MenuTypeViewModel();

                        resultItem.MenuTypeIndex = item.MenuType_Index;
                        resultItem.MenuTypeId = item.MenuType_Id;
                        resultItem.MenuTypeName = item.MenuType_Name;
                        resultItem.IsActive = item.IsActive;
                        resultItem.IsDelete = item.IsDelete;
                        resultItem.IsSystem = item.IsSystem;
                        resultItem.StatusId = item.Status_Id;
                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
                        resultItem.CreateBy = item.Create_By;
                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
                        resultItem.UpdateBy = item.Update_By;
                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
                        resultItem.CancelBy = item.Cancel_By;

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
        public String SaveChanges(MenuTypeViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    if (data.MenuTypeIndex.ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        data.MenuTypeIndex = Guid.NewGuid();
                    }
                    if (data.MenuTypeId == null)
                    {
                        var Sys_Key = new SqlParameter("Sys_Key", "MenuTypeId");
                        var resultParameter = new SqlParameter("@result", SqlDbType.Int);
                        resultParameter.Size = 2000; // some meaningfull value
                        resultParameter.Direction = ParameterDirection.Output;
                        context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
                        //var result = resultParameter.Value;
                        data.MenuTypeId = resultParameter.Value.ToString();
                    }
                    int Isactive = 1;
                    int Isdelete = 0;
                    int Issystem = 0;
                    int statusId = 0;
                    var MenuType_Index = new SqlParameter("MenuType_Index", data.MenuTypeIndex);
                    var MenuType_Id = new SqlParameter("MenuType_Id", data.MenuTypeId);
                    var MenuType_Name = new SqlParameter("MenuType_Name", data.MenuTypeName);
                    var IsActive = new SqlParameter("IsActive", Isactive);
                    var IsDelete = new SqlParameter("IsDelete", Isdelete);
                    var IsSystem = new SqlParameter("IsSystem", Issystem);
                    var Status_Id = new SqlParameter("Status_Id", statusId);
                    var Create_By = new SqlParameter("Create_By", "");
                    var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
                    var Update_By = new SqlParameter("Update_By", "");
                    var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
                    var Cancel_By = new SqlParameter("Cancel_By", "");
                    var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
                    var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_sy_MenuType  @MenuType_Index,@MenuType_Id,@MenuType_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", MenuType_Index, MenuType_Id, MenuType_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
                    return rowsAffected.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MenuTypeViewModel> getDelete(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    string pstring = " and MenuType_Index ='" + id + "'";

                    var queryResult = context.sy_MenuType.FromSql("sp_GetMenuType {0}", pstring).Where(c => c.MenuType_Index == id).ToList();

                    int isactive = 0;
                    int isdelete = 1;
                    var result = new List<MenuTypeViewModel>();
                    foreach (var item in queryResult)
                    {
                        var MenuType_Index = new SqlParameter("MenuType_Index", item.MenuType_Index);
                        var MenuType_Id = new SqlParameter("Menu_Id", item.MenuType_Id);
                        var MenuType_Name = new SqlParameter("MenuType_Name", item.MenuType_Name);
                        var IsActive = new SqlParameter("IsActive", isactive);
                        var IsDelete = new SqlParameter("IsDelete", isdelete);
                        var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
                        var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
                        var Create_By = new SqlParameter("Create_By", "");
                        var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
                        var Update_By = new SqlParameter("Update_By", "");
                        var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
                        var Cancel_By = new SqlParameter("Cancel_By", "");
                        var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
                        var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_sy_MenuType  @MenuType_Index,@MenuType_Id,@MenuType_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", MenuType_Index, MenuType_Id, MenuType_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
                        context.SaveChanges();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MenuTypeViewModel> getId(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.sy_MenuType.FromSql("sp_GetMenuType").Where(c => c.MenuType_Index == id).ToList();

                    var result = new List<MenuTypeViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new MenuTypeViewModel();

                        resultItem.MenuTypeIndex = item.MenuType_Index;
                        resultItem.MenuTypeId = item.MenuType_Id;
                        resultItem.MenuTypeName = item.MenuType_Name;
                        resultItem.IsActive = item.IsActive;
                        resultItem.IsDelete = item.IsDelete;
                        resultItem.IsSystem = item.IsSystem;
                        resultItem.StatusId = item.Status_Id;
                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
                        resultItem.CreateBy = item.Create_By;
                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
                        resultItem.UpdateBy = item.Update_By;
                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
                        resultItem.CancelBy = item.Cancel_By;

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
    }
}

using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MasterDataBusiness
{
    public class MenuService
    {
        public List<MenuViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.sy_Menu.FromSql("sp_GetMenu").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var result = new List<MenuViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new MenuViewModel();

                        resultItem.MenuIndex = item.Menu_Index;
                        resultItem.MenuTypeIndex = item.MenuType_Index;
                        resultItem.MenuControlName = item.MenuControl_Name;
                        resultItem.MenuId = item.Menu_Id;
                        resultItem.MenuName = item.Menu_Name;
                        resultItem.MenuSecondName = item.Menu_SecondName;
                        resultItem.ProductThirdName = item.Menu_ThirdName;
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
        public String SaveChanges(MenuViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    if (data.MenuIndex.ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        data.MenuIndex = Guid.NewGuid();
                    }
                    int Isactive = 1;
                    int Isdelete = 0;
                    int Issystem = 0;
                    int statusId = 0;
                    var Menu_Index = new SqlParameter("Menu_Index", data.MenuIndex);
                    var MenuType_Index = new SqlParameter("MenuType_Index", data.MenuTypeIndex);
                    var Menu_Id = new SqlParameter("Menu_Id", data.MenuId);
                    var MenuControl_Name = new SqlParameter("MenuControl_Name", data.MenuControlName);
                    var Menu_Name = new SqlParameter("Menu_Name", data.MenuName);
                    var Menu_SecondName = new SqlParameter("Menu_SecondName", data.MenuSecondName);
                    var Product_ThirdName = new SqlParameter("Product_ThirdName", data.ProductThirdName);
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
                    var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_sy_Menu  @Menu_Index,@MenuType_Index,@Menu_Id,@MenuControl_Name,@Menu_Name,@Menu_SecondName,@Product_ThirdName,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Menu_Index, MenuType_Index, Menu_Id, MenuControl_Name, Menu_Name, Menu_SecondName, Product_ThirdName, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
                    return rowsAffected.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MenuViewModel> getDelete(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    string pstring = " and Menu_Index ='" + id + "'";

                    var queryResult = context.sy_Menu.FromSql("sp_GetMenu {0}", pstring).Where(c => c.Menu_Index == id).ToList();

                    int isactive = 0;
                    int isdelete = 1;
                    var result = new List<MenuViewModel>();
                    foreach (var item in queryResult)
                    {
                        var Menu_Index = new SqlParameter("Menu_Index", item.Menu_Index);
                        var MenuType_Index = new SqlParameter("MenuType_Index", item.MenuType_Index);
                        var Menu_Id = new SqlParameter("Menu_Id", item.Menu_Id);
                        var MenuControl_Name = new SqlParameter("MenuControl_Name", item.MenuControl_Name);
                        var Menu_Name = new SqlParameter("Menu_Name", item.Menu_Name);
                        var Menu_SecondName = new SqlParameter("Menu_SecondName", item.Menu_SecondName);
                        var Product_ThirdName = new SqlParameter("Product_ThirdName", item.Menu_ThirdName);
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
                        var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_sy_Menu  @Menu_Index,@MenuType_Index,@Menu_Id,@MenuControl_Name,@Menu_Name,@Menu_SecondName,@Product_ThirdName,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Menu_Index, MenuType_Index, Menu_Id, MenuControl_Name, Menu_Name, Menu_SecondName, Product_ThirdName, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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
        public List<MenuViewModel> getId(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.sy_Menu.FromSql("sp_GetMenu").Where(c => c.Menu_Index == id).ToList();

                    var result = new List<MenuViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new MenuViewModel();

                        resultItem.MenuIndex = item.Menu_Index;
                        resultItem.MenuTypeIndex = item.MenuType_Index;
                        resultItem.MenuControlName = item.MenuControl_Name;
                        resultItem.MenuId = item.Menu_Id;
                        resultItem.MenuName = item.Menu_Name;
                        resultItem.MenuSecondName = item.Menu_SecondName;
                        resultItem.ProductThirdName = item.Menu_ThirdName;
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

using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MasterDataBusiness
{
    public class UserEquipmentService
    {
        public List<UserEquipmentViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_UserEquipment.FromSql("sp_GetUserEquipment").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var result = new List<UserEquipmentViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new UserEquipmentViewModel();
                        resultItem.UserEquipmentIndex = item.UserEquipment_Index;
                        if (item.User_Index != null)
                        {
                            var itemList = context.MS_User.FromSql("sp_GetUser").Where(c => item.User_Index == c.User_Index).FirstOrDefault();
                            if (itemList != null)
                            {
                                resultItem.UserIndex = item.User_Index;
                                resultItem.UserName = itemList.User_Name;
                            }

                        }
                        if (item.Equipment_Index != null)
                        {
                            var itemList = context.MS_Equipment.FromSql("sp_GetEquipment").Where(c => item.Equipment_Index == c.Equipment_Index).FirstOrDefault();
                            if (itemList != null)
                            {
                                resultItem.EquipmentIndex = item.Equipment_Index;
                                resultItem.EquipmentName = itemList.Equipment_Name;
                            }

                        }

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
        public List<UserEquipmentViewModel> getId(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_UserEquipment.FromSql("sp_GetUserEquipment").Where(c => c.UserEquipment_Index == id).ToList();
                    var result = new List<UserEquipmentViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new UserEquipmentViewModel();
                        resultItem.UserEquipmentIndex = item.UserEquipment_Index;
                        if (item.User_Index != null)
                        {
                            var itemList = context.MS_User.FromSql("sp_GetUser").Where(c => item.User_Index == c.User_Index).FirstOrDefault();
                            if (itemList != null)
                            {
                                resultItem.UserIndex = item.User_Index;
                                resultItem.UserName = itemList.User_Name;
                            }

                        }
                        if (item.Equipment_Index != null)
                        {
                            var itemList = context.MS_Equipment.FromSql("sp_GetEquipment").Where(c => item.Equipment_Index == c.Equipment_Index).FirstOrDefault();
                            if (itemList != null)
                            {
                                resultItem.EquipmentIndex = item.Equipment_Index;
                                resultItem.EquipmentName = itemList.Equipment_Name;
                            }

                        }
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
        public String SaveChanges(UserEquipmentViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    if (data.UserEquipmentIndex.ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        data.UserEquipmentIndex = Guid.NewGuid();
                    }
                    int isactive = 1;
                    int isdelete = 0;
                    int isSystem = 0;
                    int statusId = 0;
                    var UserEquipment_Index = new SqlParameter("UserEquipment_Index", data.UserEquipmentIndex);
                    var User_Index = new SqlParameter("User_Index", data.UserIndex);
                    var Equipment_Index = new SqlParameter("Equipment_Index", data.EquipmentIndex);
                    var IsActive = new SqlParameter("IsActive", isactive);
                    var IsDelete = new SqlParameter("IsDelete", isdelete);
                    var IsSystem = new SqlParameter("IsSystem", isSystem);
                    var Status_Id = new SqlParameter("Status_Id", statusId);
                    var Create_By = new SqlParameter("Create_By", "");
                    var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
                    var Update_By = new SqlParameter("Update_By", "");
                    var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
                    var Cancel_By = new SqlParameter("Cancel_By", "");
                    var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
                    var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_UserEquipment  @UserEquipment_Index,@User_Index,@Equipment_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", UserEquipment_Index, User_Index, Equipment_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
                    return rowsAffected.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<UserEquipmentViewModel> getDelete(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_UserEquipment.FromSql("sp_GetUserEquipment").Where(c => c.UserEquipment_Index == id).ToList();
                    var result = new List<UserEquipmentViewModel>();
                    foreach (var item in queryResult)
                    {
                        int isactive = 0;
                        int isdelete = 1;
                        var UserEquipment_Index = new SqlParameter("UserEquipment_Index", item.UserEquipment_Index);
                        var User_Index = new SqlParameter("User_Index", item.User_Index);
                        var Equipment_Index = new SqlParameter("Equipment_Index", item.Equipment_Index);
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
                        var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_UserEquipment  @UserEquipment_Index,@User_Index,@Equipment_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", UserEquipment_Index, User_Index, Equipment_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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
    }
}

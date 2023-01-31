using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{
    public class PalletService
    {
        public List<PalletViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_Pallet.FromSql("sp_GetPallet").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var result = new List<PalletViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new PalletViewModel();

                        resultItem.PalletIndex = item.Pallet_Index;
                        resultItem.PalletId = item.Pallet_Id;
                        resultItem.PalletName = item.Pallet_Name;
                        resultItem.PalletTypeIndex = item.PalletType_Index;
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

        public String SaveChanges(PalletViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    int isactive = 1;
                    int isdelete = 0;
                    int issystem = 0;
                    int statusid = 0;

                    if (data.PalletIndex.ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        data.PalletIndex = Guid.NewGuid();
                    }
                    if (data.PalletId == null)
                    {
                        var Sys_Key = new SqlParameter("Sys_Key", "PalletID");
                        var resultParameter = new SqlParameter("@result", SqlDbType.Int);
                        resultParameter.Size = 2000; // some meaningfull value
                        resultParameter.Direction = ParameterDirection.Output;
                        context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
                        //var result = resultParameter.Value;
                        data.PalletId = resultParameter.Value.ToString();
                    }

                    var Pallet_Index = new SqlParameter("Pallet_Index", data.PalletIndex);
                    var Pallet_Id = new SqlParameter("Pallet_Id", data.PalletId);
                    var Pallet_Name = new SqlParameter("Pallet_Name", data.PalletName);
                    var PalletType_Index = new SqlParameter("PalletType_Index", data.PalletTypeIndex);
                    var IsActive = new SqlParameter("IsActive", isactive);
                    var IsDelete = new SqlParameter("IsDelete", isdelete);
                    var IsSystem = new SqlParameter("IsSystem", issystem);
                    var Status_Id = new SqlParameter("Status_Id", statusid);
                    var Create_By = new SqlParameter("Create_By", data.CreateBy);
                    var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
                    var Update_By = new SqlParameter("Update_By", "");
                    var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
                    var Cancel_By = new SqlParameter("Cancel_By", "");
                    var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
                    var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_Pallet  @Pallet_Index,@Pallet_Id,@Pallet_Name,@PalletType_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Pallet_Index, Pallet_Id, Pallet_Name, PalletType_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
                    return rowsAffected.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PalletViewModel> getDelete(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_Pallet.FromSql("sp_GetPallet").Where(c => c.Pallet_Index == id).ToList();

                    int isactive = 0;
                    int isdelete = 1;
                    var result = new List<PalletViewModel>();
                    foreach (var item in queryResult)
                    {
                        var Pallet_Index = new SqlParameter("Pallet_Index", item.Pallet_Index);
                        var Pallet_Id = new SqlParameter("Pallet_Id", item.Pallet_Id);
                        var Pallet_Name = new SqlParameter("Pallet_Name", item.Pallet_Name);
                        var PalletType_Index = new SqlParameter("PalletType_Index", item.PalletType_Index);
                        var IsActive = new SqlParameter("IsActive", isactive);
                        var IsDelete = new SqlParameter("IsDelete", isdelete);
                        var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
                        var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
                        var Create_By = new SqlParameter("Create_By", item.Create_By);
                        var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
                        var Update_By = new SqlParameter("Update_By", "");
                        var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
                        var Cancel_By = new SqlParameter("Cancel_By", "");
                        var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
                        var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_Pallet  @Pallet_Index,@Pallet_Id,@Pallet_Name,@PalletType_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Pallet_Index, Pallet_Id, Pallet_Name, PalletType_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        public List<PalletViewModel> getId(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_Pallet.FromSql("sp_GetPallet").Where(c => c.Pallet_Index == id).ToList();
                    queryResult.Where(c => c.Pallet_Index == id);

                    var result = new List<PalletViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new PalletViewModel();
                        resultItem.PalletIndex = item.Pallet_Index;
                        resultItem.PalletId = item.Pallet_Id;
                        resultItem.PalletName = item.Pallet_Name;
                        resultItem.PalletTypeIndex = item.PalletType_Index;
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

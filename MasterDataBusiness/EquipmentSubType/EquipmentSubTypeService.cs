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
using static MasterDataBusiness.ViewModels.SearchEquipmentSubTypeViewModel;

namespace MasterDataBusiness
{
    public class EquipmentSubTypeService
    {
        #region BeforeCodeEquipmentSubType
        //    public List<EquipmentSubTypeViewModel> Filter()
        //    {
        //        try
        //        {
        //            using (var context = new MasterDataDbContext())
        //            {
        //                var queryResult = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //                var result = new List<EquipmentSubTypeViewModel>();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new EquipmentSubTypeViewModel();
        //                    resultItem.EquipmentSubTypeIndex = item.EquipmentSubType_Index;
        //                    resultItem.EquipmentSubTypeId = item.EquipmentSubType_Id;
        //                    resultItem.EquipmentSubTypeName = item.EquipmentSubType_Name;

        //                    if (item.EquipmentType_Index != null)
        //                    {
        //                        var itemList = context.MS_EquipmentType.FromSql("sp_GetEquipmentType").Where(c => c.EquipmentType_Index == item.EquipmentType_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            resultItem.EquipmentTypeIndex = itemList.EquipmentType_Index;
        //                            resultItem.EquipmentTypeName = itemList.EquipmentType_Name;
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

        //    public actionResultEquipmentSubTypeViewModel FilterEquipmentType(EquipmentSubTypeViewModel model)
        //    {
        //        try
        //        {
        //            using (var context = new MasterDataDbContext())

        //            {
        //                string pwhere1 = "";
        //                if (model.EquipmentSubTypeName != null)
        //                {
        //                    pwhere1 += " And EquipmentSubType_Name like N'%" + model.EquipmentSubTypeName + "%'";
        //                }
        //                else
        //                {
        //                    pwhere1 += " ";
        //                }
        //                Guid newGuid = new Guid();
        //                if (model.EquipmentTypeIndex != newGuid)
        //                {
        //                    pwhere1 += " And EquipmentType_Index = '" + model.EquipmentTypeIndex + "'";
        //                }
        //                else
        //                {
        //                    pwhere1 += " ";
        //                }


        //                var strwhere1 = new SqlParameter("@strwhere", pwhere1);
        //                // var strwhere = new SqlParameter("@strwhere", DBNull.Value);
        //                var PageNumber1 = new SqlParameter("@PageNumber", 1);
        //                var RowspPage1 = new SqlParameter("@RowspPage", 10000);
        //                var queryResultTotal = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //                string pwhere = "";
        //                if (model.EquipmentSubTypeName != null)
        //                {
        //                    pwhere = " And EquipmentSubType_Name like N'%" + model.EquipmentSubTypeName + "%'";
        //                }
        //                else
        //                {
        //                    pwhere = " ";
        //                }
        //                Guid newGuid1 = new Guid();
        //                if (model.EquipmentTypeIndex != newGuid1)
        //                {
        //                    pwhere += " And EquipmentType_Index = '" + model.EquipmentTypeIndex + "'";
        //                }
        //                else
        //                {
        //                    pwhere += " ";
        //                }

        //                var strwhere = new SqlParameter("@strwhere", pwhere);
        //                // var strwhere = new SqlParameter("@strwhere", DBNull.Value);
        //                var PageNumber = new SqlParameter("@PageNumber", model.CurrentPage);
        //                var RowspPage = new SqlParameter("@RowspPage", model.PerPage);

        //                var queryResult = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber, RowspPage).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //                var result = new List<EquipmentSubTypeViewModel>();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new EquipmentSubTypeViewModel();
        //                    resultItem.EquipmentSubTypeIndex = item.EquipmentSubType_Index;
        //                    resultItem.EquipmentSubTypeId = item.EquipmentSubType_Id;
        //                    resultItem.EquipmentSubTypeName = item.EquipmentSubType_Name;

        //                    if (item.EquipmentType_Index != null)
        //                    {
        //                        var itemList = context.MS_EquipmentType.FromSql("sp_GetEquipmentType").Where(c => c.EquipmentType_Index == item.EquipmentType_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            resultItem.EquipmentTypeIndex = itemList.EquipmentType_Index;
        //                            resultItem.EquipmentTypeName = itemList.EquipmentType_Name;
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

        //                var count = queryResultTotal.Count;
        //                var actionResultEquipmentSubType = new actionResultEquipmentSubTypeViewModel();
        //                actionResultEquipmentSubType.itemsEquipmentSubType = result.ToList();
        //                actionResultEquipmentSubType.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage };

        //                //return actionResultVender;

        //                return actionResultEquipmentSubType;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }

        //    public String SaveChanges(EquipmentSubTypeViewModel data)
        //    {
        //        try
        //        {
        //            using (var context = new MasterDataDbContext())
        //            {
        //                if (data.EquipmentSubTypeIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //                {
        //                    data.EquipmentSubTypeIndex = Guid.NewGuid();
        //                }
        //                int isactive = 1;
        //                int isdelete = 0;
        //                int issystem = 0;
        //                int statusid = 0;

        //                if (data.EquipmentSubTypeId == null)
        //                {
        //                    var Sys_Key = new SqlParameter("Sys_Key", "EquipmentSubTypeId");
        //                    var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                    resultParameter.Size = 2000; // some meaningfull value
        //                    resultParameter.Direction = ParameterDirection.Output;
        //                    context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                    //var result = resultParameter.Value;
        //                    data.EquipmentSubTypeId = resultParameter.Value.ToString();
        //                }
        //                var EquipmentSubType_Index = new SqlParameter("EquipmentSubType_Index", data.EquipmentSubTypeIndex);
        //                var EquipmentSubType_Id = new SqlParameter("EquipmentSubType_Id", data.EquipmentSubTypeId);
        //                var EquipmentSubType_Name = new SqlParameter("EquipmentSubType_Name", data.EquipmentSubTypeName);
        //                var EquipmentType_Index = new SqlParameter("EquipmentType_Index", data.EquipmentTypeIndex);
        //                var IsActive = new SqlParameter("IsActive", isactive);
        //                var IsDelete = new SqlParameter("IsDelete", isdelete);
        //                var IsSystem = new SqlParameter("IsSystem", issystem);
        //                var Status_Id = new SqlParameter("Status_Id", statusid);
        //                var Create_By = new SqlParameter("Create_By", "");
        //                var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        //                var Update_By = new SqlParameter("Update_By", "");
        //                var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        //                var Cancel_By = new SqlParameter("Cancel_By", "");
        //                var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_EquipmentSubType  @EquipmentSubType_Index,@EquipmentSubType_Id,@EquipmentSubType_Name,@EquipmentType_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", EquipmentSubType_Index, EquipmentSubType_Id, EquipmentSubType_Name, EquipmentType_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //                return rowsAffected.ToString();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    public List<EquipmentSubTypeViewModel> getDelete(Guid id)
        //    {
        //        try
        //        {
        //            using (var context = new MasterDataDbContext())
        //            {
        //                var queryResult = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType").ToList();
        //                int isactive = 0;
        //                int isdelete = 1;
        //                var result = new List<EquipmentSubTypeViewModel>();
        //                foreach (var item in queryResult.Where(c => c.EquipmentSubType_Index == id))
        //                {
        //                    var EquipmentSubType_Index = new SqlParameter("EquipmentSubType_Index", item.EquipmentSubType_Index);
        //                    var EquipmentSubType_Id = new SqlParameter("EquipmentSubType_Id", item.EquipmentSubType_Id);
        //                    var EquipmentSubType_Name = new SqlParameter("EquipmentSubType_Name", item.EquipmentSubType_Name);
        //                    var EquipmentType_Index = new SqlParameter("EquipmentType_Index", item.EquipmentType_Index);
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
        //                    var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_EquipmentSubType  @EquipmentSubType_Index,@EquipmentSubType_Id,@EquipmentSubType_Name,@EquipmentType_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", EquipmentSubType_Index, EquipmentSubType_Id, EquipmentSubType_Name, EquipmentType_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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
        //public List<EquipmentSubTypeViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            string pstring = " and EquipmentSubType_Index ='" + id + "'";                    
        //            var result = new List<EquipmentSubTypeViewModel>();

        //            //****************** Check data ว่ามี Index ของ Province หรือไม่ ***************************//
        //            var strwhere1 = new SqlParameter("@strwhere", DBNull.Value);
        //            var PageNumber1 = new SqlParameter("@PageNumber", 1);
        //            var RowspPage1 = new SqlParameter("@RowspPage", 10000);
        //            var queryResult = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
        //            //var queryResult = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType").Where(c => c.EquipmentSubType_Index == id).ToList();
        //            if (queryResult.Count > 0)
        //            {
        //                var findItem = queryResult.Where(c => c.EquipmentSubType_Index == id).ToList();
        //                var count = findItem.Count;
        //                if (findItem != null)
        //                {
        //                    foreach (var item in findItem)
        //                    {
        //                        var resultItem = new EquipmentSubTypeViewModel();
        //                        resultItem.EquipmentSubTypeIndex = item.EquipmentSubType_Index;
        //                        resultItem.EquipmentSubTypeId = item.EquipmentSubType_Id;
        //                        resultItem.EquipmentSubTypeName = item.EquipmentSubType_Name;

        //                        if (item.EquipmentType_Index != null)
        //                        {
        //                            var itemList = context.MS_EquipmentType.FromSql("sp_GetEquipmentType").Where(c => c.EquipmentType_Index == item.EquipmentType_Index).FirstOrDefault();
        //                            if (itemList != null)
        //                            {
        //                                resultItem.EquipmentTypeIndex = itemList.EquipmentType_Index;
        //                                resultItem.EquipmentTypeName = itemList.EquipmentType_Name;
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

        //                        resultItem.count = count;
        //                        result.Add(resultItem);
        //                    }
        //                }
        //                else
        //                {
        //                    var queryResult1 = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType{0}", pstring).ToList();
        //                    queryResult1.Where(c => c.EquipmentType_Index == id);

        //                    foreach (var item in queryResult1)
        //                    {
        //                        var resultItem = new EquipmentSubTypeViewModel();
        //                        resultItem.EquipmentSubTypeIndex = item.EquipmentSubType_Index;
        //                        resultItem.EquipmentSubTypeId = item.EquipmentSubType_Id;
        //                        resultItem.EquipmentSubTypeName = item.EquipmentSubType_Name;

        //                        if (item.EquipmentType_Index != null)
        //                        {
        //                            var itemList = context.MS_EquipmentType.FromSql("sp_GetEquipmentType").Where(c => c.EquipmentType_Index == item.EquipmentType_Index).FirstOrDefault();
        //                            if (itemList != null)
        //                            {
        //                                resultItem.EquipmentTypeIndex = itemList.EquipmentType_Index;
        //                                resultItem.EquipmentTypeName = itemList.EquipmentType_Name;
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

        //            }


        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<EquipmentSubTypeViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType").Where(c => c.EquipmentSubType_Index == id).ToList();

        //            var result = new List<EquipmentSubTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new EquipmentSubTypeViewModel();
        //                resultItem.EquipmentSubTypeIndex = item.EquipmentSubType_Index;
        //                resultItem.EquipmentSubTypeId = item.EquipmentSubType_Id;
        //                resultItem.EquipmentSubTypeName = item.EquipmentSubType_Name;

        //                if (item.EquipmentType_Index != null)
        //                {
        //                    var itemList = context.MS_EquipmentType.FromSql("sp_GetEquipmentType").Where(c => c.EquipmentType_Index == item.EquipmentType_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.EquipmentTypeIndex = itemList.EquipmentType_Index;
        //                        resultItem.EquipmentTypeName = itemList.EquipmentType_Name;
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
        //            if (queryResult.Count == 0)
        //            {
        //                var findItem = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType").Where(c => c.EquipmentType_Index == id).ToList();
        //                if (findItem != null)
        //                {
        //                    foreach (var item in findItem)
        //                    {
        //                        //var itemList = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType").Where(c => c.EquipmentType_Index == item.EquipmentType_Index).FirstOrDefault();
        //                        var resultItem = new EquipmentSubTypeViewModel();
        //                        resultItem.EquipmentSubTypeIndex = item.EquipmentSubType_Index;
        //                        resultItem.EquipmentSubTypeId = item.EquipmentSubType_Id;
        //                        resultItem.EquipmentSubTypeName = item.EquipmentSubType_Name;
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
        //            }
        //                return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<EquipmentSubTypeViewModel> search(EquipmentSubTypeViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<EquipmentSubTypeViewModel>();
        //            var queryResult = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType").Where(c => c.IsActive == 1 && c.IsDelete == 0)
        //                                            .ToList();
        //            if (data.EquipmentSubTypeId != "" && data.EquipmentSubTypeId != null)
        //            {
        //                pwhereFilter = " And EquipmentSubType_Id like N'%" + data.EquipmentSubTypeId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }
        //            if (data.EquipmentSubTypeName != "" && data.EquipmentSubTypeName != null)
        //            {
        //                pwhereFilter = " And EquipmentSubType_Name like N'%" + data.EquipmentSubTypeName + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }

        //            if (data.EquipmentSubTypeId != "" && data.EquipmentSubTypeId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new EquipmentSubTypeViewModel();
        //                    resultItem.EquipmentSubTypeIndex = item.EquipmentSubType_Index;
        //                    resultItem.EquipmentSubTypeId = item.EquipmentSubType_Id;
        //                    resultItem.EquipmentSubTypeName = item.EquipmentSubType_Name;

        //                    if (item.EquipmentType_Index != null)
        //                    {
        //                        var itemList = context.MS_EquipmentType.FromSql("sp_GetEquipmentType").Where(c => c.EquipmentType_Index == item.EquipmentType_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            resultItem.EquipmentTypeIndex = itemList.EquipmentType_Index;
        //                            resultItem.EquipmentTypeName = itemList.EquipmentType_Name;
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
        //            else if (data.EquipmentSubTypeName != "" && data.EquipmentSubTypeName != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new EquipmentSubTypeViewModel();
        //                    resultItem.EquipmentSubTypeIndex = item.EquipmentSubType_Index;
        //                    resultItem.EquipmentSubTypeId = item.EquipmentSubType_Id;
        //                    resultItem.EquipmentSubTypeName = item.EquipmentSubType_Name;

        //                    if (item.EquipmentType_Index != null)
        //                    {
        //                        var itemList = context.MS_EquipmentType.FromSql("sp_GetEquipmentType").Where(c => c.EquipmentType_Index == item.EquipmentType_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            resultItem.EquipmentTypeIndex = itemList.EquipmentType_Index;
        //                            resultItem.EquipmentTypeName = itemList.EquipmentType_Name;
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
        //            else if (data.EquipmentTypeName != "" && data.EquipmentTypeName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And EquipmentType_Name like N'%" + data.EquipmentTypeName + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_EquipmentType.FromSql("sp_GetEquipmentType @strwhere ", pstrwhere1).ToList();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new EquipmentSubTypeViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.EquipmentType_Index == ItemList.EquipmentType_Index)
        //                        {
        //                            resultItem.EquipmentSubTypeIndex = item.EquipmentSubType_Index;
        //                            resultItem.EquipmentSubTypeId = item.EquipmentSubType_Id;
        //                            resultItem.EquipmentSubTypeName = item.EquipmentSubType_Name;

        //                            if (item.EquipmentType_Index != null)
        //                            {
        //                                var itemList = context.MS_EquipmentType.FromSql("sp_GetEquipmentType").Where(c => c.EquipmentType_Index == item.EquipmentType_Index).FirstOrDefault();
        //                                if (itemList != null)
        //                                {
        //                                    resultItem.EquipmentTypeIndex = itemList.EquipmentType_Index;
        //                                    resultItem.EquipmentTypeName = itemList.EquipmentType_Name;
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

        //            if (data.EquipmentSubTypeId == "" && data.EquipmentSubTypeName == "" && data.EquipmentTypeName == "")
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
        #endregion

        #region FindEquipmentSubType
        public EquipmentSubTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_EquipmentSubType.Where(c => c.EquipmentSubType_Index == id).FirstOrDefault();

                var result = new EquipmentSubTypeViewModel();


                result.equipmentSubType_Index = queryResult.EquipmentSubType_Index;
                result.equipmentSubType_Id = queryResult.EquipmentSubType_Id;
                result.equipmentSubType_Name = queryResult.EquipmentSubType_Name;
                result.equipmentType_Index = queryResult.EquipmentType_Index;
                result.equipmentType_Name = queryResult.EquipmentType_Name;
                result.isActive = queryResult.IsActive;


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

        #region FilterEquipmentSubType

        //Filter
        private MasterDataDbContext db;

        public EquipmentSubTypeService()
        {
            db = new MasterDataDbContext();
        }

        public EquipmentSubTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

     
        public actionResultEquipmentSubTypeViewModel filter(SearchEquipmentSubTypeViewModel data)
        {
            try
            {
                var query = db.View_EquipmentSubType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.EquipmentSubType_Name.Contains(data.key)
                                        || c.EquipmentSubType_Id.Contains(data.key)
                                        || c.EquipmentType_Name.Contains(data.key));


                }

                var Item = new List<View_EquipmentSubType>();
                var TotalRow = new List<View_EquipmentSubType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.EquipmentSubType_Id).ToList();

                var result = new List<SearchEquipmentSubTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchEquipmentSubTypeViewModel();

                    resultItem.equipmentSubType_Index = item.EquipmentSubType_Index;
                    resultItem.equipmentSubType_Id = item.EquipmentSubType_Id;
                    resultItem.equipmentSubType_Name = item.EquipmentSubType_Name;
                    resultItem.equipmentType_Index = item.EquipmentType_Index;
                    resultItem.equipmentType_Name = item.EquipmentType_Name;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultEquipmentSubTypeViewModel = new actionResultEquipmentSubTypeViewModel();
                actionResultEquipmentSubTypeViewModel.itemsEquipmentSubType = result.ToList();
                actionResultEquipmentSubTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultEquipmentSubTypeViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDelete
        public Boolean getDelete(EquipmentSubTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var equipmentSubType = db.MS_EquipmentSubType.Find(data.equipmentSubType_Index);

                if (equipmentSubType != null)
                {
                    equipmentSubType.IsActive = 0;
                    equipmentSubType.IsDelete = 1;


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
                        olog.logging("DeleteEquipmentSubType", msglog);
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

        #region SaveChangesEquipmentSubType
        public String SaveChanges(EquipmentSubTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var EquipmentSubTypeOld = db.MS_EquipmentSubType.Find(data.equipmentSubType_Index);

                if (EquipmentSubTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.equipmentSubType_Id))
                    {
                        var query = db.MS_EquipmentSubType.FirstOrDefault(c => c.EquipmentSubType_Id == data.equipmentSubType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.equipmentSubType_Id))
                    {
                        data.equipmentSubType_Id = "EquipmentSubType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_EquipmentSubType.FirstOrDefault(c => c.EquipmentSubType_Id == data.equipmentSubType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.EquipmentSubType_Id == data.equipmentSubType_Id)
                                {
                                    data.equipmentSubType_Id = "EquipmentSubType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    //data.equipmentSubType_Id = "EquipmentSubType_Id".genAutonumber();

                    MS_EquipmentSubType Model = new MS_EquipmentSubType();

                    Model.EquipmentSubType_Index = Guid.NewGuid();
                    Model.EquipmentSubType_Id = data.equipmentSubType_Id;
                    Model.EquipmentSubType_Name = data.equipmentSubType_Name;
                    Model.EquipmentType_Index = data.equipmentType_Index;
                    Model.EquipmentType_Index = data.equipmentType_Index;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_EquipmentSubType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.equipmentSubType_Id))
                    {
                        if (EquipmentSubTypeOld.EquipmentSubType_Id != "")
                        {
                            data.equipmentSubType_Id = EquipmentSubTypeOld.EquipmentSubType_Id;
                        }
                    }
                    else
                    {
                        if (EquipmentSubTypeOld.EquipmentSubType_Id != data.equipmentSubType_Id)
                        {
                            var query = db.MS_EquipmentSubType.FirstOrDefault(c => c.EquipmentSubType_Id == data.equipmentSubType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.equipmentSubType_Id = EquipmentSubTypeOld.EquipmentSubType_Id;
                        }
                    }
                    EquipmentSubTypeOld.EquipmentSubType_Id = data.equipmentSubType_Id;
                    EquipmentSubTypeOld.EquipmentType_Index = data.equipmentType_Index;
                    EquipmentSubTypeOld.EquipmentSubType_Name = data.equipmentSubType_Name;
                    EquipmentSubTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    EquipmentSubTypeOld.Update_By = data.update_By;
                    EquipmentSubTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveEquipmentSubType", msglog);
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

        #region SearchEquipmentSubType
        public List<ItemListViewModel> autoEquipmentSubType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_EquipmentSubType.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.EquipmentSubType_Id.Contains(data.key)
                                                || c.EquipmentSubType_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.EquipmentSubType_Name, c.EquipmentSubType_Index, c.EquipmentSubType_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.EquipmentSubType_Index,
                            id = item.EquipmentSubType_Id,
                            name = item.EquipmentSubType_Id + " - " + item.EquipmentSubType_Name,
                            key = item.EquipmentSubType_Id + " - " + item.EquipmentSubType_Name,
                        };

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
        #endregion

        #region SearchEquipmentSubTypeFilter

        public List<ItemListViewModel> AutoSearchEquipmentSubTypeFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_EquipmentSubType.Where(c => c.EquipmentSubType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.EquipmentSubType_Name,
                        key = s.EquipmentSubType_Name
                    }).Distinct();

                    var query2 = db.MS_EquipmentSubType.Where(c => c.EquipmentSubType_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.EquipmentSubType_Id,
                        key = s.EquipmentSubType_Id
                    }).Distinct();

                    var query3 = db.MS_EquipmentType.Where(c => c.EquipmentType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.EquipmentType_Name,
                        key = s.EquipmentType_Name
                    }).Distinct();
                    var query = query1.Union(query2).Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion
    }

}

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
using static MasterDataBusiness.ViewModels.SearchEquipmentViewModel;

namespace MasterDataBusiness
{
    public class EquipmentService
    {
        #region  BeforeCodeEquipment
        //public List<EquipmentViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_Equipment.FromSql("sp_GetEquipment").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<EquipmentViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new EquipmentViewModel();
        //                resultItem.EquipmentIndex = item.Equipment_Index;
        //                resultItem.EquipmentId = item.Equipment_Id;
        //                resultItem.EquipmentName = item.Equipment_Name;
        //                if (item.EquipmentSubType_Index != null)
        //                {
        //                    var itemList = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType").Where(c => c.EquipmentSubType_Index == item.EquipmentSubType_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.EquipmentSubTypeIndex = itemList.EquipmentSubType_Index;
        //                        resultItem.EquipmentSubTypeName = itemList.EquipmentSubType_Name;
        //                    }
        //                }
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

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public String SaveChanges(EquipmentViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int issystem = 0;
        //            int statusid = 0;

        //            if (data.EquipmentIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.EquipmentIndex = Guid.NewGuid();
        //            }
        //            if (data.EquipmentId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "EquipmentID");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.EquipmentId = resultParameter.Value.ToString();
        //            }
        //            var Equipment_Index = new SqlParameter("Equipment_Index", data.EquipmentIndex);
        //            var Equipment_Id = new SqlParameter("Equipment_Id", data.EquipmentId);
        //            var Equipment_Name = new SqlParameter("Equipment_Name", data.EquipmentName);
        //            var EquipmentType_Index = new SqlParameter("EquipmentType_Index", data.EquipmentTypeIndex);
        //            var EquipmentSubType_Index = new SqlParameter("EquipmentSubType_Index", data.EquipmentSubTypeIndex);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_Equipment  @Equipment_Index,@Equipment_Id,@Equipment_Name,@EquipmentType_Index,@EquipmentSubType_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Equipment_Index, Equipment_Id, Equipment_Name, EquipmentType_Index, EquipmentSubType_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<EquipmentViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_Equipment.FromSql("sp_GetEquipment").Where(c => c.Equipment_Index == id).ToList();
        //            var result = new List<EquipmentViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new EquipmentViewModel();
        //                resultItem.EquipmentIndex = item.Equipment_Index;
        //                resultItem.EquipmentId = item.Equipment_Id;
        //                resultItem.EquipmentName = item.Equipment_Name;
        //                if (item.EquipmentSubType_Index != null)
        //                {
        //                    var itemList = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType").Where(c => c.EquipmentSubType_Index == item.EquipmentSubType_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.EquipmentSubTypeIndex = itemList.EquipmentSubType_Index;
        //                        resultItem.EquipmentSubTypeName = itemList.EquipmentSubType_Name;
        //                    }
        //                }
        //                if (item.EquipmentSubType_Index != null)
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

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<EquipmentViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_Equipment.FromSql("sp_GetEquipment").Where(c => c.Equipment_Index == id).ToList();
        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<EquipmentViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var Equipment_Index = new SqlParameter("Equipment_Index", item.Equipment_Index);
        //                var Equipment_Id = new SqlParameter("Equipment_Id", item.Equipment_Id);
        //                var Equipment_Name = new SqlParameter("Equipment_Name", item.Equipment_Name);
        //                var EquipmentType_Index = new SqlParameter("EquipmentType_Index", item.EquipmentType_Index);
        //                var EquipmentSubType_Index = new SqlParameter("EquipmentSubType_Index", item.EquipmentSubType_Index);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_Equipment  @Equipment_Index,@Equipment_Id,@Equipment_Name,@EquipmentType_Index,@EquipmentSubType_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Equipment_Index, Equipment_Id, Equipment_Name, EquipmentType_Index, EquipmentSubType_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        //public List<EquipmentViewModel> search(EquipmentViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<EquipmentViewModel>();
        //            var queryResult = context.MS_Equipment.FromSql("sp_GetEquipment").Where(c => c.IsActive == 1 && c.IsDelete == 0)
        //                                            .ToList();
        //            if (data.EquipmentId != "" && data.EquipmentId != null)
        //            {
        //                pwhereFilter = " And Equipment_Id like N'%" + data.EquipmentId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }
        //            if (data.EquipmentName != "" && data.EquipmentName != null)
        //            {
        //                pwhereFilter = " And Equipment_Name like N'%" + data.EquipmentName + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }

        //            if (data.EquipmentId != "" && data.EquipmentId != null || data.EquipmentName != "" && data.EquipmentName != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_Equipment.FromSql("sp_GetEquipment @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new EquipmentViewModel();
        //                    resultItem.EquipmentIndex = item.Equipment_Index;
        //                    resultItem.EquipmentId = item.Equipment_Id;
        //                    resultItem.EquipmentName = item.Equipment_Name;

        //                    if (item.EquipmentSubType_Index != null)
        //                    {
        //                        var itemList = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType").Where(c => c.EquipmentSubType_Index == item.EquipmentSubType_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            resultItem.EquipmentSubTypeIndex = itemList.EquipmentSubType_Index;
        //                            resultItem.EquipmentSubTypeName = itemList.EquipmentSubType_Name;
        //                        }
        //                    }
        //                    if (item.EquipmentSubType_Index != null)
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
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And EquipmentSubType_Name like N'%" + data.EquipmentSubTypeName + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType @strwhere ", pstrwhere1).ToList();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new EquipmentViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.EquipmentSubType_Index == ItemList.EquipmentSubType_Index)
        //                        {

        //                            resultItem.EquipmentIndex = item.Equipment_Index;
        //                            resultItem.EquipmentId = item.Equipment_Id;
        //                            resultItem.EquipmentName = item.Equipment_Name;

        //                            resultItem.EquipmentSubTypeIndex = ItemList.EquipmentSubType_Index;
        //                            resultItem.EquipmentSubTypeName = ItemList.EquipmentSubType_Name;

        //                            if (item.EquipmentSubType_Index != null)
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
        //            else if (data.EquipmentTypeName != "" && data.EquipmentTypeName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And EquipmentType_Name like N'%" + data.EquipmentTypeName + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_EquipmentType.FromSql("sp_GetEquipmentType @strwhere ", pstrwhere1).ToList();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new EquipmentViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.EquipmentType_Index == ItemList.EquipmentType_Index)
        //                        {

        //                            resultItem.EquipmentIndex = item.Equipment_Index;
        //                            resultItem.EquipmentId = item.Equipment_Id;
        //                            resultItem.EquipmentName = item.Equipment_Name;

        //                            if (item.EquipmentSubType_Index != null)
        //                            {
        //                                var itemList = context.MS_EquipmentSubType.FromSql("sp_GetEquipmentSubType").Where(c => c.EquipmentSubType_Index == item.EquipmentSubType_Index).FirstOrDefault();
        //                                if (itemList != null)
        //                                {
        //                                    resultItem.EquipmentSubTypeIndex = itemList.EquipmentSubType_Index;
        //                                    resultItem.EquipmentSubTypeName = itemList.EquipmentSubType_Name;
        //                                }
        //                            }

        //                            resultItem.EquipmentTypeIndex = ItemList.EquipmentType_Index;
        //                            resultItem.EquipmentTypeName = ItemList.EquipmentType_Name;
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
        //            if (data.EquipmentId == "" && data.EquipmentName == "" && data.EquipmentSubTypeName == "" && data.EquipmentTypeName == "")
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

        //public List<EquipmentItemViewModel> cartNumber(EquipmentItemViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            //string pstring = " and Pick_Ticket = '" + "'";
        //            string strString = "";
        //            if (data.EquipmentItemName != "" && data.EquipmentItemName != null)
        //            {
        //                strString = " And EquipmentItem_Name = '" + data.EquipmentItemName + "'";
        //            }

        //            var strwhere = new SqlParameter("@strwhere", strString);
        //            var queryResult = context.MS_EquipmentItem.FromSql("sp_GetEquipmentItem @strwhere", strwhere).FirstOrDefault();

        //            var result = new List<EquipmentItemViewModel>();
        //            if (queryResult !=  null)
        //            {
        //                var resultItem = new EquipmentItemViewModel();

        //                resultItem.EquipmentItemIndex = queryResult.EquipmentItem_Index;
        //                resultItem.EquipmentItemId = queryResult.EquipmentItem_Id;
        //                resultItem.EquipmentItemName = queryResult.EquipmentItem_Name;
        //                resultItem.EquipmentItemDesc = queryResult.EquipmentItem_Desc;
        //                if (queryResult.Equipment_Name != null)
        //                {
        //                    string strString1 = "";
        //                    if (queryResult.Equipment_Name != "" && queryResult.Equipment_Name != null)
        //                    {
        //                        strString1 = " And Equipment_Name = '" + queryResult.Equipment_Name + "'";
        //                    }

        //                    var strwhere1 = new SqlParameter("@strwhere1", strString1);
        //                    var itemList = context.MS_Equipment.FromSql("sp_GetEquipment @strwhere1", strwhere1).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.EquipmentIndex = itemList.Equipment_Index;
        //                        resultItem.EquipmentId = itemList.Equipment_Id;
        //                        resultItem.EquipmentName = itemList.Equipment_Name;
        //                    }
        //                }

        //                resultItem.IsActive = queryResult.IsActive;
        //                resultItem.IsDelete = queryResult.IsDelete;
        //                resultItem.IsSystem = queryResult.IsSystem;
        //                resultItem.StatusId = queryResult.Status_Id;
        //                resultItem.CreateDate = queryResult.Create_Date.GetValueOrDefault();
        //                resultItem.CreateBy = queryResult.Create_By;
        //                resultItem.UpdateDate = queryResult.Update_Date.GetValueOrDefault();
        //                resultItem.UpdateBy = queryResult.Update_By;
        //                resultItem.CancelDate = queryResult.Cancel_Date.GetValueOrDefault();
        //                resultItem.CancelBy = queryResult.Cancel_By;

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

        //public List<EquipmentItemViewModel> UpdateCartAssign(EquipmentItemViewModel item)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            item.TagOutPickStatus = 1;
        //            var result = new List<EquipmentItemViewModel>();
        //            var resultItem = new EquipmentItemViewModel();
        //            var TagOutPickNo = new SqlParameter("@TagOutPick_No", item.TagOutPickNo);
        //            var EquipmentIndex = new SqlParameter("@Equipment_Index", item.EquipmentIndex);
        //            var EquipmentId = new SqlParameter("@Equipment_Id", item.EquipmentId);
        //            var EquipmentName = new SqlParameter("@Equipment_Name", item.EquipmentName);
        //            var EquipmentItemIndex = new SqlParameter("@EquipmentItem_Index", item.EquipmentItemIndex);
        //            var EquipmentItemId = new SqlParameter("@EquipmentItem_Id", item.EquipmentItemId);
        //            var EquipmentItemName = new SqlParameter("@EquipmentItem_Name", item.EquipmentItemName);
        //            var TagOutPickStatus = new SqlParameter("@TagOutPick_Status", item.TagOutPickStatus);
        //            var UpdateBy = new SqlParameter("@Update_By", item.UpdateBy);


        //            if (item.UpdateBy == null)
        //            {
        //                UpdateBy.SqlValue = DBNull.Value;
        //            }

        //            context.Database.ExecuteSqlCommand("EXEC sp_Save_CartAssign @TagOutPick_No,@Equipment_Index,@Equipment_Id,@Equipment_Name,@EquipmentItem_Index,@EquipmentItem_Id,@EquipmentItem_Name,@TagOutPick_Status,@Update_By", TagOutPickNo, EquipmentIndex, EquipmentId, EquipmentName, EquipmentItemIndex, EquipmentItemId, EquipmentItemName, TagOutPickStatus, UpdateBy);


        //            if (item.EquipmentName != null)
        //            {
        //                string pwhereFilter = "";
        //                if (item.TagOutPickNo != "" && item.TagOutPickNo != null)
        //                {
        //                    pwhereFilter = " And TagOutPick_No = '" + item.TagOutPickNo + "'";
        //                }

        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var itemList = context.WM_TagOutPick.FromSql("sp_GetTagOutPick @strwhere", strwhere).FirstOrDefault();
        //                if (itemList != null)
        //                {
        //                    item.EquipmentItemIndex = itemList.Equipment_Index;
        //                    item.EquipmentItemId = itemList.Equipment_Id;
        //                    item.EquipmentItemName = itemList.Equipment_Name;
        //                }
        //            }

        //            resultItem.EquipmentName = EquipmentName.Value.ToString();
        //            resultItem.EquipmentItemName = EquipmentItemName.Value.ToString();
        //            resultItem.TagOutPickNo = TagOutPickNo.Value.ToString();
        //            result.Add(resultItem);

        //            return result;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        #region FindEquipment
        public EquipmentViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_Equipment.Where(c => c.Equipment_Index == id).FirstOrDefault();

                var result = new EquipmentViewModel();


                result.equipment_Index = queryResult.Equipment_Index;
                result.equipment_Id = queryResult.Equipment_Id;
                result.equipment_Name = queryResult.Equipment_Name;
                result.equipmentSubType_Index = queryResult.EquipmentSubType_Index;
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

        #region FilterEquipment
        //Filter
        private MasterDataDbContext db;

        public EquipmentService()
        {
            db = new MasterDataDbContext();
        }

        public EquipmentService(MasterDataDbContext db)
        {
            this.db = db;
        }

       
        public actionResultEquipmentViewModel filter(SearchEquipmentViewModel data)
        {
            try
            {
                var query = db.View_Equipment.AsQueryable();
                query = query.Where(c => c.IsDelete == 0);

                var statusModels = new List<int?>();
                var sortModels = new List<SortModel>();

                if (data.status.Count > 0)
                {
                    foreach (var item in data.status)
                    {
                        statusModels.Add(item.value);
                    }
                    query = query.Where(c => statusModels.Contains(c.Equipment_status));
                }


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Equipment_Id.Contains(data.key)
                                        || c.Equipment_Name.Contains(data.key)
                                        || c.EquipmentType_Name.Contains(data.key)
                                        || c.EquipmentSubType_Name.Contains(data.key));


                }
                if (!string.IsNullOrEmpty(data.createdateeq_date) && !string.IsNullOrEmpty(data.createdateeq_date_to))
                {
                    var dateStart = data.createdateeq_date.toBetweenDate();
                    var dateEnd = data.createdateeq_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }

                var Item = new List<View_Equipment>();
                var TotalRow = new List<View_Equipment>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Equipment_Id).ToList();

                var result = new List<SearchEquipmentViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchEquipmentViewModel();
                    string status = null;
                    string active = null;
                    if (item.Equipment_status != null) {
                        if (item.Equipment_status == 0){ status = "Offline / Non"; }
                        else if (item.Equipment_status == 1){ status = "Online"; }
                        else if (item.Equipment_status == 2){ status = "Online + Pick Ready"; }
                        else if (item.Equipment_status == 3){ status = "Online + Drop Ready"; }
                        else if (item.Equipment_status == 4){ status = "Online + Pick Ready + Drop Ready"; }
                    }
                    if (item.IsActive != null) {
                        if (item.IsActive == 1) { active = "Enabled"; } 
                        else if  (item.IsActive == 0) { active = "Disabled"; } 
                    }
                    resultItem.equipment_Id = item.Equipment_Id;
                    resultItem.equipment_Index = item.Equipment_Index;
                    resultItem.equipment_Name = item.Equipment_Name;
                    resultItem.equipment_status = status;
                    resultItem.equipmentSubType_Index = item.EquipmentSubType_Index;
                    resultItem.equipmentSubType_Name = item.EquipmentSubType_Name;
                    resultItem.equipmentType_Index = item.EquipmentType_Index;
                    resultItem.equipmentType_Name = item.EquipmentType_Name;
                    resultItem.isActive = active;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultEquipmentViewModel = new actionResultEquipmentViewModel();
                actionResultEquipmentViewModel.itemsEquipment = result.ToList();
                actionResultEquipmentViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultEquipmentViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetDelete

        public Boolean getDelete(EquipmentViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var equipment = db.MS_Equipment.Find(data.equipment_Index);

                if (equipment != null)
                {
                    equipment.IsActive = 0;
                    equipment.IsDelete = 1;


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
                        olog.logging("DeleteEquipment", msglog);
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
        
        #region SaveChangesEquipment

        public String SaveChanges(EquipmentViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var EquipmentOld = db.MS_Equipment.Find(data.equipment_Index);

                if (EquipmentOld == null)
                {
                    if (!string.IsNullOrEmpty(data.equipment_Id))
                    {
                        var query = db.MS_Equipment.FirstOrDefault(c => c.Equipment_Id == data.equipment_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.equipment_Id))
                    {
                        data.equipment_Id = "Equipment_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_Equipment.FirstOrDefault(c => c.Equipment_Id == data.equipment_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Equipment_Id == data.equipment_Id)
                                {
                                    data.equipment_Id = "Equipment_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //var Sys_Key = new SqlParameter("Sys_Key", "UserId");
                    //var resultParameter = new SqlParameter("@result", SqlDbType.Int);
                    //resultParameter.Size = 2000; // some meaningfull value
                    //resultParameter.Direction = ParameterDirection.Output;
                    //db.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);

                    //data.equipmentSubType_Id = resultParameter.Value.ToString();

                    //data.equipment_Id = "Equipment_Id".genAutonumber();

                    MS_Equipment Model = new MS_Equipment();

                    Model.Equipment_Index = Guid.NewGuid();
                    Model.Equipment_Id = data.equipment_Id;
                    Model.Equipment_Name = data.equipment_Name;
                    Model.EquipmentSubType_Index = data.equipmentSubType_Index;
                    Model.EquipmentType_Index = data.equipmentType_Index;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_Equipment.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.equipment_Id))
                    {
                        if (EquipmentOld.Equipment_Id != "")
                        {
                            data.equipment_Id = EquipmentOld.Equipment_Id;
                        }
                    }
                    else
                    {
                        if (EquipmentOld.Equipment_Id != data.equipment_Id)
                        {
                            var query = db.MS_Equipment.FirstOrDefault(c => c.Equipment_Id == data.equipment_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.equipment_Id = EquipmentOld.Equipment_Id;
                        }
                    }
                    EquipmentOld.Equipment_Id = data.equipment_Id;
                    EquipmentOld.EquipmentSubType_Index = data.equipmentSubType_Index;
                    EquipmentOld.EquipmentType_Index = data.equipmentType_Index;
                    EquipmentOld.Equipment_Name = data.equipment_Name;
                    EquipmentOld.IsActive = Convert.ToInt32(data.isActive);
                    EquipmentOld.Update_By = data.create_By;
                    EquipmentOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveEquipment", msglog);
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

        #region SearchEquipment

        public List<ItemListViewModel>autoEquipment(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Equipment.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Equipment_Id.Contains(data.key)
                                                || c.Equipment_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Equipment_Name, c.Equipment_Index, c.Equipment_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.Equipment_Index,
                            id = item.Equipment_Id,
                            name = item.Equipment_Id + " - " + item.Equipment_Name,
                            key = item.Equipment_Id + " - " + item.Equipment_Name,
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

        #region SearchEquipmentFilter

        public List<ItemListViewModel>AutoSearchEquipmentFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_Equipment.Where(c => c.Equipment_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Equipment_Name,
                        key = s.Equipment_Name
                    }).Distinct();

                    var query2 = db.MS_Equipment.Where(c => c.Equipment_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Equipment_Id,
                        key = s.Equipment_Id
                    }).Distinct();

                    var query3 = db.MS_EquipmentType.Where(c => c.EquipmentType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.EquipmentType_Name,
                        key = s.EquipmentType_Name
                    }).Distinct();

                    var query4 = db.MS_EquipmentSubType.Where(c => c.EquipmentSubType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.EquipmentSubType_Name,
                        key = s.EquipmentSubType_Name
                    }).Distinct();
                    var query = query1.Union(query2).Union(query2).Union(query3).Union(query4);

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

        #region Export Excel
        public actionResultEquipmentViewModel ExportExcel(SearchEquipmentViewModel data)
        {
            try
            {
                var query = db.View_Equipment.AsQueryable();
                query = query.Where(c => c.IsDelete == 0);

                var statusModels = new List<int?>();
                var sortModels = new List<SortModel>();

                if (data.status.Count > 0)
                {
                    foreach (var item in data.status)
                    {
                        statusModels.Add(item.value);
                    }
                    query = query.Where(c => statusModels.Contains(c.Equipment_status));
                }


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Equipment_Id.Contains(data.key)
                                        || c.Equipment_Name.Contains(data.key)
                                        || c.EquipmentType_Name.Contains(data.key)
                                        || c.EquipmentSubType_Name.Contains(data.key));


                }
                if (!string.IsNullOrEmpty(data.createdateeq_date) && !string.IsNullOrEmpty(data.createdateeq_date_to))
                {
                    var dateStart = data.createdateeq_date.toBetweenDate();
                    var dateEnd = data.createdateeq_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }

                var Item = new List<View_Equipment>();
                var TotalRow = new List<View_Equipment>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.Equipment_Id).ToList();

                var result = new List<SearchEquipmentViewModel>();
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new SearchEquipmentViewModel();
                    string status = null;
                    string active = null;
                    if (item.Equipment_status != null)
                    {
                        if (item.Equipment_status == 0) { status = "Offline / Non"; }
                        else if (item.Equipment_status == 1) { status = "Online"; }
                        else if (item.Equipment_status == 2) { status = "Online + Pick Ready"; }
                        else if (item.Equipment_status == 3) { status = "Online + Drop Ready"; }
                        else if (item.Equipment_status == 4) { status = "Online + Pick Ready + Drop Ready"; }
                    }
                    if (item.IsActive != null)
                    {
                        if (item.IsActive == 1) { active = "Enabled"; }
                        else if (item.IsActive == 0) { active = "Disabled"; }
                    }
                    resultItem.numOf = num + 1;
                    resultItem.equipment_Id = item.Equipment_Id;
                    resultItem.equipment_Index = item.Equipment_Index;
                    resultItem.equipment_Name = item.Equipment_Name;
                    resultItem.equipment_status = status;
                    resultItem.equipmentSubType_Index = item.EquipmentSubType_Index;
                    resultItem.equipmentSubType_Name = item.EquipmentSubType_Name;
                    resultItem.equipmentType_Index = item.EquipmentType_Index;
                    resultItem.equipmentType_Name = item.EquipmentType_Name;
                    resultItem.isActive = active;
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var actionResultEquipmentViewModel = new actionResultEquipmentViewModel();
                actionResultEquipmentViewModel.itemsEquipment = result.ToList();
                actionResultEquipmentViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultEquipmentViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

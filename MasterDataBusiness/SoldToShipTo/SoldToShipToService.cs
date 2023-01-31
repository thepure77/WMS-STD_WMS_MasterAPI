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
using static MasterDataBusiness.ViewModels.SearchSoldToShipToViewModel;

namespace MasterDataBusiness
{
    public class SoldToShipToService
    {

        private MasterDataDbContext db;

        public SoldToShipToService()
        {
            db = new MasterDataDbContext();
        }

        public SoldToShipToService(MasterDataDbContext db)
        {
            this.db = db;
        }
        #region BeforeCodeSoldToShipTo
        //public List<SoldToShipToViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_SoldToShipTo.FromSql("sp_GetSoldToShipTo").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            //******* Count Rows ******//
        //            var strwhere1 = new SqlParameter("@strwhere", DBNull.Value);
        //            // var strwhere = new SqlParameter("@strwhere", DBNull.Value);
        //            var PageNumber1 = new SqlParameter("@PageNumber", 1);
        //            var RowspPage1 = new SqlParameter("@RowspPage", 10000);
        //            var queryResultTotal = context.MS_SoldToShipTo.FromSql("sp_GetSoldToShipTo @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
        //            var count = queryResultTotal.Count();

        //            var result = new List<SoldToShipToViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new SoldToShipToViewModel();

        //                resultItem.SoldToShipToIndex = item.SoldToShipTo_Index;
        //                resultItem.SoldToShipToId = item.SoldToShipTo_Id;
        //                if (item.SoldTo_Index != null)
        //                {
        //                    var itemList = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.SoldTo_Index == item.SoldTo_Index).FirstOrDefault();
        //                    resultItem.SoldToIndex = itemList.SoldTo_Index;
        //                    resultItem.SoldToName = itemList.SoldTo_Name;
        //                }
        //                if (item.ShipTo_Index != null)
        //                {
        //                    var itemList = context.MS_ShipTo.FromSql("sp_GetShipTo").Where(c => c.ShipTo_Index == item.ShipTo_Index).FirstOrDefault();
        //                    resultItem.ShipToIndex = itemList.ShipTo_Index;
        //                    resultItem.ShipToName = itemList.ShipTo_Name;
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

        //                resultItem.count = count;
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
        //public actionResultSoldToShipToViewModel FilterSoldtoShipTo(SoldToShipToViewModel model)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())

        //        {
        //            var strwhere1 = new SqlParameter("@strwhere", "");                 
        //            var PageNumber1 = new SqlParameter("@PageNumber", 1);
        //            var RowspPage1 = new SqlParameter("@RowspPage", 10000);

        //            var queryResultTotal = context.MS_SoldToShipTo.FromSql("sp_GetSoldToShipTo @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
        //            var strwhere = new SqlParameter("@strwhere", "");
        //            // var strwhere = new SqlParameter("@strwhere", DBNull.Value);
        //            var PageNumber = new SqlParameter("@PageNumber", model.CurrentPage);
        //            var RowspPage = new SqlParameter("@RowspPage", model.PerPage);

        //            var queryResult = context.MS_SoldToShipTo.FromSql("sp_GetSoldToShipTo @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber, RowspPage).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<SoldToShipToViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new SoldToShipToViewModel();
        //                resultItem.SoldToShipToIndex = item.SoldToShipTo_Index;
        //                resultItem.SoldToShipToId = item.SoldToShipTo_Id;
        //                if (item.SoldTo_Index != null)
        //                {
        //                    var itemList = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.SoldTo_Index == item.SoldTo_Index).FirstOrDefault();
        //                    resultItem.SoldToIndex = itemList.SoldTo_Index;
        //                    resultItem.SoldToName = itemList.SoldTo_Name;
        //                }
        //                if (item.ShipTo_Index != null)
        //                {
        //                    var itemList = context.MS_ShipTo.FromSql("sp_GetShipTo").Where(c => c.ShipTo_Index == item.ShipTo_Index).FirstOrDefault();
        //                    resultItem.ShipToIndex = itemList.ShipTo_Index;
        //                    resultItem.ShipToName = itemList.ShipTo_Name;
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

        //            var count = queryResultTotal.Count;
        //            var actionResultSoldToShipTo = new actionResultSoldToShipToViewModel();
        //            actionResultSoldToShipTo.itemsSoldToShipTo = result.ToList();
        //            actionResultSoldToShipTo.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage };

        //            return actionResultSoldToShipTo;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<SoldToShipToViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            string pstring = " and SoldToShipTo_Index ='" + id + "'";
        //            var result = new List<SoldToShipToViewModel>();

        //            //****************** Check data ว่ามี Index ของ Province หรือไม่ ***************************//
        //            var strwhere1 = new SqlParameter("@strwhere", DBNull.Value);
        //            var PageNumber1 = new SqlParameter("@PageNumber", 1);
        //            var RowspPage1 = new SqlParameter("@RowspPage", 10000);
        //            var queryResultTotal = context.MS_SoldToShipTo.FromSql("sp_GetSoldToShipTo @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
        //            //var queryResult = context.MS_SoldToShipTo.FromSql("sp_GetSoldToShipTo").Where(c => c.SoldToShipTo_Index == id).ToList();
        //            if (queryResultTotal.Count > 0)
        //            {
        //                var findItem = queryResultTotal.Where(c => c.SoldToShipTo_Index == id).ToList();
        //                var count = queryResultTotal.Count;
        //                foreach (var item in findItem)
        //                {
        //                    var resultItem = new SoldToShipToViewModel();
        //                    resultItem.SoldToShipToIndex = item.SoldToShipTo_Index;
        //                    resultItem.SoldToShipToId = item.SoldToShipTo_Id;
        //                    if (item.SoldTo_Index != null)
        //                    {
        //                        var itemList = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.SoldTo_Index == item.SoldTo_Index).FirstOrDefault();
        //                        resultItem.SoldToIndex = itemList.SoldTo_Index;
        //                        resultItem.SoldToName = itemList.SoldTo_Name;
        //                    }
        //                    if (item.ShipTo_Index != null)
        //                    {
        //                        var itemList = context.MS_ShipTo.FromSql("sp_GetShipTo").Where(c => c.ShipTo_Index == item.ShipTo_Index).FirstOrDefault();
        //                        resultItem.ShipToIndex = itemList.ShipTo_Index;
        //                        resultItem.ShipToName = itemList.ShipTo_Name;
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
        //            else
        //            {
        //                var queryResult = context.MS_SoldToShipTo.FromSql("sp_GetSoldToShipTo{0}", pstring).ToList();
        //                queryResult.Where(c => c.SoldToShipTo_Index == id);

        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new SoldToShipToViewModel();
        //                    resultItem.SoldToShipToIndex = item.SoldToShipTo_Index;
        //                    resultItem.SoldToShipToId = item.SoldToShipTo_Id;
        //                    if (item.SoldTo_Index != null)
        //                    {
        //                        var itemList = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.SoldTo_Index == item.SoldTo_Index).FirstOrDefault();
        //                        resultItem.SoldToIndex = itemList.SoldTo_Index;
        //                        resultItem.SoldToName = itemList.SoldTo_Name;
        //                    }
        //                    if (item.ShipTo_Index != null)
        //                    {
        //                        var itemList = context.MS_ShipTo.FromSql("sp_GetShipTo").Where(c => c.ShipTo_Index == item.ShipTo_Index).FirstOrDefault();
        //                        resultItem.ShipToIndex = itemList.ShipTo_Index;
        //                        resultItem.ShipToName = itemList.ShipTo_Name;
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


        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public String SaveChanges(SoldToShipToViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.SoldToShipToIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.SoldToShipToIndex = Guid.NewGuid();
        //            }
        //            if (data.SoldToShipToId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "SoldToShipToId");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.SoldToShipToId = resultParameter.Value.ToString();
        //            }
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int isSystem = 0;
        //            int statusId = 0;
        //            var SoldToShipTo_Index = new SqlParameter("SoldToShipTo_Index", data.SoldToShipToIndex);
        //            var SoldToShipTo_Id = new SqlParameter("SoldToShipTo_Id", data.SoldToShipToId);
        //            var SoldTo_Index = new SqlParameter("SoldTo_Index", data.SoldToIndex);
        //            var ShipTo_Index = new SqlParameter("ShipTo_Index", data.ShipToIndex);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_SoldToShipTo  @SoldToShipTo_Index,@SoldToShipTo_Id,@SoldTo_Index,@ShipTo_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", SoldToShipTo_Index, SoldToShipTo_Id, SoldTo_Index, ShipTo_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<SoldToShipToViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_SoldToShipTo.FromSql("sp_GetSoldToShipTo").Where(c => c.SoldToShipTo_Index == id).ToList();
        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<SoldToShipToViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var SoldToShipTo_Index = new SqlParameter("SoldToShipTo_Index", item.SoldToShipTo_Index);
        //                var SoldToShipTo_Id = new SqlParameter("SoldToShipTo_Index", item.SoldToShipTo_Id);
        //                var SoldTo_Index = new SqlParameter("SoldTo_Index", item.SoldTo_Index);
        //                var ShipTo_Index = new SqlParameter("ShipTo_Index", item.ShipTo_Index);
        //                var IsActive = new SqlParameter("IsActive", isactive);
        //                var IsDelete = new SqlParameter("IsDelete", isdelete);
        //                var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
        //                var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
        //                var Create_By = new SqlParameter("Create_By", item.Create_By);
        //                var Create_Date = new SqlParameter("Create_Date", item.Create_Date);
        //                var Update_By = new SqlParameter("Update_By", item.Update_By);
        //                var Update_Date = new SqlParameter("Update_Date", item.Update_Date);
        //                var Cancel_By = new SqlParameter("Cancel_By", item.Cancel_By);
        //                var Cancel_Date = new SqlParameter("Cancel_Date", item.Cancel_Date);
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_SoldToShipTo  @SoldToShipTo_Index,@SoldToShipTo_Id,@SoldTo_Index,@ShipTo_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", SoldToShipTo_Index, SoldToShipTo_Id, SoldTo_Index, ShipTo_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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
        //public List<SoldToShipToViewModel> search(SoldToShipToViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhere = "";
        //            string pwhereLike = "";
        //            var queryResult = context.MS_SoldToShipTo.FromSql("sp_GetSoldToShipTo").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
        //            var result = new List<SoldToShipToViewModel>();

        //            if (data.SoldToShipToId != null && data.SoldToShipToId != "")
        //            {
        //                pwhere = " And SoldToShipTo_Id like N'%" + data.SoldToShipToId + "%'";
        //            }
        //            else
        //            {
        //                pwhere = " ";
        //            }

        //            if (data.SoldToShipToId != null && data.SoldToShipToId != "")
        //            {
        //                pwhere += " And isActive = '" + 1 + "'";
        //                pwhere += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhere);
        //                var query = context.MS_SoldToShipTo.FromSql("sp_GetSoldToShipTo @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new SoldToShipToViewModel();

        //                    resultItem.SoldToShipToIndex = item.SoldToShipTo_Index;
        //                    resultItem.SoldToShipToId = item.SoldToShipTo_Id;
        //                    if (item.SoldTo_Index != null)
        //                    {
        //                        var itemList = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.SoldTo_Index == item.SoldTo_Index).FirstOrDefault();
        //                        resultItem.SoldToIndex = itemList.SoldTo_Index;
        //                        resultItem.SoldToName = itemList.SoldTo_Name;
        //                    }
        //                    if (item.ShipTo_Index != null)
        //                    {
        //                        var itemList = context.MS_ShipTo.FromSql("sp_GetShipTo").Where(c => c.ShipTo_Index == item.ShipTo_Index).FirstOrDefault();
        //                        resultItem.ShipToIndex = itemList.ShipTo_Index;
        //                        resultItem.ShipToName = itemList.ShipTo_Name;
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
        //                    var resultItem = new SoldToShipToViewModel();

        //                    foreach (var soldTo in dataList)
        //                    {
        //                        if (item.SoldTo_Index == soldTo.SoldTo_Index)
        //                        {
        //                            resultItem.SoldToShipToIndex = item.SoldToShipTo_Index;
        //                            resultItem.SoldToShipToId = item.SoldToShipTo_Id;
        //                            if (item.SoldTo_Index != null)
        //                            {
        //                                //var itemList = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.SoldTo_Index == item.SoldTo_Index).FirstOrDefault();
        //                                resultItem.SoldToIndex = soldTo.SoldTo_Index;
        //                                resultItem.SoldToName = soldTo.SoldTo_Name;
        //                            }
        //                            if (item.ShipTo_Index != null)
        //                            {
        //                                var itemList = context.MS_ShipTo.FromSql("sp_GetShipTo").Where(c => c.ShipTo_Index == item.ShipTo_Index).FirstOrDefault();
        //                                resultItem.ShipToIndex = itemList.ShipTo_Index;
        //                                resultItem.ShipToName = itemList.ShipTo_Name;
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
        //            else if (data.ShipToName != "" && data.ShipToName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And ShipTo_Name like N'%" + data.ShipToName + "%'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_ShipTo.FromSql("sp_GetShipTo @strwhere ", strwhere).ToList();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new SoldToShipToViewModel();
        //                    foreach (var soldToSt in dataList)
        //                    {
        //                        if (item.ShipTo_Index == soldToSt.ShipTo_Index)
        //                        {
        //                            resultItem.SoldToShipToIndex = item.SoldToShipTo_Index;
        //                            resultItem.SoldToShipToId = item.SoldToShipTo_Id;
        //                            if (item.SoldTo_Index != null)
        //                            {
        //                                var itemList = context.MS_SoldTo.FromSql("sp_GetSoldTo").Where(c => c.SoldTo_Index == item.SoldTo_Index).FirstOrDefault();
        //                                resultItem.SoldToIndex = itemList.SoldTo_Index;
        //                                resultItem.SoldToName = itemList.SoldTo_Name;
        //                            }
        //                            if (item.ShipTo_Index != null)
        //                            {
        //                                //var itemList = context.MS_ShipTo.FromSql("sp_GetShipTo").Where(c => c.ShipTo_Index == item.ShipTo_Index).FirstOrDefault();
        //                                resultItem.ShipToIndex = soldToSt.ShipTo_Index;
        //                                resultItem.ShipToName = soldToSt.ShipTo_Name;
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

        //            if (data.SoldToShipToId == "" && data.SoldToName == "" && data.ShipToName == "")
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

        #region FindSoldToShipTo

        public SoldToShipToViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_SoldToShipTo.Where(c => c.SoldToShipTo_Index == id).FirstOrDefault();

                var result = new SoldToShipToViewModel();


                result.soldToShipTo_Index = queryResult.SoldToShipTo_Index;
                result.soldToShipTo_Id = queryResult.SoldToShipTo_Id;
                result.soldTo_Index = queryResult.SoldTo_Index;
                result.soldTo_Name = queryResult.SoldTo_Name;
                result.shipTo_Index = queryResult.ShipTo_Index;
                result.shipTo_Name = queryResult.ShipTo_Name;
                result.isActive = queryResult.IsActive;


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        
        #region FilterSoldToShipTo
        public actionResultSoldToShipToViewModel filter(SearchSoldToShipToViewModel data)
        {
            try
            {
                var query = db.View_SoldToShipTo.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.SoldToShipTo_Id.Contains(data.key)
                                        || c.SoldTo_Name.Contains(data.key)
                                        || c.ShipTo_Name.Contains(data.key));


                }

                var Item = new List<View_SoldToShipTo>();
                var TotalRow = new List<View_SoldToShipTo>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.SoldToShipTo_Id).ToList();

                var result = new List<SearchSoldToShipToViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchSoldToShipToViewModel();

                    resultItem.soldToShipTo_Index = item.SoldToShipTo_Index;
                    resultItem.soldToShipTo_Id = item.SoldToShipTo_Id;
                    resultItem.soldTo_Index = item.SoldTo_Index;
                    resultItem.soldTo_Name = item.SoldTo_Name;
                    resultItem.shipTo_Index = item.ShipTo_Index;
                    resultItem.shipTo_Name = item.ShipTo_Name;
                 

                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultSoldToShipToViewModel = new actionResultSoldToShipToViewModel();
                actionResultSoldToShipToViewModel.itemsSoldToShipTo = result.ToList();
                actionResultSoldToShipToViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultSoldToShipToViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDelete
        public Boolean getDelete(SoldToShipToViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var soldToShipTo = db.MS_SoldToShipTo.Find(data.soldToShipTo_Index);

                if (soldToShipTo != null)
                {
                    soldToShipTo.IsActive = 0;
                    soldToShipTo.IsDelete = 1;


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
                        olog.logging("DeleteSoldToShipTo", msglog);
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

        #region SaveChanges

        public String SaveChanges(SoldToShipToViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var SoldToShipToOld = db.MS_SoldToShipTo.Find(data.soldToShipTo_Index);

                if (SoldToShipToOld == null)
                {
                    if (!string.IsNullOrEmpty(data.soldToShipTo_Id))
                    {
                        var query = db.MS_SoldToShipTo.FirstOrDefault(c => c.SoldToShipTo_Id == data.soldToShipTo_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.soldToShipTo_Id))
                    {
                        data.soldToShipTo_Id = "SoldToShipTo_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_SoldToShipTo.FirstOrDefault(c => c.SoldToShipTo_Id == data.soldToShipTo_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.SoldToShipTo_Id == data.soldToShipTo_Id)
                                {
                                    data.soldToShipTo_Id = "SoldToShipTo_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.soldToShipTo_Id = "SoldToShipTo_Id".genAutonumber();

                    MS_SoldToShipTo Model = new MS_SoldToShipTo();

                    Model.SoldToShipTo_Index = Guid.NewGuid();
                    Model.SoldToShipTo_Id = data.soldToShipTo_Id;
                    Model.SoldTo_Index = data.soldTo_Index;
                    Model.ShipTo_Index = data.shipTo_Index;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_SoldToShipTo.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.soldToShipTo_Id))
                    {
                        if (SoldToShipToOld.SoldToShipTo_Id != "")
                        {
                            data.soldToShipTo_Id = SoldToShipToOld.SoldToShipTo_Id;
                        }
                    }
                    else
                    {
                        if (SoldToShipToOld.SoldToShipTo_Id != data.soldToShipTo_Id)
                        {
                            var query = db.MS_SoldToShipTo.FirstOrDefault(c => c.SoldToShipTo_Id == data.soldToShipTo_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.soldToShipTo_Id = SoldToShipToOld.SoldToShipTo_Id;
                        }
                    }
                    SoldToShipToOld.SoldToShipTo_Id = data.soldToShipTo_Id;
                    SoldToShipToOld.SoldTo_Index = data.soldTo_Index;
                    SoldToShipToOld.ShipTo_Index = data.shipTo_Index;
                    SoldToShipToOld.IsActive = Convert.ToInt32(data.isActive);
                    SoldToShipToOld.Update_By = data.update_By;
                    SoldToShipToOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveSoldToShipTo", msglog);
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

        #region AutoSearchSoldToShipToFilter

        public List<ItemListViewModel>autoSearchSoldToShipToFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_SoldToShipTo.Where(c => c.SoldToShipTo_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.SoldToShipTo_Id,
                        key = s.SoldToShipTo_Id
                    }).Distinct();

                    var query2 = db.View_SoldToShipTo.Where(c => c.SoldTo_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.SoldTo_Name,
                        key = s.SoldTo_Name
                    }).Distinct();

                    var query3 = db.View_SoldToShipTo.Where(c => c.ShipTo_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ShipTo_Name,
                        key = s.ShipTo_Name
                    }).Distinct();



                    var query = query1.Union(query2).Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {

            }

            return items;
        }
        #endregion

        #region SaveSoldToShipToList

        public String SaveSoldToShipToList(SoldToShipToViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                foreach (var item in data.listSoldToShipToViewModel)
                {
                    MS_SoldToShipTo Model = new MS_SoldToShipTo();

                    Model.SoldToShipTo_Index = Guid.NewGuid();

                    data.soldToShipTo_Id = "SoldToShipTo_Id".genAutonumber();
                    int i = 1;
                    while (i > 0)
                    {
                        var query = db.MS_SoldToShipTo.FirstOrDefault(c => c.SoldToShipTo_Id == data.soldToShipTo_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            if (query.SoldToShipTo_Id == data.soldToShipTo_Id)
                            {
                                data.soldToShipTo_Id = "SoldToShipTo_Id".genAutonumber();
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    Model.SoldToShipTo_Id = data.soldToShipTo_Id;
                    Model.SoldTo_Index = data.soldTo_Index;
                    Model.ShipTo_Index = item.shipTo_Index;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_SoldToShipTo.Add(Model);
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
                    olog.logging("SaveSoldToShipTo", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return "Done"; 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region filterShipTo
        public actionResultSoldToShipToViewModel filterShipTo(SearchSoldToShipToViewModel data)
        {
            try
            {
                var query = db.View_SoldToShipTo.AsQueryable();
                query = query.Where(c =>c.SoldTo_Index == data.soldTo_Index && c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                var Item = new List<View_SoldToShipTo>();
                var TotalRow = new List<View_SoldToShipTo>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.SoldToShipTo_Id).ToList();

                var result = new List<SearchSoldToShipToViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchSoldToShipToViewModel();

                    resultItem.soldToShipTo_Index = item.SoldToShipTo_Index;
                    resultItem.soldToShipTo_Id = item.SoldToShipTo_Id;
                    resultItem.soldTo_Index = item.SoldTo_Index;
                    resultItem.soldTo_Id = item.SoldTo_Id;
                    resultItem.soldTo_Name = item.SoldTo_Name;
                    resultItem.shipTo_Index = item.ShipTo_Index;
                    resultItem.shipTo_Id = item.ShipTo_Id;
                    resultItem.shipTo_Name = item.ShipTo_Name;


                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultSoldToShipToViewModel = new actionResultSoldToShipToViewModel();
                actionResultSoldToShipToViewModel.itemsSoldToShipTo = result.ToList();
                actionResultSoldToShipToViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultSoldToShipToViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

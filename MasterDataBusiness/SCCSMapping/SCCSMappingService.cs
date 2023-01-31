using Comone.Utils;
using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MasterDataBusiness
{
    public class SCCSMappingService
    {
        private TransportDbContext db;

        public SCCSMappingService()
        {
            db = new TransportDbContext();
        }

        public SCCSMappingService(TransportDbContext db)
        {
            this.db = db;
        }
        #region BeforeCode
        //public List<UserGroupViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_UserGroup.FromSql("sp_GetUserGroup").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<UserGroupViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new UserGroupViewModel();

        //                resultItem.UserGroupIndex = item.UserGroup_Index;
        //                resultItem.UserGroupId = item.UserGroup_Id;
        //                resultItem.UserGroupName = item.UserGroup_Name;
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
        //public String SaveChanges(UserGroupViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.UserGroupIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.UserGroupIndex = Guid.NewGuid();
        //            }
        //            if (data.UserGroupId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "UserGroupId");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.UserGroupId = resultParameter.Value.ToString();
        //            }
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int isSystem = 0;
        //            int statusId = 0;
        //            var UserGroup_Index = new SqlParameter("UserGroup_Index", data.UserGroupIndex);
        //            var UserGroup_Id = new SqlParameter("UserGroup_Id", data.UserGroupId);
        //            var UserGroup_Name = new SqlParameter("UserGroup_Name", data.UserGroupName);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_UserGroup  @UserGroup_Index,@UserGroup_Id,@UserGroup_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", UserGroup_Index, UserGroup_Id, UserGroup_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<UserGroupViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_UserGroup.FromSql("sp_GetUserGroup").Where(c => c.UserGroup_Index == id).ToList();

        //            var result = new List<UserGroupViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new UserGroupViewModel();

        //                resultItem.UserGroupIndex = item.UserGroup_Index;
        //                resultItem.UserGroupId = item.UserGroup_Id;
        //                resultItem.UserGroupName = item.UserGroup_Name;
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
        ////public List<UserGroupViewModel> getDelete(Guid id)
        ////{
        ////    try
        ////    {
        ////        using (var context = new MasterDataDbContext())
        ////        {
        ////            var queryResult = context.MS_UserGroup.FromSql("sp_GetUser").Where(c => c.UserGroup_Index == id).ToList();
        ////            var result = new List<UserGroupViewModel>();
        ////            foreach (var item in queryResult)
        ////            {
        ////                int isactive = 0;
        ////                int isdelete = 1;
        ////                var UserGroup_Index = new SqlParameter("UserGroup_Index", item.UserGroup_Index);
        ////                var UserGroup_Id = new SqlParameter("UserGroup_Id", item.UserGroup_Id);
        ////                var UserGroup_Name = new SqlParameter("UserGroup_Name", item.UserGroup_Name);
        ////                var IsActive = new SqlParameter("IsActive", isactive);
        ////                var IsDelete = new SqlParameter("IsDelete", isdelete);
        ////                var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
        ////                var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
        ////                var Create_By = new SqlParameter("Create_By", "");
        ////                var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        ////                var Update_By = new SqlParameter("Update_By", "");
        ////                var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        ////                var Cancel_By = new SqlParameter("Cancel_By", "");
        ////                var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        ////                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_UserGroup  @UserGroup_Index,@UserGroup_Id,@UserGroup_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", UserGroup_Index, UserGroup_Id, UserGroup_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        ////                context.SaveChanges();
        ////            }

        ////            return result;
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}

        //#region getDelete
        //public Boolean getDelete(UserGroupViewModel data)
        //{
        //    String State = "Start";
        //    String msglog = "";
        //    var olog = new logtxt();

        //    try
        //    {
        //        var UserGroup = db.MS_UserGroup.Find(data.UserGroupIndex);

        //        if (UserGroup != null)
        //        {
        //            UserGroup.IsActive = 0;
        //            UserGroup.IsDelete = 1;


        //            var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
        //            try
        //            {
        //                db.SaveChanges();
        //                transaction.Commit();
        //                return true;
        //            }

        //            catch (Exception exy)
        //            {
        //                msglog = State + " ex Rollback " + exy.Message.ToString();
        //                olog.logging("DeleteUserGroup", msglog);
        //                transaction.Rollback();
        //                throw exy;
        //            }

        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion

        //public List<UserGroupViewModel> search(UserGroupViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<UserGroupViewModel>();
        //            if (data.UserGroupId != "" && data.UserGroupId != null)
        //            {
        //                pwhereFilter = " And UserGroup_Id like N'%" + data.UserGroupId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter = "";
        //            }

        //            if (data.UserGroupName != "" && data.UserGroupName != null)
        //            {
        //                pwhereFilter += " And UserGroup_Name like N'%" + data.UserGroupName + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }


        //            if (data.UserGroupId != "" && data.UserGroupId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_UserGroup.FromSql("sp_GetUserGroup @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new UserGroupViewModel();
        //                    resultItem.UserGroupIndex = item.UserGroup_Index;
        //                    resultItem.UserGroupId = item.UserGroup_Id;
        //                    resultItem.UserGroupName = item.UserGroup_Name;
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
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_UserGroup.FromSql("sp_GetUserGroup @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new UserGroupViewModel();
        //                    resultItem.UserGroupIndex = item.UserGroup_Index;
        //                    resultItem.UserGroupId = item.UserGroup_Id;
        //                    resultItem.UserGroupName = item.UserGroup_Name;
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

        //            if (data.UserGroupId == "" && data.UserGroupName == "")
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

        #region filterSCCSMapping
        public actionResultSCCSMappingViewModel filter(SearchSCCSMappingViewModel data)
        {
            try
            {
                var query = db.ms_SCCS_Mapping.AsQueryable();
                //query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.costCenter_Id))
                {
                    query = query.Where(c => c.CostCenter_Id.Contains(data.costCenter_Id)
                                        || c.ProfitCenter.Contains(data.costCenter_Id));  
                                        
                }

                if(!string.IsNullOrEmpty(data.costCenter_Description))
                {
                    query = query.Where(c => c.CostCenter_Description.Contains(data.costCenter_Description));
                }

                if (!string.IsNullOrEmpty(data.profitCenter_Description))
                {
                    query = query.Where(c => c.ProfitCenter_Description.Contains(data.profitCenter_Description));
                }

                if (!string.IsNullOrEmpty(data.shortText_Service_line))
                {
                    query = query.Where(c => c.ShortText_Service_line.Contains(data.shortText_Service_line));
                }
                if (!string.IsNullOrEmpty(data.createdate_date) && !string.IsNullOrEmpty(data.createdate_date_to))
                {
                    var dateStart = data.createdate_date.toBetweenDate();
                    var dateEnd = data.createdate_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }

                var Item = new List<ms_SCCS_Mapping>();
                var TotalRow = new List<ms_SCCS_Mapping>();

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
                Item = Item.OrderBy(o => o.SCCS_Mapping_Id).ToList();
                var result = new List<SearchSCCSMappingViewModel>();

                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new SearchSCCSMappingViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.sCCS_Mapping_Id = item.SCCS_Mapping_Id;
                    resultItem.plant = item.Plant;
                    resultItem.costCenter_Id = item.CostCenter_Id;
                    resultItem.costCenter_Description = item.CostCenter_Description;
                    resultItem.profitCenter = item.ProfitCenter;
                    resultItem.profitCenter_Description = item.ProfitCenter_Description;
                    resultItem.customerGroup = item.CustomerGroup;
                    resultItem.shipto_Id = item.Shipto_Id;
                    resultItem.remark = item.Remark;
                    resultItem.aCC_CAT = item.ACC_CAT;
                    resultItem.itemCat = item.ItemCat;
                    resultItem.gL_ACC = item.GL_ACC;
                    resultItem.iO_Code = item.IO_Code;
                    resultItem.create_Date = item.Create_Date;
                    resultItem.version_Id = item.Version_Id;
                    resultItem.shortText_PO_item = item.ShortText_PO_item;
                    resultItem.shortText_Service_line = item.ShortText_Service_line;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultSCCSMappingViewModel = new actionResultSCCSMappingViewModel();
                actionResultSCCSMappingViewModel.itemsSCCSMapping = result.ToList();
                actionResultSCCSMappingViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultSCCSMappingViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



       /* #region SaveChanges
        public String SaveChanges(UserGroupViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var UserGroupOld = db.MS_UserGroup.Find(data.userGroup_Index);

                if (UserGroupOld == null)
                {

                    data.userGroup_Id = "UserGroup_Id".genAutonumber();

                    MS_UserGroup Model = new MS_UserGroup();


                    Model.UserGroup_Index = Guid.NewGuid();
                    Model.UserGroup_Id = data.userGroup_Id;
                    Model.UserGroup_Name = data.userGroup_Name;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_UserGroup.Add(Model);
                }
                else
                {
                    UserGroupOld.UserGroup_Name = data.userGroup_Name;
                    UserGroupOld.IsActive = Convert.ToInt32(data.isActive);
                    UserGroupOld.Update_By = data.create_By;
                    UserGroupOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveUserGroup", msglog);
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
        #endregion*/

        /*#region find
        public UserGroupViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_UserGroup.Where(c => c.UserGroup_Index == id).FirstOrDefault();

                var result = new UserGroupViewModel();

                result.userGroup_Index = queryResult.UserGroup_Index;
                result.userGroup_Id = queryResult.UserGroup_Id;
                result.userGroup_Name = queryResult.UserGroup_Name;
         
              

                result.isActive = queryResult.IsActive;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion*/

        #region Export Excel
        public SCCSMappingActionResultExportViewModel Export(SCCSMappingExportViewModel data)
        {
            try
            {
                var query = db.ms_SCCS_Mapping.AsQueryable();
                //query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.costCenter_Id))
                {
                    query = query.Where(c => c.CostCenter_Id.Contains(data.costCenter_Id)
                                        || c.ProfitCenter.Contains(data.costCenter_Id));

                }

                if (!string.IsNullOrEmpty(data.costCenter_Description))
                {
                    query = query.Where(c => c.CostCenter_Description.Contains(data.costCenter_Description));
                }

                if (!string.IsNullOrEmpty(data.profitCenter_Description))
                {
                    query = query.Where(c => c.ProfitCenter_Description.Contains(data.profitCenter_Description));
                }

                if (!string.IsNullOrEmpty(data.shortText_Service_line))
                {
                    query = query.Where(c => c.ShortText_Service_line.Contains(data.shortText_Service_line));
                }
                if (!string.IsNullOrEmpty(data.createdate_date) && !string.IsNullOrEmpty(data.createdate_date_to))
                {
                    var dateStart = data.createdate_date.toBetweenDate();
                    var dateEnd = data.createdate_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }

                var Item = new List<ms_SCCS_Mapping>();
                var TotalRow = new List<ms_SCCS_Mapping>();

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
                Item = Item.OrderBy(o => o.SCCS_Mapping_Id).ToList();
                var result = new List<SCCSMappingExportViewModel>();

                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new SCCSMappingExportViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.sCCS_Mapping_Id = item.SCCS_Mapping_Id;
                    resultItem.plant = item.Plant;
                    resultItem.costCenter_Id = item.CostCenter_Id == null ? "" : item.CostCenter_Id; 
                    resultItem.costCenter_Description = item.CostCenter_Description == null ? "" : item.CostCenter_Description;
                    resultItem.profitCenter = item.ProfitCenter == null ? "" : item.ProfitCenter;
                    resultItem.profitCenter_Description = item.ProfitCenter_Description == null ? "" : item.ProfitCenter_Description;
                    resultItem.customerGroup = item.CustomerGroup;
                    resultItem.shipto_Id = item.Shipto_Id;
                    resultItem.remark = item.Remark;
                    resultItem.aCC_CAT = item.ACC_CAT;
                    resultItem.itemCat = item.ItemCat;
                    resultItem.gL_ACC = item.GL_ACC;
                    resultItem.iO_Code = item.IO_Code == null ? "" : item.IO_Code;
                    resultItem.version_Id = item.Version_Id;
                    resultItem.shortText_PO_item = item.ShortText_PO_item;
                    resultItem.shortText_Service_line = item.ShortText_Service_line == null ? "" : item.ShortText_Service_line;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var SCCSMappingActionResultExportViewModel = new SCCSMappingActionResultExportViewModel();
                SCCSMappingActionResultExportViewModel.itemsSCCSMapping = result.ToList();
                SCCSMappingActionResultExportViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return SCCSMappingActionResultExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

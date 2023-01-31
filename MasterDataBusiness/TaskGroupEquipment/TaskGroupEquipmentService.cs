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
    public class TaskGroupEquipmentService
    {
        #region BeforeCodeTaskGroupEquipment
        //public List<TaskGroupEquipmentViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_TaskGroupEquipment.FromSql("sp_GetTaskGroupEquipment").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<TaskGroupEquipmentViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new TaskGroupEquipmentViewModel();
        //                resultItem.TaskGroupEquipmentIndex = item.TaskGroupEquipment_Index;
        //                resultItem.TaskGroupEquipmentId = item.TaskGroupEquipment_Id;
        //                if (item.TaskGroup_Index != null)
        //                {
        //                    var itemList = context.MS_TaskGroup.FromSql("sp_GetTaskGroup").Where(c => c.TaskGroup_Index == item.TaskGroup_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.TaskGroupIndex = itemList.TaskGroup_Index;
        //                        resultItem.TaskGroupName = itemList.TaskGroup_Name;
        //                    }
        //                }
        //                if (item.TaskGroup_Index != null)
        //                {
        //                    var itemList = context.MS_Equipment.FromSql("sp_GetEquipment").Where(c => c.Equipment_Index == item.Equipment_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.EquipmentIndex = itemList.Equipment_Index;
        //                        resultItem.EquipmentName = itemList.Equipment_Name;
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
        //public List<TaskGroupEquipmentViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_TaskGroupEquipment.FromSql("sp_GetTaskGroupEquipment").Where(c => c.TaskGroupEquipment_Index == id).ToList();
        //            var result = new List<TaskGroupEquipmentViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new TaskGroupEquipmentViewModel();
        //                resultItem.TaskGroupEquipmentIndex = item.TaskGroupEquipment_Index;
        //                resultItem.TaskGroupEquipmentId = item.TaskGroupEquipment_Id;
        //                if (item.TaskGroup_Index != null)
        //                {
        //                    var itemList = context.MS_TaskGroup.FromSql("sp_GetTaskGroup").Where(c => c.TaskGroup_Index == item.TaskGroup_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.TaskGroupIndex = itemList.TaskGroup_Index;
        //                        resultItem.TaskGroupName = itemList.TaskGroup_Name;
        //                    }
        //                }
        //                if (item.TaskGroup_Index != null)
        //                {
        //                    var itemList = context.MS_Equipment.FromSql("sp_GetEquipment").Where(c => c.Equipment_Index == item.Equipment_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.EquipmentIndex = itemList.Equipment_Index;
        //                        resultItem.EquipmentName = itemList.Equipment_Name;
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
        //public List<TaskGroupEquipmentViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_TaskGroupEquipment.FromSql("sp_GetTaskGroupEquipment").Where(c => c.TaskGroupEquipment_Index == id).ToList();
        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<TaskGroupEquipmentViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var TaskGroupEquipment_Index = new SqlParameter("TaskGroupEquipment_Index", item.TaskGroupEquipment_Index);
        //                var TaskGroupEquipment_Id = new SqlParameter("TaskGroupEquipment_Id", item.TaskGroupEquipment_Id);
        //                var TaskGroup_Index = new SqlParameter("TaskGroup_Index", item.TaskGroup_Index);
        //                var Equipment_Index = new SqlParameter("Equipment_Index", item.Equipment_Index);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_TaskGroupEquipment  @TaskGroupEquipment_Index,@TaskGroupEquipment_Id,@TaskGroup_Index,@Equipment_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", TaskGroupEquipment_Index, TaskGroupEquipment_Id, TaskGroup_Index, Equipment_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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
        //public String SaveChanges(TaskGroupEquipmentViewModel data)
        //{
        //    try
        //    {
        //        int isactive = 1;
        //        int isdelete = 0;
        //        int isSystem = 0;
        //        int statusId = 0;

        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.TaskGroupEquipmentIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.TaskGroupEquipmentIndex = Guid.NewGuid();
        //            }
        //            if (data.TaskGroupEquipmentId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "TaskGroupEquipmentId");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.TaskGroupEquipmentId = resultParameter.Value.ToString();
        //            }
        //            var TaskGroupEquipment_Index = new SqlParameter("TaskGroupEquipment_Index", data.TaskGroupEquipmentIndex);
        //            var TaskGroupEquipment_Id = new SqlParameter("TaskGroupEquipment_Id", data.TaskGroupEquipmentId);
        //            var TaskGroup_Index = new SqlParameter("TaskGroup_Index", data.TaskGroupIndex);
        //            var Equipment_Index = new SqlParameter("Equipment_Index", data.EquipmentIndex);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_TaskGroupEquipment  @TaskGroupEquipment_Index,@TaskGroupEquipment_Id,@TaskGroup_Index,@Equipment_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", TaskGroupEquipment_Index, TaskGroupEquipment_Id, TaskGroup_Index, Equipment_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<TaskGroupEquipmentViewModel> search(TaskGroupEquipmentViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<TaskGroupEquipmentViewModel>();
        //            var queryResult = context.MS_TaskGroupEquipment.FromSql("sp_GetTaskGroupEquipment").Where(c => c.IsActive == 1 && c.IsDelete == 0)
        //                                            .ToList();
        //            if (data.TaskGroupEquipmentId != "" && data.TaskGroupEquipmentId != null)
        //            {
        //                pwhereLike = " And TaskGroupEquipment_Id like N'%" + data.TaskGroupEquipmentId + "%'";
        //            }
        //            else
        //            {
        //                pwhereLike = " ";
        //            }

        //            if (data.TaskGroupEquipmentId != "" && data.TaskGroupEquipmentId != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereLike);
        //                var query = context.MS_TaskGroupEquipment.FromSql("sp_GetTaskGroupEquipment @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new TaskGroupEquipmentViewModel();
        //                    resultItem.TaskGroupEquipmentIndex = item.TaskGroupEquipment_Index;
        //                    resultItem.TaskGroupEquipmentId = item.TaskGroupEquipment_Id;
        //                    if (item.TaskGroup_Index != null)
        //                    {
        //                        var itemList = context.MS_TaskGroup.FromSql("sp_GetTaskGroup").Where(c => c.TaskGroup_Index == item.TaskGroup_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            resultItem.TaskGroupIndex = itemList.TaskGroup_Index;
        //                            resultItem.TaskGroupName = itemList.TaskGroup_Name;
        //                        }
        //                    }
        //                    if (item.TaskGroup_Index != null)
        //                    {
        //                        var itemList = context.MS_Equipment.FromSql("sp_GetEquipment").Where(c => c.Equipment_Index == item.Equipment_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            resultItem.EquipmentIndex = itemList.Equipment_Index;
        //                            resultItem.EquipmentName = itemList.Equipment_Name;
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
        //            else if (data.TaskGroupName != "" && data.TaskGroupName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And TaskGroup_Name like N'%" + data.TaskGroupName + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_TaskGroup.FromSql("sp_GetTaskGroup @strwhere ", pstrwhere1).ToList();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new TaskGroupEquipmentViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.TaskGroup_Index == ItemList.TaskGroup_Index)
        //                        {
        //                            resultItem.TaskGroupEquipmentIndex = item.TaskGroupEquipment_Index;
        //                            resultItem.TaskGroupEquipmentId = item.TaskGroupEquipment_Id;
        //                            if (item.TaskGroup_Index != null)
        //                            {
        //                                var itemList = context.MS_TaskGroup.FromSql("sp_GetTaskGroup").Where(c => c.TaskGroup_Index == item.TaskGroup_Index).FirstOrDefault();
        //                                if (itemList != null)
        //                                {
        //                                    resultItem.TaskGroupIndex = itemList.TaskGroup_Index;
        //                                    resultItem.TaskGroupName = itemList.TaskGroup_Name;
        //                                }
        //                            }
        //                            if (item.TaskGroup_Index != null)
        //                            {
        //                                var itemList = context.MS_Equipment.FromSql("sp_GetEquipment").Where(c => c.Equipment_Index == item.Equipment_Index).FirstOrDefault();
        //                                if (itemList != null)
        //                                {
        //                                    resultItem.EquipmentIndex = itemList.Equipment_Index;
        //                                    resultItem.EquipmentName = itemList.Equipment_Name;
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
        //            else if (data.EquipmentName != "" && data.EquipmentName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And Equipment_Name like N'%" + data.EquipmentName + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_Equipment.FromSql("sp_GetEquipment @strwhere ", pstrwhere1).ToList();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new TaskGroupEquipmentViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.Equipment_Index == ItemList.Equipment_Index)
        //                        {
        //                            resultItem.TaskGroupEquipmentIndex = item.TaskGroupEquipment_Index;
        //                            resultItem.TaskGroupEquipmentId = item.TaskGroupEquipment_Id;
        //                            if (item.TaskGroup_Index != null)
        //                            {
        //                                var itemList = context.MS_TaskGroup.FromSql("sp_GetTaskGroup").Where(c => c.TaskGroup_Index == item.TaskGroup_Index).FirstOrDefault();
        //                                if (itemList != null)
        //                                {
        //                                    resultItem.TaskGroupIndex = itemList.TaskGroup_Index;
        //                                    resultItem.TaskGroupName = itemList.TaskGroup_Name;
        //                                }
        //                            }
        //                            if (item.TaskGroup_Index != null)
        //                            {
        //                                var itemList = context.MS_Equipment.FromSql("sp_GetEquipment").Where(c => c.Equipment_Index == item.Equipment_Index).FirstOrDefault();
        //                                if (itemList != null)
        //                                {
        //                                    resultItem.EquipmentIndex = itemList.Equipment_Index;
        //                                    resultItem.EquipmentName = itemList.Equipment_Name;
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
        //            if (data.TaskGroupEquipmentId == "" && data.TaskGroupName == "" && data.EquipmentName == "")
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

        #region FindTaskGroupEquipment

        public TaskGroupEquipmentViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_TaskGroupEquipment.Where(c => c.TaskGroupEquipment_Index == id).FirstOrDefault();

                var result = new TaskGroupEquipmentViewModel();


                result.taskGroupEquipment_Index = queryResult.TaskGroupEquipment_Index;
                result.taskGroupEquipment_Id = queryResult.TaskGroupEquipment_Id;
                result.taskGroup_Index = queryResult.TaskGroup_Index;
                result.taskGroup_Name = queryResult.TaskGroup_Name;
                result.equipment_Index = queryResult.Equipment_Index;
                result.equipment_Name = queryResult.Equipment_Name;
                result.isActive = queryResult.IsActive;
                result.isDelete = queryResult.IsDelete;



                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region FilterTaskGroupEquipment
        //Filter
        private MasterDataDbContext db;

        public TaskGroupEquipmentService()
        {
            db = new MasterDataDbContext();
        }

        public TaskGroupEquipmentService(MasterDataDbContext db)
        {
            this.db = db;
        }

      
        public actionResultTaskGroupEquipmentViewModel filter(SearchTaskGroupEquipmentViewModel data)
        {
            try
            {
                var query = db.View_TaskGroupEquipment.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.TaskGroupEquipment_Id.Contains(data.key)
                                        || c.TaskGroup_Name.Contains(data.key)
                                        || c.Equipment_Name.Contains(data.key));


                }

                var Item = new List<View_TaskGroupEquipment>();
                var TotalRow = new List<View_TaskGroupEquipment>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.TaskGroupEquipment_Id).ToList();

                var result = new List<SearchTaskGroupEquipmentViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchTaskGroupEquipmentViewModel();

                    resultItem.taskGroupEquipment_Id = item.TaskGroupEquipment_Id;
                    resultItem.taskGroupEquipment_Index = item.TaskGroupEquipment_Index;
                    resultItem.taskGroup_Index = item.TaskGroup_Index;
                    resultItem.taskGroup_Name = item.TaskGroup_Name;
                    resultItem.equipment_Index = item.Equipment_Index;
                    resultItem.equipment_Name = item.Equipment_Name;

                  
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultTaskGroupEquipmentViewModel = new actionResultTaskGroupEquipmentViewModel();
                actionResultTaskGroupEquipmentViewModel.itemsTaskGroupEquipment = result.ToList();
                actionResultTaskGroupEquipmentViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultTaskGroupEquipmentViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region DeleteTaskGroupEquipment
        public Boolean getDelete(TaskGroupEquipmentViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var taskGroupEquipment = db.MS_TaskGroupEquipment.Find(data.taskGroupEquipment_Index);

                if (taskGroupEquipment != null)
                {
                    taskGroupEquipment.IsActive = 0;
                    taskGroupEquipment.IsDelete = 1;


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
                        olog.logging("DeleteTaskGroupEquipment", msglog);
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

        public String SaveChanges(TaskGroupEquipmentViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var TaskGroupEquipmentOld = db.MS_TaskGroupEquipment.Find(data.taskGroupEquipment_Index);

                if (TaskGroupEquipmentOld == null)
                {
                    if (!string.IsNullOrEmpty(data.taskGroupEquipment_Id))
                    {
                        var query = db.MS_TaskGroupEquipment.FirstOrDefault(c => c.TaskGroupEquipment_Id == data.taskGroupEquipment_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.taskGroupEquipment_Id))
                    {
                        data.taskGroupEquipment_Id = "TaskGroupEquipment_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_TaskGroupEquipment.FirstOrDefault(c => c.TaskGroupEquipment_Id == data.taskGroupEquipment_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.TaskGroupEquipment_Id == data.taskGroupEquipment_Id)
                                {
                                    data.taskGroupEquipment_Id = "TaskGroupEquipment_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.taskGroupEquipment_Id = "TaskGroupEquipment_Id".genAutonumber();

                    MS_TaskGroupEquipment Model = new MS_TaskGroupEquipment();

                    Model.TaskGroupEquipment_Index = Guid.NewGuid();
                    Model.TaskGroupEquipment_Id = data.taskGroupEquipment_Id;
                    Model.TaskGroup_Index = data.taskGroup_Index;
                    Model.Equipment_Index = data.equipment_Index;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_TaskGroupEquipment.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.taskGroupEquipment_Id))
                    {
                        if (TaskGroupEquipmentOld.TaskGroupEquipment_Id != "")
                        {
                            data.taskGroupEquipment_Id = TaskGroupEquipmentOld.TaskGroupEquipment_Id;
                        }
                    }
                    else
                    {
                        if (TaskGroupEquipmentOld.TaskGroupEquipment_Id != data.taskGroupEquipment_Id)
                        {
                            var query = db.MS_TaskGroupEquipment.FirstOrDefault(c => c.TaskGroupEquipment_Id == data.taskGroupEquipment_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.taskGroupEquipment_Id = TaskGroupEquipmentOld.TaskGroupEquipment_Id;
                        }
                    }
                    TaskGroupEquipmentOld.TaskGroupEquipment_Id = data.taskGroupEquipment_Id;
                    TaskGroupEquipmentOld.TaskGroup_Index = data.taskGroup_Index;
                    TaskGroupEquipmentOld.Equipment_Index = data.equipment_Index;
                    TaskGroupEquipmentOld.IsActive = Convert.ToInt32(data.isActive);
                    TaskGroupEquipmentOld.Update_By = data.update_By;
                    TaskGroupEquipmentOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveTaskGroupEquipment", msglog);
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


        #region autoTaskGroupEquipmentSearchFilter
        public List<ItemListViewModel> autoTaskGroupEquipmentSearchFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_TaskGroupEquipment.Where(c => c.TaskGroupEquipment_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.TaskGroupEquipment_Id,
                        key = s.TaskGroupEquipment_Id
                    }).Distinct();

                    var query2 = db.View_TaskGroupEquipment.Where(c => c.TaskGroup_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.TaskGroup_Name,
                        key = s.TaskGroup_Name
                    }).Distinct();

                    var query3 = db.View_TaskGroupEquipment.Where(c => c.Equipment_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Equipment_Name,
                        key = s.Equipment_Name

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


       
    }
}

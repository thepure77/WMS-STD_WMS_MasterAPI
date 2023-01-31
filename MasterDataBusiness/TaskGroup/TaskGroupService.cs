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
    public class TaskGroupService
    {
        #region BeforeCodeTaskGroup
        //public List<TaskGroupViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_TaskGroup.FromSql("sp_GetTaskGroup").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<TaskGroupViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new TaskGroupViewModel();

        //                resultItem.TaskGroupIndex = item.TaskGroup_Index;
        //                resultItem.TaskGroupId = item.TaskGroup_Id;
        //                resultItem.TaskGroupName = item.TaskGroup_Name;
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

        //public String SaveChanges(TaskGroupViewModel data)
        //{
        //    try
        //    {
        //        int isactive = 1;
        //        int isdelete = 0;
        //        int isSystem = 0;
        //        int statusId = 0;

        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.TaskGroupIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.TaskGroupIndex = Guid.NewGuid();
        //            }
        //            if (data.TaskGroupId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "TaskGroupID");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.TaskGroupId = resultParameter.Value.ToString();
        //            }
        //            var TaskGroup_Index = new SqlParameter("TaskGroup_Index", data.TaskGroupIndex);
        //            var TaskGroup_Id = new SqlParameter("TaskGroup_Id", data.TaskGroupId);
        //            var TaskGroup_Name = new SqlParameter("TaskGroup_Name", data.TaskGroupName);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_TaskGroup  @TaskGroup_Index,@TaskGroup_Id,@TaskGroup_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", TaskGroup_Index, TaskGroup_Id, TaskGroup_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public List<TaskGroupViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_TaskGroup.FromSql("sp_GetTaskGroup").Where(c => c.TaskGroup_Index == id).ToList();

        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<TaskGroupViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var TaskGroup_Index = new SqlParameter("TaskGroup_Index", item.TaskGroup_Index);
        //                var TaskGroup_Id = new SqlParameter("TaskGroup_Id", item.TaskGroup_Id);
        //                var TaskGroup_Name = new SqlParameter("TaskGroup_Name", item.TaskGroup_Name);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_TaskGroup  @TaskGroup_Index,@TaskGroup_Id,@TaskGroup_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", TaskGroup_Index, TaskGroup_Id, TaskGroup_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        //public List<TaskGroupViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_TaskGroup.FromSql("sp_GetTaskGroup").Where(c => c.TaskGroup_Index == id).ToList();
        //            var result = new List<TaskGroupViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new TaskGroupViewModel();
        //                resultItem.TaskGroupIndex = item.TaskGroup_Index;
        //                resultItem.TaskGroupId = item.TaskGroup_Id;
        //                resultItem.TaskGroupName = item.TaskGroup_Name;
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

        //public List<TaskGroupViewModel> search(TaskGroupViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<TaskGroupViewModel>();
        //            if (data.TaskGroupId != "" && data.TaskGroupId != null)
        //            {
        //                pwhereFilter = " And TaskGroup_Id like N'%" + data.TaskGroupId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter = "";
        //            }

        //            if (data.TaskGroupName != "" && data.TaskGroupName != null)
        //            {
        //                pwhereFilter += " And TaskGroup_Name like N'%" + data.TaskGroupName + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }


        //            if (data.TaskGroupId != "" && data.TaskGroupId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_TaskGroup.FromSql("sp_GetTaskGroup @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new TaskGroupViewModel();
        //                    resultItem.TaskGroupIndex = item.TaskGroup_Index;
        //                    resultItem.TaskGroupId = item.TaskGroup_Id;
        //                    resultItem.TaskGroupName = item.TaskGroup_Name;
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
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_TaskGroup.FromSql("sp_GetTaskGroup @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new TaskGroupViewModel();
        //                    resultItem.TaskGroupIndex = item.TaskGroup_Index;
        //                    resultItem.TaskGroupId = item.TaskGroup_Id;
        //                    resultItem.TaskGroupName = item.TaskGroup_Name;
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

        //            if (data.TaskGroupId == "" && data.TaskGroupName == "")
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

        #region FindTaskGroup

        public TaskGroupViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_TaskGroup.Where(c => c.TaskGroup_Index == id).FirstOrDefault();

                var result = new TaskGroupViewModel();


                result.taskGroup_Index = queryResult.TaskGroup_Index;
                result.taskGroup_Id = queryResult.TaskGroup_Id;
                result.taskGroup_Name = queryResult.TaskGroup_Name;
                result.isActive = queryResult.IsActive;


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region FilterTaskGroup
        //Filter
        private MasterDataDbContext db;

        public TaskGroupService()
        {
            db = new MasterDataDbContext();
        }

        public TaskGroupService(MasterDataDbContext db)
        {
            this.db = db;
        }


        
        public actionResultTaskGroupViewModel filter(SearchTaskGroupViewModel data)
        {
            try
            {
                var query = db.MS_TaskGroup.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.TaskGroup_Name.Contains(data.key)
                                        || c.TaskGroup_Id.Contains(data.key));


                }

                var Item = new List<MS_TaskGroup>();
                var TotalRow = new List<MS_TaskGroup>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.TaskGroup_Id).ToList();

                var result = new List<SearchTaskGroupViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchTaskGroupViewModel();

                    resultItem.taskGroup_Index = item.TaskGroup_Index;
                    resultItem.taskGroup_Id = item.TaskGroup_Id;
                    resultItem.taskGroup_Name = item.TaskGroup_Name;



                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultTaskGroupViewModel = new actionResultTaskGroupViewModel();
                actionResultTaskGroupViewModel.itemsTaskGroup = result.ToList();
                actionResultTaskGroupViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultTaskGroupViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDelete
        public Boolean getDelete(TaskGroupViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var taskGroup = db.MS_TaskGroup.Find(data.taskGroup_Index);

                if (taskGroup != null)
                {
                    taskGroup.IsActive = 0;
                    taskGroup.IsDelete = 1;


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
                        olog.logging("DeleteTaskGroup", msglog);
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

        public String SaveChanges(TaskGroupViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var TaskGroupOld = db.MS_TaskGroup.Find(data.taskGroup_Index);

                if (TaskGroupOld == null)
                {
                    if (!string.IsNullOrEmpty(data.taskGroup_Id))
                    {
                        var query = db.MS_TaskGroup.FirstOrDefault(c => c.TaskGroup_Id == data.taskGroup_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.taskGroup_Id))
                    {
                        data.taskGroup_Id = "TaskGroup_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_TaskGroup.FirstOrDefault(c => c.TaskGroup_Id == data.taskGroup_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.TaskGroup_Id == data.taskGroup_Id)
                                {
                                    data.taskGroup_Id = "TaskGroup_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.taskGroup_Id = "TaskGroup_Id".genAutonumber();

                    MS_TaskGroup Model = new MS_TaskGroup();

                    Model.TaskGroup_Index = Guid.NewGuid();
                    Model.TaskGroup_Id = data.taskGroup_Id;
                    Model.TaskGroup_Name = data.taskGroup_Name;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_TaskGroup.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.taskGroup_Id))
                    {
                        if (TaskGroupOld.TaskGroup_Id != "")
                        {
                            data.taskGroup_Id = TaskGroupOld.TaskGroup_Id;
                        }
                    }
                    else
                    {
                        if (TaskGroupOld.TaskGroup_Id != data.taskGroup_Id)
                        {
                            var query = db.MS_TaskGroup.FirstOrDefault(c => c.TaskGroup_Id == data.taskGroup_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.taskGroup_Id = TaskGroupOld.TaskGroup_Id;
                        }
                    }
                    TaskGroupOld.TaskGroup_Id = data.taskGroup_Id;
                    TaskGroupOld.TaskGroup_Name = data.taskGroup_Name;
                    TaskGroupOld.IsActive = Convert.ToInt32(data.isActive);
                    TaskGroupOld.Update_By = data.update_By;
                    TaskGroupOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveTaskGroup", msglog);
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

        #region autoTaskGroup

        public List<ItemListViewModel> autoTaskGroup(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_TaskGroup.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.TaskGroup_Id.Contains(data.key)
                                                || c.TaskGroup_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.TaskGroup_Name, c.TaskGroup_Index, c.TaskGroup_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.TaskGroup_Index,
                            id = item.TaskGroup_Id,
                            name = item.TaskGroup_Id + " - " + item.TaskGroup_Name,
                            key = item.TaskGroup_Id + " - " + item.TaskGroup_Name,
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

        #region SearchTaskGroup

        public List<ItemListViewModel> autoTaskGroupSearchFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_TaskGroup.Where(c => c.TaskGroup_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.TaskGroup_Name,
                        key = s.TaskGroup_Name
                    }).Distinct();

                    var query2 = db.MS_TaskGroup.Where(c => c.TaskGroup_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.TaskGroup_Id,
                        key = s.TaskGroup_Id
                    }).Distinct();
                    var query = query1.Union(query2).Union(query2);

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


        public List<TaskGroupViewModel> configTaskGroup(TaskGroupViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_TaskGroup.ToList();


                    var items = new List<TaskGroupViewModel>();


                    foreach (var item in query)
                    {
                        var resultItem = new TaskGroupViewModel();

                        resultItem.taskGroup_Index = item.TaskGroup_Index;
                        resultItem.taskGroup_Id = item.TaskGroup_Id;
                        resultItem.taskGroup_Name = item.TaskGroup_Name;

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
    }
}

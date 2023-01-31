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
    public class TaskGroupWorkAreaService
    {
        #region BeforeCodeTaskGroupWorkArea
        //public List<TaskGroupWorkAreaViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_TaskGroupWorkArea.FromSql("sp_GetTaskGroupWorkArea").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<TaskGroupWorkAreaViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new TaskGroupWorkAreaViewModel();
        //                if (item.TaskGroup_Index != null)
        //                {
        //                    var itemList = context.MS_TaskGroup.FromSql("sp_GetTaskGroup").Where(c => item.TaskGroup_Index == c.TaskGroup_Index).FirstOrDefault();
        //                    resultItem.TaskGroupIndex = itemList.TaskGroup_Index;
        //                    resultItem.TaskGroupName = itemList.TaskGroup_Name;
        //                }
        //                if (item.WorkArea_Index != null)
        //                {
        //                    var itemList = context.MS_WorkArea.FromSql("sp_GetWorkArea").Where(c => item.WorkArea_Index == c.WorkArea_Index).FirstOrDefault();
        //                    resultItem.WorkAreaIndex = itemList.WorkArea_Index;
        //                    resultItem.WorkAreaName = itemList.WorkArea_Name;
        //                }

        //                resultItem.TaskGroupWorkAreaIndex = item.TaskGroupWorkArea_Index;
        //                resultItem.TaskGroupWorkAreaId = item.TaskGroupWorkArea_Id;
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

        //public String SaveChanges(TaskGroupWorkAreaViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.TaskGroupWorkAreaIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.TaskGroupWorkAreaIndex = Guid.NewGuid();
        //            }
        //            if (data.TaskGroupWorkAreaId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "TaskGroupWorkAreaId");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.TaskGroupWorkAreaId = resultParameter.Value.ToString();
        //            }
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int isSystem = 0;
        //            int statusId = 0;
        //            var TaskGroupWorkArea_Index = new SqlParameter("TaskGroupWorkArea_Index", data.TaskGroupWorkAreaIndex);
        //            var TaskGroupWorkArea_Id = new SqlParameter("TaskGroupWorkArea_Id", data.TaskGroupWorkAreaId);
        //            var TaskGroup_Index = new SqlParameter("TaskGroup_Index", data.TaskGroupIndex);
        //            var WorkArea_Index = new SqlParameter("WorkArea_Index", data.WorkAreaIndex);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_TaskGroupWorkArea  @TaskGroupWorkArea_Index,@TaskGroupWorkArea_Id,@TaskGroup_Index,@WorkArea_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", TaskGroupWorkArea_Index, TaskGroupWorkArea_Id, TaskGroup_Index, WorkArea_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        //public List<TaskGroupWorkAreaViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_TaskGroupWorkArea.FromSql("sp_GetTaskGroupWorkArea").Where(c => c.TaskGroupWorkArea_Index == id).ToList();

        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<TaskGroupWorkAreaViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var TaskGroupWorkArea_Index = new SqlParameter("TaskGroupWorkArea_Index", item.TaskGroupWorkArea_Index);
        //                var TaskGroupWorkArea_Id = new SqlParameter("TaskGroupWorkArea_Id", item.TaskGroupWorkArea_Id);
        //                var TaskGroup_Index = new SqlParameter("TaskGroup_Index", item.TaskGroup_Index);
        //                var WorkArea_Index = new SqlParameter("WorkArea_Index", item.WorkArea_Index);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_TaskGroupWorkArea  @TaskGroupWorkArea_Index,@TaskGroupWorkArea_Id,@TaskGroup_Index,@WorkArea_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", TaskGroupWorkArea_Index, TaskGroupWorkArea_Id, TaskGroup_Index, WorkArea_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        //public List<TaskGroupWorkAreaViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            string pstring = " and TaskGroupWorkArea_Index ='" + id + "'";
        //            var queryResult = context.MS_TaskGroupWorkArea.FromSql("sp_GetTaskGroupWorkArea {0}", pstring).Where(c => c.TaskGroupWorkArea_Index == id).ToList();
        //            var result = new List<TaskGroupWorkAreaViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new TaskGroupWorkAreaViewModel();
        //                if (item.TaskGroup_Index != null)
        //                {
        //                    var itemList = context.MS_TaskGroup.FromSql("sp_GetTaskGroup").Where(c => item.TaskGroup_Index == c.TaskGroup_Index).FirstOrDefault();
        //                    resultItem.TaskGroupIndex = itemList.TaskGroup_Index;
        //                    resultItem.TaskGroupName = itemList.TaskGroup_Name;
        //                }
        //                if (item.WorkArea_Index != null)
        //                {
        //                    var itemList = context.MS_WorkArea.FromSql("sp_GetWorkArea").Where(c => item.WorkArea_Index == c.WorkArea_Index).FirstOrDefault();
        //                    resultItem.WorkAreaIndex = itemList.WorkArea_Index;
        //                    resultItem.WorkAreaName = itemList.WorkArea_Name;
        //                }
        //                resultItem.TaskGroupWorkAreaIndex = item.TaskGroupWorkArea_Index;
        //                resultItem.TaskGroupWorkAreaId = item.TaskGroupWorkArea_Id;
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

        //public List<TaskGroupWorkAreaViewModel> search(TaskGroupWorkAreaViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<TaskGroupWorkAreaViewModel>();
        //            var queryResult = context.MS_TaskGroupWorkArea.FromSql("sp_GetTaskGroupWorkArea").Where(c => c.IsActive == 1 && c.IsDelete == 0)
        //                                            .ToList();
        //            if (data.TaskGroupWorkAreaId != "" && data.TaskGroupWorkAreaId != null)
        //            {
        //                pwhereFilter = " And TaskGroupWorkArea_Id like N'%" + data.TaskGroupWorkAreaId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }

        //            if (data.TaskGroupWorkAreaId != "" && data.TaskGroupWorkAreaId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_TaskGroupWorkArea.FromSql("sp_GetTaskGroupWorkArea @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {                           
        //                    var resultItem = new TaskGroupWorkAreaViewModel();
        //                    if (item.TaskGroup_Index != null)
        //                    {
        //                        var itemList = context.MS_TaskGroup.FromSql("sp_GetTaskGroup").Where(c => item.TaskGroup_Index == c.TaskGroup_Index).FirstOrDefault();
        //                        resultItem.TaskGroupIndex = itemList.TaskGroup_Index;
        //                        resultItem.TaskGroupName = itemList.TaskGroup_Name;
        //                    }
        //                    if (item.WorkArea_Index != null)
        //                    {
        //                        var itemList = context.MS_WorkArea.FromSql("sp_GetWorkArea").Where(c => item.WorkArea_Index == c.WorkArea_Index).FirstOrDefault();
        //                        resultItem.WorkAreaIndex = itemList.WorkArea_Index;
        //                        resultItem.WorkAreaName = itemList.WorkArea_Name;
        //                    }
        //                    resultItem.TaskGroupWorkAreaIndex = item.TaskGroupWorkArea_Index;
        //                    resultItem.TaskGroupWorkAreaId = item.TaskGroupWorkArea_Id;
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
        //                    var resultItem = new TaskGroupWorkAreaViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.TaskGroup_Index == ItemList.TaskGroup_Index)
        //                        {
        //                            resultItem.TaskGroupWorkAreaIndex = item.TaskGroupWorkArea_Index;
        //                            resultItem.TaskGroupIndex = ItemList.TaskGroup_Index;
        //                            resultItem.TaskGroupName = ItemList.TaskGroup_Name;
        //                            resultItem.TaskGroupWorkAreaId = item.TaskGroupWorkArea_Id;
        //                            if (item.WorkArea_Index != null)
        //                            {
        //                                var itemList = context.MS_WorkArea.FromSql("sp_GetWorkArea").Where(c => item.WorkArea_Index == c.WorkArea_Index).FirstOrDefault();
        //                                resultItem.WorkAreaIndex = itemList.WorkArea_Index;
        //                                resultItem.WorkAreaName = itemList.WorkArea_Name;
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
        //            else if (data.WorkAreaName != "" && data.WorkAreaName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And WorkArea_Name like N'%" + data.WorkAreaName + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_WorkArea.FromSql("sp_GetWorkArea @strwhere ", pstrwhere1).ToList();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new TaskGroupWorkAreaViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.WorkArea_Index == ItemList.WorkArea_Index)
        //                        {
        //                            if (item.TaskGroup_Index != null)
        //                            {
        //                                var itemList = context.MS_TaskGroup.FromSql("sp_GetTaskGroup").Where(c => item.TaskGroup_Index == c.TaskGroup_Index).FirstOrDefault();
        //                                resultItem.TaskGroupIndex = itemList.TaskGroup_Index;
        //                                resultItem.TaskGroupName = itemList.TaskGroup_Name;
        //                            }
        //                            resultItem.WorkAreaIndex = ItemList.WorkArea_Index;
        //                            resultItem.WorkAreaName = ItemList.WorkArea_Name;
        //                            resultItem.TaskGroupWorkAreaIndex = item.TaskGroupWorkArea_Index;
        //                            resultItem.TaskGroupWorkAreaId = item.TaskGroupWorkArea_Id;
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
        //            if (data.TaskGroupName == "" && data.WorkAreaName == "" && data.TaskGroupWorkAreaId == "")
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


        #region FindTaskGroupWorkArea

        public TaskGroupWorkAreaViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_TaskGroupWorkArea.Where(c => c.TaskGroupWorkArea_Index == id).FirstOrDefault();

                var result = new TaskGroupWorkAreaViewModel();


                result.taskGroupWorkArea_Index = queryResult.TaskGroupWorkArea_Index;
                result.taskGroupWorkArea_Id = queryResult.TaskGroupWorkArea_Id;
                result.taskGroup_Index = queryResult.TaskGroup_Index;
                result.taskGroup_Name = queryResult.TaskGroup_Name;
                result.workArea_Index = queryResult.WorkArea_Index;
                result.workArea_Name = queryResult.WorkArea_Name;
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

        #region FilterTaskGroupWorkArea
        //Filter
        private MasterDataDbContext db;

        public TaskGroupWorkAreaService()
        {
            db = new MasterDataDbContext();
        }

        public TaskGroupWorkAreaService(MasterDataDbContext db)
        {
            this.db = db;
        }

       
        public actionResultTaskGroupWorkAreaViewModel filter(SearchTaskGroupWorkAreaViewModel data)
        {
            try
            {
                var query = db.View_TaskGroupWorkArea.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.TaskGroupWorkArea_Id.Contains(data.key)
                                        || c.TaskGroup_Name.Contains(data.key)
                                        || c.WorkArea_Name.Contains(data.key));


                }

                var Item = new List<View_TaskGroupWorkArea>();
                var TotalRow = new List<View_TaskGroupWorkArea>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.TaskGroupWorkArea_Id).ToList();

                var result = new List<SearchTaskGroupWorkAreaViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchTaskGroupWorkAreaViewModel();

                    resultItem.taskGroupWorkArea_Id = item.TaskGroupWorkArea_Id;
                    resultItem.taskGroupWorkArea_Index = item.TaskGroupWorkArea_Index;
                    resultItem.taskGroup_Index = item.TaskGroup_Index;
                    resultItem.taskGroup_Name = item.TaskGroup_Name;
                    resultItem.workArea_Index = item.WorkArea_Index;
                    resultItem.workArea_Name = item.WorkArea_Name;


                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultTaskGroupWorkAreaViewModel = new actionResultTaskGroupWorkAreaViewModel();
                actionResultTaskGroupWorkAreaViewModel.itemsTaskGroupWorkArea = result.ToList();
                actionResultTaskGroupWorkAreaViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultTaskGroupWorkAreaViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region DeleteTaskGroupWorkArea
        public Boolean getDelete(TaskGroupWorkAreaViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var taskGroupWorkArea = db.MS_TaskGroupWorkArea.Find(data.taskGroupWorkArea_Index);

                if (taskGroupWorkArea != null)
                {
                    taskGroupWorkArea.IsActive = 0;
                    taskGroupWorkArea.IsDelete = 1;


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
                        olog.logging("DeleteTaskGroupWorkArea", msglog);
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

        public String SaveChanges(TaskGroupWorkAreaViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var TaskGroupWorkAreaOld = db.MS_TaskGroupWorkArea.Find(data.taskGroupWorkArea_Index);

                if (TaskGroupWorkAreaOld == null)
                {
                    if (!string.IsNullOrEmpty(data.taskGroupWorkArea_Id))
                    {
                        var query = db.MS_TaskGroupWorkArea.FirstOrDefault(c => c.TaskGroupWorkArea_Id == data.taskGroupWorkArea_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.taskGroupWorkArea_Id))
                    {
                        data.taskGroupWorkArea_Id = "TaskGroupWorkArea_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_TaskGroupWorkArea.FirstOrDefault(c => c.TaskGroupWorkArea_Id == data.taskGroupWorkArea_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.TaskGroupWorkArea_Id == data.taskGroupWorkArea_Id)
                                {
                                    data.taskGroupWorkArea_Id = "TaskGroupWorkArea_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.taskGroupWorkArea_Id = "TaskGroupWorkArea_Id".genAutonumber();

                    MS_TaskGroupWorkArea Model = new MS_TaskGroupWorkArea();

                    Model.TaskGroupWorkArea_Index = Guid.NewGuid();
                    Model.TaskGroupWorkArea_Id = data.taskGroupWorkArea_Id;
                    Model.TaskGroup_Index = data.taskGroup_Index;
                    Model.WorkArea_Index = data.workArea_Index;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_TaskGroupWorkArea.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.taskGroupWorkArea_Id))
                    {
                        if (TaskGroupWorkAreaOld.TaskGroupWorkArea_Id != "")
                        {
                            data.taskGroupWorkArea_Id = TaskGroupWorkAreaOld.TaskGroupWorkArea_Id;
                        }
                    }
                    else
                    {
                        if (TaskGroupWorkAreaOld.TaskGroupWorkArea_Id != data.taskGroupWorkArea_Id)
                        {
                            var query = db.MS_TaskGroupWorkArea.FirstOrDefault(c => c.TaskGroupWorkArea_Id == data.taskGroupWorkArea_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.taskGroupWorkArea_Id = TaskGroupWorkAreaOld.TaskGroupWorkArea_Id;
                        }
                    }
                    TaskGroupWorkAreaOld.TaskGroupWorkArea_Id = data.taskGroupWorkArea_Id;
                    TaskGroupWorkAreaOld.TaskGroup_Index = data.taskGroup_Index;
                    TaskGroupWorkAreaOld.WorkArea_Index = data.workArea_Index;
                    TaskGroupWorkAreaOld.IsActive = Convert.ToInt32(data.isActive);
                    TaskGroupWorkAreaOld.Update_By = data.update_By;
                    TaskGroupWorkAreaOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveTaskGroupWorkArea", msglog);
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


        #region autoTaskGroupWorkAreaSearchFilter
        public List<ItemListViewModel> autoTaskGroupWorkAreaSearchFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_TaskGroupWorkArea.Where(c => c.TaskGroupWorkArea_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.TaskGroupWorkArea_Id,
                        key = s.TaskGroupWorkArea_Id
                    }).Distinct();

                    var query2 = db.View_TaskGroupWorkArea.Where(c => c.TaskGroup_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.TaskGroup_Name,
                        key = s.TaskGroup_Name
                    }).Distinct();

                    var query3 = db.View_TaskGroupWorkArea.Where(c => c.WorkArea_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.WorkArea_Name,
                        key = s.WorkArea_Name

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

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
    public class TaskGroupUserService
    {
        #region BeforeCodeTaskGroupUser
        //public List<TaskGroupUserViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_TaskGroupUser.FromSql("sp_GetTaskGroupUser").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<TaskGroupUserViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new TaskGroupUserViewModel();

        //                resultItem.TaskGroupUserIndex = item.TaskGroupUser_Index;
        //                resultItem.TaskGroupUserId = item.TaskGroupUser_Id;
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
        //                    var itemList = context.MS_User.FromSql("sp_GetUser").Where(c => c.User_Index == item.User_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.UserIndex = itemList.User_Index;
        //                        resultItem.UserName = itemList.User_Name;
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

        //public String SaveChanges(TaskGroupUserViewModel data)
        //{
        //    try
        //    {
        //        int isactive = 1;
        //        int isdelete = 0;
        //        int isSystem = 0;
        //        int statusId = 0;
        //        int isdefault = 0;
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.TaskGroupUserIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.TaskGroupUserIndex = Guid.NewGuid();
        //            }
        //            if (data.TaskGroupUserId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "TaskGroupUserId");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.TaskGroupUserId = resultParameter.Value.ToString();
        //            }
        //            var TaskGroupUser_Index = new SqlParameter("TaskGroupUser_Index", data.TaskGroupUserIndex);
        //            var TaskGroupUser_Id = new SqlParameter("TaskGroupUser_Id", data.TaskGroupUserId);
        //            var TaskGroup_Index = new SqlParameter("TaskGroup_Index", data.TaskGroupIndex);
        //            var User_Index = new SqlParameter("User_Index", data.UserIndex);
        //            var IsDefault = new SqlParameter("IsDefault", isdefault);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_TaskGroupUser  @TaskGroupUser_Index,@TaskGroupUser_Id,@TaskGroup_Index,@User_Index,@IsDefault,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", TaskGroupUser_Index, TaskGroupUser_Id, TaskGroup_Index, User_Index, IsDefault, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        //public List<TaskGroupUserViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_TaskGroupUser.FromSql("sp_GetTaskGroupUser").Where(c => c.TaskGroupUser_Index == id).ToList();

        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<TaskGroupUserViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var TaskGroupUser_Index = new SqlParameter("TaskGroupUser_Index", item.TaskGroupUser_Index);
        //                var TaskGroupUser_Id = new SqlParameter("TaskGroupUser_Id", item.TaskGroupUser_Id);
        //                var TaskGroup_Index = new SqlParameter("TaskGroup_Index", item.TaskGroup_Index);
        //                var User_Index = new SqlParameter("User_Index", item.User_Index);
        //                var IsDefault = new SqlParameter("IsDefault", item.IsDefault);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_TaskGroupUser  @TaskGroupUser_Index,@TaskGroupUser_Id,@TaskGroup_Index,@User_Index,@IsDefault,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", TaskGroupUser_Index, TaskGroupUser_Id, TaskGroup_Index, User_Index, IsDefault, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        //public List<TaskGroupUserViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_TaskGroupUser.FromSql("sp_GetTaskGroupUser").Where(c => c.TaskGroupUser_Index == id).ToList();
        //            var result = new List<TaskGroupUserViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new TaskGroupUserViewModel();
        //                resultItem.TaskGroupUserIndex = item.TaskGroupUser_Index;
        //                resultItem.TaskGroupUserId = item.TaskGroupUser_Id;
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
        //                    var itemList = context.MS_User.FromSql("sp_GetUser").Where(c => c.User_Index == item.User_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.UserIndex = itemList.User_Index;
        //                        resultItem.UserName = itemList.User_Name;
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

        //public List<TaskGroupUserViewModel> search(TaskGroupUserViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<TaskGroupUserViewModel>();
        //            var queryResult = context.MS_TaskGroupUser.FromSql("sp_GetTaskGroupUser").Where(c => c.IsActive == 1 && c.IsDelete == 0)
        //                                            .ToList();

        //            if (data.TaskGroupUserId != "" && data.TaskGroupUserId != null)
        //            {
        //                pwhereFilter = " And TaskGroupUser_Id like N'%" + data.TaskGroupUserId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }

        //            if (data.TaskGroupUserId != "" && data.TaskGroupUserId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_TaskGroupUser.FromSql("sp_GetTaskGroupUser @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new TaskGroupUserViewModel();
        //                    resultItem.TaskGroupUserIndex = item.TaskGroupUser_Index;
        //                    resultItem.TaskGroupUserId = item.TaskGroupUser_Id;
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
        //                        var itemList = context.MS_User.FromSql("sp_GetUser").Where(c => c.User_Index == item.User_Index).FirstOrDefault();
        //                        if (itemList != null)
        //                        {
        //                            resultItem.UserIndex = itemList.User_Index;
        //                            resultItem.UserName = itemList.User_Name;
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
        //                    var resultItem = new TaskGroupUserViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.TaskGroup_Index == ItemList.TaskGroup_Index)
        //                        {
        //                            resultItem.TaskGroupUserIndex = item.TaskGroupUser_Index;
        //                            resultItem.TaskGroupIndex = ItemList.TaskGroup_Index;
        //                            resultItem.TaskGroupName = ItemList.TaskGroup_Name;
        //                            resultItem.TaskGroupUserId = item.TaskGroupUser_Id;
        //                            if (item.TaskGroup_Index != null)
        //                            {
        //                                var itemList = context.MS_User.FromSql("sp_GetUser").Where(c => c.User_Index == item.User_Index).FirstOrDefault();
        //                                if (itemList != null)
        //                                {
        //                                    resultItem.UserIndex = itemList.User_Index;
        //                                    resultItem.UserName = itemList.User_Name;
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
        //            else if (data.UserName != "" && data.UserName != null)
        //            {
        //                pwhereLike += " And isActive = '" + 1 + "'";
        //                pwhereLike += " And isDelete = '" + 0 + "'";
        //                pwhereLike = " And User_Name like N'%" + data.UserName + "%'";
        //                var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                var dataList = context.MS_User.FromSql("sp_GetUser @strwhere ", pstrwhere1).ToList();
        //                foreach (var item in queryResult)
        //                {
        //                    var resultItem = new TaskGroupUserViewModel();
        //                    foreach (var ItemList in dataList)
        //                    {
        //                        if (item.User_Index == ItemList.User_Index)
        //                        {
        //                            resultItem.TaskGroupUserIndex = item.TaskGroupUser_Index;
        //                            resultItem.TaskGroupUserId = item.TaskGroupUser_Id;
        //                            if (item.TaskGroup_Index != null)
        //                            {
        //                                var itemList = context.MS_TaskGroup.FromSql("sp_GetTaskGroup").Where(c => c.TaskGroup_Index == item.TaskGroup_Index).FirstOrDefault();
        //                                if (itemList != null)
        //                                {
        //                                    resultItem.TaskGroupIndex = itemList.TaskGroup_Index;
        //                                    resultItem.TaskGroupName = itemList.TaskGroup_Name;
        //                                }
        //                            }


        //                            resultItem.UserIndex = ItemList.User_Index;
        //                            resultItem.UserName = ItemList.User_Name;
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
        //            if (data.TaskGroupName == "" && data.UserName == "" && data.TaskGroupUserId == "")
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

        #region FindTaskGroupUser

        public TaskGroupUserViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_TaskGroupUser.Where(c => c.TaskGroupUser_Index == id).FirstOrDefault();

                var result = new TaskGroupUserViewModel();


                result.taskGroupUser_Index = queryResult.TaskGroupUser_Index;
                result.taskGroupUser_Id = queryResult.TaskGroupUser_Id;
                result.taskGroup_Index = queryResult.TaskGroup_Index;
                result.taskGroup_Name = queryResult.TaskGroup_Name;
                result.user_Index = queryResult.User_Index;
                result.user_Name = queryResult.User_Name;
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

        #region FilterTaskGroupUser
        //Filter
        private MasterDataDbContext db;

        public TaskGroupUserService()
        {
            db = new MasterDataDbContext();
        }

        public TaskGroupUserService(MasterDataDbContext db)
        {
            this.db = db;
        }

       
        public actionResultTaskGroupUserViewModel filter(SearchTaskGroupUserViewModel data)
        {
            try
            {
                var query = db.View_TaskGroupUser.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.TaskGroupUser_Id.Contains(data.key)
                                        || c.TaskGroup_Name.Contains(data.key)
                                        || c.User_Name.Contains(data.key));


                }

                var Item = new List<View_TaskGroupUser>();
                var TotalRow = new List<View_TaskGroupUser>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.TaskGroupUser_Id).ToList();

                var result = new List<SearchTaskGroupUserViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchTaskGroupUserViewModel();

                    resultItem.taskGroupUser_Id = item.TaskGroupUser_Id;
                    resultItem.taskGroupUser_Index = item.TaskGroupUser_Index;
                    resultItem.taskGroup_Index = item.TaskGroup_Index;
                    resultItem.taskGroup_Name = item.TaskGroup_Name;
                    resultItem.user_Index = item.User_Index;
                    resultItem.user_Name = item.User_Name;


                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultTaskGroupUserViewModel = new actionResultTaskGroupUserViewModel();
                actionResultTaskGroupUserViewModel.itemsTaskGroupUser = result.ToList();
                actionResultTaskGroupUserViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultTaskGroupUserViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region DeleteTaskGroupUser
        public Boolean getDelete(TaskGroupUserViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var taskGroupUser = db.MS_TaskGroupUser.Find(data.taskGroupUser_Index);

                if (taskGroupUser != null)
                {
                    taskGroupUser.IsActive = 0;
                    taskGroupUser.IsDelete = 1;


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
                        olog.logging("DeleteTaskGroupUser", msglog);
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

        public String SaveChanges(TaskGroupUserViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var TaskGroupUserOld = db.MS_TaskGroupUser.Find(data.taskGroupUser_Index);

                if (TaskGroupUserOld == null)
                {
                    if (!string.IsNullOrEmpty(data.taskGroupUser_Id))
                    {
                        var query = db.MS_TaskGroupUser.FirstOrDefault(c => c.TaskGroupUser_Id == data.taskGroupUser_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.taskGroupUser_Id))
                    {
                        data.taskGroupUser_Id = "TaskGroupUser_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_TaskGroupUser.FirstOrDefault(c => c.TaskGroupUser_Id == data.taskGroupUser_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.TaskGroupUser_Id == data.taskGroupUser_Id)
                                {
                                    data.taskGroupUser_Id = "TaskGroupUser_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.taskGroupUser_Id = "TaskGroupUser_Id".genAutonumber();

                    MS_TaskGroupUser Model = new MS_TaskGroupUser();

                    Model.TaskGroupUser_Index = Guid.NewGuid();
                    Model.TaskGroupUser_Id = data.taskGroupUser_Id;
                    Model.TaskGroup_Index = data.taskGroup_Index;
                    Model.User_Index = data.user_Index;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_TaskGroupUser.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.taskGroupUser_Id))
                    {
                        if (TaskGroupUserOld.TaskGroupUser_Id != "")
                        {
                            data.taskGroupUser_Id = TaskGroupUserOld.TaskGroupUser_Id;
                        }
                    }
                    else
                    {
                        if (TaskGroupUserOld.TaskGroupUser_Id != data.taskGroupUser_Id)
                        {
                            var query = db.MS_TaskGroupUser.FirstOrDefault(c => c.TaskGroupUser_Id == data.taskGroupUser_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.taskGroupUser_Id = TaskGroupUserOld.TaskGroupUser_Id;
                        }
                    }
                    TaskGroupUserOld.TaskGroupUser_Id = data.taskGroupUser_Id;
                    TaskGroupUserOld.TaskGroup_Index = data.taskGroup_Index;
                    TaskGroupUserOld.User_Index = data.user_Index;
                    TaskGroupUserOld.IsActive = Convert.ToInt32(data.isActive);
                    TaskGroupUserOld.Update_By = data.update_By;
                    TaskGroupUserOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveTaskGroupUser", msglog);
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


        #region autoTaskGroupUserSearchFilter
        public List<ItemListViewModel> autoTaskGroupUserSearchFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_TaskGroupUser.Where(c => c.TaskGroupUser_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.TaskGroupUser_Id,
                        key = s.TaskGroupUser_Id
                    }).Distinct();

                    var query2 = db.View_TaskGroupUser.Where(c => c.TaskGroup_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.TaskGroup_Name,
                        key = s.TaskGroup_Name
                    }).Distinct();

                    var query3 = db.View_TaskGroupUser.Where(c => c.User_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.User_Name,
                        key = s.User_Name

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

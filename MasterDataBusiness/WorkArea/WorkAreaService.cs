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
    public class WorkAreaService
    {
        private MasterDataDbContext db;

        public WorkAreaService()
        {
            db = new MasterDataDbContext();
        }

        public WorkAreaService(MasterDataDbContext db)
        {
            this.db = db;
        }
        #region BeforeCodeWorkArea
        //public List<WorkAreaViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_WorkArea.FromSql("sp_GetWorkArea").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<WorkAreaViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new WorkAreaViewModel();

        //                resultItem.WorkAreaIndex = item.WorkArea_Index;
        //                resultItem.WorkAreaId = item.WorkArea_Id;
        //                resultItem.WorkAreaName = item.WorkArea_Name;
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

        //public String SaveChanges(WorkAreaViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.WorkAreaIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.WorkAreaIndex = Guid.NewGuid();
        //            }                   
        //            if (data.WorkAreaId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "WorkAreaID");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.WorkAreaId = resultParameter.Value.ToString();
        //            }
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int isSystem = 0;
        //            int statusId = 0;
        //            var WorkArea_Index = new SqlParameter("WorkArea_Index", data.WorkAreaIndex);
        //            var WorkArea_Id = new SqlParameter("WorkArea_Id", data.WorkAreaId);
        //            var WorkArea_Name = new SqlParameter("WorkArea_Name", data.WorkAreaName);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_WorkArea  @WorkArea_Index,@WorkArea_Id,@WorkArea_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", WorkArea_Index, WorkArea_Id, WorkArea_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<WorkAreaViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_WorkArea.FromSql("sp_GetWorkArea ").Where(c => c.WorkArea_Index == id).ToList();

        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<WorkAreaViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var WorkArea_Index = new SqlParameter("WorkArea_Index", item.WorkArea_Index);
        //                var WorkArea_Id = new SqlParameter("WorkArea_Id", item.WorkArea_Id);
        //                var WorkArea_Name = new SqlParameter("WorkArea_Name", item.WorkArea_Name);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_WorkArea  @WorkArea_Index,@WorkArea_Id,@WorkArea_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", WorkArea_Index, WorkArea_Id, WorkArea_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        //public List<WorkAreaViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            string pstring = " and WorkArea_Index ='" + id + "'";

        //            var queryResult = context.MS_WorkArea.FromSql("sp_GetWorkArea {0}", pstring).Where(c => c.WorkArea_Index == id).ToList();

        //            var result = new List<WorkAreaViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new WorkAreaViewModel();
        //                resultItem.WorkAreaIndex = item.WorkArea_Index;
        //                resultItem.WorkAreaId = item.WorkArea_Id;
        //                resultItem.WorkAreaName = item.WorkArea_Name;
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
        //public List<WorkAreaViewModel> search(WorkAreaViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<WorkAreaViewModel>();
        //            if (data.WorkAreaId != "" && data.WorkAreaId != null)
        //            {
        //                pwhereFilter = " And WorkArea_Id like N'%" + data.WorkAreaId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter = "";
        //            }

        //            if (data.WorkAreaName != "" && data.WorkAreaName != null)
        //            {
        //                pwhereFilter += " And WorkArea_Name like N'%" + data.WorkAreaName + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }


        //            if (data.WorkAreaId != "" && data.WorkAreaId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_WorkArea.FromSql("sp_GetWorkArea @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new WorkAreaViewModel();

        //                    resultItem.WorkAreaIndex = item.WorkArea_Index;
        //                    resultItem.WorkAreaId = item.WorkArea_Id;
        //                    resultItem.WorkAreaName = item.WorkArea_Name;
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
        //            else if (data.WorkAreaName != "" && data.WorkAreaName != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_WorkArea.FromSql("sp_GetWorkArea @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new WorkAreaViewModel();

        //                    resultItem.WorkAreaIndex = item.WorkArea_Index;
        //                    resultItem.WorkAreaId = item.WorkArea_Id;
        //                    resultItem.WorkAreaName = item.WorkArea_Name;
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

        //            if (data.WorkAreaId == "" && data.WorkAreaName == "")
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


        #region FindWorkArea

        public WorkAreaViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_WorkArea.Where(c => c.WorkArea_Index == id).FirstOrDefault();

                var result = new WorkAreaViewModel();


                result.workArea_Index = queryResult.WorkArea_Index;
                result.workArea_Id = queryResult.WorkArea_Id;
                result.workArea_Name = queryResult.WorkArea_Name;
                result.isActive = queryResult.IsActive;


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region FilterWorkArea
        //Filter
        public actionResultWorkAreaViewModel filter(SearchWorkAreaViewModel data)
        {
            try
            {
                var query = db.MS_WorkArea.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.WorkArea_Id.Contains(data.key)
                                        || c.WorkArea_Name.Contains(data.key));
                }
                var Item = new List<MS_WorkArea>();
                var TotalRow = new List<MS_WorkArea>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.WorkArea_Id).ToList();

                var result = new List<SearchWorkAreaViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchWorkAreaViewModel();

                    resultItem.workArea_Index = item.WorkArea_Index;
                    resultItem.workArea_Id = item.WorkArea_Id;
                    resultItem.workArea_Name = item.WorkArea_Name;



                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultWorkAreaViewModel = new actionResultWorkAreaViewModel();
                actionResultWorkAreaViewModel.itemsWorkArea = result.ToList();
                actionResultWorkAreaViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultWorkAreaViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDelete
        public Boolean getDelete(WorkAreaViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var workArea = db.MS_WorkArea.Find(data.workArea_Index);

                if (workArea != null)
                {
                    workArea.IsActive = 0;
                    workArea.IsDelete = 1;


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
                        olog.logging("DeleteWorkArea", msglog);
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

        public String SaveChanges(WorkAreaViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var WorkAreaOld = db.MS_WorkArea.Find(data.workArea_Index);

                if (WorkAreaOld == null)
                {
                    if (!string.IsNullOrEmpty(data.workArea_Id))
                    {
                        var query = db.MS_WorkArea.FirstOrDefault(c => c.WorkArea_Id == data.workArea_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.workArea_Id))
                    {
                        data.workArea_Id = "WorkArea_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_WorkArea.FirstOrDefault(c => c.WorkArea_Id == data.workArea_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.WorkArea_Id == data.workArea_Id)
                                {
                                    data.workArea_Id = "WorkArea_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.workArea_Id = "WorkArea_Id".genAutonumber();

                    MS_WorkArea Model = new MS_WorkArea();

                    Model.WorkArea_Index = Guid.NewGuid();
                    Model.WorkArea_Id = data.workArea_Id;
                    Model.WorkArea_Name = data.workArea_Name;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_WorkArea.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.workArea_Id))
                    {
                        if (WorkAreaOld.WorkArea_Id != "")
                        {
                            data.workArea_Id = WorkAreaOld.WorkArea_Id;
                        }
                    }
                    else
                    {
                        if (WorkAreaOld.WorkArea_Id != data.workArea_Id)
                        {
                            var query = db.MS_WorkArea.FirstOrDefault(c => c.WorkArea_Id == data.workArea_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.workArea_Id = WorkAreaOld.WorkArea_Id;
                        }
                    }
                    WorkAreaOld.WorkArea_Id = data.workArea_Id;
                    WorkAreaOld.WorkArea_Name = data.workArea_Name;
                    WorkAreaOld.IsActive = Convert.ToInt32(data.isActive);
                    WorkAreaOld.Update_By = data.update_By;
                    WorkAreaOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveWorkArea", msglog);
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

        #region autoSearchWorkAreaFillter

        public List<ItemListViewModel> autoSearchWorkAreaFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_WorkArea.Where(c => c.WorkArea_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.WorkArea_Id,
                        key = s.WorkArea_Id
                    }).Distinct();

                    var query2 = db.MS_WorkArea.Where(c => c.WorkArea_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.WorkArea_Name,
                        key = s.WorkArea_Name
                    }).Distinct();

                    var query = query1.Union(query2).Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {

            }

            return items;
        }
        #endregion

        #region autoSearchWorkArea

        public List<ItemListViewModel>autoSearchWorkArea(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_WorkArea.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.WorkArea_Id.Contains(data.key)
                                                || c.WorkArea_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.WorkArea_Name, c.WorkArea_Index, c.WorkArea_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.WorkArea_Index,
                            id = item.WorkArea_Id,
                            name = item.WorkArea_Id + " - " + item.WorkArea_Name,
                            key = item.WorkArea_Id + " - " + item.WorkArea_Name,
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



        #region filterPopupWorkArea
        //Filter
        public actionResultWorkAreaViewModel filterPopupWorkArea(SearchWorkAreaViewModel data)
        {
            try
            {
                var query = db.MS_WorkArea.AsQueryable();

                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                if (data.listWorkAreaViewModel != null)
                {
                    foreach (var dataItem in data.listWorkAreaViewModel)
                    {
                        query = query.Where(q => q.WorkArea_Index != dataItem.workArea_Index);
                    }
                }
                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.WorkArea_Id.Contains(data.key)
                                        || c.WorkArea_Name.Contains(data.key));


                }
                if (!string.IsNullOrEmpty(data.workArea_Id))
                {
                    query = query.Where(c => c.WorkArea_Id.Contains(data.workArea_Id));
                }
                if (!string.IsNullOrEmpty(data.workArea_Name))
                {
                    query = query.Where(c => c.WorkArea_Name.Contains(data.workArea_Name));

                }



                var Item = new List<MS_WorkArea>();
                var TotalRow = new List<MS_WorkArea>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.WorkArea_Id).ToList();

                var result = new List<SearchWorkAreaViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchWorkAreaViewModel();

                    resultItem.workArea_Index = item.WorkArea_Index;
                    resultItem.workArea_Id = item.WorkArea_Id;
                    resultItem.workArea_Name = item.WorkArea_Name;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultWorkAreaViewModel = new actionResultWorkAreaViewModel();
                actionResultWorkAreaViewModel.itemsWorkArea = result.ToList();
                actionResultWorkAreaViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultWorkAreaViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}

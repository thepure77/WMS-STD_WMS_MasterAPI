using Comone.Utils;
using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.LocationLock;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{
    public class LocationAisleService
    {
        #region BeforeLocationAisle
        //public List<LocationAisleViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_LocationAisle.FromSql("sp_GetLocationAisle").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<LocationAisleViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new LocationAisleViewModel();

        //                resultItem.LocationAisleIndex = item.LocationAisle_Index;
        //                resultItem.LocationLockId = item.LocationLock_Id;
        //                resultItem.LocationLockName = item.LocationLock_Name;
        //                resultItem.IsActive = item.IsActive;
        //                resultItem.IsDelete = item.IsDelete;
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
        //public String SaveChanges(LocationAisleViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int issystem = 0;
        //            int statusid = 0;

        //            if (data.LocationAisleIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.LocationAisleIndex = Guid.NewGuid();
        //            }
        //            if (data.LocationLockId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "LocationLockID");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.LocationLockId = resultParameter.Value.ToString();
        //            }

        //            var LocationAisle_Index = new SqlParameter("LocationAisle_Index", data.LocationAisleIndex);
        //            var LocationLock_Id = new SqlParameter("LocationLock_Id", data.LocationLockId);
        //            var LocationLock_Name = new SqlParameter("LocationLock_Name", data.LocationLockName);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_LocationAisle  @LocationAisle_Index,@LocationLock_Id,@LocationLock_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", LocationAisle_Index, LocationLock_Id, LocationLock_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<LocationAisleViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_LocationAisle.FromSql("sp_GetLocationAisle").ToList();
        //            queryResult.Where(c => c.LocationAisle_Index == id);

        //            var result = new List<LocationAisleViewModel>();
        //            foreach (var item in queryResult.Where(c => c.LocationAisle_Index == id))
        //            {
        //                var resultItem = new LocationAisleViewModel();
        //                resultItem.LocationAisleIndex = item.LocationAisle_Index;
        //                resultItem.LocationLockId = item.LocationLock_Id;
        //                resultItem.LocationLockName = item.LocationLock_Name;
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
        //public List<LocationAisleViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_LocationAisle.FromSql("sp_GetLocationAisle").ToList();
        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<LocationAisleViewModel>();
        //            foreach (var item in queryResult.Where(c => c.LocationAisle_Index == id))
        //            {
        //                var LocationAisle_Index = new SqlParameter("LocationAisle_Index", item.LocationAisle_Index);
        //                var LocationLock_Id = new SqlParameter("LocationLock_Id", item.LocationLock_Id);
        //                var LocationLock_Name = new SqlParameter("LocationLock_Name", item.LocationLock_Name);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_LocationAisle  @LocationAisle_Index,@LocationLock_Id,@LocationLock_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", LocationAisle_Index, LocationLock_Id, LocationLock_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        //public List<LocationAisleViewModel> search(LocationAisleViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<LocationAisleViewModel>();
        //            if (data.LocationLockId != "" && data.LocationLockId != null)
        //            {
        //                pwhereFilter = " And LocationLock_Id like N'%" + data.LocationLockId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter = "";
        //            }

        //            if (data.LocationLockName != "" && data.LocationLockName != null)
        //            {
        //                pwhereFilter += " And LocationLock_Name like N'%" + data.LocationLockName + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }


        //            if (data.LocationLockId != "" && data.LocationLockId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_LocationAisle.FromSql("sp_GetLocationAisle @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new LocationAisleViewModel();
        //                    resultItem.LocationAisleIndex = item.LocationAisle_Index;
        //                    resultItem.LocationLockId = item.LocationLock_Id;
        //                    resultItem.LocationLockName = item.LocationLock_Name;
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
        //            else if (data.LocationLockName != "" && data.LocationLockName != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_LocationAisle.FromSql("sp_GetLocationAisle @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new LocationAisleViewModel();
        //                    resultItem.LocationAisleIndex = item.LocationAisle_Index;
        //                    resultItem.LocationLockId = item.LocationLock_Id;
        //                    resultItem.LocationLockName = item.LocationLock_Name;
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

        //            if (data.LocationLockId == "" && data.LocationLockName == "")
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

        #region FindLocationAisle

        public LocationAisleViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_LocationAisle.Where(c => c.LocationAisle_Index == id).FirstOrDefault();

                var result = new LocationAisleViewModel();


                result.locationAisle_Index = queryResult.LocationAisle_Index;
                result.locationLock_Id = queryResult.LocationLock_Id;
                result.locationLock_Name = queryResult.LocationLock_Name;
                result.isActive = queryResult.IsActive;


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region FilterLocationAisle
        //Filter
        private MasterDataDbContext db;

        public LocationAisleService()
        {
            db = new MasterDataDbContext();
        }

        public LocationAisleService(MasterDataDbContext db)
        {
            this.db = db;
        }


        
        public actionResultLocationAisleViewModel filter(SearchLocationAisleViewModel data)
        {
            try
            {
                var query = db.MS_LocationAisle.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.LocationLock_Id.Contains(data.key)
                                        || c.LocationLock_Name.Contains(data.key));


                }
                if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                {
                    var dateStart = data.create_date.toBetweenDate();
                    var dateEnd = data.create_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                var Item = new List<MS_LocationAisle>();
                var TotalRow = new List<MS_LocationAisle>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.LocationLock_Id).ToList();

                var result = new List<SearchLocationAisleViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchLocationAisleViewModel();

                    resultItem.locationAisle_Index = item.LocationAisle_Index;
                    resultItem.locationLock_Id = item.LocationLock_Id;
                    resultItem.locationLock_Name = item.LocationLock_Name;
                 


                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultLocationAisleViewModel = new actionResultLocationAisleViewModel();
                actionResultLocationAisleViewModel.itemsLocationAisle = result.ToList();
                actionResultLocationAisleViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultLocationAisleViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDelete
        public Boolean getDelete(LocationAisleViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var locationAisle = db.MS_LocationAisle.Find(data.locationAisle_Index);

                if (locationAisle != null)
                {
                    locationAisle.IsActive = 0;
                    locationAisle.IsDelete = 1;


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
                        olog.logging("DeleteLocationAisle", msglog);
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

        public String SaveChanges(LocationAisleViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var LocationAisleOld = db.MS_LocationAisle.Find(data.locationAisle_Index);

                if (LocationAisleOld == null)
                {



                    //var Sys_Key = new SqlParameter("Sys_Key", "UserId");
                    //var resultParameter = new SqlParameter("@result", SqlDbType.Int);
                    //resultParameter.Size = 2000; // some meaningfull value
                    //resultParameter.Direction = ParameterDirection.Output;
                    //db.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);

                    //data.locationAisle_Id = resultParameter.Value.ToString();

                    //data.locationLock_Id = genAutonumber("LocationAisleID");
                    if (!string.IsNullOrEmpty(data.locationLock_Id))
                    {
                        var query = db.MS_LocationAisle.FirstOrDefault(c => c.LocationLock_Id == data.locationLock_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.locationLock_Id))
                    {
                        data.locationLock_Id = "LocationLock_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_LocationAisle.FirstOrDefault(c => c.LocationLock_Id == data.locationLock_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.LocationLock_Id == data.locationLock_Id)
                                {
                                    data.locationLock_Id = "LocationLock_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.locationLock_Id = "LocationLock_Id".genAutonumber();

                    MS_LocationAisle Model = new MS_LocationAisle();

                    Model.LocationAisle_Index = Guid.NewGuid();
                    Model.LocationLock_Id = data.locationLock_Id;
                    Model.LocationLock_Name = data.locationLock_Name;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_LocationAisle.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.locationLock_Id))
                    {
                        if (LocationAisleOld.LocationLock_Id != "")
                        {
                            data.locationLock_Id = LocationAisleOld.LocationLock_Id;
                        }
                    }
                    else
                    {
                        if (LocationAisleOld.LocationLock_Id != data.locationLock_Id)
                        {
                            var query = db.MS_LocationAisle.FirstOrDefault(c => c.LocationLock_Id == data.locationLock_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.locationLock_Id = LocationAisleOld.LocationLock_Id;
                        }
                    }
                    LocationAisleOld.LocationLock_Id = data.locationLock_Id;
                    LocationAisleOld.LocationAisle_Index = data.locationAisle_Index;
                    LocationAisleOld.LocationLock_Name = data.locationLock_Name;
                    LocationAisleOld.IsActive = Convert.ToInt32(data.isActive);
                    LocationAisleOld.Update_By = data.update_By;
                    LocationAisleOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveLocationAisle", msglog);
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

        #region SearchLocationAisle
        public List<ItemListViewModel>autoSearchLocationAisle(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_LocationAisle.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.LocationLock_Name.Contains(data.key));
                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.LocationAisle_Index, c.LocationLock_Id, c.LocationLock_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.LocationAisle_Index,
                            id = item.LocationLock_Id,
                            name = item.LocationLock_Name,
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

        #region AutoLocationAisleFilter

        public List<ItemListViewModel>autoSearchLocationAisleFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_LocationAisle.Where(c => c.LocationLock_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.LocationLock_Id,
                        key = s.LocationLock_Id
                    }).Distinct();

                    var query2 = db.MS_LocationAisle.Where(c => c.LocationLock_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.LocationLock_Name,
                        key = s.LocationLock_Name
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

        #region Export Excel
        public ResultLocationAisleViewModel Export(ResultLocationAisleExportViewModel data)
        {
            try
            {
                var query = db.MS_LocationAisle.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.LocationLock_Id.Contains(data.key)
                                        || c.LocationLock_Name.Contains(data.key));


                }
                if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                {
                    var dateStart = data.create_date.toBetweenDate();
                    var dateEnd = data.create_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                var Item = new List<MS_LocationAisle>();
                var TotalRow = new List<MS_LocationAisle>();

                TotalRow = query.ToList();
                Item = query.OrderBy(o => o.LocationLock_Id).ToList();

                var result = new List<ResultLocationAisleExportViewModel>();
                //var num = 0;
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new ResultLocationAisleExportViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.locationLock_Id = item.LocationLock_Id;
                    resultItem.locationLock_Name = item.LocationLock_Name;
                    resultItem.activeStatus = item.IsActive == 1 ? "เปิดใช้งาน" : "ปิดใช้งาน";
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.cancel_By = item.Cancel_By == null ? "" : item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date != null ? item.Cancel_Date.Value.ToString("dd/MM/yyyy") : "";
                    result.Add(resultItem);
                    num++;


                }

                var count = TotalRow.Count;

                var locationAisleExportViewModel = new ResultLocationAisleViewModel();
                locationAisleExportViewModel.itemsLocationAisle = result.ToList();
                locationAisleExportViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return locationAisleExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

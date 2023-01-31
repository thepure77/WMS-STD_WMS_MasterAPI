using Comone.Utils;
using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.LocationType;
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
    public class LocationTypeService
    {
        #region BeforeCodeLocationType
        //public List<LocationTypeViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_LocationType.FromSql("sp_GetLocationType").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<LocationTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new LocationTypeViewModel();

        //                resultItem.LocationTypeIndex = item.LocationType_Index;
        //                resultItem.LocationTypeId = item.LocationType_Id;
        //                resultItem.LocationTypeName = item.LocationType_Name;
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
        //public String SaveChanges(LocationTypeViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int issystem = 0;
        //            int statusid = 0;

        //            if (data.LocationTypeIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.LocationTypeIndex = Guid.NewGuid();
        //            }
        //            if (data.LocationTypeId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "LocationTypeID");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.LocationTypeId = resultParameter.Value.ToString();
        //            }

        //            var LocationType_Index = new SqlParameter("LocationType_Index", data.LocationTypeIndex);
        //            var LocationType_Id = new SqlParameter("LocationType_Id", data.LocationTypeId);
        //            var LocationType_Name = new SqlParameter("LocationType_Name", data.LocationTypeName);
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
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_LocationType  @LocationType_Index,@LocationType_Id,@LocationType_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", LocationType_Index, LocationType_Id, LocationType_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<LocationTypeViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_LocationType.FromSql("sp_GetLocationType").Where(c => c.LocationType_Index == id).ToList();

        //            var result = new List<LocationTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new LocationTypeViewModel();
        //                resultItem.LocationTypeIndex = item.LocationType_Index;
        //                resultItem.LocationTypeId = item.LocationType_Id;
        //                resultItem.LocationTypeName = item.LocationType_Name;
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
        //public List<LocationTypeViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_LocationType.FromSql("sp_GetLocationType").Where(c => c.LocationType_Index == id).ToList();
        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<LocationTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var LocationType_Index = new SqlParameter("LocationType_Index", item.LocationType_Index);
        //                var LocationType_Id = new SqlParameter("LocationType_Id", item.LocationType_Id);
        //                var LocationType_Name = new SqlParameter("LocationType_Name", item.LocationType_Name);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_LocationType  @LocationType_Index,@LocationType_Id,@LocationType_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", LocationType_Index, LocationType_Id, LocationType_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        //public List<LocationTypeViewModel> search(LocationTypeViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<LocationTypeViewModel>();
        //            if (data.LocationTypeId != "" && data.LocationTypeId != null)
        //            {
        //                pwhereFilter = " And LocationType_Id like N'%" + data.LocationTypeId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter = "";
        //            }

        //            if (data.LocationTypeName != "" && data.LocationTypeName != null)
        //            {
        //                pwhereFilter += " And LocationType_Name like N'%" + data.LocationTypeName + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }


        //            if (data.LocationTypeId != "" && data.LocationTypeId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_LocationType.FromSql("sp_GetLocationType @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new LocationTypeViewModel();
        //                    resultItem.LocationTypeIndex = item.LocationType_Index;
        //                    resultItem.LocationTypeId = item.LocationType_Id;
        //                    resultItem.LocationTypeName = item.LocationType_Name;
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
        //            else if (data.LocationTypeName != "" && data.LocationTypeName != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_LocationType.FromSql("sp_GetLocationType @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new LocationTypeViewModel();
        //                    resultItem.LocationTypeIndex = item.LocationType_Index;
        //                    resultItem.LocationTypeId = item.LocationType_Id;
        //                    resultItem.LocationTypeName = item.LocationType_Name;
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

        //            if (data.LocationTypeId == "" && data.LocationTypeName == "")
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

        #region FindLocationType

        public LocationTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_LocationType.Where(c => c.LocationType_Index == id).FirstOrDefault();

                var result = new LocationTypeViewModel();


                result.locationType_Index = queryResult.LocationType_Index;
                result.locationType_Id = queryResult.LocationType_Id;
                result.locationType_Name = queryResult.LocationType_Name;
                result.isActive = queryResult.IsActive;


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region FilterLocationType
        //Filter
        private MasterDataDbContext db;

        public LocationTypeService()
        {
            db = new MasterDataDbContext();
        }

        public LocationTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }


        
        public actionResultLocationTypeViewModel filter(SearchLocationTypeViewModel data)
        {
            try
            {
                var query = db.MS_LocationType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.LocationType_Id.Contains(data.key)
                                        || c.LocationType_Name.Contains(data.key));


                }
                if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                {
                    var dateStart = data.create_date.toBetweenDate();
                    var dateEnd = data.create_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                var Item = new List<MS_LocationType>();
                var TotalRow = new List<MS_LocationType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.LocationType_Id).ToList();

                var result = new List<SearchLocationTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchLocationTypeViewModel();

                    resultItem.locationType_Index = item.LocationType_Index;
                    resultItem.locationType_Id = item.LocationType_Id;
                    resultItem.locationType_Name = item.LocationType_Name;
                 


                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultLocationTypeViewModel = new actionResultLocationTypeViewModel();
                actionResultLocationTypeViewModel.itemsLocationType = result.ToList();
                actionResultLocationTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key =data.key };

                return actionResultLocationTypeViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDelete
        public Boolean getDelete(LocationTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var locationType = db.MS_LocationType.Find(data.locationType_Index);

                if (locationType != null)
                {
                    locationType.IsActive = 0;
                    locationType.IsDelete = 1;


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
                        olog.logging("DeleteLocationType", msglog);
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

        public String SaveChanges(LocationTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var LocationTypeOld = db.MS_LocationType.Find(data.locationType_Index);

                if (LocationTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.locationType_Id))
                    {
                        var query = db.MS_LocationType.FirstOrDefault(c => c.LocationType_Id == data.locationType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.locationType_Id))
                    {
                        data.locationType_Id = "LocationType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_LocationType.FirstOrDefault(c => c.LocationType_Id == data.locationType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.LocationType_Id == data.locationType_Id)
                                {
                                    data.locationType_Id = "LocationType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.locationType_Id = "LocationType_Id".genAutonumber();

                    MS_LocationType Model = new MS_LocationType();

                    Model.LocationType_Index = Guid.NewGuid();
                    Model.LocationType_Id = data.locationType_Id;
                    Model.LocationType_Name = data.locationType_Name;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_LocationType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.locationType_Id))
                    {
                        if (LocationTypeOld.LocationType_Id != "")
                        {
                            data.locationType_Id = LocationTypeOld.LocationType_Id;
                        }
                    }
                    else
                    {
                        if (LocationTypeOld.LocationType_Id != data.locationType_Id)
                        {
                            var query = db.MS_LocationType.FirstOrDefault(c => c.LocationType_Id == data.locationType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.locationType_Id = LocationTypeOld.LocationType_Id;
                        }
                    }
                    LocationTypeOld.LocationType_Id = data.locationType_Id;
                    LocationTypeOld.LocationType_Name = data.locationType_Name;
                    LocationTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    LocationTypeOld.Update_By = data.update_By;
                    LocationTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveLocationType", msglog);
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

        #region SearchLocationType


        public List<ItemListViewModel> autoSearchLocationType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_LocationType.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.LocationType_Name.Contains(data.key));
                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.LocationType_Index, c.LocationType_Id, c.LocationType_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.LocationType_Index,
                            id = item.LocationType_Id,
                            name = item.LocationType_Name,
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

        #region autoSearchLocationTypeFilter
        public List<ItemListViewModel>autoSearchLocationTypeFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_LocationType.Where(c => c.LocationType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.LocationType_Name,
                        key = s.LocationType_Name
                    }).Distinct();

                    var query2 = db.MS_LocationType.Where(c => c.LocationType_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.LocationType_Id,
                        key = s.LocationType_Id
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
        public ResultLocationTypeViewModel Export(ResultLocationTypeExportViewModel data)
        {
            try
            {
                var query = db.MS_LocationType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.LocationType_Id.Contains(data.key)
                                        || c.LocationType_Name.Contains(data.key));


                }
                if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                {
                    var dateStart = data.create_date.toBetweenDate();
                    var dateEnd = data.create_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                var Item = new List<MS_LocationType>();
                var TotalRow = new List<MS_LocationType>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.LocationType_Id).ToList();

                var result = new List<ResultLocationTypeExportViewModel>();
                //var num = 0;
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new ResultLocationTypeExportViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.locationType_Id = item.LocationType_Id;
                    resultItem.locationType_Name = item.LocationType_Name;
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

                var locationTypeExportViewModel = new ResultLocationTypeViewModel();
                locationTypeExportViewModel.itemsLocationType = result.ToList();
                locationTypeExportViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return locationTypeExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

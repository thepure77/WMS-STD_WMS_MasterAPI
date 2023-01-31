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
using System.Text;

namespace MasterDataBusiness
{
    public class ZoneLocationService
    {
        private MasterDataDbContext db;

        public ZoneLocationService()
        {
            db = new MasterDataDbContext();
        }

        public ZoneLocationService(MasterDataDbContext db)
        {
            this.db = db;
        }


        #region filterZoneLocation
        public actionResultZoneLocationViewModel filter(SearchZoneLocationViewModel data)
        {
            try
            {
                var query = db.View_ZoneLocation.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.ZoneLocation_Id.Contains(data.key)
                                         || c.Zone_Id.Contains(data.key)
                                         || c.Zone_Name.Contains(data.key)
                                         || c.Location_Id.Contains(data.key)
                                         || c.Location_Name.Contains(data.key));
                }

                var Item = new List<View_ZoneLocation>();
                var TotalRow = new List<View_ZoneLocation>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ZoneLocation_Id).ToList();

                var result = new List<SearchZoneLocationViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchZoneLocationViewModel();

                    resultItem.zoneLocation_Index = item.ZoneLocation_Index;
                    resultItem.zoneLocation_Id = item.ZoneLocation_Id;
                    resultItem.zone_Index = item.Zone_Index;
                    resultItem.zone_Id = item.Zone_Id;
                    resultItem.zone_Name = item.Zone_Name;
                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultZoneLocationViewModel = new actionResultZoneLocationViewModel();
                actionResultZoneLocationViewModel.itemsZoneLocation = result.ToList();
                actionResultZoneLocationViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultZoneLocationViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(ZoneLocationViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ZoneLocationOld = db.MS_ZoneLocation.Find(data.zoneLocation_Index);

                if (ZoneLocationOld == null)
                {
                    if (!string.IsNullOrEmpty(data.zoneLocation_Id))
                    {
                        var query = db.MS_ZoneLocation.FirstOrDefault(c => c.ZoneLocation_Id == data.zoneLocation_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.zoneLocation_Id))
                    {
                        data.zoneLocation_Id = "ZoneLocation_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_ZoneLocation.FirstOrDefault(c => c.ZoneLocation_Id == data.zoneLocation_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ZoneLocation_Id == data.zoneLocation_Id)
                                {
                                    data.zoneLocation_Id = "ZoneLocation_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.zoneLocation_Id = "ZoneLocation_Id".genAutonumber();

                    MS_ZoneLocation Model = new MS_ZoneLocation();

                    Model.ZoneLocation_Index = Guid.NewGuid();
                    Model.ZoneLocation_Id = data.zoneLocation_Id;
                    Model.Zone_Index = data.zone_Index;
                    Model.Location_Index = data.location_Index;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_ZoneLocation.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.zoneLocation_Id))
                    {
                        if (ZoneLocationOld.ZoneLocation_Id != "")
                        {
                            data.zoneLocation_Id = ZoneLocationOld.ZoneLocation_Id;
                        }
                    }
                    else
                    {
                        if (ZoneLocationOld.ZoneLocation_Id != data.zoneLocation_Id)
                        {
                            var query = db.MS_ZoneLocation.FirstOrDefault(c => c.ZoneLocation_Id == data.zoneLocation_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.zoneLocation_Id = ZoneLocationOld.ZoneLocation_Id;
                        }
                    }
                    ZoneLocationOld.ZoneLocation_Id = data.zoneLocation_Id;
                    ZoneLocationOld.Zone_Index = data.zone_Index;
                    ZoneLocationOld.Location_Index = data.location_Index;
                    ZoneLocationOld.IsActive = Convert.ToInt32(data.isActive);
                    ZoneLocationOld.Update_By = data.create_By;
                    ZoneLocationOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveZoneLocation", msglog);
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

        #region find
        public ZoneLocationViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_ZoneLocation.Where(c => c.ZoneLocation_Index == id).FirstOrDefault();

                var result = new ZoneLocationViewModel();

                result.zoneLocation_Index = queryResult.ZoneLocation_Index;
                result.zoneLocation_Id = queryResult.ZoneLocation_Id;
                result.zone_Index = queryResult.Zone_Index;
                result.zone_Id = queryResult.Zone_Id;
                result.zone_Name = queryResult.Zone_Name;
                result.location_Index = queryResult.Location_Index;
                result.location_Id = queryResult.Location_Id;
                result.location_Name = queryResult.Location_Name;
                result.key = queryResult.Zone_Id + " - " + queryResult.Zone_Name;
                result.key2 = queryResult.Location_Id + " - " + queryResult.Location_Name;
                result.isActive = queryResult.IsActive;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDelete
        public Boolean getDelete(ZoneLocationViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var warehouse = db.MS_ZoneLocation.Find(data.zoneLocation_Index);

                if (warehouse != null)
                {
                    warehouse.IsActive = 0;
                    warehouse.IsDelete = 1;


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
                        olog.logging("DeleteZoneLocation", msglog);
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

        //public List<ZoneLocationViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {


        //            string pstring = "";

        //            var strwhere = new SqlParameter("@strwhere", pstring);
        //            var PageNumber = new SqlParameter("@PageNumber", 1);
        //            var RowspPage = new SqlParameter("@RowspPage", 100);

        //            var queryResultTotal = context.View_GetZoneLocation.FromSql("sp_GetZoneLocationPagination @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber, RowspPage).ToList();

        //            var strwhere1 = new SqlParameter("@strwhere", pstring);
        //            var PageNumber1 = new SqlParameter("@PageNumber", 1);
        //            var RowspPage1 = new SqlParameter("@RowspPage", 30);
        //            var query = context.View_GetZoneLocation.FromSql("sp_GetZoneLocationPagination @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber, RowspPage).ToList();

        //            var result = new List<ZoneLocationViewModel>();
        //            foreach (var item in query)
        //            {
        //                var resultItem = new ZoneLocationViewModel();
        //                resultItem.ZoneIndex = item.Zone_Index;
        //                resultItem.ZoneName = item.Zone_Name;
        //                resultItem.LocationIndex = item.Location_Index;
        //                resultItem.LocationName = item.Location_Name;
        //                resultItem.ZoneLocationIndex = item.ZoneLocation_Index;
        //                resultItem.ZoneLocationId = item.ZoneLocation_Id;
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

        //public String SaveChanges(ZoneLocationViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {

        //            int Isactive = 1;
        //            int Isdelete = 0;
        //            int Issystem = 0;
        //            int statusId = 0;

        //            if (data.ZoneLocationIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.ZoneLocationIndex = Guid.NewGuid();
        //            }
        //            if (data.ZoneLocationId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "ZoneLocationId");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.ZoneLocationId = resultParameter.Value.ToString();
        //            }
        //            var ZoneLocation_Index = new SqlParameter("ZoneLocation_Index", data.ZoneLocationIndex);
        //            var ZoneLocation_Id = new SqlParameter("ZoneLocation_Id", data.ZoneLocationId);
        //            var Zone_Index = new SqlParameter("Zone_Index", data.ZoneIndex);
        //            var Location_Index = new SqlParameter("Location_Index", data.LocationIndex);
        //            var IsActive = new SqlParameter("IsActive", Isactive);
        //            var IsDelete = new SqlParameter("IsDelete", Isdelete);
        //            var IsSystem = new SqlParameter("IsSystem", Issystem);
        //            var Status_Id = new SqlParameter("Status_Id", statusId);
        //            var Create_By = new SqlParameter("Create_By", "");
        //            var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        //            var Update_By = new SqlParameter("Update_By", "");
        //            var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        //            var Cancel_By = new SqlParameter("Cancel_By", "");
        //            var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ZoneLocation  @ZoneLocation_Index,@ZoneLocation_Id,@Zone_Index,@Location_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ZoneLocation_Index, ZoneLocation_Id, Zone_Index, Location_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ZoneLocationViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            string pstring = " and ZoneLocation_Index ='" + id + "'";

        //            var queryResult = context.MS_ZoneLocation.FromSql("sp_GetZoneLocation {0}", pstring).Where(c => c.ZoneLocation_Index == id).ToList();

        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<ZoneLocationViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var ZoneLocation_Index = new SqlParameter("ZoneLocation_Index", item.ZoneLocation_Index);
        //                var ZoneLocation_Id = new SqlParameter("ZoneLocation_Id", item.ZoneLocation_Id);
        //                var Zone_Index = new SqlParameter("Zone_Index", item.Zone_Index);
        //                var Location_Index = new SqlParameter("Location_Index", item.Location_Index);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ZoneLocation  @ZoneLocation_Index,@ZoneLocation_Id,@Zone_Index,@Location_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ZoneLocation_Index, ZoneLocation_Id, Zone_Index, Location_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        //public List<ZoneLocationViewModelPagination> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var strwhere = new SqlParameter("@strwhere", " and ZoneLocation_Index ='" + id + "'");
        //            var queryResult = context.View_GetZoneLocation.FromSql("sp_GetZoneLocationPagination @strwhere", strwhere).ToList();

        //            var result = new List<ZoneLocationViewModelPagination>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ZoneLocationViewModelPagination();
        //                resultItem.ZoneLocationIndex = item.ZoneLocation_Index;
        //                resultItem.ZoneLocationId = item.ZoneLocation_Id;
        //                resultItem.ZoneIndex = item.Zone_Index;
        //                resultItem.ZoneName = item.Zone_Name;
        //                resultItem.LocationIndex = item.Location_Index;
        //                resultItem.LocationName = item.Location_Name;
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

        //public actionResultViewModel search(ZoneLocationViewModelPagination data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pstring = "";

        //            if (!string.IsNullOrEmpty(data.ZoneLocationId))
        //            {
        //                pstring += " And ZoneLocation_Id like N'%" + data.ZoneLocationId + "%'";
        //            }
        //            if (!string.IsNullOrEmpty(data.ZoneName))
        //            {
        //                pstring += " And Zone_Name like N'%" + data.ZoneName + "%'";
        //            }
        //            if (!string.IsNullOrEmpty(data.LocationName))
        //            {
        //                pstring += " And Location_Name like N'%" + data.LocationName + "%'";
        //            }

        //            var strwhere = new SqlParameter("@strwhere", pstring);
        //            var PageNumber = new SqlParameter("@PageNumber", 1);
        //            var RowspPage = new SqlParameter("@RowspPage", 10000);

        //            var queryResultTotal = context.View_GetZoneLocation.FromSql("sp_GetZoneLocationPagination @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber, RowspPage).ToList();

        //            var strwhere1 = new SqlParameter("@strwhere", pstring);
        //            var PageNumber1 = new SqlParameter("@PageNumber", data.CurrentPage);
        //            var RowspPage1 = new SqlParameter("@RowspPage", data.PerPage);
        //            var query = context.View_GetZoneLocation.FromSql("sp_GetZoneLocationPagination @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber1, RowspPage1).ToList();

        //            var result = new List<ZoneLocationViewModelPagination>();
        //            foreach (var item in query)
        //            {
        //                var resultItem = new ZoneLocationViewModelPagination();
        //                resultItem.ZoneIndex = item.Zone_Index;
        //                resultItem.ZoneName = item.Zone_Name;
        //                resultItem.LocationIndex = item.Location_Index;
        //                resultItem.LocationName = item.Location_Name;
        //                resultItem.ZoneLocationIndex = item.ZoneLocation_Index;
        //                resultItem.ZoneLocationId = item.ZoneLocation_Id;
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
        //            var actionResult = new actionResultViewModel();
        //            actionResult.items = result.ToList();
        //            actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage };

        //            return actionResult;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public List<ZoneLocationViewModel> zoneLocationFilter(ZoneLocationViewModel model)
        {
            try
            {
                var items = new List<ZoneLocationViewModel>();

                var query = db.MS_ZoneLocation.AsQueryable();

                if (!string.IsNullOrEmpty(model.zone_Index.ToString()))
                {
                    query = query.Where(c => c.Zone_Index == model.zone_Index);
                }

                var result = query.ToList();


                foreach (var item in result)
                {
                    var resultItem = new ZoneLocationViewModel
                    {
                        zoneLocation_Index = item.ZoneLocation_Index,
                        zoneLocation_Id = item.ZoneLocation_Id,
                        zone_Index = item.Zone_Index,
                        location_Index = item.Location_Index,
                    };

                    items.Add(resultItem);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

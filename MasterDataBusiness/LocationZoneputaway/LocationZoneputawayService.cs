using Comone.Utils;
using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.LocationZoneputaway;
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
    public class LocationZoneputawayService
    {
        private MasterDataDbContext db;

        public LocationZoneputawayService()
        {
            db = new MasterDataDbContext();
        }

        public LocationZoneputawayService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterLocationZoneputaway
        public actionResultLocationZoneputawayViewModel filter(SearchLocationZoneputawayViewModel data)
        {
            try
            {
                var query = db.View_LocationZoneputaway.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.LocationZoneputaway_Id.Contains(data.key)
                                         || c.Zoneputaway_Name.Contains(data.key)
                                         || c.Location_Name.Contains(data.key));
                }
                if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                {
                    var dateStart = data.create_date.toBetweenDate();
                    var dateEnd = data.create_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                var Item = new List<View_LocationZoneputaway>();
                var TotalRow = new List<View_LocationZoneputaway>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.LocationZoneputaway_Id).ToList();

                var result = new List<SearchLocationZoneputawayViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchLocationZoneputawayViewModel();

                    resultItem.locationZoneputaway_Index = item.LocationZoneputaway_Index;
                    resultItem.locationZoneputaway_Id = item.LocationZoneputaway_Id;
                    resultItem.zoneputaway_Name = item.Zoneputaway_Name;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultLocationZoneputawayViewModel = new actionResultLocationZoneputawayViewModel();
                actionResultLocationZoneputawayViewModel.itemsLocationZoneputaway = result.ToList();
                actionResultLocationZoneputawayViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultLocationZoneputawayViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(LocationZoneputawayViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var LocationZoneputawayOld = db.MS_LocationZoneputaway.Find(data.locationZoneputaway_Index);

                if (LocationZoneputawayOld == null)
                {
                    if (!string.IsNullOrEmpty(data.locationZoneputaway_Id))
                    {
                        var query = db.MS_LocationZoneputaway.FirstOrDefault(c => c.LocationZoneputaway_Id == data.locationZoneputaway_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.locationZoneputaway_Id))
                    {
                        data.locationZoneputaway_Id = "LocationZoneputaway_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_LocationZoneputaway.FirstOrDefault(c => c.LocationZoneputaway_Id == data.locationZoneputaway_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.LocationZoneputaway_Id == data.locationZoneputaway_Id)
                                {
                                    data.locationZoneputaway_Id = "LocationZoneputaway_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //if (!string.IsNullOrEmpty(data.locationZoneputaway_Id))
                    //{
                    //    var query = db.View_LocationZoneputaway.FirstOrDefault(c => c.LocationZoneputaway_Id == data.locationZoneputaway_Id && c.IsActive == 1);
                    //    if (query != null)
                    //    {
                    //        return "Fail";
                    //    }
                    //}
                    //if (string.IsNullOrEmpty(data.locationZoneputaway_Id))
                    //{
                    //    data.locationZoneputaway_Id = "LocationZoneputaway_Id".genAutonumber();
                    //}

                    MS_LocationZoneputaway Model = new MS_LocationZoneputaway();

                    Model.LocationZoneputaway_Index = Guid.NewGuid();
                    Model.LocationZoneputaway_Id = data.locationZoneputaway_Id;
                    Model.Zoneputaway_Index = data.zoneputaway_Index;
                    Model.Location_Index = data.location_Index;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_LocationZoneputaway.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.locationZoneputaway_Id))
                    {
                        if (LocationZoneputawayOld.LocationZoneputaway_Id != "")
                        {
                            data.locationZoneputaway_Id = LocationZoneputawayOld.LocationZoneputaway_Id;
                        }
                    }
                    else
                    {
                        if (LocationZoneputawayOld.LocationZoneputaway_Id != data.locationZoneputaway_Id)
                        {
                            var query = db.MS_LocationZoneputaway.FirstOrDefault(c => c.LocationZoneputaway_Id == data.locationZoneputaway_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.locationZoneputaway_Id = LocationZoneputawayOld.LocationZoneputaway_Id;
                        }
                    }
                    LocationZoneputawayOld.LocationZoneputaway_Id = data.locationZoneputaway_Id;
                    LocationZoneputawayOld.Zoneputaway_Index = data.zoneputaway_Index;
                    LocationZoneputawayOld.Location_Index = data.location_Index;
                    LocationZoneputawayOld.IsActive = Convert.ToInt32(data.isActive);
                    LocationZoneputawayOld.Update_By = data.create_By;
                    LocationZoneputawayOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveLocationZoneputaway", msglog);
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
        public LocationZoneputawayViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_LocationZoneputaway.Where(c => c.LocationZoneputaway_Index == id).FirstOrDefault();

                var result = new LocationZoneputawayViewModel();


                result.locationZoneputaway_Index = queryResult.LocationZoneputaway_Index;
                result.locationZoneputaway_Id = queryResult.LocationZoneputaway_Id;
                result.zoneputaway_Index = queryResult.Zoneputaway_Index;
                result.zoneputaway_Id = queryResult.Zoneputaway_Id;
                result.zoneputaway_Name = queryResult.Zoneputaway_Name;
                result.location_Index = queryResult.Location_Index;
                result.location_Id = queryResult.Location_Id;
                result.location_Name = queryResult.Location_Name;
                result.key = queryResult.Zoneputaway_Id + " - " + queryResult.Zoneputaway_Name;
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
        public Boolean getDelete(LocationZoneputawayViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var warehouse = db.MS_LocationZoneputaway.Find(data.locationZoneputaway_Index);

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
                        olog.logging("DeleteLocationZoneputaway", msglog);
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

        public List<View_LocationZoneputaway> getLocationZoneputaway(SearchLocationZoneputawayViewModel data)
        {
            try
            {
                var query = db.View_LocationZoneputaway.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                var Item = new List<View_LocationZoneputaway>();

                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

               var  listlocPut = query.ToList();

                Item = listlocPut.OrderBy(o => o.LocationZoneputaway_Id).ToList();

                var result = new List<SearchLocationZoneputawayViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchLocationZoneputawayViewModel();

                    resultItem.locationZoneputaway_Index = item.LocationZoneputaway_Index;
                    resultItem.locationZoneputaway_Id = item.LocationZoneputaway_Id;
                    resultItem.zoneputaway_Name = item.Zoneputaway_Name;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                return Item;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Export Excel
        public ResultLocationZoneputawayViewModel Export(ResultLocationZoneputawayExportViewModel data)
        {
            try
            {
                var query = db.View_LocationZoneputaway.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.LocationZoneputaway_Id.Contains(data.key)
                                         || c.Zoneputaway_Name.Contains(data.key)
                                         || c.Location_Name.Contains(data.key));
                }
                if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                {
                    var dateStart = data.create_date.toBetweenDate();
                    var dateEnd = data.create_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                var Item = new List<View_LocationZoneputaway>();
                var TotalRow = new List<View_LocationZoneputaway>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.LocationZoneputaway_Id).ToList();

                var result = new List<ResultLocationZoneputawayExportViewModel>();
                //var num = 0;
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new ResultLocationZoneputawayExportViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.locationZoneputaway_Id = item.LocationZoneputaway_Id;
                    resultItem.zoneputaway_Name = item.Zoneputaway_Name;
                    resultItem.location_Name = item.Location_Name;
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

                var zoneputawayExportViewModel = new ResultLocationZoneputawayViewModel();
                zoneputawayExportViewModel.itemsLocationZoneputway = result.ToList();
                zoneputawayExportViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };
                
                return zoneputawayExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

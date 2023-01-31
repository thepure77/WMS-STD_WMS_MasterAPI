using Comone.Utils;
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
    public class LocationEquipmentService
    {

        #region FindLocationEquipment

        public LocationEquipmentViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_LocationEquipment.Where(c => c.LocationEquipment_Index == id).FirstOrDefault();

                var result = new LocationEquipmentViewModel();


                result.locationEquipment_Index = queryResult.LocationEquipment_Index;
                result.locationEquipment_Id = queryResult.LocationEquipment_Id;
                result.location_Index = queryResult.Location_Index;
                result.location_Name = queryResult.Location_Name;
                result.equipment_Index = queryResult.Equipment_Index;
                result.equipment_Name = queryResult.Equipment_Name;
                result.isActive = queryResult.IsActive;


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region FilterLocationEquipment
        //Filter
        private MasterDataDbContext db;

        public LocationEquipmentService()
        {
            db = new MasterDataDbContext();
        }

        public LocationEquipmentService(MasterDataDbContext db)
        {
            this.db = db;
        }


        public actionResultLocationEquipmentViewModel filter(SearchLocationEquipmentViewModel data)
        {
            try
            {
                var query = db.View_LocationEquipment.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.LocationEquipment_Id.Contains(data.key)
                                        || c.Location_Name.Contains(data.key)
                                        || c.Equipment_Name.Contains(data.key));


                }

                var Item = new List<View_LocationEquipment>();
                var TotalRow = new List<View_LocationEquipment>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.LocationEquipment_Id).ToList();

                var result = new List<SearchLocationEquipmentViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchLocationEquipmentViewModel();

                    resultItem.locationEquipment_Index = item.LocationEquipment_Index;
                    resultItem.locationEquipment_Id = item.LocationEquipment_Id;
                    resultItem.equipment_Name = item.Equipment_Name;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.location_Index = item.Location_Index;
           

                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultLocationEquipmentViewModel = new actionResultLocationEquipmentViewModel();
                actionResultLocationEquipmentViewModel.itemsLocationEquipment = result.ToList();
                actionResultLocationEquipmentViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultLocationEquipmentViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDelete
        public Boolean getDelete(LocationEquipmentViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var locationEquipment = db.MS_LocationEquipment.Find(data.locationEquipment_Index);

                if (locationEquipment != null)
                {
                    locationEquipment.IsActive = 0;
                    locationEquipment.IsDelete = 1;


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
                        olog.logging("DeleteLocationEquipment", msglog);
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

        public String SaveChanges(LocationEquipmentViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var LocationEquipmentOld = db.MS_LocationEquipment.Find(data.locationEquipment_Index);

                if (LocationEquipmentOld == null)
                {
                    if (!string.IsNullOrEmpty(data.locationEquipment_Id))
                    {
                        var query = db.MS_LocationEquipment.FirstOrDefault(c => c.LocationEquipment_Id == data.locationEquipment_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.locationEquipment_Id))
                    {
                        data.locationEquipment_Id = "LocationEquipment_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_LocationEquipment.FirstOrDefault(c => c.LocationEquipment_Id == data.locationEquipment_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.LocationEquipment_Id == data.locationEquipment_Id)
                                {
                                    data.locationEquipment_Id = "LocationEquipment_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.locationEquipment_Id = "LocationEquipment_Id".genAutonumber();

                    MS_LocationEquipment Model = new MS_LocationEquipment();

                    Model.LocationEquipment_Index = Guid.NewGuid();
                    Model.LocationEquipment_Id = data.locationEquipment_Id;
                    Model.Location_Index = data.location_Index;
                    Model.Equipment_Index = data.equipment_Index;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_LocationEquipment.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.locationEquipment_Id))
                    {
                        if (LocationEquipmentOld.LocationEquipment_Id != "")
                        {
                            data.locationEquipment_Id = LocationEquipmentOld.LocationEquipment_Id;
                        }
                    }
                    else
                    {
                        if (LocationEquipmentOld.LocationEquipment_Id != data.locationEquipment_Id)
                        {
                            var query = db.MS_LocationEquipment.FirstOrDefault(c => c.LocationEquipment_Id == data.locationEquipment_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.locationEquipment_Id = LocationEquipmentOld.LocationEquipment_Id;
                        }
                    }
                    LocationEquipmentOld.LocationEquipment_Id = data.locationEquipment_Id;
                    LocationEquipmentOld.Location_Index = data.location_Index;
                    LocationEquipmentOld.Equipment_Index = data.equipment_Index;
                    LocationEquipmentOld.IsActive = Convert.ToInt32(data.isActive);
                    LocationEquipmentOld.Update_By = data.update_By;
                    LocationEquipmentOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveLocationEquipment", msglog);
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

        #region AutoLocationEquipment

        public List<ItemListViewModel>autoSearchLocationEquipmentFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_LocationEquipment.Where(c => c.Location_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Location_Name,
                        key = s.Location_Name
                    }).Distinct();

                    var query2 = db.View_LocationEquipment.Where(c => c.Equipment_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Equipment_Name,
                        key = s.Equipment_Name
                    }).Distinct();

                    var query3 = db.View_LocationEquipment.Where(c => c.LocationEquipment_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.LocationEquipment_Id,
                        key = s.LocationEquipment_Id
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


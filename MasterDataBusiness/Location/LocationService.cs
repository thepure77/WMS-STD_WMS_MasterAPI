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
using System.Linq;

namespace MasterDataBusiness
{
    public class LocationService
    {


        #region FindLocation

        public LocationViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_Location.Where(c => c.Location_Index == id).FirstOrDefault();

                var result = new LocationViewModel();


                result.location_Index = queryResult.Location_Index;
                result.warehouse_Index = queryResult.Warehouse_Index;
                result.room_Index = queryResult.Room_Index;
                result.locationType_Index = queryResult.LocationType_Index;
                result.location_Id = queryResult.Location_Id;
                result.locationType_Name = queryResult.LocationType_Name;
                result.location_Name = queryResult.Location_Name;
                result.warehouse_Name = queryResult.Warehouse_Name;
                result.room_Name = queryResult.Room_Name;
                result.locationAisle_Name = queryResult.LocationLock_Name;
                result.locationAisle_Index = queryResult.LocationAisle_Index;
                result.location_Bay = queryResult.Location_Bay;
                result.location_Depth = queryResult.Location_Depth;
                result.location_Level = queryResult.Location_Level;
                result.max_Qty = queryResult.max_Qty;
                result.max_Weight = queryResult.max_Weight;
                result.max_Volume = queryResult.max_Volume;
                result.max_Pallet = queryResult.max_Pallet;
                result.putAway_Seq = queryResult.putAway_Seq;
                result.picking_Seq = queryResult.picking_Seq;
                result.isActive = queryResult.IsActive;
                result.isDelete = queryResult.IsDelete;
                result.blockPut = queryResult.BlockPut;
                result.blockPick = queryResult.BlockPick;
                result.document_Remark = queryResult.Document_Remark;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion


        #region FilterLocation
        private MasterDataDbContext db;

        public LocationService()
        {
            db = new MasterDataDbContext();
        }

        public LocationService(MasterDataDbContext db)
        {
            this.db = db;
        }


        public actionResultLocationViewModel filter(SearchLocationViewModel data)
        {
            try
            {
                var query = db.View_Location.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Warehouse_Name.Contains(data.key)
                                        || c.Room_Name.Contains(data.key)
                                        || c.Location_Name.Contains(data.key)
                                        || c.LocationType_Name.Contains(data.key));


                }

                var Item = new List<View_Location>();
                var TotalRow = new List<View_Location>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Location_Id).ToList();

                var result = new List<SearchLocationViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchLocationViewModel();

                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.warehouse_Name = item.Warehouse_Name;
                    resultItem.room_Name = item.Room_Name;
                    resultItem.locationType_Name = item.LocationType_Name;
                    resultItem.location_Bay = item.Location_Bay;
                    resultItem.location_Depth = item.Location_Depth;
                    resultItem.location_Level = item.Location_Level;
                    resultItem.max_Qty = item.max_Qty;
                    resultItem.max_Weight = item.max_Weight;
                    resultItem.max_Volume = item.max_Volume;
                    resultItem.max_Pallet = item.max_Pallet;
                    resultItem.putAway_Seq = item.putAway_Seq;
                    resultItem.picking_Seq = item.picking_Seq;
                    resultItem.location_Level = item.Location_Level;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultLocationViewModel = new actionResultLocationViewModel();
                actionResultLocationViewModel.itemsLocation = result.ToList();
                actionResultLocationViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultLocationViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region DeleteLocation
        public Boolean getDelete(LocationViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var location = db.MS_Location.Find(data.location_Index);

                if (location != null)
                {
                    location.IsActive = 0;
                    location.IsDelete = 1;


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
                        olog.logging("DeleteLocation", msglog);
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

        public String SaveChanges(LocationViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var LocationOld = db.MS_Location.Find(data.location_Index);
                              
                if (LocationOld == null)
                {

                    if (!string.IsNullOrEmpty(data.location_Id))
                    {
                        var query = db.MS_Location.FirstOrDefault(c => c.Location_Id == data.location_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.location_Id))
                    {
                        data.location_Id = "Location_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_Location.FirstOrDefault(c => c.Location_Id == data.location_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Location_Id == data.location_Id)
                                {
                                    data.location_Id = "Location_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_Location Model = new MS_Location();

                    Model.Location_Index = Guid.NewGuid();
                    Model.Location_Id = data.location_Id;
                    Model.Warehouse_Index = data.warehouse_Index;
                    Model.Room_Index = data.room_Index;
                    Model.LocationType_Index = data.locationType_Index;
                    Model.Location_Name = data.location_Name;
                    Model.LocationAisle_Index = data.locationAisle_Index;
                    Model.Location_Bay = data.location_Bay;
                    Model.Location_Depth = data.location_Depth;
                    Model.Location_Level = data.location_Level;
                    Model.Max_Qty = data.max_Qty;
                    Model.Max_Weight = data.max_Weight;
                    Model.Max_Volume = data.max_Volume;
                    Model.Max_Pallet = data.max_Pallet;
                    Model.PutAway_Seq = data.putAway_Seq;
                    Model.Picking_Seq = data.picking_Seq;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;
                    Model.BlockPut = Convert.ToInt32(data.blockPut);
                    Model.BlockPick = Convert.ToInt32(data.blockPick);
                    Model.Document_Remark = data.document_Remark;

                    db.MS_Location.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.location_Id))
                    {
                        if (LocationOld.Location_Id != "")
                        {
                            data.location_Id = LocationOld.Location_Id;
                        }
                    }
                    else
                    {
                        if (LocationOld.Location_Id != data.location_Id)
                        {
                            var query = db.MS_Location.FirstOrDefault(c => c.Location_Id == data.location_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.location_Id = LocationOld.Location_Id;
                        }
                    }

                    LocationOld.Location_Id = data.location_Id;
                    LocationOld.Warehouse_Index = data.warehouse_Index;
                    LocationOld.Room_Index = data.room_Index;
                    LocationOld.LocationType_Index = data.locationType_Index;
                    LocationOld.Location_Name = data.location_Name;
                    LocationOld.LocationAisle_Index = data.locationAisle_Index;
                    LocationOld.Location_Bay = data.location_Bay;
                    LocationOld.Location_Depth = data.location_Depth;
                    LocationOld.Location_Level = data.location_Level;
                    LocationOld.Max_Qty = data.max_Qty;
                    LocationOld.Max_Weight = data.max_Weight;
                    LocationOld.Max_Volume = data.max_Volume;
                    LocationOld.Max_Pallet = data.max_Pallet;
                    LocationOld.PutAway_Seq = data.putAway_Seq;
                    LocationOld.Picking_Seq = data.picking_Seq;
                    LocationOld.IsActive = Convert.ToInt32(data.isActive);
                    LocationOld.Update_By = data.update_By;
                    LocationOld.Update_Date = DateTime.Now;
                    LocationOld.BlockPut = Convert.ToInt32(data.blockPut);
                    LocationOld.BlockPick = Convert.ToInt32(data.blockPick);
                    LocationOld.Document_Remark = data.document_Remark;
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
                    olog.logging("SaveLocation", msglog);
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


        #region autoLocationSearchFilter
        public List<ItemListViewModel> autoSearchLocationFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_Location.Where(c => c.Warehouse_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Warehouse_Name,
                        key = s.Warehouse_Name
                    }).Distinct();

                    var query2 = db.View_Location.Where(c => c.Room_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Room_Name,
                        key = s.Room_Name
                    }).Distinct();

                    var query3 = db.View_Location.Where(c => c.Location_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Location_Id,
                        key = s.Location_Id

                    }).Distinct();

                    var query4 = db.View_Location.Where(c => c.Location_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Location_Name,
                        key = s.Location_Name

                    }).Distinct();

                    var query5 = db.View_Location.Where(c => c.LocationType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.LocationType_Name,
                        key = s.LocationType_Name

                    }).Distinct();

                    var query = query1.Union(query2).Union(query2).Union(query3).Union(query4).Union(query5);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {

            }

            return items;
        }

        #endregion


        #region autoLocationSearch

        #region SearchSoldToType


        public List<ItemListViewModel> autoLocationSearch(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Location.AsQueryable();


                    if (data.key == "-")
                    {
                        query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Location_Name.Contains(data.key) && c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Location_Name, c.Location_Index, c.Location_Id }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.Location_Index,
                            id = item.Location_Id,
                            name = item.Location_Name
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


        public List<LocationViewModel> locationFilter(LocationViewModel model)

        {
            try
            {
                var query = db.MS_Location.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                if (!string.IsNullOrEmpty(model.warehouse_Index.ToString()))
                {
                    query = query.Where(c => c.Warehouse_Index == model.warehouse_Index);
                }

                var items = new List<LocationViewModel>();

                var result = query.ToList();

                foreach (var item in result)
                {
                    var resultItem = new LocationViewModel
                    {
                        location_Index = item.Location_Index,
                        warehouse_Index = item.Warehouse_Index,
                        room_Index = item.Room_Index,
                        locationType_Index = item.LocationType_Index,
                        location_Id = item.Location_Id,
                        location_Name = item.Location_Name,
                        locationAisle_Index = item.LocationAisle_Index,
                        location_Bay = item.Location_Bay,
                        location_Depth = item.Location_Depth,
                        location_Level = item.Location_Level,
                        max_Qty = item.Max_Qty,
                        max_Weight = item.Max_Weight,
                        max_Volume = item.Max_Volume,
                        max_Pallet = item.Max_Pallet,
                        putAway_Seq = item.PutAway_Seq,
                        picking_Seq = item.Picking_Seq,
                        blockPut = item.BlockPut,
                        blockPick = item.BlockPick
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

        public List<LocationViewModel> GetLocation(LocationViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_Location.AsQueryable();

                    var items = new List<LocationViewModel>();
                    string pwhereFilter = "";

                    if (!string.IsNullOrEmpty(data.location_Index.ToString()))
                    {
                        query = query.Where(c => c.Location_Index == data.location_Index);
                    }
                    if (!string.IsNullOrEmpty(data.location_Name))
                    {
                        query = query.Where(c => c.Location_Name.Contains(data.location_Name));
                    }
                    if (!string.IsNullOrEmpty(data.locationType_Index.ToString()) && data.locationType_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.LocationType_Index == data.locationType_Index);
                    }

                    var result = query.OrderBy(o => o.Location_Id).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new LocationViewModel();
                        resultItem.location_Index = item.Location_Index;
                        resultItem.location_Id = item.Location_Id;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.warehouse_Index = item.Warehouse_Index;
                        resultItem.locationType_Index = item.LocationType_Index;
                        resultItem.locationAisle_Index = item.LocationAisle_Index;
                        resultItem.room_Index = item.Room_Index;
                        resultItem.location_Bay = item.Location_Bay;
                        resultItem.location_Depth = item.Location_Depth;
                        resultItem.location_Level = item.Location_Level;
                        resultItem.max_Qty = item.Max_Qty;
                        resultItem.max_Weight = item.Max_Weight;
                        resultItem.max_Volume = item.Max_Volume;
                        resultItem.max_Pallet = item.Max_Pallet;
                        resultItem.putAway_Seq = item.PutAway_Seq;
                        resultItem.picking_Seq = item.Picking_Seq;
                        resultItem.isActive = item.IsActive;
                        resultItem.isDelete = item.IsDelete;
                        resultItem.isSystem = item.IsSystem;
                        resultItem.status_Id = item.Status_Id;
                        resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                        resultItem.create_By = item.Create_By;
                        resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                        resultItem.update_By = item.Update_By;
                        resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                        resultItem.cancel_By = item.Cancel_By;
                        resultItem.blockPut = item.BlockPut;
                        resultItem.blockPick = item.BlockPick;
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

        public List<LocationViewModel> GetLocationV2(LocationViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_Location.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    var items = new List<LocationViewModel>();
                    string pwhereFilter = "";

                    if (!string.IsNullOrEmpty(data.location_Index.ToString()))
                    {
                        query = query.Where(c => c.Location_Index == data.location_Index);
                    }
                    if (!string.IsNullOrEmpty(data.location_Name))
                    {
                        query = query.Where(c => c.Location_Name == data.location_Name);
                    }
                    if (!string.IsNullOrEmpty(data.locationType_Index.ToString()))
                    {
                        query = query.Where(c => c.LocationType_Index == data.locationType_Index);
                    }

                    var result = query.OrderBy(o => o.Location_Id).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new LocationViewModel();
                        resultItem.location_Index = item.Location_Index;
                        resultItem.location_Id = item.Location_Id;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.warehouse_Index = item.Warehouse_Index;
                        resultItem.locationType_Index = item.LocationType_Index;
                        resultItem.locationAisle_Index = item.LocationAisle_Index;
                        resultItem.room_Index = item.Room_Index;
                        resultItem.location_Bay = item.Location_Bay;
                        resultItem.location_Depth = item.Location_Depth;
                        resultItem.location_Level = item.Location_Level;
                        resultItem.max_Qty = item.Max_Qty;
                        resultItem.max_Weight = item.Max_Weight;
                        resultItem.max_Volume = item.Max_Volume;
                        resultItem.max_Pallet = item.Max_Pallet;
                        resultItem.putAway_Seq = item.PutAway_Seq;
                        resultItem.picking_Seq = item.Picking_Seq;
                        resultItem.isActive = item.IsActive;
                        resultItem.isDelete = item.IsDelete;
                        resultItem.isSystem = item.IsSystem;
                        resultItem.status_Id = item.Status_Id;
                        resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                        resultItem.create_By = item.Create_By;
                        resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                        resultItem.update_By = item.Update_By;
                        resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                        resultItem.cancel_By = item.Cancel_By;
                        resultItem.blockPick = item.BlockPick;
                        resultItem.blockPut = item.BlockPut;
                        resultItem.location_Prefix_desc = item.Location_Prefix_Desc;
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


        public List<LocationConfigViewModel> LocationConfig(LocationConfigViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_Location.AsQueryable();

                    var items = new List<LocationConfigViewModel>();
                    string pwhereFilter = "";

                    if (!string.IsNullOrEmpty(data.location_Index.ToString()) && data.location_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Location_Index == data.location_Index);
                    }
                    if (!string.IsNullOrEmpty(data.location_Name))
                    {
                        query = query.Where(c => c.Location_Name.Contains(data.location_Name));
                    }
                    if (!string.IsNullOrEmpty(data.locationType_Index.ToString()) && data.locationType_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.LocationType_Index == data.locationType_Index);
                    }

                    var result = query.OrderBy(o => o.Location_Id).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new LocationConfigViewModel();
                        resultItem.location_Index = item.Location_Index;
                        resultItem.location_Id = item.Location_Id;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.warehouse_Index = item.Warehouse_Index;
                        resultItem.locationType_Index = item.LocationType_Index;
                        resultItem.locationAisle_Index = item.LocationAisle_Index;
                        resultItem.room_Index = item.Room_Index;
                        resultItem.location_Bay = item.Location_Bay;
                        resultItem.location_Depth = item.Location_Depth;
                        resultItem.location_Level = item.Location_Level;
                        resultItem.max_Qty = item.Max_Qty;
                        resultItem.max_Weight = item.Max_Weight;
                        resultItem.max_Volume = item.Max_Volume;
                        resultItem.max_Pallet = item.Max_Pallet;
                        resultItem.putAway_Seq = item.PutAway_Seq;
                        resultItem.picking_Seq = item.Picking_Seq;
                        resultItem.isActive = item.IsActive;
                        resultItem.isDelete = item.IsDelete;
                        resultItem.isSystem = item.IsSystem;
                        resultItem.status_Id = item.Status_Id;
                        resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                        resultItem.create_By = item.Create_By;
                        resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                        resultItem.update_By = item.Update_By;
                        resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                        resultItem.cancel_By = item.Cancel_By;
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

        public List<View_LocatinCyclecountViewModel> ConfigViewCyclecount(View_LocatinCyclecountViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var query = context.View_LocatinCyclecount.AsQueryable();

                    var items = new List<View_LocatinCyclecountViewModel>();

                    if (!string.IsNullOrEmpty(data.zone_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                    {
                        query = query.Where(c => c.Zone_Index == data.zone_Index);
                    }

                    if (!string.IsNullOrEmpty(data.location_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                    {
                        query = query.Where(c => c.Location_Index == data.location_Index);

                    }
                    if (!string.IsNullOrEmpty(data.locationType_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                    {
                        query = query.Where(c => c.LocationType_Index == data.locationType_Index);
                    }


                    if (!string.IsNullOrEmpty(data.location_Name))
                    {
                        query = query.Where(c => c.Location_Name.Contains(data.location_Name));
                    }

                    if (data.listZoneViewModel != null)
                    {
                        if (data.listZoneViewModel.Count > 0)
                        {
                            query = query.Where(c => data.listZoneViewModel.Select(s => s.zone_Index).Contains(c.Zone_Index));
                        }
                    }



                    var result = query.OrderBy(o => o.RowIndex).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new View_LocatinCyclecountViewModel();

                        resultItem.location_Index = item.Location_Index;
                        resultItem.location_Id = item.Location_Id;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.locationType_Index = item.LocationType_Index;
                        resultItem.locationType_Id = item.LocationType_Id;
                        resultItem.locationType_Name = item.LocationType_Name;
                        resultItem.zone_Index = item.Zone_Index;
                        resultItem.zone_Id = item.Zone_Id;
                        resultItem.zone_Name = item.Zone_Name;

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

        public List<View_TaskGroupLocationWorkAreaViewModel> ConfigViewTaskGroupLocationWorkArea(View_TaskGroupLocationWorkAreaViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var query = context.View_TaskGroupLocationWorkArea.AsQueryable();

                    var items = new List<View_TaskGroupLocationWorkAreaViewModel>();

                    if (!string.IsNullOrEmpty(data.location_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                    {
                        query = query.Where(c => c.Location_Index == data.location_Index);

                        query = query.Where(c => c.Task_IsActive == 1
                                            && c.TaskGroupWorkArea_IsActive == 1
                                            && c.WorkArea_IsActive == 1);


                        var result = query.OrderBy(o => o.Location_Id).ToList();

                        foreach (var item in result)
                        {
                            var resultItem = new View_TaskGroupLocationWorkAreaViewModel();

                            resultItem.taskGroup_Index = item.TaskGroup_Index;
                            resultItem.taskGroup_Id = item.TaskGroup_Id;
                            resultItem.taskGroup_Name = item.TaskGroup_Name;
                            resultItem.task_IsActive = item.Task_IsActive;
                            resultItem.taskGroupWorkArea_Index = item.TaskGroupWorkArea_Index;
                            resultItem.taskGroupWorkArea_Id = item.TaskGroupWorkArea_Id;
                            resultItem.taskGroupWorkArea_IsActive = item.TaskGroupWorkArea_IsActive;
                            resultItem.workArea_Index = item.WorkArea_Index;
                            resultItem.workArea_Id = item.WorkArea_Id;
                            resultItem.workArea_Name = item.WorkArea_Name;
                            resultItem.workArea_IsActive = item.WorkArea_IsActive;
                            resultItem.location_Index = item.Location_Index;
                            resultItem.location_Id = item.Location_Id;
                            resultItem.location_Name = item.Location_Name;
                            resultItem.locationWorkArea_Index = item.LocationWorkArea_Index;
                            resultItem.locationWorkArea_Id = item.LocationWorkArea_Id;

                            items.Add(resultItem);
                        }
                    }

                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LocationTypeViewModel> ConfigfindlocationType(LocationConfigViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {


                    var items = new List<LocationTypeViewModel>();

                    if (!string.IsNullOrEmpty(data.location_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                    {
                      var   query = db.MS_Location.Where(c => c.Location_Index == data.location_Index).FirstOrDefault();

                        if (query != null)
                        {
                            var queryLoType = db.MS_LocationType.Where(c => c.LocationType_Index == query.LocationType_Index).ToList();


                            foreach (var item in queryLoType)
                            {
                                var resultItem = new LocationTypeViewModel();

                                resultItem.locationType_Index = item.LocationType_Index;
                                resultItem.locationType_Id = item.LocationType_Id;
                                resultItem.locationType_Name = item.LocationType_Name;

                                items.Add(resultItem);
                            }
                        }

                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String checkLocation(LocationViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {


                    var query = db.MS_Location.Where(c => c.Location_Name == data.location_Name_To && c.BlockPick != 1 && c.BlockPut != 1).FirstOrDefault();

                    if (query != null)
                    {
                        return "Y";
                    }
                    else
                    {
                        return "N";
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Export Excel
        public ResultLocationViewModel Export(LocationExportViewModel data)
        {
            try

            {
                
                //var getType_name = db.View_Location.AsQueryable();
                var query = db.View_Location.AsQueryable();
        
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Location_Id.Contains(data.key)
                                        || c.Location_Name.Contains(data.key));

                }
                //getType_name = getType_name.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                //if (!string.IsNullOrEmpty(data.key))
                //{
                //    getType_name = getType_name.Where(c => c.Location_Id.Contains(data.key)
                //                                        || c.Location_Name.Contains(data.key));

                //}
                var Item = new List<View_Location>();
                //var Item1 = new List<View_Location>();
                var TotalRow = new List<View_Location>();
              
                TotalRow = query.ToList();
                
                Item = query.OrderBy(o => o.Location_Id).ToList();

                //Item1 = getType_name.OrderBy(c => c.Location_Id).ToList();

                 var result = new List<LocationExportViewModel>();
                //var num = 0;
                int num = 0;
                foreach (var item in Item)
                {
                    //foreach (var item1 in Item1)
                    //{
                        //if(item.Location_Id == item1.Location_Id)
                        //{
                            var resultItem = new LocationExportViewModel();
                            resultItem.numBerOf = num + 1;
                            resultItem.location_Id = item.Location_Id;
                            resultItem.location_Name = item.Location_Name == null ? "" : item.Location_Name;
                            resultItem.locationType_Name = item.Location_Name == null ? "" : item.Location_Name;
                            resultItem.warehouse_Name = item.Warehouse_Name == null ? "" : item.Warehouse_Name;
                            resultItem.room_Name = item.Room_Name == null ? "" : item.Room_Name;
                            resultItem.location_Bay = item.Location_Bay;
                            resultItem.location_Depth = item.Location_Depth;
                            resultItem.location_Level = item.Location_Level;
                            resultItem.max_Qty = item.max_Qty;
                            resultItem.max_Weight = item.max_Weight;
                            resultItem.max_Volume = item.max_Volume;
                            resultItem.max_Pallet = item.max_Pallet;
                            resultItem.putAway_Seq = item.putAway_Seq;
                            resultItem.picking_Seq = item.picking_Seq;
                            resultItem.isActive = item.IsActive;
                            resultItem.isDelete = item.IsDelete;
                            resultItem.activeStatus = item.IsActive == 1 ? "เปิดใช้งาน" : "ปิดใช้งาน";
                            resultItem.create_By = item.Create_By;
                            //resultItem.create_Date = item.Create_Date;
                            resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                            resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                            resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                            resultItem.cancel_By = item.Cancel_By == null ? "" : item.Cancel_By;
                            resultItem.cancel_Date = item.Cancel_Date != null ? item.Cancel_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                            result.Add(resultItem);
                            num++;
                        //}
                        
                   // }
                    


                }

                var count = TotalRow.Count;

                var locationExportViewModel = new ResultLocationViewModel();
                locationExportViewModel.itemsLocation = result.ToList();

                return locationExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



    }
}


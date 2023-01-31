using Comone.Utils;
using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.Room;
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
    public class RoomService
    {
        private MasterDataDbContext db;

        public RoomService()
        {
            db = new MasterDataDbContext();
        }

        public RoomService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterRoom
        public actionResultRoomViewModel filter(SearchRoomViewModel data)
        {
            try
            {
                var query = db.View_Room.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Room_Id.Contains(data.key)
                                         || c.Room_Name.Contains(data.key)
                                         || c.Warehouse_Name.Contains(data.key));
                }
                if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                {
                    var dateStart = data.create_date.toBetweenDate();
                    var dateEnd = data.create_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                var Item = new List<View_Room>();
                var TotalRow = new List<View_Room>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Room_Id).ToList();

                var result = new List<SearchRoomViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchRoomViewModel();

                    resultItem.room_Index = item.Room_Index;
                    resultItem.room_Id = item.Room_Id;
                    resultItem.room_Name = item.Room_Name;
                    resultItem.warehouse_Index = item.Warehouse_Index;
                    resultItem.warehouse_Name = item.Warehouse_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultRoomViewModel = new actionResultRoomViewModel();
                actionResultRoomViewModel.itemsRoom = result.ToList();
                actionResultRoomViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultRoomViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(RoomViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var roomOld = db.MS_Room.Find(data.room_Index);

                if (roomOld == null)
                {
                    if (!string.IsNullOrEmpty(data.room_Id))
                    {
                        var query = db.MS_Room.FirstOrDefault(c => c.Room_Id == data.room_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.room_Id))
                    {
                        data.room_Id = "Room_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_Room.FirstOrDefault(c => c.Room_Id == data.room_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Room_Id == data.room_Id)
                                {
                                    data.room_Id = "Room_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_Room Model = new MS_Room();


                    Model.Room_Index = Guid.NewGuid();
                    Model.Room_Id = data.room_Id;
                    Model.Room_Name = data.room_Name;
                    Model.Warehouse_Index = data.warehouse_Index;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_Room.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.room_Id))
                    {
                        if (roomOld.Room_Id != "")
                        {
                            data.room_Id = roomOld.Room_Id;
                        }
                    }
                    else
                    {
                        if (roomOld.Room_Id != data.room_Id)
                        {
                            var query = db.MS_Room.FirstOrDefault(c => c.Room_Id == data.room_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.room_Id = roomOld.Room_Id;
                        }
                    }

                    roomOld.Room_Id = data.room_Id;
                    roomOld.Room_Name = data.room_Name;
                    roomOld.Warehouse_Index = data.warehouse_Index;
                    roomOld.IsActive = Convert.ToInt32(data.isActive);
                    roomOld.Update_By = data.create_By;
                    roomOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveRoom", msglog);
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
        public RoomViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_Room.Where(c => c.Room_Index == id).FirstOrDefault();

                var result = new RoomViewModel();

                result.room_Index = queryResult.Room_Index;
                result.room_Id = queryResult.Room_Id;
                result.room_Name = queryResult.Room_Name;
                result.warehouse_Index = queryResult.Warehouse_Index;
                result.warehouse_Name = queryResult.Warehouse_Name;
                result.key = queryResult.Warehouse_Id + " - " + queryResult.Warehouse_Name;
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
        public Boolean getDelete(RoomViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Room = db.MS_Room.Find(data.room_Index);

                if (Room != null)
                {
                    Room.IsActive = 0;
                    Room.IsDelete = 1;


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
                        olog.logging("DeleteRoom", msglog);
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

        #region Export Excel
        public ResultRoomViewModel Export(ResultRoomExportViewModel data)
        {
            try
            {
                var query = db.View_Room.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Room_Id.Contains(data.key)
                                         || c.Room_Name.Contains(data.key)
                                         || c.Warehouse_Name.Contains(data.key));
                }
                if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                {
                    var dateStart = data.create_date.toBetweenDate();
                    var dateEnd = data.create_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                var Item = new List<View_Room>();
                var TotalRow = new List<View_Room>();

                TotalRow = query.ToList();
                Item = query.OrderBy(o => o.Room_Id).ToList();

                var result = new List<ResultRoomExportViewModel>();
                //var num = 0;
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new ResultRoomExportViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.room_Id = item.Room_Id;
                    resultItem.room_Name = item.Room_Name;
                    resultItem.warehouse_Name = item.Warehouse_Name;
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

                var roomExportViewModel = new ResultRoomViewModel();
                roomExportViewModel.itemsRoom = result.ToList();
                roomExportViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };
                return roomExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}

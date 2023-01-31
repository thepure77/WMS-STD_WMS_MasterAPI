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
using Newtonsoft.Json;

namespace MasterDataBusiness
{
    public class SlocService
    {
        private MasterDataDbContext db;

        public SlocService()
        {
            db = new MasterDataDbContext();
        }

        public SlocService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public actionResultSlocServiceViewModel filterSloc(SearchSlocViewModel data)
        {
            try
            {
                var query = db.ms_Storage_Loc.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.StorageLoc_Id.Contains(data.key)
                                        || c.StorageLoc_Name.Contains(data.key));
                }
                var Item = new List<ms_Storage_Loc>();
                var TotalRow = new List<ms_Storage_Loc>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.StorageLoc_Id).ToList();

                var result = new List<SearchSlocViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchSlocViewModel();
                    resultItem.storageLoc_Id = item.StorageLoc_Id;
                    resultItem.storageLoc_Name = item.StorageLoc_Name;
                    resultItem.storageLoc_Index = item.StorageLoc_Index;
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.cancel_By = item.Cancel_By == null ? "" : item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date != null ? item.Cancel_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.activeStatus = item.IsActive == 1 ? "Active" : "Not Active";
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultSlocServiceViewModel = new actionResultSlocServiceViewModel();
                actionResultSlocServiceViewModel.itemsSloc = result.ToList();
                actionResultSlocServiceViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultSlocServiceViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region SaveChanges

        public String SaveChanges(SlocViewModel data)
        {
            String State = "Start";
            String msglog = "";
            //var olog = new logtxt();

            try
            {

                var SlocOld = db.ms_Storage_Loc.Find(data.storageLoc_Index);

                if (SlocOld == null)
                {
                    if (!string.IsNullOrEmpty(data.storageLoc_Id))
                    {
                        var query = db.ms_Storage_Loc.FirstOrDefault(c => c.StorageLoc_Id == data.storageLoc_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    //data.soldToShipTo_Id = "SoldToShipTo_Id".genAutonumber();

                    ms_Storage_Loc Model = new ms_Storage_Loc();

                    Model.StorageLoc_Index = Guid.NewGuid();
                    Model.StorageLoc_Id = data.storageLoc_Id;
                    Model.StorageLoc_Name = data.storageLoc_Name;
                    Model.Warehouse_Index = Guid.Parse("B0AD4E8F-A7B1-4952-BAC7-1A9482BABA79");
                    Model.IsActive = 1;
                    Model.IsDelete = 0; 
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.ms_Storage_Loc.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.storageLoc_Id))
                    {
                        if (SlocOld.StorageLoc_Id != "")
                        {
                            data.storageLoc_Id = SlocOld.StorageLoc_Id;
                        }
                    }
                    else
                    {
                        if (SlocOld.StorageLoc_Id != data.storageLoc_Id)
                        {
                            var query = db.ms_Storage_Loc.FirstOrDefault(c => c.StorageLoc_Id == data.storageLoc_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.storageLoc_Id = SlocOld.StorageLoc_Id;
                        }
                    }
               
                    SlocOld.StorageLoc_Id = data.storageLoc_Id;
                    SlocOld.StorageLoc_Name = data.storageLoc_Name;
                    SlocOld.Warehouse_Index = Guid.Parse("B0AD4E8F-A7B1-4952-BAC7-1A9482BABA79");
                    SlocOld.IsActive = Convert.ToInt32(data.isActive);
                    SlocOld.Update_By = data.create_By;
                    SlocOld.Update_Date = DateTime.Now;
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
                    //olog.logging("SaveSoldToShipTo", msglog);
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

        #region FindSloc
        public SlocViewModel find(string id)
        {
            try
            {

                var queryResult = db.ms_Storage_Loc.Where(c => c.StorageLoc_Id == id).FirstOrDefault();

                var result = new SlocViewModel();


                result.storageLoc_Index = queryResult.StorageLoc_Index;
                result.storageLoc_Id = queryResult.StorageLoc_Id;
                result.storageLoc_Name = queryResult.StorageLoc_Name;
                result.warehouse_Index = queryResult.Warehouse_Index;
                result.isActive = queryResult.IsActive;


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDeleteSloc

        public Boolean getDelete(SlocViewModel data)
        {
            String State = "Start";
            String msglog = "";
            //var olog = new logtxt();

            try
            {
                var Sloc = db.ms_Storage_Loc.Find(data.storageLoc_Index);

                if (Sloc != null)
                {
                    Sloc.IsActive = 0;
                    Sloc.IsDelete = 1;
                    Sloc.Cancel_By = data.cancel_By;
                    Sloc.Cancel_Date = DateTime.Now;


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
                        //olog.logging("DeleteShipTo", msglog);
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

        #region autoSloc
        public List<ItemListViewModel> autoSloc(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.ms_Storage_Loc.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.StorageLoc_Id.Contains(data.key)
                                                || c.StorageLoc_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.StorageLoc_Id, c.StorageLoc_Name, }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            id = item.StorageLoc_Id,
                            name = item.StorageLoc_Name,

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

        #region Export Excel
        public SlocActionResultExportViewModel Export(SlocExportViewModel data)
        {
            try
            {
                var query = db.ms_Storage_Loc.AsQueryable();


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.StorageLoc_Id.Contains(data.key)
                                        || c.StorageLoc_Name.Contains(data.key));

                }

                var Item = new List<ms_Storage_Loc>();
                var TotalRow = new List<ms_Storage_Loc>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.StorageLoc_Id).ToList();

                var result = new List<SlocExportViewModel>();

                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new SlocExportViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.storageLoc_Id = item.StorageLoc_Id;
                    resultItem.storageLoc_Name = item.StorageLoc_Name;
                    resultItem.storageLoc_Index = item.StorageLoc_Index;
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.cancel_By = item.Cancel_By == null ? "" : item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date != null ? item.Cancel_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
                    resultItem.activeStatus = item.IsActive == 1 ? "เปิดใช้งาน" : "ปิดใช้งาน";
                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var SlocActionResultExportViewModel = new SlocActionResultExportViewModel();
                SlocActionResultExportViewModel.itemsSloc = result.ToList();

                return SlocActionResultExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }


}


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

using Newtonsoft.Json;

namespace MasterDataBusiness
{
    public class WarehouseService
    {
        private MasterDataDbContext db;

        public WarehouseService()
        {
            db = new MasterDataDbContext();
        }

        public WarehouseService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterWarehouse

        public actionResultWarehouseViewModel FilterInClause(string jsonData)
        {
            try
            {
                SearchWareHouseInClauseViewModel data = JsonConvert.DeserializeObject<SearchWareHouseInClauseViewModel>(jsonData);
                var query = db.MS_Warehouse.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!(data.List_WareHouse_Index is null) && data.List_WareHouse_Index.Count > 0)
                {
                    query = query.Where(c => data.List_WareHouse_Index.Contains(c.Warehouse_Index));
                }

                if (!(data.List_WareHouse_Id is null) && data.List_WareHouse_Id.Count > 0)
                {
                    query = query.Where(c => data.List_WareHouse_Id.Contains(c.Warehouse_Id));
                }

                var Item = new List<MS_Warehouse>();
                var TotalRow = new List<MS_Warehouse>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Warehouse_Id).ToList();

                var result = new List<SearchWarehouseViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchWarehouseViewModel();

                    resultItem.warehouse_Index = item.Warehouse_Index;
                    resultItem.warehouse_Id = item.Warehouse_Id;
                    resultItem.warehouse_Name = item.Warehouse_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultWarehouseViewModel = new actionResultWarehouseViewModel();
                actionResultWarehouseViewModel.itemsWarehouse = result.ToList();
                actionResultWarehouseViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = null };

                return actionResultWarehouseViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public actionResultWarehouseViewModel filter(SearchWarehouseViewModel data)
        {
            try
            {
                var query = db.MS_Warehouse.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                
                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Warehouse_Id.Contains(data.key)
                                         || c.Warehouse_Name.Contains(data.key));
                }

                var Item = new List<MS_Warehouse>();
                var TotalRow = new List<MS_Warehouse>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Warehouse_Id).ToList();

                var result = new List<SearchWarehouseViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchWarehouseViewModel();

                    resultItem.warehouse_Index = item.Warehouse_Index;
                    resultItem.warehouse_Id = item.Warehouse_Id;
                    resultItem.warehouse_Name = item.Warehouse_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultWarehouseViewModel = new actionResultWarehouseViewModel();
                actionResultWarehouseViewModel.itemsWarehouse = result.ToList();
                actionResultWarehouseViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultWarehouseViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(WarehouseViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var warehouseOld = db.MS_Warehouse.Find(data.warehouse_Index);

                if (warehouseOld == null)
                {
                    if (!string.IsNullOrEmpty(data.warehouse_Id))
                    {
                        var query = db.MS_Warehouse.FirstOrDefault(c => c.Warehouse_Id == data.warehouse_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.warehouse_Id))
                    {
                        data.warehouse_Id = "Warehouse_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_Warehouse.FirstOrDefault(c => c.Warehouse_Id == data.warehouse_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Warehouse_Id == data.warehouse_Id)
                                {
                                    data.warehouse_Id = "Warehouse_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_Warehouse Model = new MS_Warehouse();

                    Model.Warehouse_Index = Guid.NewGuid();
                    Model.Warehouse_Id = data.warehouse_Id;
                    Model.Warehouse_Name = data.warehouse_Name;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_Warehouse.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.warehouse_Id))
                    {
                        if (warehouseOld.Warehouse_Id != "")
                        {
                            data.warehouse_Id = warehouseOld.Warehouse_Id;
                        }
                    }
                    else
                    {
                        if (warehouseOld.Warehouse_Id != data.warehouse_Id)
                        {
                            var query = db.MS_Warehouse.FirstOrDefault(c => c.Warehouse_Id == data.warehouse_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.warehouse_Id = warehouseOld.Warehouse_Id;
                        }
                    }

                    warehouseOld.Warehouse_Id = data.warehouse_Id;
                    warehouseOld.Warehouse_Name = data.warehouse_Name;
                    warehouseOld.IsActive = Convert.ToInt32(data.isActive);
                    warehouseOld.Update_By = data.create_By;
                    warehouseOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveWarehouse", msglog);
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
        public WarehouseViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_Warehouse.Where(c => c.Warehouse_Index == id).FirstOrDefault();

                var result = new WarehouseViewModel();


                result.warehouse_Index = queryResult.Warehouse_Index;
                result.warehouse_Id = queryResult.Warehouse_Id;
                result.warehouse_Name = queryResult.Warehouse_Name;
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
        public Boolean getDelete(WarehouseViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var warehouse = db.MS_Warehouse.Find(data.warehouse_Index);

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
                        olog.logging("DeleteWarehouse", msglog);
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


        public List<warehouseDocViewModel> warehousefilter(warehouseDocViewModel model)
        {
            var olog = new logtxt();
            String msglog = "";
            try
            {
                var items = new List<warehouseDocViewModel>();
                var query = db.MS_Warehouse.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();


                if (model.warehouse_Index != new Guid("00000000-0000-0000-0000-000000000000".ToString()))
                {
                    msglog = "Warehouse_Index";
                    query = query.Where(c => c.Warehouse_Index == model.warehouse_Index);
                }

                if (!string.IsNullOrEmpty(model.warehouse_Id))
                {
                    query = query.Where(c => c.Warehouse_Id == model.warehouse_Id);
                }

                var result = query.ToList();

                foreach (var item in result)
                {
                    msglog = "by Values";
                    var resultItem = new warehouseDocViewModel
                    {
                        warehouse_Index = item.Warehouse_Index,
                        warehouse_Id = item.Warehouse_Id,
                        warehouse_Name = item.Warehouse_Name
                    };

                    items.Add(resultItem);
                }

                return items;
            }
            catch (Exception ex)
            {
                olog.logging("warehousefilter", msglog + " ex Rollback " +ex.Message.ToString());
                throw ex;
            }
        }
    }
}

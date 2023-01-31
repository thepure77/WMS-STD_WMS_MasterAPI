using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.CostCenter;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness.CostCenter
{
    public class CostCenterService
    {
        private MasterDataDbContext db;

        public CostCenterService()
        {
            db = new MasterDataDbContext();
        }

        public CostCenterService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region FilterCostCenter
        //Filter
        public actionResultCostCenterViewModel costCenterfilter(SearchCostCenterViewModel data)
        {
            try
            {
                var query = db.ms_CostCenter.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.CostCenter_Name.Contains(data.key)
                                        || c.CostCenter_Description.Contains(data.key)
                                        || c.CostCenter_Id.Contains(data.key));


                }

                var Item = new List<ms_CostCenter>();
                var TotalRow = new List<ms_CostCenter>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.CostCenter_Id).ToList();

                var result = new List<SearchCostCenterViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchCostCenterViewModel();

                    resultItem.costCenter_Index = item.CostCenter_Index;
                    resultItem.costCenter_Id = item.CostCenter_Id;
                    resultItem.costCenter_Name = item.CostCenter_Name;
                    resultItem.costCenter_Description = item.CostCenter_Description;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultCostCenterViewModel = new actionResultCostCenterViewModel();
                actionResultCostCenterViewModel.itemsCostCenter = result.ToList();
                actionResultCostCenterViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultCostCenterViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region findCostCenter
        public CostCenterViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_CostCenter.Where(c => c.CostCenter_Index == id).FirstOrDefault();

                var result = new CostCenterViewModel();


                result.costCenter_Index = queryResult.CostCenter_Index;
                result.costCenter_Name = queryResult.CostCenter_Name;
                result.costCenter_Description = queryResult.CostCenter_Description;
                result.costCenter_Id = queryResult.CostCenter_Id;
                result.isActive = queryResult.IsActive;


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetDeleteCostCenter
        public Boolean getDelete(CostCenterViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var costCenter = db.ms_CostCenter.Find(data.costCenter_Index);

                if (costCenter != null)
                {
                    costCenter.IsActive = 0;
                    costCenter.IsDelete = 1;


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
                        olog.logging("DeleteCostCenter", msglog);
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

        #region SaveChangesCostCenter

        public String SaveChanges(CostCenterViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var CostCenterOld = db.ms_CostCenter.Find(data.costCenter_Index);

                if (CostCenterOld == null)
                {
                    if (!string.IsNullOrEmpty(data.costCenter_Id))
                    {
                        var query = db.ms_CostCenter.FirstOrDefault(c => c.CostCenter_Id == data.costCenter_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.costCenter_Id))
                    {
                        data.costCenter_Id = "CostCenter_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_CostCenter.FirstOrDefault(c => c.CostCenter_Id == data.costCenter_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.CostCenter_Id == data.costCenter_Id)
                                {
                                    data.costCenter_Id = "CostCenter_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.costCenter_Id = "CostCenter_Id".genAutonumber();

                    ms_CostCenter Model = new ms_CostCenter();
                    Model.CostCenter_Index = Guid.NewGuid();
                    Model.CostCenter_Id = data.costCenter_Id;
                    Model.CostCenter_Name = data.costCenter_Name;
                    Model.CostCenter_Description = data.costCenter_Description;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.ms_CostCenter.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.costCenter_Id))
                    {
                        if (CostCenterOld.CostCenter_Id != "")
                        {
                            data.costCenter_Id = CostCenterOld.CostCenter_Id;
                        }
                    }
                    else
                    {
                        if (CostCenterOld.CostCenter_Id != data.costCenter_Id)
                        {
                            var query = db.ms_CostCenter.FirstOrDefault(c => c.CostCenter_Id == data.costCenter_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.costCenter_Id = CostCenterOld.CostCenter_Id;
                        }
                    }
                    CostCenterOld.CostCenter_Id = data.costCenter_Id;
                    CostCenterOld.CostCenter_Index = data.costCenter_Index;
                    CostCenterOld.CostCenter_Name = data.costCenter_Name;
                    CostCenterOld.CostCenter_Description = data.costCenter_Description;
                    CostCenterOld.IsActive = Convert.ToInt32(data.isActive);
                    CostCenterOld.Update_By = data.update_By;
                    CostCenterOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveCostCenter" +
                        "", msglog);
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

        #region SearchCostCenterFilter

        public List<ItemListViewModel> autoSearchCostCenterFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.ms_CostCenter.Where(c => c.CostCenter_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.CostCenter_Id,
                        key = s.CostCenter_Id
                    }).Distinct();

                    var query2 = db.ms_CostCenter.Where(c => c.CostCenter_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.CostCenter_Name,
                        key = s.CostCenter_Name
                    }).Distinct();

                    var query3 = db.ms_CostCenter.Where(c => c.CostCenter_Description.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.CostCenter_Description,
                        key = s.CostCenter_Description
                    }).Distinct();
                    var query = query1.Union(query2).Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }

        #endregion

        public List<CostCenterViewModel> CostCenterfilterdropdown(CostCenterViewModel data)
        {
            try
            {
                var result = new List<CostCenterViewModel>();

                var query = db.ms_CostCenter.AsQueryable();


                var queryResult = query.OrderBy(o => o.CostCenter_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new CostCenterViewModel
                    {
                        costCenter_Index = item.CostCenter_Index,
                        costCenter_Id = item.CostCenter_Id,
                        costCenter_Name = item.CostCenter_Name,

                    };

                    result.Add(resultItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Export Excel
        public ResultCostCenterViewModel Export(CostCenterExportViewModel data)
        {
            try
            {
                var query = db.ms_CostCenter.AsQueryable();

                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.CostCenter_Id.Contains(data.key)
                                        || c.CostCenter_Name.Contains(data.key));

                }

                var Item = new List<ms_CostCenter>();
                var TotalRow = new List<ms_CostCenter>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.CostCenter_Id).ToList();

                var result = new List<CostCenterExportViewModel>();
                //var num = 0;
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new CostCenterExportViewModel();
                  
                    resultItem.numBerOf = num + 1;
                    resultItem.costCenter_Index= item.CostCenter_Index;
                    resultItem.costCenter_Id = item.CostCenter_Id;
                    resultItem.costCenter_Name = item.CostCenter_Name;
                    resultItem.costCenter_Description = item.CostCenter_Description;
                   
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
                }

                var count = TotalRow.Count;

                var costCenterExportViewModel = new ResultCostCenterViewModel();
                costCenterExportViewModel.itemsCostCenter = result.ToList();

                return costCenterExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

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

namespace MasterDataBusiness.DockDoor
{
    public class DockDoorService
    {
        private MasterDataDbContext db;

        public DockDoorService()
        {
            db = new MasterDataDbContext();
        }

        public DockDoorService(MasterDataDbContext db)
        {
            this.db = db;
        }
        public List<DockDoorViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_DockDoor.FromSql("sp_GetDockDoor").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var result = new List<DockDoorViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new DockDoorViewModel();
                        resultItem.DockDoorIndex = item.DockDoor_Index;
                        resultItem.DockDoorId = item.DockDoor_Id;
                        resultItem.DockDoorName = item.DockDoor_Name;
                        resultItem.IsActive = item.IsActive;
                        resultItem.IsDelete = item.IsDelete;
                        resultItem.IsSystem = item.IsSystem;
                        resultItem.StatusId = item.Status_Id;
                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
                        resultItem.CreateBy = item.Create_By;
                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
                        resultItem.UpdateBy = item.Update_By;
                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
                        resultItem.CancelBy = item.Cancel_By;
                        result.Add(resultItem);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DockDoorViewModel> search(DockDoorViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    string pwhereFilter = "";
                    string pwhereLike = "";
                    var result = new List<DockDoorViewModel>();
                    if (data.DockDoorId != "" && data.DockDoorId != null)
                    {
                        pwhereFilter = " And DockDoor_Id like N'%" + data.DockDoorId + "%'";
                    }
                    else
                    {
                        pwhereFilter = "";
                    }

                    if (data.DockDoorName != "" && data.DockDoorName != null)
                    {
                        pwhereFilter += " And DockDoor_Name like N'%" + data.DockDoorName + "%'";
                    }
                    else
                    {
                        pwhereFilter += "";
                    }


                    pwhereFilter += " And isActive = '" + 1 + "'";
                    pwhereFilter += " And isDelete = '" + 0 + "'";
                    var strwhere = new SqlParameter("@strwhere", pwhereFilter);
                    var query = context.MS_DockDoor.FromSql("sp_GetDockDoor @strwhere ", strwhere).ToList();
                    foreach (var item in query)
                    {
                        var resultItem = new DockDoorViewModel();

                        resultItem.DockDoorIndex = item.DockDoor_Index;
                        resultItem.DockDoorId = item.DockDoor_Id;
                        resultItem.DockDoorName = item.DockDoor_Name;
                        resultItem.IsActive = item.IsActive;
                        resultItem.IsDelete = item.IsDelete;
                        resultItem.IsSystem = item.IsSystem;
                        resultItem.StatusId = item.Status_Id;
                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
                        resultItem.CreateBy = item.Create_By;
                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
                        resultItem.UpdateBy = item.Update_By;
                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
                        resultItem.CancelBy = item.Cancel_By;

                        result.Add(resultItem);
                    }


                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region filter
        public actionResultDockDoorViewModel filter(SearchDockDoorViewModel data)
        {
            try
            {
                var query = db.MS_DockDoor.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.DockDoor_Id.Contains(data.key)
                                         || c.DockDoor_Name.Contains(data.key));
                }

                var Item = new List<MS_DockDoor>();
                var TotalRow = new List<MS_DockDoor>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.DockDoor_Id).ToList();

                var result = new List<SearchDockDoorViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchDockDoorViewModel();

                    resultItem.dockDoor_Index = item.DockDoor_Index;
                    resultItem.dockDoor_Id = item.DockDoor_Id;
                    resultItem.dockDoor_Name = item.DockDoor_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultDockDoorViewModel = new actionResultDockDoorViewModel();
                actionResultDockDoorViewModel.itemsDockDoor = result.ToList();
                actionResultDockDoorViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultDockDoorViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(DockDoorViewModelV2 data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var DockDoorTypeOld = db.MS_DockDoor.Find(data.dockDoor_Index);

                if (DockDoorTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.dockDoor_Id))
                    {
                        var query = db.MS_DockDoor.FirstOrDefault(c => c.DockDoor_Id == data.dockDoor_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.dockDoor_Id))
                    {
                        data.dockDoor_Id = "DockDoor_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_DockDoor.FirstOrDefault(c => c.DockDoor_Id == data.dockDoor_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.DockDoor_Id == data.dockDoor_Id)
                                {
                                    data.dockDoor_Id = "DockDoor_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_DockDoor Model = new MS_DockDoor();


                    Model.DockDoor_Index = Guid.NewGuid();
                    Model.DockDoor_Id = data.dockDoor_Id;
                    Model.DockDoor_Name = data.dockDoor_Name;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_DockDoor.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.dockDoor_Id))
                    {
                        if (DockDoorTypeOld.DockDoor_Id != "")
                        {
                            data.dockDoor_Id = DockDoorTypeOld.DockDoor_Id;
                        }
                    }
                    else
                    {
                        if (DockDoorTypeOld.DockDoor_Id != data.dockDoor_Id)
                        {
                            var query = db.MS_DockDoor.FirstOrDefault(c => c.DockDoor_Id == data.dockDoor_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.dockDoor_Id = DockDoorTypeOld.DockDoor_Id;
                        }
                    }

                    DockDoorTypeOld.DockDoor_Id = data.dockDoor_Id;
                    DockDoorTypeOld.DockDoor_Name = data.dockDoor_Name;
                    DockDoorTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    DockDoorTypeOld.Update_By = data.create_By;
                    DockDoorTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveDockDoor", msglog);
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
        public DockDoorViewModelV2 find(Guid id)
        {
            try
            {

                var queryResult = db.MS_DockDoor.Where(c => c.DockDoor_Index == id).FirstOrDefault();

                var result = new DockDoorViewModelV2();

                result.dockDoor_Index = queryResult.DockDoor_Index;
                result.dockDoor_Id = queryResult.DockDoor_Id;
                result.dockDoor_Name = queryResult.DockDoor_Name;
                result.isActive = queryResult.IsActive;
                result.isDelete = queryResult.IsDelete;
                result.isSystem = queryResult.IsSystem;
                result.status_Id = queryResult.Status_Id;
                result.create_By = queryResult.Create_By;
                result.create_Date = queryResult.Create_Date;
                result.update_By = queryResult.Update_By;
                result.update_Date = queryResult.Update_Date;
                result.cancel_By = queryResult.Cancel_By;
                result.cancel_Date = queryResult.Cancel_Date;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDelete
        public Boolean getDelete(DockDoorViewModelV2 data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var DockDoor = db.MS_DockDoor.Find(data.dockDoor_Index);

                if (DockDoor != null)
                {
                    DockDoor.IsActive = 0;
                    DockDoor.IsDelete = 1;


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
                        olog.logging("DeleteDockDoor", msglog);
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

        #region dropdown
        public List<DockDoorViewModelV2> dockDoorDropdown(DockDoorViewModelV2 data)
        {
            try
            {
                var result = new List<DockDoorViewModelV2>();

                var query = db.MS_DockDoor.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.DockDoor_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new DockDoorViewModelV2();

                    resultItem.dockDoor_Index = item.DockDoor_Index;
                    resultItem.dockDoor_Id = item.DockDoor_Id;
                    resultItem.dockDoor_Name = item.DockDoor_Name;

                    result.Add(resultItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

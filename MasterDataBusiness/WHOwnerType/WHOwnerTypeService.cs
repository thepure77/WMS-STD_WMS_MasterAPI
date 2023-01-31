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
    public class WHOwnerTypeService
    {
        private MasterDataDbContext db;

        public WHOwnerTypeService()
        {
            db = new MasterDataDbContext();
        }

        public WHOwnerTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterWHOnerType
        public actionResultWHOwnerTypeViewModel filter(SearchWHOwnerTypeViewModel data)
        {
            try
            {
                var query = db.MS_WHOwnerType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.WHOwnerType_Id.Contains(data.key)
                                         || c.WHOwnerType_Name.Contains(data.key));
                }

                var Item = new List<MS_WHOwnerType>();
                var TotalRow = new List<MS_WHOwnerType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.WHOwnerType_Index).ToList();

                var result = new List<SearchWHOwnerTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchWHOwnerTypeViewModel();

                    resultItem.whOwnerType_Index = item.WHOwnerType_Index;
                    resultItem.whOwnerType_Id = item.WHOwnerType_Id;
                    resultItem.whOwnerType_Name = item.WHOwnerType_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultWHOwnerTypeViewModel = new actionResultWHOwnerTypeViewModel();
                actionResultWHOwnerTypeViewModel.itemsWHOwnerType = result.ToList();
                actionResultWHOwnerTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultWHOwnerTypeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(WHOwnerTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var WHOwnerTypeOld = db.MS_WHOwnerType.Find(data.whOwnerType_Index);

                if (WHOwnerTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.whOwnerType_Id))
                    {
                        var query = db.MS_WHOwnerType.FirstOrDefault(c => c.WHOwnerType_Id == data.whOwnerType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.whOwnerType_Id))
                    {
                        data.whOwnerType_Id = "WHOwnerType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_WHOwnerType.FirstOrDefault(c => c.WHOwnerType_Id == data.whOwnerType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.WHOwnerType_Id == data.whOwnerType_Id)
                                {
                                    data.whOwnerType_Id = "WHOwnerType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_WHOwnerType Model = new MS_WHOwnerType();

                    Model.WHOwnerType_Index = Guid.NewGuid();
                    Model.WHOwnerType_Id = data.whOwnerType_Id;
                    Model.WHOwnerType_Name = data.whOwnerType_Name;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_WHOwnerType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.whOwnerType_Id))
                    {
                        if (WHOwnerTypeOld.WHOwnerType_Id != "")
                        {
                            data.whOwnerType_Id = WHOwnerTypeOld.WHOwnerType_Id;
                        }
                    }
                    else
                    {
                        if (WHOwnerTypeOld.WHOwnerType_Id != data.whOwnerType_Id)
                        {
                            var query = db.MS_WHOwnerType.FirstOrDefault(c => c.WHOwnerType_Id == data.whOwnerType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.whOwnerType_Id = WHOwnerTypeOld.WHOwnerType_Id;
                        }
                    }

                    WHOwnerTypeOld.WHOwnerType_Id = data.whOwnerType_Id;
                    WHOwnerTypeOld.WHOwnerType_Name = data.whOwnerType_Name;
                    WHOwnerTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    WHOwnerTypeOld.Update_By = data.create_By;
                    WHOwnerTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveWHOwnerType", msglog);
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

        #region findWHOnerType
        public WHOwnerTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_WHOwnerType.Where(c => c.WHOwnerType_Index == id).FirstOrDefault();

                var result = new WHOwnerTypeViewModel();


                result.whOwnerType_Index = queryResult.WHOwnerType_Index;
                result.whOwnerType_Id = queryResult.WHOwnerType_Id;
                result.whOwnerType_Name = queryResult.WHOwnerType_Name;
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
        public Boolean getDelete(WHOwnerTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var warehouse = db.MS_WHOwnerType.Find(data.whOwnerType_Index);

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
                        olog.logging("DeleteWHOwnerType", msglog);
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

    }
}

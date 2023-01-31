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
    public class FacilityTypeService
    {
        private MasterDataDbContext db;

        public FacilityTypeService()
        {
            db = new MasterDataDbContext();
        }

        public FacilityTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterFacility
        public actionResultFacilityTypeViewModel filter(SearchFacilityTypeViewModel data)
        {
            try
            {
                var query = db.MS_FacilityType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.FacilityType_Id.Contains(data.key)
                                         || c.FacilityType_Name.Contains(data.key));
                }

                var Item = new List<MS_FacilityType>();
                var TotalRow = new List<MS_FacilityType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.FacilityType_Id).ToList();

                var result = new List<SearchFacilityTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchFacilityTypeViewModel();

                    resultItem.facilityType_Index = item.FacilityType_Index;
                    resultItem.facilityType_Id = item.FacilityType_Id;
                    resultItem.facilityType_Name = item.FacilityType_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultFacilityTypeViewModel = new actionResultFacilityTypeViewModel();
                actionResultFacilityTypeViewModel.itemsFacilityType = result.ToList();
                actionResultFacilityTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultFacilityTypeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(FacilityTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var FacilityTypeOld = db.MS_FacilityType.Find(data.facilityType_Index);

                if (FacilityTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.facilityType_Id))
                    {
                        var query = db.MS_FacilityType.FirstOrDefault(c => c.FacilityType_Id == data.facilityType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.facilityType_Id))
                    {
                        data.facilityType_Id = "FacilityType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_FacilityType.FirstOrDefault(c => c.FacilityType_Id == data.facilityType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.FacilityType_Id == data.facilityType_Id)
                                {
                                    data.facilityType_Id = "FacilityType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_FacilityType Model = new MS_FacilityType();

                    Model.FacilityType_Index = Guid.NewGuid();
                    Model.FacilityType_Id = data.facilityType_Id;
                    Model.FacilityType_Name = data.facilityType_Name;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_FacilityType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.facilityType_Id))
                    {
                        if (FacilityTypeOld.FacilityType_Id != "")
                        {
                            data.facilityType_Id = FacilityTypeOld.FacilityType_Id;
                        }
                    }
                    else
                    {
                        if (FacilityTypeOld.FacilityType_Id != data.facilityType_Id)
                        {
                            var query = db.MS_FacilityType.FirstOrDefault(c => c.FacilityType_Id == data.facilityType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.facilityType_Id = FacilityTypeOld.FacilityType_Id;
                        }
                    }

                    FacilityTypeOld.FacilityType_Id = data.facilityType_Id;
                    FacilityTypeOld.FacilityType_Name = data.facilityType_Name;
                    FacilityTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    FacilityTypeOld.Update_By = data.create_By;
                    FacilityTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveFacilityType", msglog);
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
        public FacilityTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_FacilityType.Where(c => c.FacilityType_Index == id).FirstOrDefault();

                var result = new FacilityTypeViewModel();


                result.facilityType_Index = queryResult.FacilityType_Index;
                result.facilityType_Id = queryResult.FacilityType_Id;
                result.facilityType_Name = queryResult.FacilityType_Name;
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
        public Boolean getDelete(FacilityTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var FacilityType = db.MS_FacilityType.Find(data.facilityType_Index);

                if (FacilityType != null)
                {
                    FacilityType.IsActive = 0;
                    FacilityType.IsDelete = 1;


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
                        olog.logging("DeleteFacilityType", msglog);
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

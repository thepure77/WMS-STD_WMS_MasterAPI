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
    public class FacilityService
    {
        private MasterDataDbContext db;

        public FacilityService()
        {
            db = new MasterDataDbContext();
        }

        public FacilityService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterFacility
        public actionResultFacilityViewModel filter(SearchFacilityViewModel data)
        {
            try
            {
                var query = db.View_Facility.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Facility_Id.Contains(data.key)
                                         || c.Facility_Name.Contains(data.key));
                }

                var Item = new List<View_Facility>();
                var TotalRow = new List<View_Facility>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Facility_Id).ToList();

                var result = new List<SearchFacilityViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchFacilityViewModel();

                    resultItem.facility_Index = item.Facility_Index;
                    resultItem.facility_Id = item.Facility_Id;
                    resultItem.facility_Name = item.Facility_Name;
                    resultItem.facilityType_Index = item.FacilityType_Index;
                    resultItem.facilityType_Id = item.FacilityType_Id;
                    resultItem.facilityType_Name = item.FacilityType_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultFacilityViewModel = new actionResultFacilityViewModel();
                actionResultFacilityViewModel.itemsFacility = result.ToList();
                actionResultFacilityViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultFacilityViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(FacilityViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var facilityOld = db.MS_Facility.Find(data.facility_Index);

                if (facilityOld == null)
                {
                    if (!string.IsNullOrEmpty(data.facility_Id))
                    {
                        var query = db.MS_Facility.FirstOrDefault(c => c.Facility_Id == data.facility_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.facility_Id))
                    {
                        data.facility_Id = "Facility_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_Facility.FirstOrDefault(c => c.Facility_Id == data.facility_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Facility_Id == data.facility_Id)
                                {
                                    data.facility_Id = "Facility_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_Facility Model = new MS_Facility();

                    Model.Facility_Index = Guid.NewGuid();
                    Model.Facility_Id = data.facility_Id;
                    Model.Facility_Name = data.facility_Name;
                    Model.FacilityType_Index = data.facilityType_Index;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_Facility.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.facility_Id))
                    {
                        if (facilityOld.Facility_Id != "")
                        {
                            data.facility_Id = facilityOld.Facility_Id;
                        }
                    }
                    else
                    {
                        if (facilityOld.Facility_Id != data.facility_Id)
                        {
                            var query = db.MS_Facility.FirstOrDefault(c => c.Facility_Id == data.facility_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.facility_Id = facilityOld.Facility_Id;
                        }
                    }

                    facilityOld.Facility_Id = data.facility_Id;
                    facilityOld.Facility_Name = data.facility_Name;
                    facilityOld.FacilityType_Index = data.facilityType_Index;
                    facilityOld.IsActive = Convert.ToInt32(data.isActive);
                    facilityOld.Update_By = data.create_By;
                    facilityOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveFacility", msglog);
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
        public FacilityViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_Facility.Where(c => c.Facility_Index == id).FirstOrDefault();

                var result = new FacilityViewModel();


                result.facility_Index = queryResult.Facility_Index;
                result.facility_Id = queryResult.Facility_Id;
                result.facility_Name = queryResult.Facility_Name;
                result.facilityType_Index = queryResult.FacilityType_Index;
                result.key = queryResult.FacilityType_Id + " - " + queryResult.FacilityType_Name;
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
        public Boolean getDelete(FacilityViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var facility = db.MS_Facility.Find(data.facility_Index);

                if (facility != null)
                {
                    facility.IsActive = 0;
                    facility.IsDelete = 1;


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
                        olog.logging("DeleteFacility", msglog);
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

        #region autoSearchFacility
        public List<ItemListViewModel> autoSearchFacility (ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_Facility.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Facility_Name.Contains(data.key));
                    }


                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Facility_Index, c.Facility_Id, c.Facility_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Facility_Index,
                            id = item.Facility_Id,
                            name = item.Facility_Name,
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

    }
}

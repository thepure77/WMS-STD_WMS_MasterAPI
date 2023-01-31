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
using System.Security.Cryptography;
using System.Text;

namespace MasterDataBusiness
{
    public class WHOwnerService
    {
        private MasterDataDbContext db;

        public WHOwnerService()
        {
            db = new MasterDataDbContext();
        }

        public WHOwnerService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterWHOwner
        public actionResultWHOwnerViewModel filter(SearchWHOwnerViewModel data)
        {
            try
            {
                var query = db.View_WHOwner.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.WHOwner_Id.Contains(data.key)
                                         || c.WHOwner_Name.Contains(data.key)
                                         || c.WHOwnerType_Name.Contains(data.key));
                }

                var Item = new List<View_WHOwner>();
                var TotalRow = new List<View_WHOwner>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.WHOwner_Id).ToList();

                var result = new List<SearchWHOwnerViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchWHOwnerViewModel();

                    resultItem.whOwner_Index = item.WHOwner_Index;
                    resultItem.whOwner_Id = item.WHOwner_Id;
                    resultItem.whOwner_Name = item.WHOwner_Name;
                    resultItem.whOwner_Address = item.WHOwner_Address;
                    resultItem.whOwnerType_Name = item.WHOwnerType_Name;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.district_Name = item.District_Name;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
                    resultItem.postcode_Name = item.Postcode_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultWHOwnerViewModel = new actionResultWHOwnerViewModel();
                actionResultWHOwnerViewModel.itemsWHOwner = result.ToList();
                actionResultWHOwnerViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultWHOwnerViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(WHOwnerViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var WHOwnerOld = db.MS_WHOwner.Find(data.whOwner_Index);

                if (WHOwnerOld == null)
                {
                    if (!string.IsNullOrEmpty(data.whOwner_Id))
                    {
                        var query = db.MS_WHOwner.FirstOrDefault(c => c.WHOwner_Id == data.whOwner_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.whOwner_Id))
                    {
                        data.whOwner_Id = "WHOwner_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_WHOwner.FirstOrDefault(c => c.WHOwner_Id == data.whOwner_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.WHOwner_Id == data.whOwner_Id)
                                {
                                    data.whOwner_Id = "WHOwner_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_WHOwner Model = new MS_WHOwner();

                    Model.WHOwner_Index = Guid.NewGuid();
                    Model.WHOwner_Id = data.whOwner_Id;
                    Model.WHOwner_Name = data.whOwner_Name;
                    Model.WHOwnerType_Index = data.whOwnerType_Index;
                    Model.Owner_TaxID = data.owner_TaxID;
                    Model.Owner_Email = data.owner_Email;
                    Model.Owner_Fax = data.owner_Fax;
                    Model.Owner_Tel = data.owner_Tel;
                    Model.Owner_Mobile = data.owner_Mobile;
                    Model.Owner_Barcode = data.owner_Barcode;
                    Model.Contact_Person = data.contact_Person;
                    Model.Contact_Tel = data.contact_Tel;
                    Model.Contact_Email = data.contact_Email;
                    Model.WHOwner_Address = data.whOwner_Address;
                    Model.Country_Index = data.country_Index;
                    Model.Province_Index = data.province_Index;
                    Model.District_Index = data.district_Index;
                    Model.SubDistrict_Index = data.subDistrict_Index;
                    Model.Postcode_Index = new Guid("00000000-0000-0000-0000-000000000000");
                    Model.Postcode_Name = data.postcode_Name;
                    Model.Postcode_Id = "";
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_WHOwner.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.whOwner_Id))
                    {
                        if (WHOwnerOld.WHOwner_Id != "")
                        {
                            data.whOwner_Id = WHOwnerOld.WHOwner_Id;
                        }
                    }
                    else
                    {
                        if (WHOwnerOld.WHOwner_Id != data.whOwner_Id)
                        {
                            var query = db.MS_WHOwner.FirstOrDefault(c => c.WHOwner_Id == data.whOwner_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.whOwner_Id = WHOwnerOld.WHOwner_Id;
                        }
                    }

                    WHOwnerOld.WHOwner_Id = data.whOwner_Id;
                    WHOwnerOld.WHOwner_Name = data.whOwner_Name;
                    WHOwnerOld.WHOwnerType_Index = data.whOwnerType_Index;
                    WHOwnerOld.Owner_TaxID = data.owner_TaxID;
                    WHOwnerOld.Owner_Email = data.owner_Email;
                    WHOwnerOld.Owner_Fax = data.owner_Fax;
                    WHOwnerOld.Owner_Tel = data.owner_Tel;
                    WHOwnerOld.Owner_Mobile = data.owner_Mobile;
                    WHOwnerOld.Owner_Barcode = data.owner_Barcode;
                    WHOwnerOld.Contact_Person = data.contact_Person;
                    WHOwnerOld.Contact_Tel = data.contact_Tel;
                    WHOwnerOld.Contact_Email = data.contact_Email;
                    WHOwnerOld.WHOwner_Address = data.whOwner_Address;
                    WHOwnerOld.Country_Index = data.country_Index;
                    WHOwnerOld.Province_Index = data.province_Index;
                    WHOwnerOld.District_Index = data.district_Index;
                    WHOwnerOld.SubDistrict_Index = data.subDistrict_Index;
                    WHOwnerOld.Postcode_Name = data.postcode_Name;
                    WHOwnerOld.IsActive = Convert.ToInt32(data.isActive);
                    WHOwnerOld.Update_By = data.create_By;
                    WHOwnerOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveWHOwner", msglog);
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
        public WHOwnerViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_WHOwner.Where(c => c.WHOwner_Index == id).FirstOrDefault();

                var result = new WHOwnerViewModel();


                result.whOwner_Index = queryResult.WHOwner_Index;
                result.whOwner_Id = queryResult.WHOwner_Id;
                result.whOwner_Name = queryResult.WHOwner_Name;
                result.whOwnerType_Index = queryResult.WHOwnerType_Index;
                result.whOwnerType_Name = queryResult.WHOwnerType_Name;
                result.owner_TaxID = queryResult.Owner_TaxID;
                result.owner_Email = queryResult.Owner_Email;
                result.owner_Fax = queryResult.Owner_Fax;
                result.owner_Tel = queryResult.Owner_Tel;
                result.owner_Mobile = queryResult.Owner_Mobile;
                result.owner_Barcode = queryResult.Owner_Barcode;
                result.contact_Person = queryResult.Contact_Person;
                result.contact_Tel = queryResult.Contact_Tel;
                result.contact_Email = queryResult.Contact_Email;
                result.whOwner_Address = queryResult.WHOwner_Address;
                result.country_Index = queryResult.Country_Index;
                result.country_Name = queryResult.Country_Name;
                result.province_Index = queryResult.Province_Index;
                result.province_Name = queryResult.Province_Name;
                result.district_Index = queryResult.District_Index;
                result.district_Name = queryResult.District_Name;
                result.subDistrict_Index = queryResult.SubDistrict_Index;
                result.subDistrict_Name = queryResult.SubDistrict_Name;
                result.postcode_Index = queryResult.Postcode_Index;
                result.postcode_Name = queryResult.Postcode_Name;
                result.key = queryResult.WHOwnerType_Id + " - " + queryResult.WHOwnerType_Name;
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
        public Boolean getDelete(WHOwnerViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var vendor = db.MS_WHOwner.Find(data.whOwner_Index);

                if (vendor != null)
                {
                    vendor.IsActive = 0;
                    vendor.IsDelete = 1;


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
                        olog.logging("DeleteWHOwner", msglog);
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

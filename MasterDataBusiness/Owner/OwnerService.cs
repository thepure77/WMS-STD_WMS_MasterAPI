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
    public class OwnerService
    {
        private MasterDataDbContext db;

        public OwnerService()
        {
            db = new MasterDataDbContext();
        }

        public OwnerService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterOwner
        public actionResultOwnerViewModel filter(SearchOwnerViewModel data)
        {
            try
            {
                var query = db.MS_Owner.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Owner_Id.Contains(data.key)
                                         || c.Owner_Name.Contains(data.key)
                                         || c.OwnerType_Name.Contains(data.key)
                                         || c.Ref_No2.Contains(data.key)
                                         );
                }

                var Item = new List<MS_Owner>();
                var TotalRow = new List<MS_Owner>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                var num = 1;
                if (data.PerPage == 100)
                {
                    for (int i = 1; i < data.CurrentPage; i++)
                    {
                        num = num + 100;
                    }
                }
                if (data.PerPage == 50)
                {
                    for (int i = 1; i < data.CurrentPage; i++)
                    {
                        num = num + 50;
                    }
                }
                int rowCount = num;

                Item = query.OrderBy(o => o.Owner_Id).ToList();

                var result = new List<SearchOwnerViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchOwnerViewModel();

                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.owner_SecondName = item.Owner_SecondName;
                    resultItem.owner_Address = item.Owner_Address;
                    resultItem.ownerType_Index = item.OwnerType_Index;
                    resultItem.ownerType_Id = item.OwnerType_Id;
                    resultItem.ownerType_Name = item.OwnerType_Name;
                    resultItem.country_Index = item.Country_Index;
                    resultItem.country_Id = item.Country_Id;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.province_Index = item.Province_Index;
                    resultItem.province_Id = item.Province_Id;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.district_Index = item.District_Index;
                    resultItem.district_Id = item.District_Id;
                    resultItem.district_Name = item.District_Name;
                    resultItem.subDistrict_Index = item.SubDistrict_Index;
                    resultItem.subDistrict_Id = item.SubDistrict_Id;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
                    resultItem.postcode_Index = item.Postcode_Index;
                    resultItem.postcode_Id = item.Postcode_Id;
                    resultItem.postcode_Name = item.Postcode_Name;
                    resultItem.owner_TaxID = item.Owner_TaxID;
                    resultItem.owner_Email = item.Owner_Email;
                    resultItem.owner_Fax = item.Owner_Fax;
                    resultItem.owner_Tel = item.Owner_Tel;
                    resultItem.owner_Mobile = item.Owner_Mobile;
                    resultItem.owner_Barcode = item.Owner_Barcode;
                    resultItem.contact_Person = item.Contact_Person;
                    resultItem.contact_Person2 = item.Contact_Person2;
                    resultItem.contact_Person3 = item.Contact_Person3;
                    resultItem.contact_Tel = item.Contact_Tel;
                    resultItem.contact_Tel2 = item.Contact_Tel2;
                    resultItem.contact_Tel3 = item.Contact_Tel3;
                    resultItem.contact_Email = item.Contact_Email;
                    resultItem.contact_Email2 = item.Contact_Email2;
                    resultItem.contact_Email3 = item.Contact_Email3;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.remark = item.Remark;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
                    resultItem.isSystem = item.IsSystem;
                    resultItem.status_Id = item.Status_Id;
                    resultItem.create_By = item.Create_By;
                    resultItem.create_Date = item.Create_Date;
                    resultItem.update_By = item.Update_By;
                    resultItem.update_Date = item.Update_Date;
                    resultItem.cancel_By = item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date;
                    resultItem.row_Count = rowCount;
                    result.Add(resultItem);
                    rowCount++;
                }

                var count = TotalRow.Count;

                var actionResultOwnerViewModel = new actionResultOwnerViewModel();
                actionResultOwnerViewModel.itemsOwner = result.ToList();
                actionResultOwnerViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultOwnerViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public actionResultOwnerViewModel FilterInClause(string jsonData)
        {
            try
            {
                SearchOwnerInClauseViewModel data = JsonConvert.DeserializeObject<SearchOwnerInClauseViewModel>(jsonData);

                var query = db.View_Owner.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!(data.List_Owner_Index is null) && data.List_Owner_Index.Count > 0)
                {
                    query = query.Where(c => data.List_Owner_Index.Contains(c.Owner_Index));
                }

                if (!(data.List_Owner_Id is null) && data.List_Owner_Id.Count > 0)
                {
                    query = query.Where(c => data.List_Owner_Id.Contains(c.Owner_Id));
                }
                if (!(data.List_Owner_Ref is null) && data.List_Owner_Ref.Count > 0)
                {
                    query = query.Where(c => data.List_Owner_Ref.Contains(c.Ref_No2));
                }


                var Item = new List<View_Owner>();
                var TotalRow = new List<View_Owner>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Owner_Id).ToList();

                var result = new List<SearchOwnerViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchOwnerViewModel();

                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.owner_Address = item.Owner_Address;
                    resultItem.ownerType_Index = item.OwnerType_Index;
                    resultItem.ownerType_Name = item.OwnerType_Name;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.district_Name = item.District_Name;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
                    resultItem.postcode_Name = item.Postcode_Name;
                    resultItem.isActive = item.IsActive;

                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;

                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultOwnerViewModel = new actionResultOwnerViewModel();
                actionResultOwnerViewModel.itemsOwner = result.ToList();
                actionResultOwnerViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = null };

                return actionResultOwnerViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(OwnerViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var OwnerOld = db.MS_Owner.Find(data.owner_Index);

                if (OwnerOld == null)
                {

                    if (!string.IsNullOrEmpty(data.owner_Id))
                    {
                        var query = db.MS_Owner.FirstOrDefault(c => c.Owner_Id == data.owner_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.owner_Id))
                    {
                        data.owner_Id = "Owner_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_Owner.FirstOrDefault(c => c.Owner_Id == data.owner_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Owner_Id == data.owner_Id)
                                {
                                    data.owner_Id = "Owner_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    
                    MS_Owner Model = new MS_Owner();

                    Model.Owner_Index = Guid.NewGuid();
                    Model.Owner_Id = data.owner_Id;
                    Model.Owner_Name = data.owner_Name;
                    Model.Owner_SecondName = data.owner_SecondName;
                    Model.Owner_Address = data.owner_Address;
                    Model.OwnerType_Index = data.ownerType_Index;
                    Model.OwnerType_Id = data.ownerType_Id;
                    Model.OwnerType_Name = data.ownerType_Name;
                    Model.Country_Index = data.country_Index;
                    Model.Country_Id = data.country_Id;
                    Model.Country_Name = data.country_Name;
                    Model.Province_Index = data.province_Index;
                    Model.Province_Id = data.province_Id;
                    Model.Province_Name = data.province_Name;
                    Model.District_Index = data.district_Index;
                    Model.District_Id = data.district_Id;
                    Model.District_Name = data.district_Name;
                    Model.SubDistrict_Index = data.subDistrict_Index;
                    Model.SubDistrict_Id = data.subDistrict_Id;
                    Model.SubDistrict_Name = data.subDistrict_Name;
                    Model.Postcode_Index = data.postcode_Index;
                    Model.Postcode_Id = data.postcode_Id;
                    Model.Postcode_Name = data.postcode_Name;
                    Model.Owner_TaxID = data.owner_TaxID;
                    Model.Owner_Email = data.owner_Email;
                    Model.Owner_Fax = data.owner_Fax;
                    Model.Owner_Tel = data.owner_Tel;
                    Model.Owner_Mobile = data.owner_Mobile;
                    Model.Owner_Barcode = data.owner_Barcode;
                    Model.Contact_Person = data.contact_Person;
                    Model.Contact_Person2 = data.contact_Person2;
                    Model.Contact_Person3 = data.contact_Person3;
                    Model.Contact_Tel = data.contact_Tel;
                    Model.Contact_Tel2 = data.contact_Tel2;
                    Model.Contact_Tel3 = data.contact_Tel3;
                    Model.Contact_Email = data.contact_Email;
                    Model.Contact_Email2 = data.contact_Email2;
                    Model.Contact_Email3 = data.contact_Email3;
                    Model.Ref_No1 = data.ref_No1;
                    Model.Ref_No2 = data.ref_No2;
                    Model.Ref_No3 = data.ref_No3;
                    Model.Ref_No4 = data.ref_No4;
                    Model.Ref_No5 = data.ref_No5;
                    Model.Remark = data.remark;
                    Model.UDF_1 = data.udf_1;
                    Model.UDF_2 = data.udf_2;
                    Model.UDF_3 = data.udf_3;
                    Model.UDF_4 = data.udf_4;
                    Model.UDF_5 = data.udf_5; 
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_Owner.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.owner_Id))
                    {
                        if (OwnerOld.Owner_Id != "")
                        {
                            data.owner_Id = OwnerOld.Owner_Id;
                        }
                    }
                    else
                    {
                        if (OwnerOld.Owner_Id != data.owner_Id)
                        {
                            var query = db.MS_Owner.FirstOrDefault(c => c.Owner_Id == data.owner_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.owner_Id = OwnerOld.Owner_Id;
                        }
                    }

                    OwnerOld.Owner_Id = data.owner_Id;
                    OwnerOld.Owner_Name = data.owner_Name;
                    OwnerOld.Owner_SecondName = data.owner_SecondName;
                    OwnerOld.Owner_Address = data.owner_Address;
                    OwnerOld.OwnerType_Index = data.ownerType_Index;
                    OwnerOld.OwnerType_Id = data.ownerType_Id;
                    OwnerOld.OwnerType_Name = data.ownerType_Name;
                    OwnerOld.Country_Index = data.country_Index;
                    OwnerOld.Country_Id = data.country_Id;
                    OwnerOld.Country_Name = data.country_Name;
                    OwnerOld.Province_Index = data.province_Index;
                    OwnerOld.Province_Id = data.province_Id;
                    OwnerOld.Province_Name = data.province_Name;
                    OwnerOld.District_Index = data.district_Index;
                    OwnerOld.District_Id = data.district_Id;
                    OwnerOld.District_Name = data.district_Name;
                    OwnerOld.SubDistrict_Index = data.subDistrict_Index;
                    OwnerOld.SubDistrict_Id = data.subDistrict_Id;
                    OwnerOld.SubDistrict_Name = data.subDistrict_Name;
                    OwnerOld.Postcode_Index = data.postcode_Index;
                    OwnerOld.Postcode_Id = data.postcode_Id;
                    OwnerOld.Postcode_Name = data.postcode_Name;
                    OwnerOld.Owner_TaxID = data.owner_TaxID;
                    OwnerOld.Owner_Email = data.owner_Email;
                    OwnerOld.Owner_Fax = data.owner_Fax;
                    OwnerOld.Owner_Tel = data.owner_Tel;
                    OwnerOld.Owner_Mobile = data.owner_Mobile;
                    OwnerOld.Owner_Barcode = data.owner_Barcode;
                    OwnerOld.Contact_Person = data.contact_Person;
                    OwnerOld.Contact_Person2 = data.contact_Person2;
                    OwnerOld.Contact_Person3 = data.contact_Person3;
                    OwnerOld.Contact_Tel = data.contact_Tel;
                    OwnerOld.Contact_Tel2 = data.contact_Tel2;
                    OwnerOld.Contact_Tel3 = data.contact_Tel3;
                    OwnerOld.Contact_Email = data.contact_Email;
                    OwnerOld.Contact_Email2 = data.contact_Email2;
                    OwnerOld.Contact_Email3 = data.contact_Email3;
                    OwnerOld.Ref_No1 = data.ref_No1;
                    OwnerOld.Ref_No2 = data.ref_No2;
                    OwnerOld.Ref_No3 = data.ref_No3;
                    OwnerOld.Ref_No4 = data.ref_No4;
                    OwnerOld.Ref_No5 = data.ref_No5;
                    OwnerOld.Remark = data.remark;
                    OwnerOld.UDF_1 = data.udf_1;
                    OwnerOld.UDF_2 = data.udf_2;
                    OwnerOld.UDF_3 = data.udf_3;
                    OwnerOld.UDF_4 = data.udf_4;
                    OwnerOld.UDF_5 = data.udf_5;
                    OwnerOld.IsActive = Convert.ToInt32(data.isActive);
                    OwnerOld.Update_By = data.create_By;
                    OwnerOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveOwner", msglog);
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
        public OwnerViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_Owner.Where(c => c.Owner_Index == id).FirstOrDefault();

                var result = new OwnerViewModel();


                result.owner_Index = queryResult.Owner_Index;
                result.owner_Id = queryResult.Owner_Id;
                result.owner_Name = queryResult.Owner_Name;
                result.owner_SecondName = queryResult.Owner_SecondName;
                result.ownerType_Index = queryResult.OwnerType_Index;
                result.ownerType_Id = queryResult.OwnerType_Id;
                result.ownerType_Name = queryResult.OwnerType_Name;
                result.owner_Address = queryResult.Owner_Address;
                result.district_Index = queryResult.District_Index;
                result.district_Id = queryResult.District_Id;
                result.district_Name = queryResult.District_Name;
                result.subDistrict_Index = queryResult.SubDistrict_Index;
                result.subDistrict_Id = queryResult.SubDistrict_Id;
                result.subDistrict_Name = queryResult.SubDistrict_Name;
                result.province_Index = queryResult.Province_Index;
                result.province_Id = queryResult.Province_Id;
                result.province_Name = queryResult.Province_Name;
                result.country_Index = queryResult.Country_Index;
                result.country_Id = queryResult.Country_Id;
                result.country_Name = queryResult.Country_Name;
                result.postcode_Index = queryResult.Postcode_Index;
                result.postcode_Id = queryResult.Postcode_Id;
                result.postcode_Name = queryResult.Postcode_Name;
                result.owner_TaxID = queryResult.Owner_TaxID;
                result.owner_Email = queryResult.Owner_Email;
                result.owner_Fax = queryResult.Owner_Fax;
                result.owner_Tel = queryResult.Owner_Tel;
                result.owner_Mobile = queryResult.Owner_Mobile;
                result.owner_Barcode = queryResult.Owner_Barcode;
                result.contact_Person = queryResult.Contact_Person;
                result.contact_Person2 = queryResult.Contact_Person2;
                result.contact_Person3 = queryResult.Contact_Person3;
                result.contact_Tel = queryResult.Contact_Tel;
                result.contact_Tel2 = queryResult.Contact_Tel2;
                result.contact_Tel3 = queryResult.Contact_Tel3;
                result.contact_Email = queryResult.Contact_Email;
                result.contact_Email2 = queryResult.Contact_Email2;
                result.contact_Email3 = queryResult.Contact_Email3;
                result.ref_No1 = queryResult.Ref_No1;
                result.ref_No2 = queryResult.Ref_No2;
                result.ref_No3 = queryResult.Ref_No3;
                result.ref_No4 = queryResult.Ref_No4;
                result.ref_No5 = queryResult.Ref_No5;
                result.remark = queryResult.Remark;
                result.udf_1 = queryResult.UDF_1;
                result.udf_2 = queryResult.UDF_2;
                result.udf_3 = queryResult.UDF_3;
                result.udf_4 = queryResult.UDF_4;
                result.udf_5 = queryResult.UDF_5;
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
                result.value1 = queryResult.OwnerType_Id + " - " + queryResult.OwnerType_Name;
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDelete
        public Boolean getDelete(OwnerViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Owner = db.MS_Owner.Find(data.owner_Index);

                if (Owner != null)
                {
                    Owner.IsActive = 0;
                    Owner.IsDelete = 1;


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
                        olog.logging("DeleteOwner", msglog);
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

        #region ownerfilter
        public List<OwnerViewModel> ownerfilter(OwnerViewModel data)
        {
            try
            {
                var query = db.MS_Owner.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                var Item = new List<MS_Owner>();
                var TotalRow = new List<MS_Owner>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.Owner_Id).ToList();

                var result = new List<OwnerViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new OwnerViewModel();


                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.owner_SecondName = item.Owner_SecondName;
                    resultItem.owner_Address = item.Owner_Address;
                    resultItem.ownerType_Index = item.OwnerType_Index;
                    resultItem.ownerType_Id = item.OwnerType_Id;
                    resultItem.ownerType_Name = item.OwnerType_Name;
                    resultItem.country_Index = item.Country_Index;
                    resultItem.country_Id = item.Country_Id;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.province_Index = item.Province_Index;
                    resultItem.province_Id = item.Province_Id;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.district_Index = item.District_Index;
                    resultItem.district_Id = item.District_Id;
                    resultItem.district_Name = item.District_Name;
                    resultItem.subDistrict_Index = item.SubDistrict_Index;
                    resultItem.subDistrict_Id = item.SubDistrict_Id;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
                    resultItem.postcode_Index = item.Postcode_Index;
                    resultItem.postcode_Id = item.Postcode_Id;
                    resultItem.postcode_Name = item.Postcode_Name;
                    resultItem.owner_TaxID = item.Owner_TaxID;
                    resultItem.owner_Email = item.Owner_Email;
                    resultItem.owner_Fax = item.Owner_Fax;
                    resultItem.owner_Tel = item.Owner_Tel;
                    resultItem.owner_Mobile = item.Owner_Mobile;
                    resultItem.owner_Barcode = item.Owner_Barcode;
                    resultItem.contact_Person = item.Contact_Person;
                    resultItem.contact_Person2 = item.Contact_Person2;
                    resultItem.contact_Person3 = item.Contact_Person3;
                    resultItem.contact_Tel = item.Contact_Tel;
                    resultItem.contact_Tel2 = item.Contact_Tel2;
                    resultItem.contact_Tel3 = item.Contact_Tel3;
                    resultItem.contact_Email = item.Contact_Email;
                    resultItem.contact_Email2 = item.Contact_Email2;
                    resultItem.contact_Email3 = item.Contact_Email3;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.remark = item.Remark;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
                    resultItem.isSystem = item.IsSystem;
                    resultItem.status_Id = item.Status_Id;
                    resultItem.create_By = item.Create_By;
                    resultItem.create_Date = item.Create_Date;
                    resultItem.update_By = item.Update_By;
                    resultItem.update_Date = item.Update_Date;
                    resultItem.cancel_By = item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;


                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region filterOwnerPopupV2
        public actionResultOwnerViewModel filterOwnerPopupV2(SearchOwnerViewModel data)
        {
            try
            {
                var query = db.MS_Owner.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (data.listOwnerViewModel != null)
                {
                    foreach (var dataItem in data.listOwnerViewModel)
                    {
                        query = query.Where(c => c.Owner_Index != dataItem.owner_Index);
                    }
                }
                if (!string.IsNullOrEmpty(data.owner_Id))
                {
                    query = query.Where(c => c.Owner_Id.Contains(data.owner_Id));
                }
                if (!string.IsNullOrEmpty(data.owner_Name))
                {
                    query = query.Where(c => c.Owner_Name.Contains(data.owner_Name));
                }

                var Item = new List<MS_Owner>();
                var TotalRow = new List<MS_Owner>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                var num = 1;
                if (data.PerPage == 100)
                {
                    for (int i = 1; i < data.CurrentPage; i++)
                    {
                        num = num + 100;
                    }
                }
                if (data.PerPage == 50)
                {
                    for (int i = 1; i < data.CurrentPage; i++)
                    {
                        num = num + 50;
                    }
                }
                int rowCount = num;

                Item = query.OrderBy(o => o.Owner_Id).ToList();

                var result = new List<SearchOwnerViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchOwnerViewModel();

                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.owner_SecondName = item.Owner_SecondName;
                    resultItem.owner_Address = item.Owner_Address;
                    resultItem.ownerType_Index = item.OwnerType_Index;
                    resultItem.ownerType_Id = item.OwnerType_Id;
                    resultItem.ownerType_Name = item.OwnerType_Name;
                    resultItem.country_Index = item.Country_Index;
                    resultItem.country_Id = item.Country_Id;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.province_Index = item.Province_Index;
                    resultItem.province_Id = item.Province_Id;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.district_Index = item.District_Index;
                    resultItem.district_Id = item.District_Id;
                    resultItem.district_Name = item.District_Name;
                    resultItem.subDistrict_Index = item.SubDistrict_Index;
                    resultItem.subDistrict_Id = item.SubDistrict_Id;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
                    resultItem.postcode_Index = item.Postcode_Index;
                    resultItem.postcode_Id = item.Postcode_Id;
                    resultItem.postcode_Name = item.Postcode_Name;
                    resultItem.owner_TaxID = item.Owner_TaxID;
                    resultItem.owner_Email = item.Owner_Email;
                    resultItem.owner_Fax = item.Owner_Fax;
                    resultItem.owner_Tel = item.Owner_Tel;
                    resultItem.owner_Mobile = item.Owner_Mobile;
                    resultItem.owner_Barcode = item.Owner_Barcode;
                    resultItem.contact_Person = item.Contact_Person;
                    resultItem.contact_Person2 = item.Contact_Person2;
                    resultItem.contact_Person3 = item.Contact_Person3;
                    resultItem.contact_Tel = item.Contact_Tel;
                    resultItem.contact_Tel2 = item.Contact_Tel2;
                    resultItem.contact_Tel3 = item.Contact_Tel3;
                    resultItem.contact_Email = item.Contact_Email;
                    resultItem.contact_Email2 = item.Contact_Email2;
                    resultItem.contact_Email3 = item.Contact_Email3;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.remark = item.Remark;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
                    resultItem.isSystem = item.IsSystem;
                    resultItem.status_Id = item.Status_Id;
                    resultItem.create_By = item.Create_By;
                    resultItem.create_Date = item.Create_Date;
                    resultItem.update_By = item.Update_By;
                    resultItem.update_Date = item.Update_Date;
                    resultItem.cancel_By = item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date;
                    resultItem.row_Count = rowCount;
                    result.Add(resultItem);
                    rowCount++;
                }

                var count = TotalRow.Count;

                var actionResultOwnerViewModel = new actionResultOwnerViewModel();
                actionResultOwnerViewModel.itemsOwner = result.ToList();
                actionResultOwnerViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultOwnerViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //public List<ItemListViewModel> autoSearchOwner(ItemListViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())

        //        {
        //            var query = context.MS_Owner.AsQueryable();

        //            if (!string.IsNullOrEmpty(data.key))
        //            {
        //                query = query.Where(c => c.Owner_Name.Contains(data.key));
        //            }


        //            var items = new List<ItemListViewModel>();

        //            var result = query.Select(c => new { c.Owner_Index, c.Owner_Id, c.Owner_Name }).Distinct().Take(10).ToList();

        //            foreach (var item in result)
        //            {
        //                var resultItem = new ItemListViewModel
        //                {
        //                    index = item.Owner_Index,
        //                    id = item.Owner_Id,
        //                    name = item.Owner_Name,
        //                };

        //                items.Add(resultItem);
        //            }



        //            return items;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}

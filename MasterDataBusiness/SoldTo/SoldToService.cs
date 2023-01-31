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
    public class SoldToService
    {
        private MasterDataDbContext db;

        public SoldToService()
        {
            db = new MasterDataDbContext();
        }

        public SoldToService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region FindSoldTo
        public SoldToViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_SoldTo.Where(c => c.SoldTo_Index == id).FirstOrDefault();

                var result = new SoldToViewModel();


                result.soldTo_Index = queryResult.SoldTo_Index;
                result.soldTo_Id = queryResult.SoldTo_Id;
                result.soldTo_Name = queryResult.SoldTo_Name;
                result.soldTo_SecondName = queryResult.SoldTo_SecondName;
                result.soldToType_Index = queryResult.SoldToType_Index;
                result.soldToType_Id = queryResult.SoldToType_Id;
                result.soldToType_Name = queryResult.SoldToType_Name;
                result.soldTo_Address = queryResult.SoldTo_Address;
                result.country_Index = queryResult.Country_Index;
                result.country_Id = queryResult.Country_Id;
                result.country_Name = queryResult.Country_Name;
                result.province_Index = queryResult.Province_Index;
                result.province_Id = queryResult.Province_Id;
                result.province_Name = queryResult.Province_Name;
                result.district_Index = queryResult.District_Index;
                result.district_Id = queryResult.District_Id;
                result.district_Name = queryResult.District_Name;
                result.subDistrict_Index = queryResult.SubDistrict_Index;
                result.subDistrict_Id = queryResult.SubDistrict_Id;
                result.subDistrict_Name = queryResult.SubDistrict_Name;
                result.postcode_Index = queryResult.Postcode_Index;
                result.postcode_Id = queryResult.Postcode_Id;
                result.postcode_Name = queryResult.Postcode_Name;
                result.soldTo_Mobile = queryResult.SoldTo_Mobile;
                result.soldTo_TaxID = queryResult.SoldTo_TaxID;
                result.soldTo_Email = queryResult.SoldTo_Email;
                result.soldTo_Fax = queryResult.SoldTo_Fax;
                result.soldTo_Tel = queryResult.SoldTo_Tel;
                result.soldTo_Barcode = queryResult.SoldTo_Barcode;
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


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region FilterSoldTo
        //Filter
        public actionResultSoldToViewModel FilterInClause(string jsonData)
        {
            try
            {
                SearchSoldToInClauseViewModel data = JsonConvert.DeserializeObject<SearchSoldToInClauseViewModel>(jsonData);

                var query = db.View_SoldTo.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if ((data?.List_SoldTo_Index?.Count ?? 0) > 0)
                {
                    query = query.Where(c => data.List_SoldTo_Index.Contains(c.SoldTo_Index));
                }

                if ((data?.List_SoldTo_Id?.Count ?? 0) > 0)
                {
                    query = query.Where(c => data.List_SoldTo_Id.Contains(c.SoldTo_Id));
                }

                var Item = new List<View_SoldTo>();
                var TotalRow = new List<View_SoldTo>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.SoldTo_Id).ToList();

                var result = new List<SearchSoldToViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchSoldToViewModel();

                    resultItem.soldTo_Index = item.SoldTo_Index;
                    resultItem.soldTo_Id = item.SoldTo_Id;
                    resultItem.soldTo_Name = item.SoldTo_Name;
                    resultItem.soldTo_Address = item.SoldTo_Address;
                    resultItem.soldToType_Name = item.SoldToType_Name;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
                    resultItem.district_Name = item.District_Name;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.postcode_Name = item.Postcode_Name;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultSoldToViewModel = new actionResultSoldToViewModel();
                actionResultSoldToViewModel.itemsSoldTo = result.ToList();
                actionResultSoldToViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = null };

                return actionResultSoldToViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public actionResultSoldToViewModel filterSoldTo(SearchSoldToViewModel data)
        {
            try
            {
                var query = db.View_SoldTo.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!string.IsNullOrEmpty(data.soldTo_Index.ToString()))
                {
                    query = query.Where(c => c.SoldTo_Index == data.soldTo_Index);
                }

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.SoldTo_Name.Contains(data.key)
                                        || c.SoldTo_Id.Contains(data.key)
                                        || c.SoldToType_Name.Contains(data.key));


                }
                if (!string.IsNullOrEmpty(data.soldTo_Id))
                {
                    query = query.Where(c => c.SoldTo_Id.Contains(data.soldTo_Id));
                }
                if (!string.IsNullOrEmpty(data.soldTo_Name))
                {
                    query = query.Where(c => c.SoldTo_Name.Contains(data.soldTo_Name));

                }
                if (!string.IsNullOrEmpty(data.createdatesoldto_date) && !string.IsNullOrEmpty(data.createdatesoldto_date_to))
                {
                    var dateStart = data.createdatesoldto_date.toBetweenDate();
                    var dateEnd = data.createdatesoldto_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }

                var Item = new List<View_SoldTo>();
                var TotalRow = new List<View_SoldTo>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }
   
                Item = query.OrderBy(o => o.SoldTo_Id).ToList();

                var result = new List<SearchSoldToViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchSoldToViewModel();

                    resultItem.soldTo_Index = item.SoldTo_Index;
                    resultItem.soldTo_Id = item.SoldTo_Id;
                    resultItem.soldTo_Name = item.SoldTo_Name;
                    resultItem.soldTo_SecondName = item.SoldTo_SecondName;
                    resultItem.soldTo_Address = item.SoldTo_Address;
                    resultItem.soldToType_Index = item.SoldToType_Index;
                    resultItem.soldToType_Id = item.SoldToType_Id;
                    resultItem.soldToType_Name = item.SoldToType_Name;
                    resultItem.subDistrict_Index = item.SubDistrict_Index;
                    resultItem.subDistrict_Id = item.SubDistrict_Id;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
                    resultItem.district_Index = item.District_Index;
                    resultItem.district_Id = item.District_Id;
                    resultItem.district_Name = item.District_Name;
                    resultItem.province_Index = item.Province_Index;
                    resultItem.province_Id = item.Province_Id;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.country_Index = item.Country_Index;
                    resultItem.country_Id = item.Country_Id;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.postcode_Index = item.Province_Index;
                    resultItem.postcode_Id = item.Postcode_Id;
                    resultItem.postcode_Name = item.Postcode_Name;
                    resultItem.soldTo_TaxID = item.SoldTo_TaxID;
                    resultItem.soldTo_Email = item.SoldTo_Email;
                    resultItem.soldTo_Fax = item.SoldTo_Fax;
                    resultItem.soldTo_Tel = item.SoldTo_Tel;
                    resultItem.soldTo_Mobile = item.SoldTo_Mobile;
                    resultItem.soldTo_Barcode = item.SoldTo_Barcode;
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
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultSoldToViewModel = new actionResultSoldToViewModel();
                actionResultSoldToViewModel.itemsSoldTo = result.ToList();
                actionResultSoldToViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultSoldToViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region GetDeleteSoldTo
        public Boolean getDelete(SoldToViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var soldTo = db.MS_SoldTo.Find(data.soldTo_Index);

                if (soldTo != null)
                {
                    soldTo.IsActive = 0;
                    soldTo.IsDelete = 1;


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
                        olog.logging("DeleteSoldTo", msglog);
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

        #region SaveChangesSoldTo

        public String SaveChanges(SoldToViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var SoldToOld = db.MS_SoldTo.Find(data.soldTo_Index);

                if (SoldToOld == null)
                {
                    if (!string.IsNullOrEmpty(data.soldTo_Id))
                    {
                        var query = db.MS_SoldTo.FirstOrDefault(c => c.SoldTo_Id == data.soldTo_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.soldTo_Id))
                    {
                        data.soldTo_Id = "SoldTo_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_SoldTo.FirstOrDefault(c => c.SoldTo_Id == data.soldTo_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.SoldTo_Id == data.soldTo_Id)
                                {
                                    data.soldTo_Id = "SoldTo_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.soldTo_Id = "SoldTo_Id".genAutonumber();

                    MS_SoldTo Model = new MS_SoldTo();

                    Model.SoldTo_Index = Guid.NewGuid();
                    Model.SoldTo_Id = data.soldTo_Id;
                    Model.SoldToType_Index = data.soldToType_Index.sParse<Guid>();
                    Model.SoldToType_Id = data.soldToType_Id;
                    Model.SoldTo_Name = data.soldTo_Name;
                    Model.SoldTo_SecondName = data.soldTo_SecondName;
                    Model.SoldTo_Address = data.soldTo_Address;
                    Model.Country_Index = data.country_Index;
                    Model.Country_Id = data.country_Id;
                    Model.Country_Name = data.country_Name;
                    Model.Province_Index = data.province_Index;
                    Model.Province_Id = data.province_Id;
                    Model.Province_Name = data.province_Name;
                    Model.District_Index = data.district_Index;
                    Model.SubDistrict_Index = data.subDistrict_Index;
                    Model.SubDistrict_Id = data.subDistrict_Id;
                    Model.SubDistrict_Name = data.subDistrict_Name;
                    Model.Postcode_Index = data.postcode_Index;
                    Model.Postcode_Name = data.postcode_Name;
                    Model.Postcode_Id = data.postcode_Id;
                    Model.SoldTo_Mobile = data.soldTo_Mobile;
                    Model.SoldTo_TaxID = data.soldTo_TaxID;
                    Model.SoldTo_Email = data.soldTo_Email;
                    Model.SoldTo_Fax = data.soldTo_Fax;
                    Model.SoldTo_Tel = data.soldTo_Tel;
                    Model.SoldTo_Barcode = data.soldTo_Barcode;
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
                    Model.UDF_1 = null;
                    Model.UDF_2 = null;
                    Model.UDF_3 = null;
                    Model.UDF_4 = null;
                    Model.UDF_5 = null;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_SoldTo.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.soldTo_Id))
                    {
                        if (SoldToOld.SoldTo_Id != "")
                        {
                            data.soldTo_Id = SoldToOld.SoldTo_Id;
                        }
                    }
                    else
                    {
                        if (SoldToOld.SoldTo_Id != data.soldTo_Id)
                        {
                            var query = db.MS_SoldTo.FirstOrDefault(c => c.SoldTo_Id == data.soldTo_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.soldTo_Id = SoldToOld.SoldTo_Id;
                        }
                    }
                    SoldToOld.SoldTo_Id = data.soldTo_Id;
                    SoldToOld.SoldToType_Index = data.soldToType_Index.sParse<Guid>();
                    SoldToOld.SoldToType_Id = data.soldToType_Id;
                    SoldToOld.SoldToType_Name = data.soldToType_Name;
                    SoldToOld.SoldTo_Name = data.soldTo_Name;
                    SoldToOld.SoldTo_SecondName = data.soldTo_SecondName;
                    SoldToOld.SoldTo_Address = data.soldTo_Address;
                    SoldToOld.Country_Index = data.country_Index;
                    SoldToOld.Country_Id = data.country_Id;
                    SoldToOld.Country_Name = data.country_Name;
                    SoldToOld.Province_Index = data.province_Index;
                    SoldToOld.Province_Id = data.postcode_Id;
                    SoldToOld.Province_Name = data.province_Name;
                    SoldToOld.District_Index = data.district_Index;
                    SoldToOld.District_Id = data.district_Id;
                    SoldToOld.District_Name = data.district_Name;
                    SoldToOld.SubDistrict_Index = data.subDistrict_Index;
                    SoldToOld.SubDistrict_Id = data.subDistrict_Id;
                    SoldToOld.Postcode_Index = data.postcode_Index;
                    SoldToOld.Postcode_Id = data.postcode_Id;
                    SoldToOld.Postcode_Name = data.postcode_Name;
                    SoldToOld.SoldTo_Mobile = data.soldTo_Mobile;
                    SoldToOld.SoldTo_TaxID = data.soldTo_TaxID;
                    SoldToOld.SoldTo_Email = data.soldTo_Email;
                    SoldToOld.SoldTo_Fax = data.soldTo_Fax;
                    SoldToOld.SoldTo_Tel = data.soldTo_Tel;
                    SoldToOld.SoldTo_Barcode = data.soldTo_Barcode;
                    SoldToOld.Contact_Person = data.contact_Person;
                    SoldToOld.Contact_Person2 = data.contact_Person2;
                    SoldToOld.Contact_Person3 = data.contact_Person3;
                    SoldToOld.Contact_Tel = data.contact_Tel;
                    SoldToOld.Contact_Tel2 = data.contact_Tel2;
                    SoldToOld.Contact_Tel3 = data.contact_Tel3;
                    SoldToOld.Contact_Email = data.contact_Email;
                    SoldToOld.Contact_Email2 = data.contact_Email2;
                    SoldToOld.Contact_Email3 = data.contact_Email3;
                    SoldToOld.Ref_No1 = data.ref_No1;
                    SoldToOld.Ref_No2 = data.ref_No2;
                    SoldToOld.Ref_No3 = data.ref_No3;
                    SoldToOld.Ref_No4 = data.ref_No4;
                    SoldToOld.Ref_No5 = data.ref_No5;
                    SoldToOld.Remark = data.remark;
                    SoldToOld.UDF_1 = null;
                    SoldToOld.UDF_2 = null;
                    SoldToOld.UDF_3 = null;
                    SoldToOld.UDF_4 = null;
                    SoldToOld.UDF_5 = null;
                    SoldToOld.IsActive = Convert.ToInt32(data.isActive);
                    SoldToOld.Update_By = data.update_By;
                    SoldToOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveSoldTo", msglog);
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

        #region SearchSoldToFilter

        public List<ItemListViewModel> autoSearchSoldToFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_SoldTo.Where(c => c.SoldTo_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.SoldTo_Name,
                        key = s.SoldTo_Name
                    }).Distinct();

                    var query2 = db.View_SoldTo.Where(c => c.SoldTo_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.SoldTo_Id,
                        key = s.SoldTo_Id
                    }).Distinct();

                    var query3 = db.View_SoldTo.Where(c => c.SoldToType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.SoldToType_Name,
                        key = s.SoldToType_Name
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

        #region autoSoldToSearch

        public List<ItemListViewModel> autoSearchSoldTo(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_SoldTo.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.SoldTo_Id.Contains(data.key)
                                                || c.SoldTo_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.SoldTo_Name, c.SoldTo_Index, c.SoldTo_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.SoldTo_Index,
                            id = item.SoldTo_Id,
                            name = item.SoldTo_Id + " - " + item.SoldTo_Name,
                            key = item.SoldTo_Id + " - " + item.SoldTo_Name,
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


        public List<SoldToViewModel> filter(SoldToViewModel data)
        {
            try
            {
                var query = db.MS_SoldTo.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!string.IsNullOrEmpty(data.soldTo_Index.ToString()) && data.soldTo_Index !=  new Guid("00000000-0000-0000-0000-000000000000"))
                {
                    query = query.Where(c => c.SoldTo_Index == data.soldTo_Index);
                }


                if (!string.IsNullOrEmpty(data.soldTo_Id))
                {
                    query = query.Where(c => c.SoldTo_Id == data.soldTo_Id);
                }
                if (!string.IsNullOrEmpty(data.soldTo_Name))
                {
                    query = query.Where(c => c.SoldTo_Name == data.soldTo_Name);

                }

                var Item = new List<MS_SoldTo>();



                Item = query.OrderBy(o => o.SoldTo_Id).ToList();

                var result = new List<SoldToViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SoldToViewModel();

                    resultItem.soldTo_Index = item.SoldTo_Index;
                    resultItem.soldTo_Id = item.SoldTo_Id;
                    resultItem.soldTo_Name = item.SoldTo_Name;
                    resultItem.soldTo_SecondName = item.SoldTo_SecondName;
                    resultItem.soldTo_Address = item.SoldTo_Address;
                    resultItem.soldToType_Index = item.SoldToType_Index;
                    resultItem.soldToType_Id = item.SoldToType_Id;
                    resultItem.soldToType_Name = item.SoldToType_Name;
                    resultItem.subDistrict_Index = item.SubDistrict_Index;
                    resultItem.subDistrict_Id = item.SubDistrict_Id;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
                    resultItem.district_Index = item.District_Index;
                    resultItem.district_Id = item.District_Id;
                    resultItem.district_Name = item.District_Name;
                    resultItem.province_Index = item.Province_Index;
                    resultItem.province_Id = item.Province_Id;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.country_Index = item.Country_Index;
                    resultItem.country_Id = item.Country_Id;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.postcode_Index = item.Province_Index;
                    resultItem.postcode_Id = item.Postcode_Id;
                    resultItem.postcode_Name = item.Postcode_Name;
                    resultItem.soldTo_TaxID = item.SoldTo_TaxID;
                    resultItem.soldTo_Email = item.SoldTo_Email;
                    resultItem.soldTo_Fax = item.SoldTo_Fax;
                    resultItem.soldTo_Tel = item.SoldTo_Tel;
                    resultItem.soldTo_Mobile = item.SoldTo_Mobile;
                    resultItem.soldTo_Barcode = item.SoldTo_Barcode;
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
                    result.Add(resultItem);
                }



                return result;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        #region filterPopupSoldTo
        //Filter
        public actionResultSoldToPopupViewModel filterPopupSoldTo(SoldToPopupViewModel data)
        {
            try
            {
                var query = db.View_SoldTo.AsQueryable();

                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                if (data.listSoldToViewModel != null)
                {
                    foreach (var dataItem in data.listSoldToViewModel)
                    {
                        query = query.Where(q => q.SoldTo_Index != dataItem.soldTo_Index);
                    }
                }
                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.SoldTo_Name.Contains(data.key)
                                        || c.SoldTo_Id.Contains(data.key)
                                        || c.SoldToType_Name.Contains(data.key));


                }
                if (!string.IsNullOrEmpty(data.soldTo_Id))
                {
                    query = query.Where(c => c.SoldTo_Id.Contains(data.soldTo_Id));
                }
                if (!string.IsNullOrEmpty(data.soldTo_Name))
                {
                    query = query.Where(c => c.SoldTo_Name.Contains(data.soldTo_Name));

                }



                var Item = new List<View_SoldTo>();
                var TotalRow = new List<View_SoldTo>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }
                //foreach (var item in data.listSoldToViewModel)
                //{
                //    query = query.Where(q => q.SoldTo_Index != item.soldTo_Index);
                //}

                Item = query.OrderBy(o => o.SoldTo_Id).ToList();


                //Item = query.Where(c => c.)

                var result = new List<SoldToPopupViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SoldToPopupViewModel();

                    resultItem.soldTo_Index = item.SoldTo_Index;
                    //if(data.listSoldToViewModel != null)
                    //{
                    //    foreach(var dataList in data.listSoldToViewModel)
                    //    {
                    //        if(resultItem.soldTo_Index == dataList.soldTo_Index)
                    //        {
                    //            resultItem.checkSelected = 1;
                    //            resultItem.selected = true;
                    //        }
                    //    }
                    //}
                    resultItem.soldTo_Id = item.SoldTo_Id;
                    resultItem.soldTo_Name = item.SoldTo_Name;
                    resultItem.soldTo_Address = item.SoldTo_Address;
                    resultItem.soldToType_Name = item.SoldToType_Name;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
                    resultItem.district_Name = item.District_Name;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.postcode_Name = item.Postcode_Name;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultSoldToPopupViewModel = new actionResultSoldToPopupViewModel();
                actionResultSoldToPopupViewModel.itemsSoldToPopup = result.ToList();
                actionResultSoldToPopupViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultSoldToPopupViewModel;
    
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Export Excel
        public actionResultSoldToViewModel Export(SearchSoldToViewModel data)
        {
            try
            {
                var query = db.View_SoldTo.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!string.IsNullOrEmpty(data.soldTo_Index.ToString()))
                {
                    query = query.Where(c => c.SoldTo_Index == data.soldTo_Index);
                }

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.SoldTo_Name.Contains(data.key)
                                        || c.SoldTo_Id.Contains(data.key)
                                        || c.SoldToType_Name.Contains(data.key));


                }
                if (!string.IsNullOrEmpty(data.soldTo_Id))
                {
                    query = query.Where(c => c.SoldTo_Id.Contains(data.soldTo_Id));
                }
                if (!string.IsNullOrEmpty(data.soldTo_Name))
                {
                    query = query.Where(c => c.SoldTo_Name.Contains(data.soldTo_Name));

                }
                if (!string.IsNullOrEmpty(data.createdatesoldto_date) && !string.IsNullOrEmpty(data.createdatesoldto_date_to))
                {
                    var dateStart = data.createdatesoldto_date.toBetweenDate();
                    var dateEnd = data.createdatesoldto_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }

                var Item = new List<View_SoldTo>();
                var TotalRow = new List<View_SoldTo>();

                TotalRow = query.ToList();
            

                Item = query.OrderBy(o => o.SoldTo_Id).ToList();

                var result = new List<SearchSoldToViewModel>();
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new SearchSoldToViewModel();
                    resultItem.row_Count = num + 1;
                    resultItem.soldTo_Index = item.SoldTo_Index;
                    resultItem.soldTo_Id = item.SoldTo_Id;
                    resultItem.soldTo_Name = item.SoldTo_Name;
                    resultItem.soldTo_SecondName = item.SoldTo_SecondName == null ? "" : item.SoldTo_SecondName;
                    resultItem.soldTo_Address = item.SoldTo_Address == null ? "" : item.SoldTo_Address;
                    resultItem.soldToType_Index = item.SoldToType_Index;
                    resultItem.soldToType_Id = item.SoldToType_Id;
                    resultItem.soldToType_Name = item.SoldToType_Name;
                    resultItem.subDistrict_Index = item.SubDistrict_Index;
                    resultItem.subDistrict_Id = item.SubDistrict_Id;
                    resultItem.subDistrict_Name = item.SubDistrict_Name == null ? "" : item.SubDistrict_Name;
                    resultItem.district_Index = item.District_Index;
                    resultItem.district_Id = item.District_Id;
                    resultItem.district_Name = item.District_Name == null ? "" : item.District_Name;
                    resultItem.province_Index = item.Province_Index;
                    resultItem.province_Id = item.Province_Id;
                    resultItem.province_Name = item.Province_Name == null ? "" : item.Province_Name;
                    resultItem.country_Index = item.Country_Index;
                    resultItem.country_Id = item.Country_Id;
                    resultItem.country_Name = item.Country_Name == null ? "" : item.Country_Name;
                    resultItem.postcode_Index = item.Province_Index;
                    resultItem.postcode_Id = item.Postcode_Id;
                    resultItem.postcode_Name = item.Postcode_Name == null ? "" : item.Postcode_Name;
                    resultItem.soldTo_TaxID = item.SoldTo_TaxID == null ? "" : item.SoldTo_TaxID;
                    resultItem.soldTo_Email = item.SoldTo_Email == null ? "" : item.SoldTo_Email;
                    resultItem.soldTo_Fax = item.SoldTo_Fax == null ? "" : item.SoldTo_Fax;
                    resultItem.soldTo_Tel = item.SoldTo_Tel == null ? "" : item.SoldTo_Tel;
                    resultItem.soldTo_Mobile = item.SoldTo_Mobile == null ? "" : item.SoldTo_Mobile;
                    resultItem.soldTo_Barcode = item.SoldTo_Barcode == null ? "" : item.SoldTo_Barcode;
                    resultItem.contact_Person = item.Contact_Person == null ? "" : item.Contact_Person;
                    resultItem.contact_Person2 = item.Contact_Person2 == null ? "" : item.Contact_Person2;
                    resultItem.contact_Person3 = item.Contact_Person3 == null ? "" : item.Contact_Person3;
                    resultItem.contact_Tel = item.Contact_Tel == null ? "" : item.Contact_Tel;
                    resultItem.contact_Tel2 = item.Contact_Tel2 == null ? "" : item.Contact_Tel2;
                    resultItem.contact_Tel3 = item.Contact_Tel3 == null ? "" : item.Contact_Tel3;
                    resultItem.contact_Email = item.Contact_Email == null ? "" : item.Contact_Email;
                    resultItem.contact_Email2 = item.Contact_Email2 == null ? "" : item.Contact_Email2;
                    resultItem.contact_Email3 = item.Contact_Email3 == null ? "" : item.Contact_Email3;
                    resultItem.ref_No1 = item.Ref_No1 == null ? "" : item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2 == null ? "" : item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3 == null ? "" : item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4 == null ? "" : item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5 == null ? "" : item.Ref_No5;
                    resultItem.remark = item.Remark == null ? "" : item.Remark;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var actionResultSoldToViewModel = new actionResultSoldToViewModel();
                actionResultSoldToViewModel.itemsSoldTo = result.ToList();
                actionResultSoldToViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultSoldToViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}


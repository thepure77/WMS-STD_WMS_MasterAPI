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
    public class ShipToService
    {
        private MasterDataDbContext db;

        public ShipToService()
        {
            db = new MasterDataDbContext();
        }

        public ShipToService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region FindShipTo
        public ShipToViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_ShipTo.Where(c => c.ShipTo_Index == id).FirstOrDefault();

                var result = new ShipToViewModel();


                result.shipTo_Index = queryResult.ShipTo_Index;
                result.shipTo_Id = queryResult.ShipTo_Id;
                result.shipToType_Name = queryResult.ShipToType_Name;
                result.shipToType_Index = queryResult.ShipToType_Index;
                result.shipToType_Id = queryResult.ShipToType_Id;
                result.shipTo_Name = queryResult.ShipTo_Name;
                result.shipTo_SecondName = queryResult.ShipTo_SecondName;
                result.shipTo_Address = queryResult.ShipTo_Address;
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
                result.shipTo_Mobile = queryResult.ShipTo_Mobile;
                result.shipTo_TaxID = queryResult.ShipTo_TaxID;
                result.shipTo_Email = queryResult.ShipTo_Email;
                result.shipTo_Fax = queryResult.ShipTo_Fax;
                result.shipTo_Tel = queryResult.ShipTo_Tel;
                result.shipTo_Barcode = queryResult.ShipTo_Barcode;
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
                result.businessUnit_Index = queryResult.BusinessUnit_Index;
                result.businessUnit_Id = queryResult.BusinessUnit_Id;
                result.businessUnit_Name = queryResult.BusinessUnit_Name;
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

        #region FilterShipTo
        //Filter
        public actionResultShipToViewModel FilterInClause(string jsonData)
        {
            try
            {
                SearchShipToInClauseViewModel data = JsonConvert.DeserializeObject<SearchShipToInClauseViewModel>(jsonData);

                var query = db.View_ShipTo.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if ((data?.List_ShipTo_Index?.Count ?? 0) > 0)
                {
                    query = query.Where(c => data.List_ShipTo_Index.Contains(c.ShipTo_Index));
                }

                if ((data?.List_ShipTo_Id?.Count ?? 0) > 0)
                {
                    query = query.Where(c => data.List_ShipTo_Id.Contains(c.ShipTo_Id));
                }

                var Item = new List<View_ShipTo>();
                var TotalRow = new List<View_ShipTo>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ShipTo_Id).ToList();

                var result = new List<SearchShipToViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchShipToViewModel();

                    resultItem.shipTo_Index = item.ShipTo_Index;
                    resultItem.shipTo_Id = item.ShipTo_Id;
                    resultItem.shipTo_Name = item.ShipTo_Name;
                    resultItem.shipTo_Address = item.ShipTo_Address;
                    resultItem.shipToType_Name = item.ShipToType_Name;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
                    resultItem.district_Name = item.District_Name;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.postcode_Name = item.Postcode_Name;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultShipToViewModel = new actionResultShipToViewModel();
                actionResultShipToViewModel.itemsShipTo = result.ToList();
                actionResultShipToViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = null };

                return actionResultShipToViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public actionResultShipToViewModel filter(SearchShipToViewModel data)
        {
            try
            {
                var query = db.View_ShipTo.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (data.changeSet != "1")
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ShipTo_Name.Contains(data.key)
                                            || c.ShipTo_Id.Contains(data.key)
                                            || c.ShipToType_Name.Contains(data.key));


                    }

                    if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                    {
                        var dateStart = data.create_date.toBetweenDate();
                        var dateEnd = data.create_date_to.toBetweenDate();
                        query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ShipTo_Name.Contains(data.key)
                                            || c.ShipTo_Id.Contains(data.key)
                                            || c.ShipToType_Name.Contains(data.key));


                    }
                }             


                /*if (!string.IsNullOrEmpty(data.businessUnit_Index.ToString())
                {
                    query = query.Where(c => c.BusinessUnit_Index.Contains(data.businessUnit_Index.ToString())
                                        || c.BusinessUnit_Id.Contains(data.businessUnit_Index)
                                        || c.BusinessUnit_Name.Contains(data.businessUnit_Index));


                }*/

                var Item = new List<View_ShipTo>();
                var TotalRow = new List<View_ShipTo>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ShipTo_Id).ToList();

                var result = new List<SearchShipToViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchShipToViewModel();

                    resultItem.shipTo_Index = item.ShipTo_Index;
                    resultItem.shipTo_Id = item.ShipTo_Id;
                    resultItem.shipToType_Name = item.ShipToType_Name;
                    resultItem.shipToType_Index = item.ShipToType_Index;
                    resultItem.shipToType_Id = item.ShipToType_Id;
                    resultItem.shipTo_Name = item.ShipTo_Name;
                    resultItem.shipTo_SecondName = item.ShipTo_SecondName;
                    resultItem.shipTo_Address = item.ShipTo_Address;
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
                    resultItem.shipTo_Mobile = item.ShipTo_Mobile;
                    resultItem.shipTo_TaxID = item.ShipTo_TaxID;
                    resultItem.shipTo_Email = item.ShipTo_Email;
                    resultItem.shipTo_Fax = item.ShipTo_Fax;
                    resultItem.shipTo_Tel = item.ShipTo_Tel;
                    resultItem.shipTo_Barcode = item.ShipTo_Barcode;
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
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.businessUnit_Index = item.BusinessUnit_Index;
                    resultItem.businessUnit_Id = item.BusinessUnit_Id;
                    resultItem.businessUnit_Name = item.BusinessUnit_Name;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultShipToViewModel = new actionResultShipToViewModel();
                actionResultShipToViewModel.itemsShipTo = result.ToList();
                actionResultShipToViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultShipToViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDeleteShipTo

        public Boolean getDelete(ShipToViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var shipTo = db.MS_ShipTo.Find(data.shipTo_Index);

                if (shipTo != null)
                {
                    shipTo.IsActive = 0;
                    shipTo.IsDelete = 1;


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
                        olog.logging("DeleteShipTo", msglog);
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

        #region SaveChanges 
        public String SaveChanges(ShipToViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ShipToOld = db.MS_ShipTo.Find(data.shipTo_Index);

                if (ShipToOld == null)
                {
                    if (!string.IsNullOrEmpty(data.shipTo_Id))
                    {
                        var query = db.MS_ShipTo.FirstOrDefault(c => c.ShipTo_Id == data.shipTo_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.shipTo_Id))
                    {
                        data.shipTo_Id = "ShipTo_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_ShipTo.FirstOrDefault(c => c.ShipTo_Id == data.shipTo_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ShipTo_Id == data.shipTo_Id)
                                {
                                    data.shipTo_Id = "ShipTo_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_ShipTo Model = new MS_ShipTo();

                    Model.ShipTo_Index = Guid.NewGuid();
                    Model.ShipTo_Id = data.shipTo_Id;
                    Model.ShipToType_Index = data.shipToType_Index.sParse<Guid>();
                    Model.ShipToType_Id = data.shipToType_Id;
                    Model.ShipToType_Name = data.shipToType_Name;
                    Model.ShipTo_Name = data.shipTo_Name;
                    Model.ShipTo_SecondName = data.shipTo_SecondName;
                    Model.ShipTo_Address = data.shipTo_Address;
                    Model.Country_Index = data.country_Index;
                    Model.Country_Id = data.country_Id;
                    Model.Country_Name = data.country_Name;
                    Model.Province_Index = data.province_Index;
                    Model.Province_Id = data.postcode_Id;
                    Model.Province_Name = data.postcode_Name;
                    Model.District_Index = data.district_Index;
                    Model.District_Id = data.district_Id;
                    Model.District_Name = data.district_Name;
                    Model.SubDistrict_Index = data.subDistrict_Index;
                    Model.SubDistrict_Id = data.subDistrict_Id;
                    Model.SubDistrict_Name = data.subDistrict_Name;
                    Model.Postcode_Index = data.postcode_Index;
                    Model.Postcode_Id = data.postcode_Id;
                    Model.Postcode_Name = data.postcode_Name;
                    Model.ShipTo_Mobile = data.shipTo_Mobile;
                    Model.ShipTo_TaxID = data.shipTo_TaxID;
                    Model.ShipTo_Email = data.shipTo_Email;
                    Model.ShipTo_Fax = data.shipTo_Fax;
                    Model.ShipTo_Tel = data.shipTo_Tel;
                    Model.ShipTo_Barcode = data.shipTo_Barcode;
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
                    Model.BusinessUnit_Index = data.businessUnit_Index;
                    Model.BusinessUnit_Id = data.businessUnit_Id;
                    Model.BusinessUnit_Name = data.businessUnit_Name;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_ShipTo.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.shipTo_Id))
                    {
                        if (ShipToOld.ShipTo_Id != "")
                        {
                            data.shipTo_Id = ShipToOld.ShipTo_Id;
                        }
                    }
                    else
                    {
                        if (ShipToOld.ShipTo_Id != data.shipTo_Id)
                        {
                            var query = db.MS_ShipTo.FirstOrDefault(c => c.ShipTo_Id == data.shipTo_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.shipTo_Id = ShipToOld.ShipTo_Id;
                        }
                    }

                    ShipToOld.ShipTo_Id = data.shipTo_Id;
                    ShipToOld.ShipToType_Index = data.shipToType_Index.sParse<Guid>();
                    ShipToOld.ShipToType_Id = data.shipToType_Id;
                    ShipToOld.ShipToType_Name = data.shipToType_Name;
                    ShipToOld.ShipTo_Name = data.shipTo_Name;
                    ShipToOld.ShipTo_SecondName = data.shipTo_SecondName;
                    ShipToOld.ShipTo_Address = data.shipTo_Address;
                    ShipToOld.Country_Index = data.country_Index;
                    ShipToOld.Country_Id = data.country_Id;
                    ShipToOld.Country_Name = data.country_Name;
                    ShipToOld.Province_Index = data.province_Index;
                    ShipToOld.Province_Id = data.province_Id;
                    ShipToOld.Province_Name = data.province_Name;
                    ShipToOld.District_Index = data.district_Index;
                    ShipToOld.District_Id = data.district_Id;
                    ShipToOld.SubDistrict_Index = data.subDistrict_Index;
                    ShipToOld.SubDistrict_Id = data.subDistrict_Id;
                    ShipToOld.SubDistrict_Name = data.subDistrict_Name;
                    ShipToOld.Postcode_Index = data.postcode_Index;
                    ShipToOld.Postcode_Name = data.postcode_Name;
                    ShipToOld.Postcode_Id = data.postcode_Id;
                    ShipToOld.ShipTo_Mobile = data.shipTo_Mobile;
                    ShipToOld.ShipTo_TaxID = data.shipTo_TaxID;
                    ShipToOld.ShipTo_Email = data.shipTo_Email;
                    ShipToOld.ShipTo_Fax = data.shipTo_Fax;
                    ShipToOld.ShipTo_Tel = data.shipTo_Tel;
                    ShipToOld.ShipTo_Barcode = data.shipTo_Barcode;
                    ShipToOld.Contact_Person = data.contact_Person;
                    ShipToOld.Contact_Tel = data.contact_Tel;
                    ShipToOld.Contact_Email = data.contact_Email;
                    ShipToOld.Ref_No1 = data.ref_No1;
                    ShipToOld.Ref_No2 = data.ref_No2;
                    ShipToOld.Ref_No3 = data.ref_No3;
                    ShipToOld.Ref_No4 = data.ref_No4;
                    ShipToOld.Ref_No5 = data.ref_No5;
                    ShipToOld.Remark = data.remark;
                    ShipToOld.UDF_1 = null;
                    ShipToOld.UDF_2 = null;
                    ShipToOld.UDF_3 = null;
                    ShipToOld.UDF_4 = null;
                    ShipToOld.UDF_5 = null;
                    ShipToOld.BusinessUnit_Index = data.businessUnit_Index;
                    ShipToOld.BusinessUnit_Id = data.businessUnit_Id;
                    ShipToOld.BusinessUnit_Name = data.businessUnit_Name;
                    ShipToOld.IsActive = Convert.ToInt32(data.isActive);
                    ShipToOld.Update_By = data.update_By;
                    ShipToOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveShipTo", msglog);
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

        #region SearchShipToFilter
        public List<ItemListViewModel> autoSearchShipToFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_ShipTo.Where(c => c.ShipTo_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ShipTo_Name,
                        key = s.ShipTo_Name
                    }).Distinct();

                    var query2 = db.View_ShipTo.Where(c => c.ShipTo_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ShipTo_Id,
                        key = s.ShipTo_Id
                    }).Distinct();

                    var query3 = db.View_ShipTo.Where(c => c.ShipToType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ShipToType_Name,
                        key = s.ShipToType_Name
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

        #region autoShipToSearch
        public List<ItemListViewModel> autoSearchShipTo(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_ShipTo.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ShipTo_Id.Contains(data.key)
                                                || c.ShipTo_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.ShipTo_Name, c.ShipTo_Index, c.ShipTo_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.ShipTo_Index,
                            id = item.ShipTo_Id,
                            name = item.ShipTo_Id + " - " + item.ShipTo_Name,
                            key = item.ShipTo_Id + " - " + item.ShipTo_Name,
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

        #region filterShipToPopup
        public actionResultShipToViewModel filterShiptoPopup(SearchShipToViewModel data)
        {
            try
            {
                var query = db.View_ShipTo.AsQueryable();

                query = query.Where(c =>c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (data.listShipToViewModel != null)
                {
                    foreach (var dataItem in data.listShipToViewModel)
                    {
                        query = query.Where(c => c.ShipTo_Index != dataItem.shipTo_Index);
                    }
                }
                if (!string.IsNullOrEmpty(data.shipTo_Id))
                {
                    query = query.Where(c => c.ShipTo_Id.Contains(data.shipTo_Id));
                }
                if (!string.IsNullOrEmpty(data.shipTo_Name))
                {
                    query = query.Where(c => c.ShipTo_Name.Contains(data.shipTo_Name));
                }

                var Item = new List<View_ShipTo>();
                var TotalRow = new List<View_ShipTo>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ShipTo_Id).ToList();

                var result = new List<SearchShipToViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchShipToViewModel();

                    resultItem.shipTo_Index = item.ShipTo_Index;
                    resultItem.shipTo_Id = item.ShipTo_Id;
                    resultItem.shipTo_Name = item.ShipTo_Name;
                    resultItem.shipTo_Address = item.ShipTo_Address;
                    resultItem.shipToType_Name = item.ShipToType_Name;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
                    resultItem.district_Name = item.District_Name;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.postcode_Name = item.Postcode_Name;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultShipToViewModel = new actionResultShipToViewModel();
                actionResultShipToViewModel.itemsShipTo = result.ToList();
                actionResultShipToViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultShipToViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region shipToFilter
        public List<ShipToViewModel> shipToFilter(ShipToViewModel data)
        {
            try
            {
                var query = db.MS_ShipTo.AsQueryable();


                if (!string.IsNullOrEmpty(data.shipTo_Index.ToString()) && data.shipTo_Index != new Guid("00000000-0000-0000-0000-000000000000"))
                {
                    query = query.Where(c => c.ShipTo_Index == data.shipTo_Index);
                }

                if (!string.IsNullOrEmpty(data.shipTo_Id))
                {
                    query = query.Where(c => c.ShipTo_Id == data.shipTo_Id);
                }

                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                var Item = new List<MS_ShipTo>();


                Item = query.OrderBy(o => o.ShipTo_Id).ToList();

                var result = new List<ShipToViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new ShipToViewModel();
                    var route = db.MS_Route.Where(c => c.Route_Id == item.UDF_1).FirstOrDefault();
                    if (route != null)
                    {
                        resultItem.Route_Index = route.Route_Index.ToString();
                        resultItem.Route_Id = route.Route_Id;
                        resultItem.Route_Name = route.Route_Name;
                    }
                    var subroute = db.MS_SubRoute.Where(c => c.SubRoute_Id == item.UDF_2).FirstOrDefault();
                    if(subroute != null)
                    {
                        resultItem.SubRoute_Index = subroute.SubRoute_Index.ToString();
                        resultItem.SubRoute_Id = subroute.SubRoute_Id;
                        resultItem.SubRoute_Name = subroute.SubRoute_Name;
                    }


                    resultItem.shipTo_Index = item.ShipTo_Index;
                    resultItem.shipTo_Id = item.ShipTo_Id;
                    resultItem.shipTo_Name = item.ShipTo_Name;
                    resultItem.shipTo_SecondName = item.ShipTo_SecondName;
                    resultItem.shipTo_Address = item.ShipTo_Address;
                    resultItem.shipToType_Index = item.ShipToType_Index;
                    resultItem.shipToType_Id = item.ShipToType_Id;
                    resultItem.shipToType_Name = item.ShipToType_Name;
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
                    resultItem.shipTo_TaxID = item.ShipTo_TaxID;
                    resultItem.shipTo_Email = item.ShipTo_Email;
                    resultItem.shipTo_Fax = item.ShipTo_Fax;
                    resultItem.shipTo_Tel = item.ShipTo_Tel;
                    resultItem.shipTo_Mobile = item.ShipTo_Mobile;
                    resultItem.shipTo_Barcode = item.ShipTo_Barcode;
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



                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region shipTotypeFilter
        public List<ShipToViewModel> shipTotypeFilter(ShipToViewModel data)
        {
            try
            {
                var query = db.MS_ShipTo.Where(c=> c.IsActive == 1 && c.IsDelete == 0 && c.ShipToType_Index == data.shipToType_Index);
                
                var Item = query.OrderBy(o => o.ShipTo_Id).ToList();

                var result = new List<ShipToViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new ShipToViewModel();
                    resultItem.shipTo_Index = item.ShipTo_Index;
                    resultItem.shipTo_Id = item.ShipTo_Id;
                    resultItem.shipTo_Name = item.ShipTo_Name;
                    resultItem.shipTo_SecondName = item.ShipTo_SecondName;
                    resultItem.shipTo_Address = item.ShipTo_Address;
                    resultItem.shipToType_Index = item.ShipToType_Index;
                    resultItem.shipToType_Id = item.ShipToType_Id;
                    resultItem.shipToType_Name = item.ShipToType_Name;
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
                    resultItem.shipTo_TaxID = item.ShipTo_TaxID;
                    resultItem.shipTo_Email = item.ShipTo_Email;
                    resultItem.shipTo_Fax = item.ShipTo_Fax;
                    resultItem.shipTo_Tel = item.ShipTo_Tel;
                    resultItem.shipTo_Mobile = item.ShipTo_Mobile;
                    resultItem.shipTo_Barcode = item.ShipTo_Barcode;
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



                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public string genBranchCode(BranchCodeViewModel model)
        {
            //var result = "";
            var shipto = db.MS_ShipTo.Where(c => c.ShipTo_Index == model.shipTo_Index).FirstOrDefault();
            var result = ("Branch_Code ("+model.format_Text+")").genAutonumber();
            int i = 1;
            while (i > 0)
            {
                var query = db.MS_ShipTo.FirstOrDefault(c => c.Ref_No3 == result && c.IsActive == 1);
                if (query != null)
                {
                    if (query.Ref_No3 == result)
                    {
                        result = ("Branch_Code (" + model.format_Text + ")").genAutonumber();
                    }
                }
                else
                {
                    break;
                }
            }
            int runningNumber = int.Parse(result);
            shipto.Ref_No3 = model.format_Text + runningNumber.ToString("00000");
            db.SaveChanges();
            return model.format_Text + result;
        }

        #region Export Excel
        public ResultShipToViewModel Export(ShipToExportViewModel data)
        {
            try
            {
                var query = db.MS_ShipTo.AsQueryable();

                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (data.changeSet != "1")
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ShipTo_Name.Contains(data.key)
                                            || c.ShipTo_Id.Contains(data.key)
                                            || c.ShipToType_Name.Contains(data.key));


                    }

                    if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                    {
                        var dateStart = data.create_date.toBetweenDate();
                        var dateEnd = data.create_date_to.toBetweenDate();
                        query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ShipTo_Name.Contains(data.key)
                                            || c.ShipTo_Id.Contains(data.key)
                                            || c.ShipToType_Name.Contains(data.key));


                    }
                }

                var Item = new List<MS_ShipTo>();
                var TotalRow = new List<MS_ShipTo>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.ShipTo_Id).ToList();

                var result = new List<ShipToExportViewModel>();
                //var num = 0;
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new ShipToExportViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.shipTo_Index = item.ShipTo_Index;
                    resultItem.shipTo_Id = item.ShipTo_Id;
                    resultItem.shipTo_Name = item.ShipTo_Name == null ? "" : item.ShipTo_Name;
                    resultItem.shipTo_SecondName = item.ShipTo_SecondName == null ? "" : item.ShipTo_SecondName;
                    resultItem.shipToType_Name = item.ShipToType_Name;
                    resultItem.shipTo_TaxID = item.ShipTo_TaxID == null ? "" : item.ShipTo_TaxID;
                    resultItem.shipTo_Email = item.ShipTo_Email == null ? "" : item.ShipTo_Email;
                    resultItem.shipTo_Fax = item.ShipTo_Fax == null ? "" : item.ShipTo_Fax;
                    resultItem.shipTo_Tel = item.ShipTo_Tel == null ? "" : item.ShipTo_Tel;

                    resultItem.shipTo_Mobile = item.ShipTo_Mobile == null ? "" : item.ShipTo_Mobile;
                    resultItem.shipTo_Barcode = item.ShipTo_Barcode == null ? "" : item.ShipTo_Barcode;
                    resultItem.contact_Person = item.Contact_Person == null ? "" : item.Contact_Person;
                    resultItem.contact_Person2 = item.Contact_Person2 == null ? "" : item.Contact_Person2;
                    resultItem.contact_Person3 = item.Contact_Person3 == null ? "" : item.Contact_Person3;
                    resultItem.contact_Tel = item.Contact_Tel == null ? "" : item.Contact_Tel;
                    resultItem.contact_Tel2 = item.Contact_Tel2 == null ? "" : item.Contact_Tel2;
                    resultItem.contact_Tel3 = item.Contact_Tel3 == null ? "" : item.Contact_Tel3;
                    resultItem.contact_Email = item.Contact_Email == null ? "" : item.Contact_Email;
                    resultItem.contact_Email2 = item.Contact_Email2 == null ? "" : item.Contact_Email2;
                    resultItem.contact_Email3 = item.Contact_Email3  == null ? "" : item.Contact_Email3;
                    resultItem.shipTo_Address = item.ShipTo_Address == null ? "" : item.ShipTo_Address;
                    resultItem.subDistrict_Name = item.SubDistrict_Name == null ? "" : item.SubDistrict_Name;
                    resultItem.district_Name = item.District_Name == null ? "" : item.District_Name;
                    resultItem.province_Name = item.Province_Name == null ? "" : item.Province_Name;
                    resultItem.country_Name = item.Country_Name == null ? "" : item.Country_Name;
                    resultItem.postcode_Index = item.Postcode_Index == null ? Guid.Empty : item.Postcode_Index ;
                    resultItem.postcode_Name = item.Postcode_Name == null ? "" : item.Postcode_Name;
                    resultItem.remark = item.Remark == null ? "" : item.Remark;
                    resultItem.businessUnit_Index = item.BusinessUnit_Index;
                    resultItem.businessUnit_Id = item.BusinessUnit_Id == null ? "" : item.BusinessUnit_Id;
                    resultItem.businessUnit_Name = item.BusinessUnit_Name == null ? "" : item.BusinessUnit_Name;



                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
                    resultItem.activeStatus = item.IsActive == 1 ? "เปิดใช้งาน" : "ปิดใช้งาน";
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.cancel_By = item.Cancel_By == null ? "" : item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date != null ? item.Cancel_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    result.Add(resultItem);
                    num++;


                }

                var count = TotalRow.Count;

                var shipToExportViewModel = new ResultShipToViewModel();
                shipToExportViewModel.itemsShipTo = result.ToList();

                return shipToExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UpdateShiptoByTMS 
        public String updateShipToByTMS(ShipToViewModel data)
        {
            String State = "Start";
            String msglog = "";
            //var olog = new logtxt();

            try
            {

                var ShipToOld = db.MS_ShipTo.Where(c => c.ShipTo_Id == data.shipTo_Id && c.IsDelete == 0).FirstOrDefault();

                if (ShipToOld == null)
                {
                    return "ShipTo not found.";
                }

                ShipToOld.ShipTo_Id = data.shipTo_Id;
                ShipToOld.ShipTo_Name = data.shipTo_Name;
                ShipToOld.ShipTo_Address = data.shipTo_Address;
                ShipToOld.ShipTo_Barcode = data.shipTo_Barcode;
                ShipToOld.Ref_No3 = data.ref_No3;
                ShipToOld.UDF_1 = data.udf_1;
                ShipToOld.UDF_2 = data.udf_2;
                ShipToOld.UDF_3 = data.udf_3;
                ShipToOld.UDF_4 = data.udf_4;
                ShipToOld.UDF_5 = data.udf_5;
                ShipToOld.Remark = data.remark;
                ShipToOld.Update_By = "TickerByTMS";
                ShipToOld.Update_Date = DateTime.Now;

                var shiptoid = new SqlParameter("@ShipTo_Id", ShipToOld.ShipTo_Id);

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();

                    var resultTask = db.Database.ExecuteSqlCommand("EXEC Sync_ShipTo_WMS_UPDATE_By_TMS @ShipTo_Id", shiptoid);
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    //olog.logging("SaveShipTo", msglog);
                    transactionx.Rollback();

                    throw exy;
                }



                return "SUCCESS"; 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}


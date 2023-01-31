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

namespace MasterDataBusiness.Forwarder
{
    public class ForwarderService
    {
        private MasterDataDbContext db;

        public ForwarderService()
        {
            db = new MasterDataDbContext();
        }

        public ForwarderService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public actionResultForwarderViewModel ForwarderFilter(SearchForwarderViewModel data)
        {
            try
            {
                var result = new List<ForwarderViewModel>();
                var query = db.ms_Forwarder.AsQueryable();

                query = query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                if (data.forwarder_Index != Guid.Empty && data.forwarder_Index != null)
                {
                    query = query.Where(c => c.Forwarder_Index == data.forwarder_Index);
                }
                if (!string.IsNullOrEmpty(data.forwarder_Id))
                {
                    query = query.Where(c => c.Forwarder_Id == data.forwarder_Id);
                }
                if (!string.IsNullOrEmpty(data.forwarder_Name))
                {
                    query = query.Where(c => c.Forwarder_Name == data.forwarder_Name);
                }
                if (!string.IsNullOrEmpty(data.forwarder_SecondName))
                {
                    query = query.Where(c => c.Forwarder_SecondName == data.forwarder_SecondName);
                }
                if (!string.IsNullOrEmpty(data.forwarder_ThirdName))
                {
                    query = query.Where(c => c.Forwarder_ThirdName == data.forwarder_ThirdName);
                }

                if (data.forwarderType_Index != Guid.Empty && data.forwarderType_Index != null)
                {
                    query = query.Where(c => c.ForwarderType_Index == data.forwarderType_Index);
                }

                if (data.district_Index != Guid.Empty && data.district_Index != null)
                {
                    query = query.Where(c => c.District_Index == data.district_Index);
                }
                if (!string.IsNullOrEmpty(data.district_Id))
                {
                    query = query.Where(c => c.District_Id == data.district_Id);
                }
                if (!string.IsNullOrEmpty(data.district_Name))
                {
                    query = query.Where(c => c.District_Name == data.district_Name);
                }

                if (data.subDistrict_Index != Guid.Empty && data.subDistrict_Index != null)
                {
                    query = query.Where(c => c.SubDistrict_Index == data.subDistrict_Index);
                }
                if (!string.IsNullOrEmpty(data.subDistrict_Id))
                {
                    query = query.Where(c => c.SubDistrict_Id == data.subDistrict_Id);
                }
                if (!string.IsNullOrEmpty(data.subDistrict_Name))
                {
                    query = query.Where(c => c.SubDistrict_Name == data.subDistrict_Name);
                }

                if (data.province_Index != Guid.Empty && data.province_Index != null)
                {
                    query = query.Where(c => c.Province_Index == data.province_Index);
                }
                if (!string.IsNullOrEmpty(data.province_Id))
                {
                    query = query.Where(c => c.Province_Id == data.province_Id);
                }
                if (!string.IsNullOrEmpty(data.province_Name))
                {
                    query = query.Where(c => c.Province_Name == data.province_Name);
                }

                if (data.country_Index != Guid.Empty && data.country_Index != null)
                {
                    query = query.Where(c => c.Country_Index == data.country_Index);
                }
                if (!string.IsNullOrEmpty(data.country_Id))
                {
                    query = query.Where(c => c.Country_Id == data.country_Id);
                }
                if (!string.IsNullOrEmpty(data.country_Name))
                {
                    query = query.Where(c => c.Country_Name == data.country_Name);
                }

                if (data.postcode_Index != Guid.Empty && data.postcode_Index != null)
                {
                    query = query.Where(c => c.Postcode_Index == data.postcode_Index);
                }
                if (!string.IsNullOrEmpty(data.postcode_Id))
                {
                    query = query.Where(c => c.Postcode_Id == data.postcode_Id);
                }
                if (!string.IsNullOrEmpty(data.postcode_Name))
                {
                    query = query.Where(c => c.Postcode_Name == data.postcode_Name);
                }

                var queryResult = query.OrderBy(o => o.Forwarder_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new ForwarderViewModel();

                    resultItem.forwarder_Index = item.Forwarder_Index;
                    resultItem.forwarder_Id = item.Forwarder_Id;
                    resultItem.forwarder_Name = item.Forwarder_Name;
                    resultItem.forwarder_SecondName = item.Forwarder_SecondName;
                    resultItem.forwarder_ThirdName = item.Forwarder_ThirdName;
                    resultItem.forwarder_Address = item.Forwarder_Address;
                    resultItem.forwarderType_Index = item.ForwarderType_Index;
                    resultItem.district_Index = item.District_Index;
                    resultItem.district_Id = item.District_Id;
                    resultItem.district_Name = item.District_Name;
                    resultItem.subDistrict_Index = item.SubDistrict_Index;
                    resultItem.subDistrict_Id = item.SubDistrict_Id;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
                    resultItem.province_Index = item.Province_Index;
                    resultItem.province_Id = item.Province_Id;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.country_Index = item.Country_Index;
                    resultItem.country_Id = item.Country_Id;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.postcode_Index = item.Postcode_Index;
                    resultItem.postcode_Id = item.Postcode_Id;
                    resultItem.postcode_Name = item.Postcode_Name;
                    resultItem.forwarder_TaxID = item.Forwarder_TaxID;
                    resultItem.forwarder_Email = item.Forwarder_Email;
                    resultItem.forwarder_Fax = item.Forwarder_Fax;
                    resultItem.forwarder_Tel = item.Forwarder_Tel;
                    resultItem.forwarder_Mobile = item.Forwarder_Mobile;
                    resultItem.forwarder_Barcode = item.Forwarder_Barcode;
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


                var actionResult = new actionResultForwarderViewModel();
                actionResult.items = result.ToList();
                actionResult.pagination = new Pagination() { TotalRow = queryResult.Count(), CurrentPage = data.CurrentPage, PerPage = data.PerPage, };


                return actionResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region SaveChanges
        public String SaveChanges(ForwarderViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ForwarderOld = db.ms_Forwarder.Find(data.forwarder_Index);

                if (ForwarderOld == null)
                {
                    if (!string.IsNullOrEmpty(data.forwarder_Id))
                    {
                        var query = db.ms_Forwarder.FirstOrDefault(c => c.Forwarder_Id == data.forwarder_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.forwarder_Id))
                    {
                        data.forwarder_Id = "Forwarder_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_Forwarder.FirstOrDefault(c => c.Forwarder_Id == data.forwarder_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Forwarder_Id == data.forwarder_Id)
                                {
                                    data.forwarder_Id = "Forwarder_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_Forwarder Model = new ms_Forwarder();


                    Model.Forwarder_Index = Guid.NewGuid();
                    Model.Forwarder_Id = data.forwarder_Id;
                    Model.Forwarder_Name = data.forwarder_Name;
                    Model.Forwarder_SecondName = data.forwarder_SecondName;
                    Model.Forwarder_ThirdName = data.forwarder_ThirdName;
                    Model.Forwarder_Address = data.forwarder_Address;
                    Model.ForwarderType_Index = data.forwarderType_Index;
                    Model.District_Index = data.district_Index;
                    Model.District_Id = data.district_Id;
                    Model.District_Name = data.district_Name;
                    Model.SubDistrict_Index = data.subDistrict_Index;
                    Model.SubDistrict_Id = data.subDistrict_Id;
                    Model.SubDistrict_Name = data.subDistrict_Name;
                    Model.Province_Index = data.province_Index;
                    Model.Province_Id = data.province_Id;
                    Model.Province_Name = data.province_Name;
                    Model.Country_Index = data.country_Index;
                    Model.Country_Id = data.country_Id;
                    Model.Country_Name = data.country_Name;
                    Model.Postcode_Index = data.postcode_Index;
                    Model.Postcode_Id = data.postcode_Id;
                    Model.Postcode_Name = data.postcode_Name;
                    Model.Forwarder_TaxID = data.forwarder_TaxID;
                    Model.Forwarder_Email = data.forwarder_Email;
                    Model.Forwarder_Fax = data.forwarder_Fax;
                    Model.Forwarder_Tel = data.forwarder_Tel;
                    Model.Forwarder_Mobile = data.forwarder_Mobile;
                    Model.Forwarder_Barcode = data.forwarder_Barcode;
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

                    db.ms_Forwarder.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.forwarder_Id))
                    {
                        if (ForwarderOld.Forwarder_Id != "")
                        {
                            data.forwarder_Id = ForwarderOld.Forwarder_Id;
                        }
                    }
                    else
                    {
                        if (ForwarderOld.Forwarder_Id != data.forwarder_Id)
                        {
                            var query = db.ms_Forwarder.FirstOrDefault(c => c.Forwarder_Id == data.forwarder_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.forwarder_Id = ForwarderOld.Forwarder_Id;
                        }
                    }

                    ForwarderOld.Forwarder_Id = data.forwarder_Id;
                    ForwarderOld.Forwarder_Name = data.forwarder_Name;
                    ForwarderOld.Forwarder_SecondName = data.forwarder_SecondName;
                    ForwarderOld.Forwarder_ThirdName = data.forwarder_ThirdName;
                    ForwarderOld.Forwarder_Address = data.forwarder_Address;
                    ForwarderOld.ForwarderType_Index = data.forwarderType_Index;
                    ForwarderOld.District_Index = data.district_Index;
                    ForwarderOld.District_Id = data.district_Id;
                    ForwarderOld.District_Name = data.district_Name;
                    ForwarderOld.SubDistrict_Index = data.subDistrict_Index;
                    ForwarderOld.SubDistrict_Id = data.subDistrict_Id;
                    ForwarderOld.SubDistrict_Name = data.subDistrict_Name;
                    ForwarderOld.Province_Index = data.province_Index;
                    ForwarderOld.Province_Id = data.province_Id;
                    ForwarderOld.Province_Name = data.province_Name;
                    ForwarderOld.Country_Index = data.country_Index;
                    ForwarderOld.Country_Id = data.country_Id;
                    ForwarderOld.Country_Name = data.country_Name;
                    ForwarderOld.Postcode_Index = data.postcode_Index;
                    ForwarderOld.Postcode_Id = data.postcode_Id;
                    ForwarderOld.Postcode_Name = data.postcode_Name;
                    ForwarderOld.Forwarder_TaxID = data.forwarder_TaxID;
                    ForwarderOld.Forwarder_Email = data.forwarder_Email;
                    ForwarderOld.Forwarder_Fax = data.forwarder_Fax;
                    ForwarderOld.Forwarder_Tel = data.forwarder_Tel;
                    ForwarderOld.Forwarder_Mobile = data.forwarder_Mobile;
                    ForwarderOld.Forwarder_Barcode = data.forwarder_Barcode;
                    ForwarderOld.Contact_Person = data.contact_Person;
                    ForwarderOld.Contact_Person2 = data.contact_Person2;
                    ForwarderOld.Contact_Person3 = data.contact_Person3;
                    ForwarderOld.Contact_Tel = data.contact_Tel;
                    ForwarderOld.Contact_Tel2 = data.contact_Tel2;
                    ForwarderOld.Contact_Tel3 = data.contact_Tel3;
                    ForwarderOld.Contact_Email = data.contact_Email;
                    ForwarderOld.Contact_Email2 = data.contact_Email2;
                    ForwarderOld.Contact_Email3 = data.contact_Email3;
                    ForwarderOld.Ref_No1 = data.ref_No1;
                    ForwarderOld.Ref_No2 = data.ref_No2;
                    ForwarderOld.Ref_No3 = data.ref_No3;
                    ForwarderOld.Ref_No4 = data.ref_No4;
                    ForwarderOld.Ref_No5 = data.ref_No5;
                    ForwarderOld.Remark = data.remark;
                    ForwarderOld.UDF_1 = data.udf_1;
                    ForwarderOld.UDF_2 = data.udf_2;
                    ForwarderOld.UDF_3 = data.udf_3;
                    ForwarderOld.UDF_4 = data.udf_4;
                    ForwarderOld.UDF_5 = data.udf_5;
                    ForwarderOld.IsActive = Convert.ToInt32(data.isActive);
                    ForwarderOld.Update_By = data.create_By;
                    ForwarderOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveForwarder", msglog);
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
        public ForwarderViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_Forwarder.Where(c => c.Forwarder_Index == id).FirstOrDefault();

                var result = new ForwarderViewModel();

                result.forwarder_Index = queryResult.Forwarder_Index;
                result.forwarder_Id = queryResult.Forwarder_Id;
                result.forwarder_Name = queryResult.Forwarder_Name;
                result.forwarder_SecondName = queryResult.Forwarder_SecondName;
                result.forwarder_ThirdName = queryResult.Forwarder_ThirdName;
                result.forwarder_Address = queryResult.Forwarder_Address;
                result.forwarderType_Index = queryResult.ForwarderType_Index;
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
                result.forwarder_TaxID = queryResult.Forwarder_TaxID;
                result.forwarder_Email = queryResult.Forwarder_Email;
                result.forwarder_Fax = queryResult.Forwarder_Fax;
                result.forwarder_Tel = queryResult.Forwarder_Tel;
                result.forwarder_Mobile = queryResult.Forwarder_Mobile;
                result.forwarder_Barcode = queryResult.Forwarder_Barcode;
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
        public Boolean getDelete(ForwarderViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Forwarder = db.ms_Forwarder.Find(data.forwarder_Index);

                if (Forwarder != null)
                {
                    Forwarder.IsActive = 0;
                    Forwarder.IsDelete = 1;


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
                        olog.logging("DeleteForwarder", msglog);
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
        public List<ForwarderViewModel> forwarderDropdown(ForwarderViewModel data)
        {
            try
            {
                var result = new List<ForwarderViewModel>();

                var query = db.ms_Forwarder.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.Forwarder_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new ForwarderViewModel();

                    resultItem.forwarder_Index = item.Forwarder_Index;
                    resultItem.forwarder_Id = item.Forwarder_Id;
                    resultItem.forwarder_Name = item.Forwarder_Name;

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

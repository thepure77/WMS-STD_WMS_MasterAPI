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
using System.Security.Cryptography;
using System.Text;

namespace MasterDataBusiness
{
    public class VendorService
    {

        private MasterDataDbContext db;

        public VendorService()
        {
            db = new MasterDataDbContext();
        }

        public VendorService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterVendor
        public actionResultVendorViewModel filter(SearchVendorViewModel data)
        {
            try
            {
                var query = db.MS_Vendor.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                
                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Vendor_Id.Contains(data.key)
                                         || c.Vendor_Name.Contains(data.key)
                                         || c.VendorType_Name.Contains(data.key));
                }
                if (!string.IsNullOrEmpty(data.vendor_Id))
                {
                    query = query.Where(c => c.Vendor_Id.Contains(data.vendor_Id));
                }
                if (!string.IsNullOrEmpty(data.vendor_Name))
                {
                    query = query.Where(c => c.Vendor_Name.Contains(data.vendor_Name));

                }
                if (!string.IsNullOrEmpty(data.createdatevendor_date) && !string.IsNullOrEmpty(data.createdatevendor_date_to))
                {
                    var dateStart = data.createdatevendor_date.toBetweenDate();
                    var dateEnd = data.createdatevendor_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }


                var Item = new List<MS_Vendor>();
                var TotalRow = new List<MS_Vendor>();

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

                Item = query.OrderBy(o => o.Vendor_Id).ToList();

                var result = new List<SearchVendorViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchVendorViewModel();

                    resultItem.vendor_Index = item.Vendor_Index;
                    resultItem.vendor_Id = item.Vendor_Id;
                    resultItem.vendor_Name = item.Vendor_Name;
                    resultItem.vendor_SecondName = item.Vendor_SecondName;
                    resultItem.vendor_ThirdName = item.Vendor_ThirdName;
                    resultItem.vendor_FourthName = item.Vendor_FourthName;
                    resultItem.vendor_Address = item.Vendor_Address;
                    resultItem.vendorType_Index = item.VendorType_Index;
                    resultItem.vendorType_Id = item.VendorType_Id;
                    resultItem.vendorType_Name = item.VendorType_Name;
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
                    resultItem.vendor_TaxID = item.Vendor_TaxID;
                    resultItem.vendor_Email = item.Vendor_Email;
                    resultItem.vendor_Fax = item.Vendor_Fax;
                    resultItem.vendor_Tel = item.Vendor_Tel;
                    resultItem.vendor_Mobile = item.Vendor_Mobile;
                    resultItem.vendor_Barcode = item.Vendor_Barcode;
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
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.cancel_By = item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date;
                    resultItem.row_Count = rowCount;
                    result.Add(resultItem);
                    rowCount++;
                }

                var count = TotalRow.Count;

                var actionResultVendorViewModel = new actionResultVendorViewModel();
                actionResultVendorViewModel.itemsVendor = result.ToList();
                actionResultVendorViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultVendorViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(VendorViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var VendorOld = db.MS_Vendor.Find(data.vendor_Index);

                if (VendorOld == null)
                {
                     if (!string.IsNullOrEmpty(data.vendor_Id))
                    {
                        var query = db.MS_Vendor.FirstOrDefault(c => c.Vendor_Id == data.vendor_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.vendor_Id))
                    {
                        data.vendor_Id = "Vendor_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_Vendor.FirstOrDefault(c => c.Vendor_Id == data.vendor_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Vendor_Id == data.vendor_Id)
                                {
                                    data.vendor_Id = "Vendor_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
      
                    MS_Vendor Model = new MS_Vendor();

                    Model.Vendor_Index = Guid.NewGuid();
                    Model.Vendor_Id = data.vendor_Id;
                    Model.Vendor_Name = data.vendor_Name;
                    Model.Vendor_SecondName = data.vendor_SecondName;
                    Model.Vendor_ThirdName = data.vendor_ThirdName;
                    Model.Vendor_FourthName = data.vendor_FourthName;
                    Model.VendorType_Index = data.vendorType_Index;
                    Model.VendorType_Id = data.vendorType_Id;
                    Model.VendorType_Name = data.vendorType_Name;
                    Model.Vendor_Address = data.vendor_Address;
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
                    Model.Vendor_TaxID = data.vendor_TaxID;
                    Model.Vendor_Email = data.vendor_Email;
                    Model.Vendor_Fax = data.vendor_Fax;
                    Model.Vendor_Tel = data.vendor_Tel;
                    Model.Vendor_Mobile = data.vendor_Mobile;
                    Model.Vendor_Barcode = data.vendor_Barcode;
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

                    db.MS_Vendor.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.vendor_Id))
                    {
                        if (VendorOld.Vendor_Id != "")
                        {
                            data.vendor_Id = VendorOld.Vendor_Id;
                        }
                    }
                    else
                    {
                        if (VendorOld.Vendor_Id != data.vendor_Id)
                        {
                            var query = db.MS_Vendor.FirstOrDefault(c => c.Vendor_Id == data.vendor_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.vendor_Id = VendorOld.Vendor_Id;
                        }
                    }
                    VendorOld.Vendor_Id = data.vendor_Id;
                    VendorOld.Vendor_Name = data.vendor_Name;
                    VendorOld.Vendor_SecondName = data.vendor_SecondName;
                    VendorOld.Vendor_ThirdName = data.vendor_ThirdName;
                    VendorOld.Vendor_FourthName = data.vendor_FourthName;
                    VendorOld.VendorType_Index = data.vendorType_Index;
                    VendorOld.VendorType_Id = data.vendorType_Id;
                    VendorOld.VendorType_Name = data.vendorType_Name;
                    VendorOld.Vendor_Address = data.vendor_Address;
                    VendorOld.Country_Index = data.country_Index;
                    VendorOld.Country_Id = data.country_Id;
                    VendorOld.Country_Name = data.country_Name;
                    VendorOld.Province_Index = data.province_Index;
                    VendorOld.Province_Id = data.province_Id;
                    VendorOld.Province_Name = data.province_Name;
                    VendorOld.District_Index = data.district_Index;
                    VendorOld.District_Id = data.district_Id;
                    VendorOld.District_Name = data.district_Name;
                    VendorOld.SubDistrict_Index = data.subDistrict_Index;
                    VendorOld.SubDistrict_Id = data.subDistrict_Id;
                    VendorOld.SubDistrict_Name = data.subDistrict_Name;
                    VendorOld.Postcode_Index = data.postcode_Index;
                    VendorOld.Postcode_Id = data.postcode_Id;
                    VendorOld.Postcode_Name = data.postcode_Name;
                    VendorOld.Vendor_TaxID = data.vendor_TaxID;
                    VendorOld.Vendor_Email = data.vendor_Email;
                    VendorOld.Vendor_Fax = data.vendor_Fax;
                    VendorOld.Vendor_Tel = data.vendor_Tel;
                    VendorOld.Vendor_Mobile = data.vendor_Mobile;
                    VendorOld.Vendor_Barcode = data.vendor_Barcode;
                    VendorOld.Contact_Person = data.contact_Person;
                    VendorOld.Contact_Person2 = data.contact_Person2;
                    VendorOld.Contact_Person3 = data.contact_Person3;
                    VendorOld.Contact_Tel = data.contact_Tel;
                    VendorOld.Contact_Tel2 = data.contact_Tel2;
                    VendorOld.Contact_Tel3 = data.contact_Tel3;
                    VendorOld.Contact_Email = data.contact_Email;
                    VendorOld.Contact_Email2 = data.contact_Email2;
                    VendorOld.Contact_Email3 = data.contact_Email3;
                    VendorOld.Ref_No1 = data.ref_No1;
                    VendorOld.Ref_No2 = data.ref_No2;
                    VendorOld.Ref_No3 = data.ref_No3;
                    VendorOld.Ref_No4 = data.ref_No4;
                    VendorOld.Ref_No5 = data.ref_No5;
                    VendorOld.Remark = data.remark;
                    VendorOld.UDF_1 = data.udf_1;
                    VendorOld.UDF_2 = data.udf_2;
                    VendorOld.UDF_3 = data.udf_3;
                    VendorOld.UDF_4 = data.udf_4;
                    VendorOld.UDF_5 = data.udf_5;
                    VendorOld.IsActive = Convert.ToInt32(data.isActive);
                    VendorOld.Update_By = data.create_By;
                    VendorOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveVendor", msglog);
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
        public VendorViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_Vendor.Where(c => c.Vendor_Index == id).FirstOrDefault();

                var result = new VendorViewModel();


                result.vendor_Index = queryResult.Vendor_Index;
                result.vendor_Id = queryResult.Vendor_Id;
                result.vendor_Name = queryResult.Vendor_Name;
                result.vendor_SecondName = queryResult.Vendor_SecondName;
                result.vendor_ThirdName = queryResult.Vendor_ThirdName;
                result.vendor_FourthName = queryResult.Vendor_FourthName;
                result.vendorType_Index = queryResult.VendorType_Index;
                result.vendorType_Id = queryResult.VendorType_Id;
                result.vendorType_Name = queryResult.VendorType_Name;
                result.vendor_Address = queryResult.Vendor_Address;
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
                result.vendor_TaxID = queryResult.Vendor_TaxID;
                result.vendor_Email = queryResult.Vendor_Email;
                result.vendor_Fax = queryResult.Vendor_Fax;
                result.vendor_Tel = queryResult.Vendor_Tel;
                result.vendor_Mobile = queryResult.Vendor_Mobile;
                result.vendor_Barcode = queryResult.Vendor_Barcode;
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
                result.value1 = queryResult.VendorType_Id + " - " + queryResult.VendorType_Name;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDelete
        public Boolean getDelete(VendorViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var vendor = db.MS_Vendor.Find(data.vendor_Index);

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
                        olog.logging("DeleteVendor", msglog);
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


        #region filterPopupVendor
        //Filter
        public actionResultVendorPopupViewModel filterPopupVendor(VendorPopupViewModel data)
        {
            try
            {
                var query = db.View_Vendor.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (data.listVendorViewModel != null)
                {
                    foreach (var item in data.listVendorViewModel)
                    {
                        query = query.Where(q => q.Vendor_Index != item.vendor_Index);
                    }

                }


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Vendor_Id.Contains(data.key)
                                          || c.Vendor_Name.Contains(data.key)
                                          || c.VendorType_Name.Contains(data.key));


                }
                if (!string.IsNullOrEmpty(data.vendor_Id))
                {
                    query = query.Where(c => c.Vendor_Id.Contains(data.vendor_Id));
                }
                if (!string.IsNullOrEmpty(data.vendor_Name))
                {
                    query = query.Where(c => c.Vendor_Name.Contains(data.vendor_Name));

                }

                var Item = new List<View_Vendor>();
                var TotalRow = new List<View_Vendor>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Vendor_Id).ToList();

                var result = new List<VendorPopupViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new VendorPopupViewModel();

                    resultItem.vendor_Index = item.Vendor_Index;
                    //if (data.listVendorViewModel != null)
                    //{
                    //    foreach (var dataList in data.listVendorViewModel)
                    //    {
                    //        if (resultItem.vendor_Index == dataList.vendor_Index)
                    //        {
                    //            resultItem.selected = true;
                    //        }
                    //    }
                    //}
                    resultItem.vendor_Index = item.Vendor_Index;
                    resultItem.vendor_Id = item.Vendor_Id;
                    resultItem.vendor_Name = item.Vendor_Name;
                    resultItem.vendor_Address = item.Vendor_Address;
                    resultItem.vendorType_Name = item.VendorType_Name;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.district_Name = item.District_Name;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
                    resultItem.postcode_Name = item.Postcode_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultVendorPopupViewModel = new actionResultVendorPopupViewModel();
                actionResultVendorPopupViewModel.itemsVendorPopup = result.ToList();
                actionResultVendorPopupViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultVendorPopupViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        //public String SaveChanges(VendorViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int issystem = 0;
        //            int statusid = 0;

        //            if (data.VendorIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.VendorIndex = Guid.NewGuid();
        //            }
        //            if (data.VendorId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "VendorID");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.VendorId = resultParameter.Value.ToString();
        //            }
        //            var Vendor_Index = new SqlParameter("Vendor_Index", data.VendorIndex);
        //            var Vendor_Id = new SqlParameter("Vendor_Id", data.VendorId);
        //            var Vendor_Name = new SqlParameter("Vendor_Name", data.VendorName);
        //            var Vendor_Address = new SqlParameter("Vendor_Address", data.VendorAddress);
        //            var VendorType_Index = new SqlParameter("VendorType_Index", data.VendorTypeIndex);
        //            var SubDistrict_Index = new SqlParameter("SubDistrict_Index", data.SubDistrictIndex);
        //            var District_Index = new SqlParameter("District_Index", data.DistrictIndex);
        //            var Province_Index = new SqlParameter("Province_Index", data.ProvinceIndex);
        //            var Country_Index = new SqlParameter("Country_Index", data.CountryIndex);
        //            var Postcode_Index = new SqlParameter("Postcode_Index", data.PostCodeIndex);
        //            var Vendor_TaxID = new SqlParameter("Vendor_TaxID", "");
        //            var Vendor_Email = new SqlParameter("Vendor_Email", "");
        //            var Vendor_Fax = new SqlParameter("Vendor_Fax", "");
        //            var Vendor_Tel = new SqlParameter("Vendor_Tel", "");
        //            var Vendor_Mobile = new SqlParameter("Vendor_Mobile", "");
        //            var Vendor_Barcode = new SqlParameter("Vendor_Barcode", "");
        //            var Contact_Person = new SqlParameter("Contact_Person", "");
        //            var Contact_Person2 = new SqlParameter("Contact_Person2", "");
        //            var Contact_Person3 = new SqlParameter("Contact_Person3", "");
        //            var Contact_Tel = new SqlParameter("Contact_Tel", "");
        //            var Contact_Tel2 = new SqlParameter("Contact_Tel2", "");
        //            var Contact_Tel3 = new SqlParameter("Contact_Tel3", "");
        //            var Contact_Email = new SqlParameter("Contact_Email", "");
        //            var Contact_Email2 = new SqlParameter("Contact_Email2", "");
        //            var Contact_Email3 = new SqlParameter("Contact_Email3", "");
        //            var IsActive = new SqlParameter("IsActive", isactive);
        //            var IsDelete = new SqlParameter("IsDelete", isdelete);
        //            var IsSystem = new SqlParameter("IsSystem", issystem);
        //            var Status_Id = new SqlParameter("Status_Id", statusid);
        //            var Create_By = new SqlParameter("Create_By", "");
        //            var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        //            var Update_By = new SqlParameter("Update_By", "");
        //            var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        //            var Cancel_By = new SqlParameter("Cancel_By", "");
        //            var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_Vendor  @Vendor_Index,@Vendor_Id,@Vendor_Name,@Vendor_Address,@VendorType_Index,@SubDistrict_Index,@District_Index,@Province_Index,@Country_Index,@Postcode_Index,@Vendor_TaxID,@Vendor_Email,@Vendor_Fax,@Vendor_Tel,@Vendor_Mobile,@Vendor_Barcode,@Contact_Person,@Contact_Person2,@Contact_Person3,@Contact_Tel,@Contact_Tel2,@Contact_Tel3,@Contact_Email,@Contact_Email2,@Contact_Email3,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Vendor_Index, Vendor_Id, Vendor_Name, Vendor_Address, VendorType_Index, SubDistrict_Index, District_Index, Province_Index, Country_Index, Postcode_Index, Vendor_TaxID, Vendor_Email, Vendor_Fax, Vendor_Tel, Vendor_Mobile, Vendor_Barcode, Contact_Person, Contact_Person2, Contact_Person3, Contact_Tel, Contact_Tel2, Contact_Tel3, Contact_Email, Contact_Email2, Contact_Email3, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #region filterPopup
        public actionResultVendorViewModel filterPopup(SearchVendorViewModel data)
        {
            try
            {
                var query = db.MS_Vendor.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);



                if (!string.IsNullOrEmpty(data.vendor_Id))
                {
                    query = query.Where(c => c.Vendor_Id.Contains(data.vendor_Id));
                }
                if (!string.IsNullOrEmpty(data.vendor_Name))
                {
                    query = query.Where(c => c.Vendor_Name.Contains(data.vendor_Name));

                }


                var Item = new List<MS_Vendor>();
                var TotalRow = new List<MS_Vendor>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Vendor_Id).ToList();

                var result = new List<SearchVendorViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchVendorViewModel();

                    resultItem.vendor_Index = item.Vendor_Index;
                    resultItem.vendor_Id = item.Vendor_Id;
                    resultItem.vendor_Name = item.Vendor_Name;
                    resultItem.vendor_Address = item.Vendor_Address;
                    resultItem.postcode_Name = item.Postcode_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultVendorViewModel = new actionResultVendorViewModel();
                actionResultVendorViewModel.itemsVendor = result.ToList();
                actionResultVendorViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultVendorViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Export Excel
        public actionResultVendorViewModel Export(SearchVendorViewModel data)
        {
            try
            {
                var query = db.MS_Vendor.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Vendor_Id.Contains(data.key)
                                         || c.Vendor_Name.Contains(data.key)
                                         || c.VendorType_Name.Contains(data.key));
                }
                if (!string.IsNullOrEmpty(data.vendor_Id))
                {
                    query = query.Where(c => c.Vendor_Id.Contains(data.vendor_Id));
                }
                if (!string.IsNullOrEmpty(data.vendor_Name))
                {
                    query = query.Where(c => c.Vendor_Name.Contains(data.vendor_Name));

                }
                if (!string.IsNullOrEmpty(data.createdatevendor_date) && !string.IsNullOrEmpty(data.createdatevendor_date_to))
                {
                    var dateStart = data.createdatevendor_date.toBetweenDate();
                    var dateEnd = data.createdatevendor_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }

                var Item = new List<MS_Vendor>();
                var TotalRow = new List<MS_Vendor>();

                TotalRow = query.ToList();

                var num = 1;
                int rowCount = num;

                Item = query.OrderBy(o => o.Vendor_Id).ToList();

                var result = new List<SearchVendorViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchVendorViewModel();

                    resultItem.vendor_Index = item.Vendor_Index;
                    resultItem.vendor_Id = item.Vendor_Id;
                    resultItem.vendor_Name = item.Vendor_Name;
                    resultItem.vendor_SecondName = item.Vendor_SecondName == null ? "" : item.Vendor_SecondName;
                    resultItem.vendor_ThirdName = item.Vendor_ThirdName == null ? "" : item.Vendor_ThirdName;
                    resultItem.vendor_FourthName = item.Vendor_FourthName == null ? "" : item.Vendor_FourthName;
                    resultItem.vendor_Address = item.Vendor_Address;
                    resultItem.vendorType_Index = item.VendorType_Index;
                    resultItem.vendorType_Id = item.VendorType_Id;
                    resultItem.vendorType_Name = item.VendorType_Name;
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
                    resultItem.vendor_TaxID = item.Vendor_TaxID;
                    resultItem.vendor_Email = item.Vendor_Email;
                    resultItem.vendor_Fax = item.Vendor_Fax;
                    resultItem.vendor_Tel = item.Vendor_Tel == null ? "" : item.Vendor_Tel;
                    resultItem.vendor_Mobile = item.Vendor_Mobile;
                    resultItem.vendor_Barcode = item.Vendor_Barcode;
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
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.cancel_By = item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date;
                    resultItem.row_Count = rowCount;
                    result.Add(resultItem);
                    rowCount++;
                }

                var count = TotalRow.Count;

                var actionResultVendorViewModel = new actionResultVendorViewModel();
                actionResultVendorViewModel.itemsVendor = result.ToList();
                actionResultVendorViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultVendorViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}

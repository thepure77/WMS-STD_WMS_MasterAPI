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
    public class AddressPostcodeService
    {
        public List<AddressPostcodeViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_AddressPostcode.FromSql("sp_GetAddressPostcode").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
                   
                    //******* Count Rows ******//
                    var strwhere1 = new SqlParameter("@strwhere", DBNull.Value);                    
                    var PageNumber1 = new SqlParameter("@PageNumber", 1);
                    var RowspPage1 = new SqlParameter("@RowspPage", 10000);
                    var queryResultTotal = context.MS_AddressPostcode.FromSql("sp_GetAddressPostcode @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
                    var count = queryResultTotal.Count();

                    var result = new List<AddressPostcodeViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new AddressPostcodeViewModel();
                        resultItem.PostcodeIndex = item.Postcode_Index;
                        resultItem.PostcodeId = item.Postcode_Id;
                        resultItem.PostcodeName = item.Postcode_Name;
                        resultItem.SubDistrictIndex = item.SubDistrict_Index;
                        resultItem.DistrictIndex = item.District_Index;
                        resultItem.ProvinceIndex = item.Province_Index;
                        resultItem.CountryIndex = item.Country_Index;
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

                        resultItem.count = count;
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
        public actionResultPostcodeViewModel FilterPostcode(AddressPostcodeViewModel model)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    string pwhere1 = "";
                    if (model.PostcodeName != null)
                    {
                        pwhere1 = " And Postcode_Name like N'%" + model.PostcodeName + "%'";
                    }
                    else
                    {
                        pwhere1 += " ";
                    }
                    Guid newGuid = new Guid();
                    if (model.SubDistrictIndex != newGuid)
                    {
                        pwhere1 += " And SubDistrict_Index = '" + model.SubDistrictIndex + "'";
                    }
                    else
                    {
                        pwhere1 += " ";
                    }
                   
                    var strwhere1 = new SqlParameter("@strwhere", pwhere1);
                    // var strwhere = new SqlParameter("@strwhere", DBNull.Value);
                    var PageNumber1 = new SqlParameter("@PageNumber", 1);
                    var RowspPage1 = new SqlParameter("@RowspPage", 10000);
                    var queryResultTotal = context.MS_AddressPostcode.FromSql("sp_GetAddressPostcode @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    string pwhere = "";
                    if (model.PostcodeName != null)
                    {
                        pwhere = " And Postcode_Name like N'%" + model.PostcodeName + "%'";
                    }
                    else
                    {
                        pwhere = " ";
                    }
                    Guid newGuid1 = new Guid();
                    if (model.SubDistrictIndex != newGuid1)
                    {
                        pwhere += " And SubDistrict_Index = '" + model.SubDistrictIndex + "'";
                    }
                    else
                    {
                        pwhere += " ";
                    }
                   
                    var strwhere = new SqlParameter("@strwhere", pwhere);
                    // var strwhere = new SqlParameter("@strwhere", DBNull.Value);
                    var PageNumber = new SqlParameter("@PageNumber", model.CurrentPage);
                    var RowspPage = new SqlParameter("@RowspPage", model.PerPage);

                    var queryResult = context.MS_AddressPostcode.FromSql("sp_GetAddressPostcode @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber, RowspPage).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var result = new List<AddressPostcodeViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new AddressPostcodeViewModel();
                        resultItem.PostcodeIndex = item.Postcode_Index;
                        resultItem.PostcodeId = item.Postcode_Id;
                        resultItem.PostcodeName = item.Postcode_Name;
                        resultItem.SubDistrictIndex = item.SubDistrict_Index;
                        resultItem.DistrictIndex = item.District_Index;
                        resultItem.ProvinceIndex = item.Province_Index;
                        resultItem.CountryIndex = item.Country_Index;
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

                    var count = queryResultTotal.Count;
                    var actionResultPostcode = new actionResultPostcodeViewModel();
                    actionResultPostcode.itemsPostcode = result.ToList();
                    actionResultPostcode.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage };
                    return actionResultPostcode;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public String SaveChanges(AddressPostcodeViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    int isactive = 1;
                    int isdelete = 0;
                    int issystem = 0;
                    int statusid = 0;

                    if (data.PostcodeIndex.ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        data.PostcodeIndex = Guid.NewGuid();
                    }
                    if (data.PostcodeId == null)
                    {
                        var Sys_Key = new SqlParameter("Sys_Key", "PostcodeID");
                        var resultParameter = new SqlParameter("@result", SqlDbType.Int);
                        resultParameter.Size = 2000; // some meaningfull value
                        resultParameter.Direction = ParameterDirection.Output;
                        context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
                        //var result = resultParameter.Value;
                        data.PostcodeId = resultParameter.Value.ToString();
                    }
                    var Postcode_Index = new SqlParameter("Postcode_Index", data.PostcodeIndex);
                    var Postcode_Id = new SqlParameter("Postcode_Id", data.PostcodeId);
                    var Postcode_Name = new SqlParameter("Postcode_Name", data.PostcodeName);
                    var SubDistrict_Index = new SqlParameter("SubDistrict_Index", data.SubDistrictIndex);
                    var District_Index = new SqlParameter("District_Index", data.DistrictIndex);
                    var Province_Index = new SqlParameter("Province_Index", data.ProvinceIndex);
                    var Country_Index = new SqlParameter("Country_Index", data.CountryIndex);
                    var IsActive = new SqlParameter("IsActive", isactive);
                    var IsDelete = new SqlParameter("IsDelete", isdelete);
                    var IsSystem = new SqlParameter("IsSystem", issystem);
                    var Status_Id = new SqlParameter("Status_Id", statusid);
                    var Create_By = new SqlParameter("Create_By", "");
                    var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
                    var Update_By = new SqlParameter("Update_By", "");
                    var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
                    var Cancel_By = new SqlParameter("Cancel_By", "");
                    var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
                    var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_AddressPostcode  @Postcode_Index,@Postcode_Id,@Postcode_Name,@SubDistrict_Index,@District_Index,@Province_Index,@Country_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Postcode_Index, Postcode_Id, Postcode_Name, SubDistrict_Index, District_Index, Province_Index, Country_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
                    return rowsAffected.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AddressPostcodeViewModel> getId(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    string pstring = " and Postcode_Index ='" + id + "'";
                    var result = new List<AddressPostcodeViewModel>();

                    //****************** Check data ว่ามี Index ของ Province หรือไม่ ***************************//
                    var strwhere1 = new SqlParameter("@strwhere", DBNull.Value);
                    var PageNumber1 = new SqlParameter("@PageNumber", 1);
                    var RowspPage1 = new SqlParameter("@RowspPage", 10000);
                    var queryResultTotal = context.MS_AddressPostcode.FromSql("sp_GetAddressPostcode @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    if (queryResultTotal.Count > 0)
                    {
                        var findItem = queryResultTotal.Where(c => c.Postcode_Index == id).ToList();
                        var count = findItem.Count;
                        if (findItem != null)
                        {
                            foreach (var item in findItem.OrderByDescending(c => c.Postcode_Index))
                            {
                                //var itemList = context.MS_AddressPostcode.FromSql("sp_GetAddressPostcode").Where(c => c.SubDistrict_Index == item.SubDistrict_Index).FirstOrDefault();
                                if (item != null)
                                {
                                    var resultItem = new AddressPostcodeViewModel();
                                    resultItem.PostcodeIndex = item.Postcode_Index;
                                    resultItem.PostcodeId = item.Postcode_Id;
                                    resultItem.PostcodeName = item.Postcode_Name;
                                    resultItem.SubDistrictIndex = item.SubDistrict_Index;
                                    resultItem.DistrictIndex = item.District_Index;
                                    resultItem.ProvinceIndex = item.Province_Index;
                                    resultItem.CountryIndex = item.Country_Index;
                                    resultItem.IsActive = item.IsActive;
                                    resultItem.IsDelete = item.IsDelete;
                                    resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
                                    resultItem.CreateBy = item.Create_By;
                                    resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
                                    resultItem.UpdateBy = item.Update_By;
                                    resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
                                    resultItem.CancelBy = item.Cancel_By;
                                    result.Add(resultItem);
                                }

                            }
                        }
                    }
                    else
                    {
                        var queryResult = context.MS_AddressPostcode.FromSql("sp_GetAddressPostcode {0}", pstring).ToList();
                        queryResult.Where(c => c.Postcode_Index == id);


                        foreach (var item in queryResult)
                        {
                            var resultItem = new AddressPostcodeViewModel();
                            resultItem.PostcodeIndex = item.Postcode_Index;
                            resultItem.PostcodeId = item.Postcode_Id;
                            resultItem.PostcodeName = item.Postcode_Name;
                            resultItem.SubDistrictIndex = item.SubDistrict_Index;
                            resultItem.DistrictIndex = item.District_Index;
                            resultItem.ProvinceIndex = item.Province_Index;
                            resultItem.CountryIndex = item.Country_Index;
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
                        if (queryResult.Count == 0)
                        {
                            var findItem = context.MS_AddressPostcode.FromSql("sp_GetAddressPostcode").Where(c => c.SubDistrict_Index == id).ToList();
                            if (findItem != null)
                            {
                                foreach (var item in findItem.OrderByDescending(c => c.SubDistrict_Index))
                                {
                                    var itemList = context.MS_AddressPostcode.FromSql("sp_GetAddressPostcode").Where(c => c.SubDistrict_Index == item.SubDistrict_Index).FirstOrDefault();
                                    if (itemList != null)
                                    {
                                        var resultItem = new AddressPostcodeViewModel();
                                        resultItem.PostcodeIndex = item.Postcode_Index;
                                        resultItem.PostcodeId = item.Postcode_Id;
                                        resultItem.PostcodeName = item.Postcode_Name;
                                        resultItem.SubDistrictIndex = item.SubDistrict_Index;
                                        resultItem.DistrictIndex = item.District_Index;
                                        resultItem.ProvinceIndex = item.Province_Index;
                                        resultItem.CountryIndex = item.Country_Index;
                                        resultItem.IsActive = itemList.IsActive;
                                        resultItem.IsDelete = itemList.IsDelete;
                                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
                                        resultItem.CreateBy = item.Create_By;
                                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
                                        resultItem.UpdateBy = item.Update_By;
                                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
                                        resultItem.CancelBy = item.Cancel_By;
                                        result.Add(resultItem);
                                    }

                                }
                            }

                        }
                    }
                    
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AddressPostcodeViewModel> getDelete(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_AddressPostcode.FromSql("sp_GetAddressPostcode").ToList();
                    int isactive = 0;
                    int isdelete = 1;
                    var result = new List<AddressPostcodeViewModel>();
                    foreach (var item in queryResult.Where(c => c.Postcode_Index == id))
                    {
                        var Postcode_Index = new SqlParameter("Postcode_Index", item.Postcode_Index);
                        var Postcode_Id = new SqlParameter("Postcode_Id", item.Postcode_Id);
                        var Postcode_Name = new SqlParameter("Postcode_Name", item.Postcode_Name);
                        var SubDistrict_Index = new SqlParameter("SubDistrict_Index", item.SubDistrict_Index);
                        var District_Index = new SqlParameter("District_Index", item.District_Index);
                        var Province_Index = new SqlParameter("Province_Index", item.Province_Index);
                        var Country_Index = new SqlParameter("Country_Index", item.Country_Index);
                        var IsActive = new SqlParameter("IsActive", isactive);
                        var IsDelete = new SqlParameter("IsDelete", isdelete);
                        var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
                        var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
                        var Create_By = new SqlParameter("Create_By", "");
                        var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
                        var Update_By = new SqlParameter("Update_By", "");
                        var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
                        var Cancel_By = new SqlParameter("Cancel_By", "");
                        var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
                        var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_AddressPostcode  @Postcode_Index,@Postcode_Id,@Postcode_Name,@SubDistrict_Index,@District_Index,@Province_Index,@Country_Index,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Postcode_Index, Postcode_Id, Postcode_Name, SubDistrict_Index, District_Index, Province_Index, Country_Index, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
                        context.SaveChanges();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private MasterDataDbContext db;

        public AddressPostcodeService()
        {
            db = new MasterDataDbContext();
        }

        public AddressPostcodeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterV2
        public actionResultPostcodeViewModelV2 filter(AddressPostcodeViewModelV2 data)
        {
            try
            {
                var query = db.MS_AddressPostcode.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Postcode_Name.Contains(data.key)
                                         || c.Postcode_SecondName.Contains(data.key)
                                         || c.Postcode_Id.Contains(data.key));
                }

                var Item = new List<MS_AddressPostcode>();
                var TotalRow = new List<MS_AddressPostcode>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Postcode_Id).ToList();

                var result = new List<AddressPostcodeViewModelV2>();

                foreach (var item in Item)
                {
                    var resultItem = new AddressPostcodeViewModelV2();

                    resultItem.postcode_Index = item.Postcode_Index;
                    resultItem.postcode_Id = item.Postcode_Id;
                    resultItem.postcode_Name = item.Postcode_Name;
                    resultItem.postcode_SecondName = item.Postcode_SecondName;
                    resultItem.province_Index = item.Province_Index;
                    resultItem.province_Id = item.Province_Id;
                    resultItem.province_Name = item.Province_Name;
                    resultItem.country_Index = item.Country_Index;
                    resultItem.country_Id = item.Country_Id;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.district_Index = item.District_Index;
                    resultItem.district_Id = item.District_Id;
                    resultItem.district_Name = item.District_Name;
                    resultItem.subDistrict_Index = item.SubDistrict_Index;
                    resultItem.subDistrict_Id = item.SubDistrict_Id;
                    resultItem.subDistrict_Name = item.SubDistrict_Name;
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

                var actionResultPostcodeViewModelV2 = new actionResultPostcodeViewModelV2();
                actionResultPostcodeViewModelV2.itemsPostcode = result.ToList();
                actionResultPostcodeViewModelV2.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultPostcodeViewModelV2;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChangesV2
        public String SaveChanges(AddressPostcodeViewModelV2 data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var PostcodeOld = db.MS_AddressPostcode.Find(data.postcode_Index);

                if (PostcodeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.postcode_Id))
                    {
                        var query = db.MS_AddressPostcode.FirstOrDefault(c => c.Postcode_Id == data.postcode_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.postcode_Id))
                    {
                        data.postcode_Id = "Postcode_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_AddressPostcode.FirstOrDefault(c => c.Postcode_Id == data.postcode_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Postcode_Id == data.postcode_Id)
                                {
                                    data.postcode_Id = "Postcode_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_AddressPostcode Model = new MS_AddressPostcode();


                    Model.Postcode_Index = Guid.NewGuid();
                    Model.Postcode_Id = data.postcode_Id;
                    Model.Postcode_Name = data.postcode_Name;
                    Model.Postcode_SecondName = data.postcode_SecondName;
                    Model.Province_Index = data.province_Index;
                    Model.Province_Id = data.province_Id;
                    Model.Province_Name = data.province_Name;
                    Model.Country_Index = data.country_Index;
                    Model.Country_Id = data.country_Id;
                    Model.Country_Name = data.country_Name;
                    Model.District_Index = data.district_Index;
                    Model.District_Id = data.district_Id;
                    Model.District_Name = data.district_Name;
                    Model.SubDistrict_Index = data.subDistrict_Index;
                    Model.SubDistrict_Id = data.subDistrict_Id;
                    Model.SubDistrict_Name = data.subDistrict_Name;
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

                    db.MS_AddressPostcode.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.postcode_Id))
                    {
                        if (PostcodeOld.Postcode_Id != "")
                        {
                            data.postcode_Id = PostcodeOld.Postcode_Id;
                        }
                    }
                    else
                    {
                        if (PostcodeOld.Postcode_Id != data.postcode_Id)
                        {
                            var query = db.MS_AddressPostcode.FirstOrDefault(c => c.Postcode_Id == data.postcode_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.postcode_Id = PostcodeOld.Postcode_Id;
                        }
                    }

                    PostcodeOld.Postcode_Id = data.postcode_Id;
                    PostcodeOld.Postcode_Name = data.postcode_Name;
                    PostcodeOld.Postcode_SecondName = data.postcode_SecondName;
                    PostcodeOld.Province_Index = data.province_Index;
                    PostcodeOld.Province_Id = data.province_Id;
                    PostcodeOld.Province_Name = data.province_Name;
                    PostcodeOld.Country_Index = data.country_Index;
                    PostcodeOld.Country_Id = data.country_Id;
                    PostcodeOld.Country_Name = data.country_Name;
                    PostcodeOld.District_Index = data.district_Index;
                    PostcodeOld.District_Id = data.district_Id;
                    PostcodeOld.District_Name = data.district_Name;
                    PostcodeOld.SubDistrict_Index = data.subDistrict_Index;
                    PostcodeOld.SubDistrict_Id = data.subDistrict_Id;
                    PostcodeOld.SubDistrict_Name = data.subDistrict_Name;
                    PostcodeOld.Ref_No1 = data.ref_No1;
                    PostcodeOld.Ref_No2 = data.ref_No2;
                    PostcodeOld.Ref_No3 = data.ref_No3;
                    PostcodeOld.Ref_No4 = data.ref_No4;
                    PostcodeOld.Ref_No5 = data.ref_No5;
                    PostcodeOld.Remark = data.remark;
                    PostcodeOld.UDF_1 = data.udf_1;
                    PostcodeOld.UDF_2 = data.udf_2;
                    PostcodeOld.UDF_3 = data.udf_3;
                    PostcodeOld.UDF_4 = data.udf_4;
                    PostcodeOld.UDF_5 = data.udf_5;
                    PostcodeOld.IsActive = Convert.ToInt32(data.isActive);
                    PostcodeOld.Update_By = data.create_By;
                    PostcodeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SavePostcode", msglog);
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

        #region findV2
        public AddressPostcodeViewModelV2 find(Guid id)
        {
            try
            {

                var queryResult = db.MS_AddressPostcode.Where(c => c.Postcode_Index == id).FirstOrDefault();

                var result = new AddressPostcodeViewModelV2();

                result.postcode_Index = queryResult.Postcode_Index;
                result.postcode_Id = queryResult.Postcode_Id;
                result.postcode_Name = queryResult.Postcode_Name;
                result.postcode_SecondName = queryResult.Postcode_SecondName;
                result.province_Index = queryResult.Province_Index;
                result.province_Id = queryResult.Province_Id;
                result.province_Name = queryResult.Province_Name;
                result.country_Index = queryResult.Country_Index;
                result.country_Id = queryResult.Country_Id;
                result.country_Name = queryResult.Country_Name;
                result.district_Index = queryResult.District_Index;
                result.district_Id = queryResult.District_Id;
                result.district_Name = queryResult.District_Name;
                result.subDistrict_Index = queryResult.SubDistrict_Index;
                result.subDistrict_Id = queryResult.SubDistrict_Id;
                result.subDistrict_Name = queryResult.SubDistrict_Name;
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

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDeleteV2
        public Boolean getDelete(AddressPostcodeViewModelV2 data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Postcode = db.MS_AddressPostcode.Find(data.postcode_Index);

                if (Postcode != null)
                {
                    Postcode.IsActive = 0;
                    Postcode.IsDelete = 1;


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
                        olog.logging("DeletePostcode", msglog);
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

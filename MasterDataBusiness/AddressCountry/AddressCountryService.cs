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
    public class AddressCountryService
    {
        public List<AddressCountryViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_AddressCountry.FromSql("sp_GetAddressCountry").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    //******* Count Rows ******//
                    var strwhere1 = new SqlParameter("@strwhere", DBNull.Value);
                    var PageNumber1 = new SqlParameter("@PageNumber", 1);
                    var RowspPage1 = new SqlParameter("@RowspPage", 10000);
                    var queryResultTotal = context.MS_AddressCountry.FromSql("sp_GetAddressCountry @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
                    var count = queryResultTotal.Count();

                    var result = new List<AddressCountryViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new AddressCountryViewModel();
                        resultItem.CountryIndex = item.Country_Index;
                        resultItem.CountryId = item.Country_Id;
                        resultItem.CountryName = item.Country_Name;
                        resultItem.CountryNameEN = item.Country_NameEN;
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
        public actionResultCountryViewModel FilterCountry(AddressCountryViewModel model)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    string pwhere1 = " And Country_Name like N'%" + model.CountryName + "%'";
                    var strwhere1 = new SqlParameter("@strwhere", pwhere1);
                    // var strwhere = new SqlParameter("@strwhere", DBNull.Value);
                    var PageNumber1 = new SqlParameter("@PageNumber", 1);
                    var RowspPage1 = new SqlParameter("@RowspPage", 10000);
                    var queryResultTotal = context.MS_AddressCountry.FromSql("sp_GetAddressCountry @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();


                    string pwhere = " And Country_Name like N'%" + model.CountryName + "%'";
                    var strwhere = new SqlParameter("@strwhere", pwhere);
                    // var strwhere = new SqlParameter("@strwhere", DBNull.Value);
                    var PageNumber = new SqlParameter("@PageNumber", model.CurrentPage);
                    var RowspPage = new SqlParameter("@RowspPage", model.PerPage);

                    var queryResult = context.MS_AddressCountry.FromSql("sp_GetAddressCountry @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber, RowspPage).Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var result = new List<AddressCountryViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new AddressCountryViewModel();
                        resultItem.CountryIndex = item.Country_Index;
                        resultItem.CountryId = item.Country_Id;
                        resultItem.CountryName = item.Country_Name;
                        resultItem.CountryNameEN = item.Country_NameEN;
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
                    var actionResultCountry = new actionResultCountryViewModel();
                    actionResultCountry.itemsCountry = result.ToList();
                    actionResultCountry.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage };

                    //return actionResultVender;

                    return actionResultCountry;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String SaveChanges(AddressCountryViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    int isactive = 1;
                    int isdelete = 0;
                    int issystem = 0;
                    int statusid = 0;

                    if (data.CountryIndex.ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        data.CountryIndex = Guid.NewGuid();
                    }
                    if (data.CountryId == null)
                    {
                        var Sys_Key = new SqlParameter("Sys_Key", "CountryID");
                        var resultParameter = new SqlParameter("@result", SqlDbType.Int);
                        resultParameter.Size = 2000; // some meaningfull value
                        resultParameter.Direction = ParameterDirection.Output;
                        context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
                        //var result = resultParameter.Value;
                        data.CountryId = resultParameter.Value.ToString();
                    }

                    var Country_Index = new SqlParameter("Country_Index", data.CountryIndex);
                    var Country_Id = new SqlParameter("Country_Id", data.CountryId);
                    var Country_Name = new SqlParameter("Country_Name", data.CountryName);
                    var Country_NameEN = new SqlParameter("Country_NameEN", data.CountryNameEN);
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
                    var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_AddressCountry  @Country_Index,@Country_Id,@Country_Name,@Country_NameEN,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Country_Index, Country_Id, Country_Name, Country_NameEN, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
                    return rowsAffected.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AddressCountryViewModel> getId(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    string pstring = " and Country_Index ='" + id + "'";
                    var queryResult = context.MS_AddressCountry.FromSql("sp_GetAddressCountry {0}", pstring).ToList();
                    queryResult.Where(c => c.Country_Index == id);

                    var result = new List<AddressCountryViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new AddressCountryViewModel();
                        resultItem.CountryIndex = item.Country_Index;
                        resultItem.CountryId = item.Country_Id;
                        resultItem.CountryName = item.Country_Name;
                        resultItem.CountryNameEN = item.Country_NameEN;
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

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AddressCountryViewModel> getDelete(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_AddressCountry.FromSql("sp_GetAddressCountry").ToList();
                    int isactive = 0;
                    int isdelete = 1;
                    var result = new List<AddressCountryViewModel>();
                    foreach (var item in queryResult.Where(c => c.Country_Index == id))
                    {
                        var Country_Index = new SqlParameter("Country_Index", item.Country_Index);
                        var Country_Id = new SqlParameter("Country_Id", item.Country_Id);
                        var Country_Name = new SqlParameter("Country_Name", item.Country_Name);
                        var Country_NameEN = new SqlParameter("Country_NameEN", item.Country_NameEN);
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
                        var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_AddressCountry  @Country_Index,@Country_Id,@Country_Name,@Country_NameEN,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Country_Index, Country_Id, Country_Name, Country_NameEN, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        public AddressCountryService()
        {
            db = new MasterDataDbContext();
        }

        public AddressCountryService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterV2
        public actionResultCountryViewModelV2 filter(AddressCountryViewModelV2 data)
        {
            try
            {
                var query = db.MS_AddressCountry.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Country_Name.Contains(data.key)
                                         || c.Country_SecondName.Contains(data.key)
                                         || c.Country_Id.Contains(data.key));
                }

                var Item = new List<MS_AddressCountry>();
                var TotalRow = new List<MS_AddressCountry>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Country_Id).ToList();

                var result = new List<AddressCountryViewModelV2>();

                foreach (var item in Item)
                {
                    var resultItem = new AddressCountryViewModelV2();

                    resultItem.country_Index = item.Country_Index;
                    resultItem.country_Id = item.Country_Id;
                    resultItem.country_Name = item.Country_Name;
                    resultItem.country_SecondName = item.Country_SecondName;
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

                var actionResultCountryViewModelV2 = new actionResultCountryViewModelV2();
                actionResultCountryViewModelV2.itemsCountry = result.ToList();
                actionResultCountryViewModelV2.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultCountryViewModelV2;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChangesV2
        public String SaveChanges(AddressCountryViewModelV2 data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var countryOld = db.MS_AddressCountry.Find(data.country_Index);

                if (countryOld == null)
                {
                    if (!string.IsNullOrEmpty(data.country_Id))
                    {
                        var query = db.MS_AddressCountry.FirstOrDefault(c => c.Country_Id == data.country_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.country_Id))
                    {
                        data.country_Id = "Country_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_AddressCountry.FirstOrDefault(c => c.Country_Id == data.country_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Country_Id == data.country_Id)
                                {
                                    data.country_Id = "Country_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_AddressCountry Model = new MS_AddressCountry();


                    Model.Country_Index = Guid.NewGuid();
                    Model.Country_Id = data.country_Id;
                    Model.Country_Name = data.country_Name;
                    Model.Country_SecondName = data.country_SecondName;
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

                    db.MS_AddressCountry.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.country_Id))
                    {
                        if (countryOld.Country_Id != "")
                        {
                            data.country_Id = countryOld.Country_Id;
                        }
                    }
                    else
                    {
                        if (countryOld.Country_Id != data.country_Id)
                        {
                            var query = db.MS_AddressCountry.FirstOrDefault(c => c.Country_Id == data.country_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.country_Id = countryOld.Country_Id;
                        }
                    }

                    countryOld.Country_Id = data.country_Id;
                    countryOld.Country_Name = data.country_Name;
                    countryOld.Country_SecondName = data.country_SecondName;
                    countryOld.Ref_No1 = data.ref_No1;
                    countryOld.Ref_No2 = data.ref_No2;
                    countryOld.Ref_No3 = data.ref_No3;
                    countryOld.Ref_No4 = data.ref_No4;
                    countryOld.Ref_No5 = data.ref_No5;
                    countryOld.Remark = data.remark;
                    countryOld.UDF_1 = data.udf_1;
                    countryOld.UDF_2 = data.udf_2;
                    countryOld.UDF_3 = data.udf_3;
                    countryOld.UDF_4 = data.udf_4;
                    countryOld.UDF_5 = data.udf_5;
                    countryOld.IsActive = Convert.ToInt32(data.isActive);
                    countryOld.Update_By = data.create_By;
                    countryOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveCountry", msglog);
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
        public AddressCountryViewModelV2 find(Guid id)
        {
            try
            {

                var queryResult = db.MS_AddressCountry.Where(c => c.Country_Index == id).FirstOrDefault();

                var result = new AddressCountryViewModelV2();

                result.country_Index = queryResult.Country_Index;
                result.country_Id = queryResult.Country_Id;
                result.country_Name = queryResult.Country_Name;
                result.country_SecondName = queryResult.Country_SecondName;
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
        public Boolean getDelete(AddressCountryViewModelV2 data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Country = db.MS_AddressCountry.Find(data.country_Index);

                if (Country != null)
                {
                    Country.IsActive = 0;
                    Country.IsDelete = 1;


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
                        olog.logging("DeleteCountry", msglog);
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

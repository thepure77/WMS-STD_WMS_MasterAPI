using Comone.Utils;
using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness;
using MasterDataBusiness.BusinessUnit;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{
    public class BusinessUnitService
    {
        private MasterDataDbContext db;

        public BusinessUnitService()
        {
            db = new MasterDataDbContext();
        }

        public BusinessUnitService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterBusinessUnit
        public actionResultBusinessUnitViewModel filter(SearchBusinessUnitViewModel data)
        {
            try
            {

                var query = db.ms_BusinessUnit.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.BusinessUnit_Id.Contains(data.key)
                                         || c.BusinessUnit_Name.Contains(data.key));
                }
                if (!string.IsNullOrEmpty(data.createdate_date) && !string.IsNullOrEmpty(data.createdate_date_to))
                {
                    var dateStart = data.createdate_date.toBetweenDate();
                    var dateEnd = data.createdate_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }

                var Item = new List<ms_BusinessUnit>();
                var TotalRow = new List<ms_BusinessUnit>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.BusinessUnit_Id).ToList();

                var result = new List<SearchBusinessUnitViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchBusinessUnitViewModel();

                    resultItem.businessUnit_Index = item.BusinessUnit_Index;
                    resultItem.businessUnit_Id = item.BusinessUnit_Id;
                    resultItem.businessUnit_Name = item.BusinessUnit_Name;
                    resultItem.businessUnit_SecondName = item.BusinessUnit_SecondName;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.remark = item.Remark;

                    
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultBusinessUnitViewModel = new actionResultBusinessUnitViewModel();
                actionResultBusinessUnitViewModel.itemsBusinessUnit = result.ToList();
                actionResultBusinessUnitViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultBusinessUnitViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(BusinessUnitViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var BusinessUnitOld = db.ms_BusinessUnit.Find(data.businessUnit_Index);

                if (BusinessUnitOld == null)
                {
                    if (!string.IsNullOrEmpty(data.businessUnit_Id))
                    {
                        var query = db.ms_BusinessUnit.FirstOrDefault(c => c.BusinessUnit_Id == data.businessUnit_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.businessUnit_Id))
                    {
                        data.businessUnit_Id = "BusinessUnit_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_BusinessUnit.FirstOrDefault(c => c.BusinessUnit_Id == data.businessUnit_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.BusinessUnit_Id == data.businessUnit_Id)
                                {
                                    data.businessUnit_Id = "BusinessUnit_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_BusinessUnit Model = new ms_BusinessUnit();

                    Model.BusinessUnit_Index = Guid.NewGuid();
                    Model.BusinessUnit_Id = data.businessUnit_Id;
                    Model.BusinessUnit_Name = data.businessUnit_Name;
                    Model.BusinessUnit_SecondName = data.businessUnit_SecondName;
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
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.ms_BusinessUnit.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.businessUnit_Id))
                    {
                        if (BusinessUnitOld.BusinessUnit_Id != "")
                        {
                            data.businessUnit_Id = BusinessUnitOld.BusinessUnit_Id;
                        }
                    }
                    else
                    {
                        if (BusinessUnitOld.BusinessUnit_Id != data.businessUnit_Id)
                        {
                            var query = db.ms_BusinessUnit.FirstOrDefault(c => c.BusinessUnit_Id == data.businessUnit_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.businessUnit_Id = BusinessUnitOld.BusinessUnit_Id;
                        }
                    }

                    BusinessUnitOld.BusinessUnit_Id = data.businessUnit_Id;
                    BusinessUnitOld.BusinessUnit_Name = data.businessUnit_Name;
                    BusinessUnitOld.BusinessUnit_SecondName = data.businessUnit_SecondName;
                    BusinessUnitOld.Ref_No1 = data.ref_No1;
                    BusinessUnitOld.Ref_No2 = data.ref_No2;
                    BusinessUnitOld.Ref_No3 = data.ref_No3;
                    BusinessUnitOld.Ref_No4 = data.ref_No4;
                    BusinessUnitOld.Ref_No5 = data.ref_No5;
                    BusinessUnitOld.Remark = data.remark;
                    BusinessUnitOld.UDF_1 = null;
                    BusinessUnitOld.UDF_2 = null;
                    BusinessUnitOld.UDF_3 = null;
                    BusinessUnitOld.UDF_4 = null;
                    BusinessUnitOld.UDF_5 = null;
                    BusinessUnitOld.IsActive = Convert.ToInt32(data.isActive);
                    BusinessUnitOld.Update_By = data.create_By;
                    BusinessUnitOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveBusinessUnit", msglog);
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
        public BusinessUnitViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_BusinessUnit.Where(c => c.BusinessUnit_Index == id).FirstOrDefault();

                var result = new BusinessUnitViewModel();


                result.businessUnit_Index = queryResult.BusinessUnit_Index;
                result.businessUnit_Id = queryResult.BusinessUnit_Id;
                result.businessUnit_Name = queryResult.BusinessUnit_Name;
                result.businessUnit_SecondName = queryResult.BusinessUnit_SecondName;
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

        #region getDelete
        public Boolean getDelete(BusinessUnitViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Product = db.ms_BusinessUnit.Find(data.businessUnit_Index);

                if (Product != null)
                {
                    Product.IsActive = 0;
                    Product.IsDelete = 1;
                    Product.Cancel_By = data.create_By;
                    Product.Cancel_Date = DateTime.Now;

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
                        olog.logging("DeleteBusinessUnit" +
                            "" +
                            "", msglog);
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

        #region Export Excel
        public BusinessUnitActionResultExportViewModel Export(BusinessUnitExportViewModel data)
        {
            try
            {
                var query = db.ms_BusinessUnit.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.BusinessUnit_Id.Contains(data.key)
                                         || c.BusinessUnit_Name.Contains(data.key));
                }
                if (!string.IsNullOrEmpty(data.createdate_date) && !string.IsNullOrEmpty(data.createdate_date_to))
                {
                    var dateStart = data.createdate_date.toBetweenDate();
                    var dateEnd = data.createdate_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);

                }

                var Item = new List<ms_BusinessUnit>();
                var TotalRow = new List<ms_BusinessUnit>();

                TotalRow = query.ToList();
                Item = query.OrderBy(o => o.BusinessUnit_Id).ToList();

                var result = new List<BusinessUnitExportViewModel>();

                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new BusinessUnitExportViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.businessUnit_Index = item.BusinessUnit_Index;
                    resultItem.businessUnit_Id = item.BusinessUnit_Id;
                    resultItem.businessUnit_Name = item.BusinessUnit_Name;
                    resultItem.businessUnit_SecondName = item.BusinessUnit_SecondName;
                    resultItem.ref_No1 = item.Ref_No1 == null ? "" : item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2 == null ? "" : item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3 == null ? "" : item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4 == null ? "" : item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5 == null ? "" : item.Ref_No5;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.remark = item.Remark;
                    resultItem.isActive = item.IsActive;
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.cancel_By = item.Cancel_By == null ? "" : item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date != null ? item.Cancel_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var BusinessUnitActionResultExportViewModel = new BusinessUnitActionResultExportViewModel();
                BusinessUnitActionResultExportViewModel.itemsBusinessUnit = result.ToList();

                return BusinessUnitActionResultExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

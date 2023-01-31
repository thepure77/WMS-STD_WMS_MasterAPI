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
    public class ServiceChargeService
    {
        private MasterDataDbContext db;

        public ServiceChargeService()
        {
            db = new MasterDataDbContext();
        }

        public ServiceChargeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterZone
        public actionResultServiceChargeViewModel filter(SearchServiceChargeViewModel data)
        {
            try
            {
                var query = db.ms_ServiceCharge.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.ServiceCharge_Id.Contains(data.key)
                                         || c.ServiceCharge_Name.Contains(data.key));
                }

                var Item = new List<ms_ServiceCharge>();
                var TotalRow = new List<ms_ServiceCharge>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ServiceCharge_Id).ToList();

                var result = new List<SearchServiceChargeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchServiceChargeViewModel();

                    resultItem.serviceCharge_Index = item.ServiceCharge_Index;
                    resultItem.serviceCharge_Id = item.ServiceCharge_Id;
                    resultItem.serviceCharge_Name = item.ServiceCharge_Name;
                    resultItem.serviceCharge_SecondName = item.ServiceCharge_SecondName;
                    resultItem.default_Process_Index = item.DEFAULT_Process_Index;
                    resultItem.isSkuUse = item.IsSkuUse;
                    resultItem.vatRate = item.VatRate;
                    resultItem.vatType = item.VatType;
                    resultItem.vatCode = item.VatCode;
                    resultItem.vatGroup = item.VatGroup;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.remark = item.Remark;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultServiceChargeViewModel = new actionResultServiceChargeViewModel();
                actionResultServiceChargeViewModel.items = result.ToList();
                actionResultServiceChargeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultServiceChargeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(ServiceChargeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ServiceChargeOld = db.ms_ServiceCharge.Find(data.serviceCharge_Index);

                if (ServiceChargeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.serviceCharge_Id))
                    {
                        var query = db.ms_ServiceCharge.FirstOrDefault(c => c.ServiceCharge_Id == data.serviceCharge_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.serviceCharge_Id))
                    {
                        data.serviceCharge_Id = "ServiceCharge_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_ServiceCharge.FirstOrDefault(c => c.ServiceCharge_Id == data.serviceCharge_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ServiceCharge_Id == data.serviceCharge_Id)
                                {
                                    data.serviceCharge_Id = "ServiceCharge_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_ServiceCharge Model = new ms_ServiceCharge();

                    Model.ServiceCharge_Index = Guid.NewGuid();
                    Model.ServiceCharge_Id = data.serviceCharge_Id;
                    Model.ServiceCharge_Name = data.serviceCharge_Name;

                    Model.ServiceCharge_SecondName = data.serviceCharge_SecondName;
                    Model.DEFAULT_Process_Index = data.default_Process_Index;
                    Model.IsSkuUse = Convert.ToInt32(data.isSkuUse);
                    Model.VatRate = data.vatRate;
                    Model.VatType = data.vatType;
                    Model.VatCode = data.vatCode;
                    Model.VatGroup = data.vatGroup;
                    Model.Ref_No1 = data.ref_No1;
                    Model.Ref_No2 = data.ref_No2;
                    Model.Ref_No3 = data.ref_No3;
                    Model.Remark = data.remark;

                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.ms_ServiceCharge.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.serviceCharge_Id))
                    {
                        if (ServiceChargeOld.ServiceCharge_Id != "")
                        {
                            data.serviceCharge_Id = ServiceChargeOld.ServiceCharge_Id;
                        }
                    }
                    else
                    {
                        if (ServiceChargeOld.ServiceCharge_Id != data.serviceCharge_Id)
                        {
                            var query = db.ms_ServiceCharge.FirstOrDefault(c => c.ServiceCharge_Id == data.serviceCharge_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.serviceCharge_Id = ServiceChargeOld.ServiceCharge_Id;
                        }
                    }

                    ServiceChargeOld.ServiceCharge_Id = data.serviceCharge_Id;
                    ServiceChargeOld.ServiceCharge_Name = data.serviceCharge_Name;

                    ServiceChargeOld.ServiceCharge_SecondName = data.serviceCharge_SecondName;
                    ServiceChargeOld.DEFAULT_Process_Index = data.default_Process_Index;
                    ServiceChargeOld.IsSkuUse = Convert.ToInt32(data.isSkuUse);
                    ServiceChargeOld.VatRate = data.vatRate;
                    ServiceChargeOld.VatType = data.vatType;
                    ServiceChargeOld.VatCode = data.vatCode;
                    ServiceChargeOld.VatGroup = data.vatGroup;
                    ServiceChargeOld.Ref_No1 = data.ref_No1;
                    ServiceChargeOld.Ref_No2 = data.ref_No2;
                    ServiceChargeOld.Ref_No3 = data.ref_No3;
                    ServiceChargeOld.Remark = data.remark;

                    ServiceChargeOld.IsActive = Convert.ToInt32(data.isActive);
                    ServiceChargeOld.Update_By = data.create_By;
                    ServiceChargeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveServiceCharge", msglog);
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
        public ServiceChargeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_ServiceCharge.Where(c => c.ServiceCharge_Index == id).FirstOrDefault();

                var result = new ServiceChargeViewModel();


                result.serviceCharge_Index = queryResult.ServiceCharge_Index;
                result.serviceCharge_Id = queryResult.ServiceCharge_Id;
                result.serviceCharge_Name = queryResult.ServiceCharge_Name;
                result.serviceCharge_SecondName = queryResult.ServiceCharge_SecondName;
                result.default_Process_Index = queryResult.DEFAULT_Process_Index;
                result.isSkuUse = queryResult.IsSkuUse;
                result.vatRate = queryResult.VatRate;
                result.vatType = queryResult.VatType;
                result.vatCode = queryResult.VatCode;
                result.vatGroup = queryResult.VatGroup;
                result.ref_No1 = queryResult.Ref_No1;
                result.ref_No2 = queryResult.Ref_No2;
                result.ref_No3 = queryResult.Ref_No3;
                result.remark = queryResult.Remark;
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
        public Boolean getDelete(ServiceChargeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var ServiceCharge = db.ms_ServiceCharge.Find(data.serviceCharge_Index);

                if (ServiceCharge != null)
                {
                    ServiceCharge.IsActive = 0;
                    ServiceCharge.IsDelete = 1;


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
                        olog.logging("DeleteServiceCharge", msglog);
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


        #region dropDownServiceCharge
        public List<ServiceChargeViewModel> dropDownServiceCharge(ServiceChargeViewModel model)
        {
            var olog = new logtxt();
            String msglog = "";
            try
            {
                var items = new List<ServiceChargeViewModel>();

                var query = db.ms_ServiceCharge.Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();



                foreach (var item in query)
                {
                    var resultItem = new ServiceChargeViewModel
                    {
                        serviceCharge_Index = item.ServiceCharge_Index,
                        serviceCharge_Id = item.ServiceCharge_Id,
                        serviceCharge_Name = item.ServiceCharge_Name
                    };

                    items.Add(resultItem);
                }

                return items;
            }
            catch (Exception ex)
            {
                olog.logging("dropDownServiceCharge", msglog + " ex Rollback " + ex.Message.ToString());
                throw ex;
            }
        }

        #endregion

        #region dropDownServiceChargeFix
        public List<View_ServiceChargeFixViewModel> dropDownServiceChargeFix(View_ServiceChargeFixViewModel model)
        {
            var olog = new logtxt();
            String msglog = "";
            try
            {
                var items = new List<View_ServiceChargeFixViewModel>();

                var query = db.View_ServiceChargeFix.Where(c => c.Owner_Index == model.owner_Index).ToList();


                foreach (var item in query)
                {
                    var resultItem = new View_ServiceChargeFixViewModel
                    {
                        serviceCharge_Index = item.ServiceCharge_Index,
                        serviceCharge_Id = item.ServiceCharge_Id,
                        serviceCharge_Name = item.ServiceCharge_Name,
                        rate = item.rate
                    };

                    items.Add(resultItem);
                }

                return items;
            }
            catch (Exception ex)
            {
                olog.logging("dropDownServiceChargeFix", msglog + " ex Rollback " + ex.Message.ToString());
                throw ex;
            }
        }

        #endregion

    }
}

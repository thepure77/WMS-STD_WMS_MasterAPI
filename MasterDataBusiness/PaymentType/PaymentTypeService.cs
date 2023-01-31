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

namespace MasterDataBusiness
{
    public class PaymentTypeService
    {
        private MasterDataDbContext db;

        public PaymentTypeService()
        {
            db = new MasterDataDbContext();
        }

        public PaymentTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterPaymentType
        public actionResultPaymentTypeViewModel filter(SearchPaymentTypeViewModel data)
        {
            try
            {
                var query = db.ms_PaymentType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.PaymentType_Id.Contains(data.key)
                                         || c.PaymentType_Name.Contains(data.key));
                }

                var Item = new List<ms_PaymentType>();
                var TotalRow = new List<ms_PaymentType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.PaymentType_Id).ToList();

                var result = new List<SearchPaymentTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchPaymentTypeViewModel();

                    resultItem.paymentType_Index = item.PaymentType_Index;

                    resultItem.paymentType_Id = item.PaymentType_Id;

                    resultItem.paymentType_Name = item.PaymentType_Name;

                    resultItem.paymentType_SecondName = item.PaymentType_SecondName;

                    resultItem.paymentType_ThirdName = item.PaymentType_ThirdName;

                    resultItem.ref_No1 = item.Ref_No1;

                    resultItem.ref_No2 = item.Ref_No2;

                    resultItem.ref_No3 = item.Ref_No3;

                    resultItem.ref_No4 = item.Ref_No4;

                    resultItem.ref_No5 = item.Ref_No5;

                    resultItem.remark = item.Remark;

                    resultItem.uDF_1 = item.UDF_1;

                    resultItem.uDF_2 = item.UDF_2;

                    resultItem.uDF_3 = item.UDF_3;

                    resultItem.uDF_4 = item.UDF_4;

                    resultItem.uDF_5 = item.UDF_5;

                    resultItem.isActive = item.IsActive;

                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultPaymentTypeViewModel = new actionResultPaymentTypeViewModel();
                actionResultPaymentTypeViewModel.itemsPaymentType = result.ToList();
                actionResultPaymentTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultPaymentTypeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(PaymentTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var PaymentTypeOld = db.ms_PaymentType.Find(data.paymentType_Index);

                if (PaymentTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.paymentType_Id))
                    {
                        var query = db.ms_PaymentType.FirstOrDefault(c => c.PaymentType_Id == data.paymentType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.paymentType_Id))
                    {
                        data.paymentType_Id = "PaymentType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_PaymentType.FirstOrDefault(c => c.PaymentType_Id == data.paymentType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.PaymentType_Id == data.paymentType_Id)
                                {
                                    data.paymentType_Id = "PaymentType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_PaymentType Model = new ms_PaymentType();


                    Model.PaymentType_Index = Guid.NewGuid();
                    Model.PaymentType_Id = data.paymentType_Id;
                    Model.PaymentType_Name = data.paymentType_Name;
                    Model.PaymentType_SecondName = data.paymentType_SecondName;
                    Model.PaymentType_ThirdName = data.paymentType_ThirdName;
                    Model.Ref_No1 = data.ref_No1;
                    Model.Ref_No2 = data.ref_No2;
                    Model.Ref_No3 = data.ref_No3;
                    Model.Ref_No4 = data.ref_No4;
                    Model.Ref_No5 = data.ref_No5;
                    Model.Remark = data.remark;
                    Model.UDF_1 = data.uDF_1;
                    Model.UDF_2 = data.uDF_2;
                    Model.UDF_3 = data.uDF_3;
                    Model.UDF_4 = data.uDF_4;
                    Model.UDF_5 = data.uDF_5;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.ms_PaymentType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.paymentType_Id))
                    {
                        if (PaymentTypeOld.PaymentType_Id != "")
                        {
                            data.paymentType_Id = PaymentTypeOld.PaymentType_Id;
                        }
                    }
                    else
                    {
                        if (PaymentTypeOld.PaymentType_Id != data.paymentType_Id)
                        {
                            var query = db.ms_PaymentType.FirstOrDefault(c => c.PaymentType_Id == data.paymentType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.paymentType_Id = PaymentTypeOld.PaymentType_Id;
                        }
                    }

                    PaymentTypeOld.PaymentType_Id = data.paymentType_Id;
                    PaymentTypeOld.PaymentType_Name = data.paymentType_Name;
                    PaymentTypeOld.PaymentType_SecondName = data.paymentType_SecondName;
                    PaymentTypeOld.PaymentType_ThirdName = data.paymentType_ThirdName;
                    PaymentTypeOld.Ref_No1 = data.ref_No1;
                    PaymentTypeOld.Ref_No2 = data.ref_No2;
                    PaymentTypeOld.Ref_No3 = data.ref_No3;
                    PaymentTypeOld.Ref_No4 = data.ref_No4;
                    PaymentTypeOld.Ref_No5 = data.ref_No5;
                    PaymentTypeOld.Remark = data.remark;
                    PaymentTypeOld.UDF_1 = data.uDF_1;
                    PaymentTypeOld.UDF_2 = data.uDF_2;
                    PaymentTypeOld.UDF_3 = data.uDF_3;
                    PaymentTypeOld.UDF_4 = data.uDF_4;
                    PaymentTypeOld.UDF_5 = data.uDF_5;
                    PaymentTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    PaymentTypeOld.Update_By = data.update_By;
                    PaymentTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SavePaymentType", msglog);
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
        public PaymentTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_PaymentType.Where(c => c.PaymentType_Index == id).FirstOrDefault();

                var result = new PaymentTypeViewModel();

                result.paymentType_Index = queryResult.PaymentType_Index;
                result.paymentType_Id = queryResult.PaymentType_Id;
                result.paymentType_Name = queryResult.PaymentType_Name;
                result.paymentType_SecondName = queryResult.PaymentType_SecondName;
                result.paymentType_ThirdName = queryResult.PaymentType_ThirdName;
                result.ref_No1 = queryResult.Ref_No1;
                result.ref_No2 = queryResult.Ref_No2;
                result.ref_No3 = queryResult.Ref_No3;
                result.ref_No4 = queryResult.Ref_No4;
                result.ref_No5 = queryResult.Ref_No5;
                result.remark = queryResult.Remark;
                result.uDF_1 = queryResult.UDF_1;
                result.uDF_2 = queryResult.UDF_2;
                result.uDF_3 = queryResult.UDF_3;
                result.uDF_4 = queryResult.UDF_4;
                result.uDF_5 = queryResult.UDF_5;
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
        public Boolean getDelete(PaymentTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var PaymentType = db.ms_PaymentType.Find(data.paymentType_Index);

                if (PaymentType != null)
                {
                    PaymentType.IsActive = 0;
                    PaymentType.IsDelete = 1;


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
                        olog.logging("DeletePaymentType", msglog);
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
        public List<PaymentTypeViewModel> paymentTypedropdown(PaymentTypeViewModel data)
        {
            try
            {
                var result = new List<PaymentTypeViewModel>();

                var query = db.ms_PaymentType.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.PaymentType_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new PaymentTypeViewModel();

                    resultItem.paymentType_Index = item.PaymentType_Index;
                    resultItem.paymentType_Id = item.PaymentType_Id;
                    resultItem.paymentType_Name = item.PaymentType_Name;

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

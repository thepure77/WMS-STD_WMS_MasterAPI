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
    public class ShippingMethodService
    {
        private MasterDataDbContext db;

        public ShippingMethodService()
        {
            db = new MasterDataDbContext();
        }

        public ShippingMethodService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterShippingMethod
        public actionResultShippingMethodViewModel filter(SearchShippingMethodViewModel data)
        {
            try
            {
                var query = db.ms_ShippingMethod.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.ShippingMethod_Id.Contains(data.key)
                                         || c.ShippingMethod_Name.Contains(data.key));
                }

                var Item = new List<ms_ShippingMethod>();
                var TotalRow = new List<ms_ShippingMethod>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ShippingMethod_Id).ToList();

                var result = new List<SearchShippingMethodViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchShippingMethodViewModel();

                    resultItem.shippingMethod_Index = item.ShippingMethod_Index;

                    resultItem.shippingMethod_Id = item.ShippingMethod_Id;

                    resultItem.shippingMethod_Name = item.ShippingMethod_Name;

                    resultItem.shippingMethod_SecondName = item.ShippingMethod_SecondName;

                    resultItem.shippingMethod_ThirdName = item.ShippingMethod_ThirdName;

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

                var actionResultShippingMethodViewModel = new actionResultShippingMethodViewModel();
                actionResultShippingMethodViewModel.itemsShippingMethod = result.ToList();
                actionResultShippingMethodViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultShippingMethodViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(ShippingMethodViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ShippingMethodOld = db.ms_ShippingMethod.Find(data.shippingMethod_Index);

                if (ShippingMethodOld == null)
                {
                    if (!string.IsNullOrEmpty(data.shippingMethod_Id))
                    {
                        var query = db.ms_ShippingMethod.FirstOrDefault(c => c.ShippingMethod_Id == data.shippingMethod_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.shippingMethod_Id))
                    {
                        data.shippingMethod_Id = "ShippingMethod_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_ShippingMethod.FirstOrDefault(c => c.ShippingMethod_Id == data.shippingMethod_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ShippingMethod_Id == data.shippingMethod_Id)
                                {
                                    data.shippingMethod_Id = "ShippingMethod_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_ShippingMethod Model = new ms_ShippingMethod();


                    Model.ShippingMethod_Index = Guid.NewGuid();
                    Model.ShippingMethod_Id = data.shippingMethod_Id;
                    Model.ShippingMethod_Name = data.shippingMethod_Name;
                    Model.ShippingMethod_SecondName = data.shippingMethod_SecondName;
                    Model.ShippingMethod_ThirdName = data.shippingMethod_ThirdName;
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

                    db.ms_ShippingMethod.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.shippingMethod_Id))
                    {
                        if (ShippingMethodOld.ShippingMethod_Id != "")
                        {
                            data.shippingMethod_Id = ShippingMethodOld.ShippingMethod_Id;
                        }
                    }
                    else
                    {
                        if (ShippingMethodOld.ShippingMethod_Id != data.shippingMethod_Id)
                        {
                            var query = db.ms_ShippingMethod.FirstOrDefault(c => c.ShippingMethod_Id == data.shippingMethod_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.shippingMethod_Id = ShippingMethodOld.ShippingMethod_Id;
                        }
                    }

                    ShippingMethodOld.ShippingMethod_Id = data.shippingMethod_Id;
                    ShippingMethodOld.ShippingMethod_Name = data.shippingMethod_Name;
                    ShippingMethodOld.ShippingMethod_SecondName = data.shippingMethod_SecondName;
                    ShippingMethodOld.ShippingMethod_ThirdName = data.shippingMethod_ThirdName;
                    ShippingMethodOld.Ref_No1 = data.ref_No1;
                    ShippingMethodOld.Ref_No2 = data.ref_No2;
                    ShippingMethodOld.Ref_No3 = data.ref_No3;
                    ShippingMethodOld.Ref_No4 = data.ref_No4;
                    ShippingMethodOld.Ref_No5 = data.ref_No5;
                    ShippingMethodOld.Remark = data.remark;
                    ShippingMethodOld.UDF_1 = data.uDF_1;
                    ShippingMethodOld.UDF_2 = data.uDF_2;
                    ShippingMethodOld.UDF_3 = data.uDF_3;
                    ShippingMethodOld.UDF_4 = data.uDF_4;
                    ShippingMethodOld.UDF_5 = data.uDF_5;
                    ShippingMethodOld.IsActive = Convert.ToInt32(data.isActive);
                    ShippingMethodOld.Update_By = data.create_By;
                    ShippingMethodOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveShippingMethod", msglog);
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
        public ShippingMethodViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_ShippingMethod.Where(c => c.ShippingMethod_Index == id).FirstOrDefault();

                var result = new ShippingMethodViewModel();

                result.shippingMethod_Index = queryResult.ShippingMethod_Index;
                result.shippingMethod_Id = queryResult.ShippingMethod_Id;
                result.shippingMethod_Name = queryResult.ShippingMethod_Name;
                result.shippingMethod_SecondName = queryResult.ShippingMethod_SecondName;
                result.shippingMethod_ThirdName = queryResult.ShippingMethod_ThirdName;
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
        public Boolean getDelete(ShippingMethodViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var ShippingMethod = db.ms_ShippingMethod.Find(data.shippingMethod_Index);

                if (ShippingMethod != null)
                {
                    ShippingMethod.IsActive = 0;
                    ShippingMethod.IsDelete = 1;


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
                        olog.logging("DeleteShippingMethod", msglog);
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
        public List<ShippingMethodViewModel> shippingMethoddropdown(ShippingMethodViewModel data)
        {
            try
            {
                var result = new List<ShippingMethodViewModel>();

                var query = db.ms_ShippingMethod.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.ShippingMethod_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new ShippingMethodViewModel();

                    resultItem.shippingMethod_Index = item.ShippingMethod_Index;
                    resultItem.shippingMethod_Id = item.ShippingMethod_Id;
                    resultItem.shippingMethod_Name = item.ShippingMethod_Name;

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

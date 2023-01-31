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
    public class ShippingTermsService
    {
        private MasterDataDbContext db;

        public ShippingTermsService()
        {
            db = new MasterDataDbContext();
        }

        public ShippingTermsService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterShippingTerms
        public actionResultShippingTermsViewModel filter(SearchShippingTermsViewModel data)
        {
            try
            {
                var query = db.ms_ShippingTerms.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.ShippingTerms_Id.Contains(data.key)
                                         || c.ShippingTerms_Name.Contains(data.key));
                }

                var Item = new List<ms_ShippingTerms>();
                var TotalRow = new List<ms_ShippingTerms>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ShippingTerms_Id).ToList();

                var result = new List<SearchShippingTermsViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchShippingTermsViewModel();

                    resultItem.shippingTerms_Index = item.ShippingTerms_Index;

                    resultItem.shippingTerms_Id = item.ShippingTerms_Id;

                    resultItem.shippingTerms_Name = item.ShippingTerms_Name;

                    resultItem.shippingTerms_SecondName = item.ShippingTerms_SecondName;

                    resultItem.shippingTerms_ThirdName = item.ShippingTerms_ThirdName;

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

                var actionResultShippingTermsViewModel = new actionResultShippingTermsViewModel();
                actionResultShippingTermsViewModel.itemsShippingTerms = result.ToList();
                actionResultShippingTermsViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultShippingTermsViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(ShippingTermsViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ShippingTermsOld = db.ms_ShippingTerms.Find(data.shippingTerms_Index);

                if (ShippingTermsOld == null)
                {
                    if (!string.IsNullOrEmpty(data.shippingTerms_Id))
                    {
                        var query = db.ms_ShippingTerms.FirstOrDefault(c => c.ShippingTerms_Id == data.shippingTerms_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.shippingTerms_Id))
                    {
                        data.shippingTerms_Id = "ShippingTerms_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_ShippingTerms.FirstOrDefault(c => c.ShippingTerms_Id == data.shippingTerms_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ShippingTerms_Id == data.shippingTerms_Id)
                                {
                                    data.shippingTerms_Id = "ShippingTerms_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_ShippingTerms Model = new ms_ShippingTerms();


                    Model.ShippingTerms_Index = Guid.NewGuid();
                    Model.ShippingTerms_Id = data.shippingTerms_Id;
                    Model.ShippingTerms_Name = data.shippingTerms_Name;
                    Model.ShippingTerms_SecondName = data.shippingTerms_SecondName;
                    Model.ShippingTerms_ThirdName = data.shippingTerms_ThirdName;
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

                    db.ms_ShippingTerms.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.shippingTerms_Id))
                    {
                        if (ShippingTermsOld.ShippingTerms_Id != "")
                        {
                            data.shippingTerms_Id = ShippingTermsOld.ShippingTerms_Id;
                        }
                    }
                    else
                    {
                        if (ShippingTermsOld.ShippingTerms_Id != data.shippingTerms_Id)
                        {
                            var query = db.ms_ShippingTerms.FirstOrDefault(c => c.ShippingTerms_Id == data.shippingTerms_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.shippingTerms_Id = ShippingTermsOld.ShippingTerms_Id;
                        }
                    }

                    ShippingTermsOld.ShippingTerms_Id = data.shippingTerms_Id;
                    ShippingTermsOld.ShippingTerms_Name = data.shippingTerms_Name;
                    ShippingTermsOld.ShippingTerms_SecondName = data.shippingTerms_SecondName;
                    ShippingTermsOld.ShippingTerms_ThirdName = data.shippingTerms_ThirdName;
                    ShippingTermsOld.Ref_No1 = data.ref_No1;
                    ShippingTermsOld.Ref_No2 = data.ref_No2;
                    ShippingTermsOld.Ref_No3 = data.ref_No3;
                    ShippingTermsOld.Ref_No4 = data.ref_No4;
                    ShippingTermsOld.Ref_No5 = data.ref_No5;
                    ShippingTermsOld.Remark = data.remark;
                    ShippingTermsOld.UDF_1 = data.uDF_1;
                    ShippingTermsOld.UDF_2 = data.uDF_2;
                    ShippingTermsOld.UDF_3 = data.uDF_3;
                    ShippingTermsOld.UDF_4 = data.uDF_4;
                    ShippingTermsOld.UDF_5 = data.uDF_5;
                    ShippingTermsOld.IsActive = Convert.ToInt32(data.isActive);
                    ShippingTermsOld.Update_By = data.create_By;
                    ShippingTermsOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveShippingTerms", msglog);
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
        public ShippingTermsViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_ShippingTerms.Where(c => c.ShippingTerms_Index == id).FirstOrDefault();

                var result = new ShippingTermsViewModel();

                result.shippingTerms_Index = queryResult.ShippingTerms_Index;
                result.shippingTerms_Id = queryResult.ShippingTerms_Id;
                result.shippingTerms_Name = queryResult.ShippingTerms_Name;
                result.shippingTerms_SecondName = queryResult.ShippingTerms_SecondName;
                result.shippingTerms_ThirdName = queryResult.ShippingTerms_ThirdName;
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
        public Boolean getDelete(ShippingTermsViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var ShippingTerms = db.ms_ShippingTerms.Find(data.shippingTerms_Index);

                if (ShippingTerms != null)
                {
                    ShippingTerms.IsActive = 0;
                    ShippingTerms.IsDelete = 1;


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
                        olog.logging("DeleteShippingTerms", msglog);
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
        public List<ShippingTermsViewModel> shippingTermsdropdown(ShippingTermsViewModel data)
        {
            try
            {
                var result = new List<ShippingTermsViewModel>();

                var query = db.ms_ShippingTerms.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.ShippingTerms_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new ShippingTermsViewModel();

                    resultItem.shippingTerms_Index = item.ShippingTerms_Index;
                    resultItem.shippingTerms_Id = item.ShippingTerms_Id;
                    resultItem.shippingTerms_Name = item.ShippingTerms_Name;

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

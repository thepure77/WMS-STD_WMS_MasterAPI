using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.TempCondition;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static MasterDataBusiness.ViewModels.SearchTempConditionViewModel;

namespace MasterDataBusiness
{
    public class TempConditionService
    {
        private MasterDataDbContext db;

        public TempConditionService()
        {
            db = new MasterDataDbContext();
        }

        public TempConditionService(MasterDataDbContext db)
        {
            this.db = db;
        }
        #region filterTempCondition
        public actionResultTempConditionViewModel filter(SearchTempConditionViewModel data)
        {
            try
            {

                var query = db.ms_TempCondition.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.TempCondition_Id.Contains(data.key)
                                         || c.TempCondition_Name.Contains(data.key));
                }

                var Item = new List<ms_TempCondition>();
                var TotalRow = new List<ms_TempCondition>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.TempCondition_Id).ToList();

                var result = new List<SearchTempConditionViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchTempConditionViewModel();

                    resultItem.tempCondition_Index = item.TempCondition_Index;
                    resultItem.tempCondition_Id = item.TempCondition_Id;
                    resultItem.tempCondition_Name = item.TempCondition_Name;
                    resultItem.tempCondition_SecondName = item.TempCondition_SecondName;
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

                var actionResultTempConditionViewModel = new actionResultTempConditionViewModel();
                actionResultTempConditionViewModel.itemsTempCondition = result.ToList();
                actionResultTempConditionViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultTempConditionViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(TempConditionViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var TempConditionOld = db.ms_TempCondition.Find(data.tempCondition_Index);

                if (TempConditionOld == null)
                {
                    if (!string.IsNullOrEmpty(data.tempCondition_Id))
                    {
                        var query = db.ms_TempCondition.FirstOrDefault(c => c.TempCondition_Id == data.tempCondition_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.tempCondition_Id))
                    {
                        data.tempCondition_Id = "TempCondition_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_TempCondition.FirstOrDefault(c => c.TempCondition_Id == data.tempCondition_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.TempCondition_Id == data.tempCondition_Id)
                                {
                                    data.tempCondition_Id = "TempCondition_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_TempCondition Model = new ms_TempCondition();

                    Model.TempCondition_Index = Guid.NewGuid();
                    Model.TempCondition_Id = data.tempCondition_Id;
                    Model.TempCondition_Name = data.tempCondition_Name;
                    Model.TempCondition_SecondName = data.tempCondition_SecondName;
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

                    db.ms_TempCondition.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.tempCondition_Id))
                    {
                        if (TempConditionOld.TempCondition_Id != "")
                        {
                            data.tempCondition_Id = TempConditionOld.TempCondition_Id;
                        }
                    }
                    else
                    {
                        if (TempConditionOld.TempCondition_Id != data.tempCondition_Id)
                        {
                            var query = db.ms_TempCondition.FirstOrDefault(c => c.TempCondition_Id == data.tempCondition_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.tempCondition_Id = TempConditionOld.TempCondition_Id;
                        }
                    }

                    TempConditionOld.TempCondition_Id = data.tempCondition_Id;
                    TempConditionOld.TempCondition_Name = data.tempCondition_Name;
                    TempConditionOld.TempCondition_SecondName = data.tempCondition_SecondName;
                    TempConditionOld.Ref_No1 = data.ref_No1;
                    TempConditionOld.Ref_No2 = data.ref_No2;
                    TempConditionOld.Ref_No3 = data.ref_No3;
                    TempConditionOld.Ref_No4 = data.ref_No4;
                    TempConditionOld.Ref_No5 = data.ref_No5;
                    TempConditionOld.Remark = data.remark;
                    TempConditionOld.UDF_1 = null;
                    TempConditionOld.UDF_2 = null;
                    TempConditionOld.UDF_3 = null;
                    TempConditionOld.UDF_4 = null;
                    TempConditionOld.UDF_5 = null;
                    TempConditionOld.IsActive = Convert.ToInt32(data.isActive);
                    TempConditionOld.Update_By = data.create_By;
                    TempConditionOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveTempCondition", msglog);
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
        public TempConditionViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_TempCondition.Where(c => c.TempCondition_Index == id).FirstOrDefault();

                var result = new TempConditionViewModel();


                result.tempCondition_Index = queryResult.TempCondition_Index;
                result.tempCondition_Id = queryResult.TempCondition_Id;
                result.tempCondition_Name = queryResult.TempCondition_Name;
                result.tempCondition_SecondName = queryResult.TempCondition_SecondName;
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
        public Boolean getDelete(TempConditionViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Product = db.ms_TempCondition.Find(data.tempCondition_Index);

                if (Product != null)
                {
                    Product.IsActive = 0;
                    Product.IsDelete = 1;


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
                        olog.logging("DeleteTempCondition" +
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
    }
}

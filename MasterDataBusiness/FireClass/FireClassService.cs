using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.FireClass;
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
    public class FireClassService
    {
        private MasterDataDbContext db;

        public FireClassService()
        {
            db = new MasterDataDbContext();
        }

        public FireClassService(MasterDataDbContext db)
        {
            this.db = db;
        }
        #region filterFireClass
        public actionResultFireClassViewModel filter(SearchFireClassViewModel data)
        {
            try
            {

                var query = db.ms_FireClass.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.FireClass_Id.Contains(data.key)
                                         || c.FireClass_Name.Contains(data.key));
                }

                var Item = new List<ms_FireClass>();
                var TotalRow = new List<ms_FireClass>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.FireClass_Id).ToList();

                var result = new List<SearchFireClassViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchFireClassViewModel();

                    resultItem.fireClass_Index = item.FireClass_Index;
                    resultItem.fireClass_Id = item.FireClass_Id;
                    resultItem.fireClass_Name = item.FireClass_Name;
                    resultItem.fireClass_SecondName = item.FireClass_SecondName;
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

                var actionResultFireClassViewModel = new actionResultFireClassViewModel();
                actionResultFireClassViewModel.itemsFireClass = result.ToList();
                actionResultFireClassViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultFireClassViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(FireClassViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var FireClassOld = db.ms_FireClass.Find(data.fireClass_Index);

                if (FireClassOld == null)
                {
                    if (!string.IsNullOrEmpty(data.fireClass_Id))
                    {
                        var query = db.ms_FireClass.FirstOrDefault(c => c.FireClass_Id == data.fireClass_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.fireClass_Id))
                    {
                        data.fireClass_Id = "FireClass_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_FireClass.FirstOrDefault(c => c.FireClass_Id == data.fireClass_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.FireClass_Id == data.fireClass_Id)
                                {
                                    data.fireClass_Id = "FireClass_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_FireClass Model = new ms_FireClass();

                    Model.FireClass_Index = Guid.NewGuid();
                    Model.FireClass_Id = data.fireClass_Id;
                    Model.FireClass_Name = data.fireClass_Name;
                    Model.FireClass_SecondName = data.fireClass_SecondName;
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

                    db.ms_FireClass.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.fireClass_Id))
                    {
                        if (FireClassOld.FireClass_Id != "")
                        {
                            data.fireClass_Id = FireClassOld.FireClass_Id;
                        }
                    }
                    else
                    {
                        if (FireClassOld.FireClass_Id != data.fireClass_Id)
                        {
                            var query = db.ms_FireClass.FirstOrDefault(c => c.FireClass_Id == data.fireClass_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.fireClass_Id = FireClassOld.FireClass_Id;
                        }
                    }

                    FireClassOld.FireClass_Id = data.fireClass_Id;
                    FireClassOld.FireClass_Name = data.fireClass_Name;
                    FireClassOld.FireClass_SecondName = data.fireClass_SecondName;
                    FireClassOld.Ref_No1 = data.ref_No1;
                    FireClassOld.Ref_No2 = data.ref_No2;
                    FireClassOld.Ref_No3 = data.ref_No3;
                    FireClassOld.Ref_No4 = data.ref_No4;
                    FireClassOld.Ref_No5 = data.ref_No5;
                    FireClassOld.Remark = data.remark;
                    FireClassOld.UDF_1 = null;
                    FireClassOld.UDF_2 = null;
                    FireClassOld.UDF_3 = null;
                    FireClassOld.UDF_4 = null;
                    FireClassOld.UDF_5 = null;
                    FireClassOld.IsActive = Convert.ToInt32(data.isActive);
                    FireClassOld.Update_By = data.create_By;
                    FireClassOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveFireClass", msglog);
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
        public FireClassViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_FireClass.Where(c => c.FireClass_Index == id).FirstOrDefault();

                var result = new FireClassViewModel();


                result.fireClass_Index = queryResult.FireClass_Index;
                result.fireClass_Id = queryResult.FireClass_Id;
                result.fireClass_Name = queryResult.FireClass_Name;
                result.fireClass_SecondName = queryResult.FireClass_SecondName;
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
        public Boolean getDelete(FireClassViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Product = db.ms_FireClass.Find(data.fireClass_Index);

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
                        olog.logging("DeleteFireClass" +
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
   

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
using System.Linq;
using System.Text;
using static MasterDataBusiness.ViewModels.SearchMovingConditionViewModel;

namespace MasterDataBusiness.MovingCondition
{
    public class MovingConditionService
    {
        private MasterDataDbContext db;

        public MovingConditionService()
        {
            db = new MasterDataDbContext();
        }

        public MovingConditionService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterMovingCondition
        public actionResultMovingConditionViewModel filter(SearchMovingConditionViewModel data)
        {
            try
            {

                var query = db.ms_MovingCondition.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.MovingCondition_Id.Contains(data.key)
                                         || c.MovingCondition_Name.Contains(data.key));
                }

                var Item = new List<ms_MovingCondition>();
                var TotalRow = new List<ms_MovingCondition>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.MovingCondition_Id).ToList();

                var result = new List<SearchMovingConditionViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchMovingConditionViewModel();

                    resultItem.movingCondition_Index = item.MovingCondition_Index;
                    resultItem.movingCondition_Id = item.MovingCondition_Id;
                    resultItem.movingCondition_Name = item.MovingCondition_Name;
                    resultItem.movingCondition_SecondName = item.MovingCondition_SecondName;
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

                var actionResultMovingConditionViewModel = new actionResultMovingConditionViewModel();
                actionResultMovingConditionViewModel.itemsMovingCondition = result.ToList();
                actionResultMovingConditionViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultMovingConditionViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(MovingConditionViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var MovingConditionOld = db.ms_MovingCondition.Find(data.movingCondition_Index);

                if (MovingConditionOld == null)
                {
                    if (!string.IsNullOrEmpty(data.movingCondition_Id))
                    {
                        var query = db.ms_MovingCondition.FirstOrDefault(c => c.MovingCondition_Id == data.movingCondition_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.movingCondition_Id))
                    {
                        data.movingCondition_Id = "MovingCondition_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_MovingCondition.FirstOrDefault(c => c.MovingCondition_Id == data.movingCondition_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.MovingCondition_Id == data.movingCondition_Id)
                                {
                                    data.movingCondition_Id = "MovingCondition_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_MovingCondition Model = new ms_MovingCondition();

                    Model.MovingCondition_Index = Guid.NewGuid();
                    Model.MovingCondition_Id = data.movingCondition_Id;
                    Model.MovingCondition_Name = data.movingCondition_Name;
                    Model.MovingCondition_SecondName = data.movingCondition_SecondName;
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

                    db.ms_MovingCondition.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.movingCondition_Id))
                    {
                        if (MovingConditionOld.MovingCondition_Id != "")
                        {
                            data.movingCondition_Id = MovingConditionOld.MovingCondition_Id;
                        }
                    }
                    else
                    {
                        if (MovingConditionOld.MovingCondition_Id != data.movingCondition_Id)
                        {
                            var query = db.ms_MovingCondition.FirstOrDefault(c => c.MovingCondition_Id == data.movingCondition_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.movingCondition_Id = MovingConditionOld.MovingCondition_Id;
                        }
                    }

                    MovingConditionOld.MovingCondition_Id = data.movingCondition_Id;
                    MovingConditionOld.MovingCondition_Name = data.movingCondition_Name;
                    MovingConditionOld.MovingCondition_SecondName = data.movingCondition_SecondName;
                    MovingConditionOld.Ref_No1 = data.ref_No1;
                    MovingConditionOld.Ref_No2 = data.ref_No2;
                    MovingConditionOld.Ref_No3 = data.ref_No3;
                    MovingConditionOld.Ref_No4 = data.ref_No4;
                    MovingConditionOld.Ref_No5 = data.ref_No5;
                    MovingConditionOld.Remark = data.remark;
                    MovingConditionOld.UDF_1 = null;
                    MovingConditionOld.UDF_2 = null;
                    MovingConditionOld.UDF_3 = null;
                    MovingConditionOld.UDF_4 = null;
                    MovingConditionOld.UDF_5 = null;
                    MovingConditionOld.IsActive = Convert.ToInt32(data.isActive);
                    MovingConditionOld.Update_By = data.create_By;
                    MovingConditionOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveMovingCondition", msglog);
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
        public MovingConditionViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_MovingCondition.Where(c => c.MovingCondition_Index == id).FirstOrDefault();

                var result = new MovingConditionViewModel();


                result.movingCondition_Index = queryResult.MovingCondition_Index;
                result.movingCondition_Id = queryResult.MovingCondition_Id;
                result.movingCondition_Name = queryResult.MovingCondition_Name;
                result.movingCondition_SecondName = queryResult.MovingCondition_SecondName;
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
        public Boolean getDelete(MovingConditionViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Product = db.ms_MovingCondition.Find(data.movingCondition_Index);

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
                        olog.logging("DeleteMovingCondition" +
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

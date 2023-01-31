using DataAccess;
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

namespace MasterDataBusiness
{
    public class RuleConditionService
    {
        private MasterDataDbContext db;

        public RuleConditionService()
        {
            db = new MasterDataDbContext();
        }

        public RuleConditionService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterRuleCondition
        public actionResultRuleConditionViewModel filter(SearchRuleConditionViewModel data)
        {
            try
            {
                var query = db.View_RuleCondition.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.RuleCondition_Param.Contains(data.key)
                      || c.Rule_Id.Contains(data.key)
                      || c.Rule_Name.Contains(data.key)
                      || c.RuleConditionField_Name.Contains(data.key)
                      || c.RuleConditionOperationType.Contains(data.key));
                }

                var Item = new List<View_RuleCondition>();
                var TotalRow = new List<View_RuleCondition>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.ToList();

                var result = new List<SearchRuleConditionViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchRuleConditionViewModel();

                    resultItem.ruleCondition_Index = item.RuleCondition_Index;
                    resultItem.ruleCondition_Param = item.RuleCondition_Param;
                    resultItem.ruleConditionField_Index = item.RuleConditionField_Index;
                    resultItem.ruleConditionField_Name = item.RuleConditionField_Name;
                    resultItem.ruleConditionOperation_Index = item.RuleConditionOperation_Index;
                    resultItem.ruleConditionOperationType = item.RuleConditionOperationType;
                    resultItem.ruleConditionOperation = item.RuleConditionOperation;
                    resultItem.rule_Index = item.Rule_Index;
                    resultItem.rule_Id = item.Rule_Id;
                    resultItem.rule_Name = item.Rule_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultRuleConditionViewModel = new actionResultRuleConditionViewModel();
                actionResultRuleConditionViewModel.itemsRuleCondition = result.ToList();
                actionResultRuleConditionViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultRuleConditionViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

       

        #region SaveChanges
        public String SaveChanges(RuleConditionViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var RuleConditionOld = db.MS_RuleCondition.Find(data.ruleCondition_Index);

                if (RuleConditionOld == null)
                {

                    MS_RuleCondition Model = new MS_RuleCondition();


                    Model.RuleCondition_Index = Guid.NewGuid();
                    Model.Rule_Index = data.rule_Index;
                    Model.RuleConditionField_Index = data.ruleConditionField_Index;
                    Model.RuleConditionOperation_Index = data.ruleConditionOperation_Index;
                    Model.RuleCondition_Param = data.ruleCondition_Param;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_RuleCondition.Add(Model);
                }
                else
                {
                    RuleConditionOld.Rule_Index = data.rule_Index;
                    RuleConditionOld.RuleConditionField_Index = data.ruleConditionField_Index;
                    RuleConditionOld.RuleConditionOperation_Index = data.ruleConditionOperation_Index;
                    RuleConditionOld.RuleCondition_Param = data.ruleCondition_Param;
                    RuleConditionOld.IsActive = Convert.ToInt32(data.isActive);
                    RuleConditionOld.Update_By = data.create_By;
                    RuleConditionOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveRuleCondition", msglog);
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
        public RuleConditionViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_RuleCondition.Where(c => c.RuleCondition_Index == id).FirstOrDefault();

                var result = new RuleConditionViewModel();

                result.ruleCondition_Index = queryResult.RuleCondition_Index;
                result.ruleConditionField_Index = queryResult.RuleConditionField_Index;
                result.ruleConditionField_Name = queryResult.RuleConditionField_Name;
                result.ruleConditionOperation_Index = queryResult.RuleConditionOperation_Index;
                result.ruleConditionOperationType = queryResult.RuleConditionOperationType;
                result.ruleConditionOperation = queryResult.RuleConditionOperation;
                result.ruleCondition_Param = queryResult.RuleCondition_Param;
                result.rule_Index = queryResult.Rule_Index;
                result.rule_Id = queryResult.Rule_Id;
                result.rule_Name = queryResult.Rule_Name;
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
        public Boolean getDelete(RuleConditionViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var RuleConditionField = db.MS_RuleCondition.Find(data.ruleCondition_Index);

                if (RuleConditionField != null)
                {
                    RuleConditionField.IsActive = 0;
                    RuleConditionField.IsDelete = 1;


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
                        olog.logging("DeleteRuleCondition", msglog);
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
        //public List<RuleConditionViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_RuleCondition.FromSql("sp_GetRuleCondition").ToList();

        //            var result = new List<RuleConditionViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new RuleConditionViewModel();

        //                resultItem.RuleIndex = item.Rule_Index;
        //                resultItem.RuleConditionIndex = item.RuleCondition_Index;
        //                resultItem.RuleConditionFieldIndex = item.RuleConditionField_Index;
        //                resultItem.RuleConditionOperationIndex = item.RuleConditionOperation_Index;
        //                resultItem.RuleConditionParam = item.RuleCondition_Param;
        //                resultItem.IsActive = item.IsActive;
        //                resultItem.IsDelete = item.IsDelete;
        //                resultItem.IsSystem = item.IsSystem;
        //                resultItem.StatusId = item.Status_Id;
        //                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                resultItem.CreateBy = item.Create_By;
        //                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                resultItem.UpdateBy = item.Update_By;
        //                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.CancelBy = item.Cancel_By;

        //                result.Add(resultItem);
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}

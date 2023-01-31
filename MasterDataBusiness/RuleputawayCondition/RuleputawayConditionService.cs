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
    public class RuleputawayConditionService
    {
        private MasterDataDbContext db;

        public RuleputawayConditionService()
        {
            db = new MasterDataDbContext();
        }

        public RuleputawayConditionService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterRuleputawayCondition
        public actionResultRuleputawayConditionViewModel filter(SearchRuleputawayConditionViewModel data)
        {
            try
            {
                var query = db.View_RuleputawayCondition.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.RuleputawayCondition_Id.Contains(data.key)
                                         || c.RuleputawayCondition_Name.Contains(data.key));
                }

                var Item = new List<View_RuleputawayCondition>();
                var TotalRow = new List<View_RuleputawayCondition>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.RuleputawayCondition_Id).ToList();

                var result = new List<SearchRuleputawayConditionViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchRuleputawayConditionViewModel();

                    resultItem.ruleputawayCondition_Index = item.RuleputawayCondition_Index;
                    resultItem.ruleputawayCondition_Id = item.RuleputawayCondition_Id;
                    resultItem.ruleputawayCondition_Name = item.RuleputawayCondition_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultRuleputawayConditionViewModel = new actionResultRuleputawayConditionViewModel();
                actionResultRuleputawayConditionViewModel.itemsRuleputawayCondition = result.ToList();
                actionResultRuleputawayConditionViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultRuleputawayConditionViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(RuleputawayConditionViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var RuleputawayConditionOld = db.MS_RuleputawayCondition.Find(data.ruleputawayCondition_Index);

                if (RuleputawayConditionOld == null)
                {
                    if (!string.IsNullOrEmpty(data.ruleputawayCondition_Id))
                    {
                        var query = db.MS_RuleputawayCondition.FirstOrDefault(c => c.RuleputawayCondition_Id == data.ruleputawayCondition_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.ruleputawayCondition_Id))
                    {
                        data.ruleputawayCondition_Id = "RuleputawayCondition_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_RuleputawayCondition.FirstOrDefault(c => c.RuleputawayCondition_Id == data.ruleputawayCondition_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.RuleputawayCondition_Id == data.ruleputawayCondition_Id)
                                {
                                    data.ruleputawayCondition_Id = "RuleputawayCondition_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_RuleputawayCondition Model = new MS_RuleputawayCondition();

                    Model.RuleputawayCondition_Index = Guid.NewGuid();
                    Model.RuleputawayCondition_Id = data.ruleputawayCondition_Id;
                    Model.RuleputawayCondition_Name = data.ruleputawayCondition_Name;
                    Model.RuleputawayConditionOperator = data.ruleputawayConditionOperator;
                    Model.RuleputawayCondition_Param = data.ruleputawayCondition_Param;
                    Model.RuleputawayConditionField_Index = data.ruleputawayConditionField_Index;
                    Model.Zoneputaway_Index = data.zoneputaway_Index;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_RuleputawayCondition.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.ruleputawayCondition_Id))
                    {
                        if (RuleputawayConditionOld.RuleputawayCondition_Id != "")
                        {
                            data.ruleputawayCondition_Id = RuleputawayConditionOld.RuleputawayCondition_Id;
                        }
                    }
                    else
                    {
                        if (RuleputawayConditionOld.RuleputawayCondition_Id != data.ruleputawayCondition_Id)
                        {
                            var query = db.MS_RuleputawayCondition.FirstOrDefault(c => c.RuleputawayCondition_Id == data.ruleputawayCondition_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.ruleputawayCondition_Id = RuleputawayConditionOld.RuleputawayCondition_Id;
                        }
                    }

                    RuleputawayConditionOld.RuleputawayCondition_Id = data.ruleputawayCondition_Id;
                    RuleputawayConditionOld.RuleputawayCondition_Name = data.ruleputawayCondition_Name;
                    RuleputawayConditionOld.RuleputawayConditionOperator = data.ruleputawayConditionOperator;
                    RuleputawayConditionOld.RuleputawayCondition_Param = data.ruleputawayCondition_Param;
                    RuleputawayConditionOld.RuleputawayConditionField_Index = data.ruleputawayConditionField_Index;
                    RuleputawayConditionOld.Zoneputaway_Index = data.zoneputaway_Index;
                    RuleputawayConditionOld.IsActive = Convert.ToInt32(data.isActive);
                    RuleputawayConditionOld.Update_By = data.create_By;
                    RuleputawayConditionOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveRuleputawayCondition", msglog);
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
        public RuleputawayConditionViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_RuleputawayCondition.Where(c => c.RuleputawayCondition_Index == id).FirstOrDefault();

                var result = new RuleputawayConditionViewModel();
                
                result.ruleputawayCondition_Index = queryResult.RuleputawayCondition_Index;
                result.ruleputawayCondition_Id = queryResult.RuleputawayCondition_Id;
                result.ruleputawayCondition_Name = queryResult.RuleputawayCondition_Name;
                result.ruleputawayConditionOperator = queryResult.RuleputawayConditionOperator;
                result.ruleputawayCondition_Param = queryResult.RuleputawayCondition_Param;
                result.ruleputawayConditionField_Index = queryResult.RuleputawayConditionField_Index;
                result.ruleputawayConditionField_Id = queryResult.RuleputawayConditionField_Id;
                result.ruleputawayConditionField_Name = queryResult.RuleputawayConditionField_Name;
                result.zoneputaway_Index = queryResult.Zoneputaway_Index;
                result.zoneputaway_Id = queryResult.Zoneputaway_Id;
                result.zoneputaway_Name = queryResult.Zoneputaway_Name;
                result.key = queryResult.RuleputawayConditionField_Id + " - " + queryResult.RuleputawayConditionField_Name;
                result.key2 = queryResult.Zoneputaway_Id + " - " + queryResult.Zoneputaway_Name;
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
        public Boolean getDelete(RuleputawayConditionViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var warehouse = db.MS_RuleputawayCondition.Find(data.ruleputawayCondition_Index);

                if (warehouse != null)
                {
                    warehouse.IsActive = 0;
                    warehouse.IsDelete = 1;


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
                        olog.logging("DeleteRuleputawayCondition", msglog);
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

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
    public class RuleputawayConditionFieldService
    {
        private MasterDataDbContext db;

        public RuleputawayConditionFieldService()
        {
            db = new MasterDataDbContext();
        }

        public RuleputawayConditionFieldService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterRuleputawayConditionField
        public actionResultRuleputawayConditionFieldViewModel filter(SearchRuleputawayConditionFieldViewModel data)
        {
            try
            {
                var query = db.MS_RuleputawayConditionField.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.RuleputawayConditionField_Id.Contains(data.key)
                                         || c.RuleputawayConditionField_Name.Contains(data.key));
                }

                var Item = new List<MS_RuleputawayConditionField>();
                var TotalRow = new List<MS_RuleputawayConditionField>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.RuleputawayConditionField_Id).ToList();

                var result = new List<SearchRuleputawayConditionFieldViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchRuleputawayConditionFieldViewModel();

                    resultItem.ruleputawayConditionField_Index = item.RuleputawayConditionField_Index;
                    resultItem.ruleputawayConditionField_Id = item.RuleputawayConditionField_Id;
                    resultItem.ruleputawayConditionField_Name = item.RuleputawayConditionField_Name;
                    resultItem.ruleputawayConditionField_Description = item.RuleputawayConditionField_Description;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultRuleputawayConditionFieldViewModel = new actionResultRuleputawayConditionFieldViewModel();
                actionResultRuleputawayConditionFieldViewModel.itemsRuleputawayConditionField = result.ToList();
                actionResultRuleputawayConditionFieldViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultRuleputawayConditionFieldViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(RuleputawayConditionFieldViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var RuleputawayConditionFieldOld = db.MS_RuleputawayConditionField.Find(data.ruleputawayConditionField_Index);

                if (RuleputawayConditionFieldOld == null)
                {
                    if (!string.IsNullOrEmpty(data.ruleputawayConditionField_Id))
                    {
                        var query = db.MS_RuleputawayConditionField.FirstOrDefault(c => c.RuleputawayConditionField_Id == data.ruleputawayConditionField_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.ruleputawayConditionField_Id))
                    {
                        data.ruleputawayConditionField_Id = "RuleputawayConditionField_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_RuleputawayConditionField.FirstOrDefault(c => c.RuleputawayConditionField_Id == data.ruleputawayConditionField_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.RuleputawayConditionField_Id == data.ruleputawayConditionField_Id)
                                {
                                    data.ruleputawayConditionField_Id = "RuleputawayConditionField_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_RuleputawayConditionField Model = new MS_RuleputawayConditionField();

                    Model.RuleputawayConditionField_Index = Guid.NewGuid();
                    Model.RuleputawayConditionField_Id = data.ruleputawayConditionField_Id;
                    Model.RuleputawayConditionField_Name = data.ruleputawayConditionField_Name;
                    Model.RuleputawayConditionField_Description = data.ruleputawayConditionField_Description;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_RuleputawayConditionField.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.ruleputawayConditionField_Id))
                    {
                        if (RuleputawayConditionFieldOld.RuleputawayConditionField_Id != "")
                        {
                            data.ruleputawayConditionField_Id = RuleputawayConditionFieldOld.RuleputawayConditionField_Id;
                        }
                    }
                    else
                    {
                        if (RuleputawayConditionFieldOld.RuleputawayConditionField_Id != data.ruleputawayConditionField_Id)
                        {
                            var query = db.MS_RuleputawayConditionField.FirstOrDefault(c => c.RuleputawayConditionField_Id == data.ruleputawayConditionField_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.ruleputawayConditionField_Id = RuleputawayConditionFieldOld.RuleputawayConditionField_Id;
                        }
                    }

                    RuleputawayConditionFieldOld.RuleputawayConditionField_Id = data.ruleputawayConditionField_Id;
                    RuleputawayConditionFieldOld.RuleputawayConditionField_Name = data.ruleputawayConditionField_Name;
                    RuleputawayConditionFieldOld.RuleputawayConditionField_Description = data.ruleputawayConditionField_Description;
                    RuleputawayConditionFieldOld.IsActive = Convert.ToInt32(data.isActive);
                    RuleputawayConditionFieldOld.Update_By = data.create_By;
                    RuleputawayConditionFieldOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveRuleputawayConditionField", msglog);
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
        public RuleputawayConditionFieldViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_RuleputawayConditionField.Where(c => c.RuleputawayConditionField_Index == id).FirstOrDefault();

                var result = new RuleputawayConditionFieldViewModel();


                result.ruleputawayConditionField_Index = queryResult.RuleputawayConditionField_Index;
                result.ruleputawayConditionField_Id = queryResult.RuleputawayConditionField_Id;
                result.ruleputawayConditionField_Name = queryResult.RuleputawayConditionField_Name;
                result.ruleputawayConditionField_Description = queryResult.RuleputawayConditionField_Description;
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
        public Boolean getDelete(RuleputawayConditionFieldViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var warehouse = db.MS_RuleputawayConditionField.Find(data.ruleputawayConditionField_Index);

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
                        olog.logging("DeleteRuleputawayConditionField", msglog);
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

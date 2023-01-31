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

namespace MasterDataBusiness
{
    public class RuleService
    {
        private MasterDataDbContext db;

        public RuleService()
        {
            db = new MasterDataDbContext();
        }

        public RuleService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterRule
        public actionResultRuleViewModel filter(SearchRuleViewModel data)
        {
            try
            {
                var query = db.View_Rule.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Rule_Name.Contains(data.key)
                      || c.Rule_Id.Contains(data.key)
                      || c.Process_Id.Contains(data.key)
                      || c.Process_Name.Contains(data.key));
                }

                var Item = new List<View_Rule>();
                var TotalRow = new List<View_Rule>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Rule_Name).ToList();

                var result = new List<SearchRuleViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchRuleViewModel();

                    resultItem.rule_Index = item.Rule_Index;
                    resultItem.rule_Id = item.Rule_Id;
                    resultItem.rule_Name = item.Rule_Name;
                    resultItem.process_Index = item.Process_Index;
                    resultItem.process_Id = item.Process_Id;
                    resultItem.process_Name = item.Process_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultRuleViewModel = new actionResultRuleViewModel();
                actionResultRuleViewModel.itemsRule = result.ToList();
                actionResultRuleViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultRuleViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
             
        #region SaveChanges
        public String SaveChanges(RuleViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var RuleOld = db.MS_Rule.Find(data.rule_Index);

                if (RuleOld == null)
                {
                    if (!string.IsNullOrEmpty(data.rule_Id))
                    {
                        var query = db.MS_Rule.FirstOrDefault(c => c.Rule_Id == data.rule_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.rule_Id))
                    {
                        data.rule_Id = "Rule_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_Rule.FirstOrDefault(c => c.Rule_Id == data.rule_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Rule_Id == data.rule_Id)
                                {
                                    data.rule_Id = "Rule_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_Rule Model = new MS_Rule();


                    Model.Rule_Index = Guid.NewGuid();
                    Model.Rule_Id = data.rule_Id;
                    Model.Rule_Name = data.rule_Name;
                    Model.Rule_Seq = 1;
                    Model.Process_Index = data.process_Index;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_Rule.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.rule_Id))
                    {
                        if (RuleOld.Rule_Id != "")
                        {
                            data.rule_Id = RuleOld.Rule_Id;
                        }
                    }
                    else
                    {
                        if (RuleOld.Rule_Id != data.rule_Id)
                        {
                            var query = db.MS_Rule.FirstOrDefault(c => c.Rule_Id == data.rule_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.rule_Id = RuleOld.Rule_Id;
                        }
                    }

                    RuleOld.Rule_Id = data.rule_Id;
                    RuleOld.Rule_Name = data.rule_Name;
                    RuleOld.Process_Index = data.process_Index;
                    RuleOld.IsActive = Convert.ToInt32(data.isActive);
                    RuleOld.Update_By = data.create_By;
                    RuleOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveRule", msglog);
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
        public RuleViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_Rule.Where(c => c.Rule_Index == id).FirstOrDefault();

                var result = new RuleViewModel();

                result.rule_Index = queryResult.Rule_Index;
                result.rule_Id = queryResult.Rule_Id;
                result.rule_Name = queryResult.Rule_Name;
                result.process_Index = queryResult.Process_Index;
                result.process_Id = queryResult.Process_Id;
                result.process_Name = queryResult.Process_Name;
                result.key = queryResult.Process_Id + " - " + queryResult.Process_Name;
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
        public Boolean getDelete(RuleViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Rule = db.MS_Rule.Find(data.rule_Index);

                if (Rule != null)
                {
                    Rule.IsActive = 0;
                    Rule.IsDelete = 1;


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
                        olog.logging("DeleteRule", msglog);
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



        //public List<RuleViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_Rule.FromSql("sp_GetRule").ToList();

        //            var result = new List<RuleViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new RuleViewModel();

        //                resultItem.RuleIndex = item.Rule_Index;
        //                resultItem.RuleId = item.Rule_Id;
        //                resultItem.RuleName= item.Rule_Name;
        //                resultItem.RuleSeq = item.Rule_Seq;
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

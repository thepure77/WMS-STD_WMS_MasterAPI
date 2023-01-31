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
    public class RuleputawayService
    {
        private MasterDataDbContext db;

        public RuleputawayService()
        {
            db = new MasterDataDbContext();
        }

        public RuleputawayService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterRuleputaway
        public actionResultRuleputawayViewModel filter(SearchRuleputawayViewModel data)
        {
            try
            {
                var query = db.MS_Ruleputaway.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Ruleputaway_Id.Contains(data.key)
                                         || c.Ruleputaway_Name.Contains(data.key));
                }

                var Item = new List<MS_Ruleputaway>();
                var TotalRow = new List<MS_Ruleputaway>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Ruleputaway_Id).ToList();

                var result = new List<SearchRuleputawayViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchRuleputawayViewModel();

                    resultItem.ruleputaway_Index = item.Ruleputaway_Index;
                    resultItem.ruleputaway_Id = item.Ruleputaway_Id;
                    resultItem.ruleputaway_Name = item.Ruleputaway_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultRuleputawayViewModel = new actionResultRuleputawayViewModel();
                actionResultRuleputawayViewModel.itemsRuleputaway = result.ToList();
                actionResultRuleputawayViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key =data.key };

                return actionResultRuleputawayViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(RuleputawayViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var RuleputawayOld = db.MS_Ruleputaway.Find(data.ruleputaway_Index);

                if (RuleputawayOld == null)
                {
                    if (!string.IsNullOrEmpty(data.ruleputaway_Id))
                    {
                        var query = db.MS_Ruleputaway.FirstOrDefault(c => c.Ruleputaway_Id == data.ruleputaway_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.ruleputaway_Id))
                    {
                        data.ruleputaway_Id = "Ruleputaway_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_Ruleputaway.FirstOrDefault(c => c.Ruleputaway_Id == data.ruleputaway_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Ruleputaway_Id == data.ruleputaway_Id)
                                {
                                    data.ruleputaway_Id = "Ruleputaway_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_Ruleputaway Model = new MS_Ruleputaway();

                    Model.Ruleputaway_Index = Guid.NewGuid();
                    Model.Ruleputaway_Id = data.ruleputaway_Id;
                    Model.Ruleputaway_Name = data.ruleputaway_Name;
                    Model.Ruleputaway_Seq = data.ruleputaway_Seq;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_Ruleputaway.Add(Model);

                    foreach (var item in data.listRuleputawayItemViewModel)
                    {
                        MS_RuleputawaySuggest resultItem = new MS_RuleputawaySuggest();

                        resultItem.RuleputawaySuggest_Index = Guid.NewGuid();
                        resultItem.Ruleputaway_Index = Model.Ruleputaway_Index;
                        resultItem.RuleputawaySuggest_Seq = item.ruleputawaySuggest_Seq;
                        resultItem.RuleputawayCondition_Index = item.ruleputawayCondition_Index;
                        resultItem.IsActive = 1;
                        resultItem.IsDelete = 0;
                        resultItem.IsSystem = 0;
                        resultItem.Status_Id = 0;
                        resultItem.Create_By = data.create_By;
                        resultItem.Create_Date = DateTime.Now;
                        db.MS_RuleputawaySuggest.Add(resultItem);

                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(data.ruleputaway_Id))
                    {
                        if (RuleputawayOld.Ruleputaway_Id != "")
                        {
                            data.ruleputaway_Id = RuleputawayOld.Ruleputaway_Id;
                        }
                    }
                    else
                    {
                        if (RuleputawayOld.Ruleputaway_Id != data.ruleputaway_Id)
                        {
                            var query = db.MS_Ruleputaway.FirstOrDefault(c => c.Ruleputaway_Id == data.ruleputaway_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.ruleputaway_Id = RuleputawayOld.Ruleputaway_Id;
                        }
                    }

                    RuleputawayOld.Ruleputaway_Id = data.ruleputaway_Id;
                    RuleputawayOld.Ruleputaway_Name = data.ruleputaway_Name;
                    RuleputawayOld.Ruleputaway_Seq = data.ruleputaway_Seq;
                    RuleputawayOld.IsActive = Convert.ToInt32(data.isActive);
                    RuleputawayOld.Update_By = data.create_By;
                    RuleputawayOld.Update_Date = DateTime.Now;

                    foreach (var item in data.listRuleputawayItemViewModel)
                    {
                        var RuleputawayItemOld = db.MS_RuleputawaySuggest.Find(item.ruleputawaySuggest_Index);
                        if (RuleputawayItemOld != null)
                        {
                            RuleputawayItemOld.RuleputawayCondition_Index = item.ruleputawayCondition_Index;
                            RuleputawayItemOld.RuleputawaySuggest_Seq = item.ruleputawaySuggest_Seq;
                            RuleputawayItemOld.Update_By = data.create_By;
                            RuleputawayItemOld.IsActive = Convert.ToInt32(item.isActive);
                            RuleputawayItemOld.IsDelete = Convert.ToInt32(item.isDelete);
                            RuleputawayItemOld.Update_Date = DateTime.Now;
                        }
                        else
                        {
                            MS_RuleputawaySuggest resultItem = new MS_RuleputawaySuggest();

                            resultItem.RuleputawaySuggest_Index = Guid.NewGuid();
                            resultItem.Ruleputaway_Index = RuleputawayOld.Ruleputaway_Index;
                            resultItem.RuleputawaySuggest_Seq = item.ruleputawaySuggest_Seq;
                            resultItem.RuleputawayCondition_Index = item.ruleputawayCondition_Index;
                            resultItem.IsActive = 1;
                            resultItem.IsDelete = 0;
                            resultItem.IsSystem = 0;
                            resultItem.Status_Id = 0;
                            resultItem.Create_By = data.create_By;
                            resultItem.Create_Date = DateTime.Now;
                            db.MS_RuleputawaySuggest.Add(resultItem);
                        }
                    }
                    var deleteItem = db.MS_RuleputawaySuggest.Where(c => !data.listRuleputawayItemViewModel.Select(s => s.ruleputawaySuggest_Index).Contains(c.RuleputawaySuggest_Index)
                                           && c.Ruleputaway_Index == RuleputawayOld.Ruleputaway_Index).ToList();

                    foreach (var c in deleteItem)
                    {
                        var deleteRuleputawaySuggest = db.MS_RuleputawaySuggest.Find(c.RuleputawaySuggest_Index);

                        deleteRuleputawaySuggest.IsActive = 0;
                        deleteRuleputawaySuggest.IsDelete = 1;
                        deleteRuleputawaySuggest.Update_By = data.update_By;
                        deleteRuleputawaySuggest.Update_Date = DateTime.Now;

                    }
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
                    olog.logging("SaveRuleputaway", msglog);
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
        public RuleputawayViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_Ruleputaway.Where(c => c.Ruleputaway_Index == id).FirstOrDefault();
               
                var result = new RuleputawayViewModel();
                

                result.ruleputaway_Index = queryResult.Ruleputaway_Index;
                result.ruleputaway_Id = queryResult.Ruleputaway_Id;
                result.ruleputaway_Name = queryResult.Ruleputaway_Name;
                result.ruleputaway_Seq = queryResult.Ruleputaway_Seq;
                result.isActive = queryResult.IsActive;
                                
                var queryResultItem = db.MS_RuleputawaySuggest.AsQueryable();
                queryResultItem = queryResultItem.Where(c => c.Ruleputaway_Index == id && c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                result.listRuleputawayItemViewModel = new List<RuleputawayItemViewModel>();

                foreach (var item in queryResultItem)
                {
                    var resultItem = new RuleputawayItemViewModel();

                    var queryRuleputawayCondition = db.MS_RuleputawayCondition.FirstOrDefault(c => c.RuleputawayCondition_Index == item.RuleputawayCondition_Index);
                    resultItem.ruleputawaySuggest_Index = item.RuleputawaySuggest_Index;
                    resultItem.ruleputaway_Index = item.Ruleputaway_Index;
                    resultItem.ruleputawaySuggest_Seq = item.RuleputawaySuggest_Seq;
                    resultItem.isActive = item.IsActive;
                    resultItem.ruleputawayCondition_Index = queryRuleputawayCondition.RuleputawayCondition_Index;
                    resultItem.ruleputawayCondition_Id = queryRuleputawayCondition.RuleputawayCondition_Id;
                    resultItem.ruleputawayCondition_Name = queryRuleputawayCondition.RuleputawayCondition_Name;
                    result.listRuleputawayItemViewModel.Add(resultItem);
                }
                       
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDelete
        public Boolean getDelete(RuleputawayViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var warehouse = db.MS_Ruleputaway.Find(data.ruleputaway_Index);

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
                        olog.logging("DeleteRuleputaway", msglog);
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


        #region GetSugesstionPutaway
        public List<View_SugesstionPutaway> GetSugesstionPutaway(RuleputawayViewModel data)
        {
            try
            {
                var query = db.View_SugesstionPutaway.OrderBy(o => o.Ruleputaway_Seq).ToList();

                return query;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetSugesstionPutaway
        public List<MS_Ruleputaway> GetRuleputaway(RuleputawayViewModel data)
        {
            try
            {
                var query = db.MS_Ruleputaway.Where(c => c.IsActive == 1 && c.IsDelete == 0).OrderBy(o => o.Ruleputaway_Seq).ToList();

                return query;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}

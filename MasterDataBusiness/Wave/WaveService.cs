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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MasterDataBusiness
{
    public class WaveService
    {
        private MasterDataDbContext db;

        public WaveService()
        {
            db = new MasterDataDbContext();
        }

        public WaveService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterWave
        public actionResultWaveViewModel filter(SearchWaveViewModel data)
        {
            try
            {
                var query = db.MS_Wave.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Wave_Name.Contains(data.key)
                      || c.Wave_Id.Contains(data.key));
                }

                var Item = new List<MS_Wave>();
                var TotalRow = new List<MS_Wave>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Wave_Name).ToList();

                var result = new List<SearchWaveViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchWaveViewModel();

                    resultItem.wave_Index = item.Wave_Index;
                    resultItem.wave_Id = item.Wave_Id;
                    resultItem.wave_Case = item.Wave_Case;
                    resultItem.wave_Name = item.Wave_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultWaveViewModel = new actionResultWaveViewModel();
                actionResultWaveViewModel.itemsWave = result.ToList();
                actionResultWaveViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultWaveViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(WaveViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var WaveOld = db.MS_Wave.Find(data.wave_Index);

                if (WaveOld == null)
                {
                    if (!string.IsNullOrEmpty(data.wave_Id))
                    {
                        var query = db.MS_Wave.FirstOrDefault(c => c.Wave_Id == data.wave_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.wave_Id))
                    {
                        data.wave_Id = "Wave_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_Wave.FirstOrDefault(c => c.Wave_Id == data.wave_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Wave_Id == data.wave_Id)
                                {
                                    data.wave_Id = "Wave_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_Wave Model = new MS_Wave();

                    Model.Wave_Index = Guid.NewGuid();
                    Model.Wave_Id = data.wave_Id;
                    Model.Wave_Name = data.wave_Name;
                    Model.Wave_Case = data.wave_Case;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_Wave.Add(Model);

                    foreach (var item in data.listWaveRuleItemViewModel)
                    {
                        ms_WaveRule resultItem = new ms_WaveRule();

                        resultItem.WaveRule_Index = Guid.NewGuid();
                        resultItem.WaveRule_Id = "WaveRule_Id".genAutonumber();
                        resultItem.WaveRule_Seq = Convert.ToInt32(item.waveRule_Seq);
                        resultItem.Wave_Index = Model.Wave_Index;
                        resultItem.Wave_Id = Model.Wave_Id;
                        resultItem.Wave_Name = Model.Wave_Name;
                        resultItem.Rule_Index = item.rule_Index;
                        resultItem.Rule_Id = item.rule_Id;
                        resultItem.Rule_Name = item.rule_Name;
                        resultItem.IsActive = 1;
                        resultItem.IsDelete = 0;
                        resultItem.IsSystem = 0;
                        resultItem.Status_Id = 0;
                        resultItem.Create_By = data.create_By;
                        resultItem.Create_Date = DateTime.Now;
                        db.MS_WaveRule.Add(resultItem);

                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(data.wave_Id))
                    {
                        if (WaveOld.Wave_Id != "")
                        {
                            data.wave_Id = WaveOld.Wave_Id;
                        }
                    }
                    else
                    {
                        if (WaveOld.Wave_Id != data.wave_Id)
                        {
                            var query = db.MS_Wave.FirstOrDefault(c => c.Wave_Id == data.wave_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.wave_Id = WaveOld.Wave_Id;
                        }
                    }
                    WaveOld.Wave_Id = data.wave_Id;
                    WaveOld.Wave_Name = data.wave_Name;
                    WaveOld.IsActive = Convert.ToInt32(data.isActive);
                    WaveOld.Update_By = data.create_By;
                    WaveOld.Update_Date = DateTime.Now;

                    foreach (var item in data.listWaveRuleItemViewModel)
                    {
                        var WaveRuleItemOld = db.MS_WaveRule.Find(item.waveRule_Index);
                        if (WaveRuleItemOld != null)
                        {
                            WaveRuleItemOld.WaveRule_Seq = Convert.ToInt32(item.waveRule_Seq);
                            WaveRuleItemOld.Rule_Index = item.rule_Index;
                            WaveRuleItemOld.Rule_Id = item.rule_Id;
                            WaveRuleItemOld.Rule_Name = item.rule_Name;
                            WaveRuleItemOld.Update_By = data.create_By;
                            WaveRuleItemOld.IsActive = Convert.ToInt32(item.isActive);
                            WaveRuleItemOld.IsDelete = Convert.ToInt32(item.isDelete);
                            WaveRuleItemOld.Update_Date = DateTime.Now;
                        }
                        else
                        {
                            ms_WaveRule resultItem = new ms_WaveRule();

                            resultItem.WaveRule_Index = Guid.NewGuid();
                            resultItem.WaveRule_Id = "WaveRule_Id".genAutonumber();
                            resultItem.WaveRule_Seq = Convert.ToInt32(item.waveRule_Seq);
                            resultItem.Wave_Index = data.wave_Index;
                            resultItem.Wave_Id = data.wave_Id;
                            resultItem.Wave_Name = data.wave_Name;
                            resultItem.Rule_Index = item.rule_Index;
                            resultItem.Rule_Id = item.rule_Id;
                            resultItem.Rule_Name = item.rule_Name;
                            resultItem.IsActive = 1;
                            resultItem.IsDelete = 0;
                            resultItem.IsSystem = 0;
                            resultItem.Status_Id = 0;
                            resultItem.Create_By = data.create_By;
                            resultItem.Create_Date = DateTime.Now;
                            db.MS_WaveRule.Add(resultItem);
                        }
                    }
                    var deleteItem = db.MS_WaveRule.Where(c => !data.listWaveRuleItemViewModel.Select(s => s.waveRule_Index).Contains(c.WaveRule_Index)
                                           && c.Wave_Index == WaveOld.Wave_Index).ToList();

                    foreach (var c in deleteItem)
                    {
                        var deleteWaveRule = db.MS_WaveRule.Find(c.WaveRule_Index);

                        deleteWaveRule.IsActive = 0;
                        deleteWaveRule.IsDelete = 1;
                        deleteWaveRule.Update_By = data.update_By;
                        deleteWaveRule.Update_Date = DateTime.Now;

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
                    olog.logging("SaveWave", msglog);
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
        public WaveViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_Wave.Where(c => c.Wave_Index == id).FirstOrDefault();

                var result = new WaveViewModel();

                result.wave_Index = queryResult.Wave_Index;
                result.wave_Id = queryResult.Wave_Id;
                result.wave_Name = queryResult.Wave_Name;
                result.wave_Case = queryResult.Wave_Case;
                result.isActive = queryResult.IsActive;

                var queryResultItem = db.MS_WaveRule.AsQueryable();
                queryResultItem = queryResultItem.Where(c => c.Wave_Index == id && c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                result.listWaveRuleItemViewModel = new List<WaveRuleItemViewModel>();

                foreach (var item in queryResultItem)
                {
                    var resultItem = new WaveRuleItemViewModel();

                    resultItem.waveRule_Index = item.WaveRule_Index;
                    resultItem.waveRule_Id = item.WaveRule_Id;
                    resultItem.waveRule_Seq = item.WaveRule_Seq.ToString();
                    resultItem.wave_Index = item.Wave_Index;
                    resultItem.wave_Id = item.Wave_Id;
                    resultItem.wave_Name = item.Wave_Name;
                    resultItem.rule_Index = item.Rule_Index;
                    resultItem.rule_Id = item.Rule_Id;
                    resultItem.rule_Name = item.Rule_Name;
                    resultItem.isActive = item.IsActive;
                    result.listWaveRuleItemViewModel.Add(resultItem);
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
        public Boolean getDelete(WaveViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Wave = db.MS_Wave.Find(data.wave_Index);

                if (Wave != null)
                {
                    Wave.IsActive = 0;
                    Wave.IsDelete = 1;


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

        public List<WaveViewModel> waveFilter(WaveViewModel model)
        {
            try
            {
                var items = new List<WaveViewModel>();
                var result = db.MS_Wave.ToList();

                foreach (var item in result)
                {
                    var resultItem = new WaveViewModel
                    {
                        wave_Index = item.Wave_Index,
                        wave_Id = item.Wave_Id,
                        wave_Name = item.Wave_Name,
                        wave_Case = item.Wave_Case,
                        isActive = item.IsActive,
                        isDelete = item.IsDelete
                    };

                    items.Add(resultItem);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WaveRuleViewModel> GetWaveRule(WaveRuleFilterViewModel model)
        {
            try
            {
                var items = new List<WaveRuleViewModel>();
                var query = db.View_WaveRule.AsQueryable();

                if (!string.IsNullOrEmpty(model.process_Index))
                {
                    query = query.Where(c => c.Process_Index == Guid.Parse(model.process_Index));
                }

                if (!string.IsNullOrEmpty(model.wave_Index))
                {
                    query = query.Where(c => c.Wave_Index == Guid.Parse(model.wave_Index));
                }

                var resulte = query.ToList();

                foreach (var item in resulte)
                {
                    var i = new WaveRuleViewModel();
                    i.waveRule_Index = item.WaveRule_Index.ToString();
                    i.waveRule_Id = item.WaveRule_Id;
                    i.waveRule_Seq = item.WaveRule_Seq;
                    i.wave_Index = item.Wave_Index.ToString();
                    i.wave_Id = item.Wave_Id;
                    i.wave_Name = item.Wave_Name;
                    i.rule_Index = item.Rule_Index.ToString();
                    i.rule_Id = item.Rule_Id;
                    i.rule_Name = item.Rule_Name;
                    i.isActive = item.IsActive;
                    i.isDelete = item.IsDelete;
                    i.isSystem = item.IsSystem;
                    i.status_Id = item.Status_Id;
                    i.create_By = item.Create_By;
                    i.create_Date = item.Create_Date.ToString();
                    i.update_By = item.Update_By;
                    i.update_Date = item.Update_Date.ToString();
                    i.cancel_By = item.Cancel_By;
                    i.cancel_Date = item.Cancel_Date.ToString();
                    i.process_Index = item.Process_Index.ToString();

                    items.Add(i);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WaveTemplateViewModel> GetViewWaveTemplate(WaveTemplateFilterViewModel model)
        {
            try
            {
                var items = new List<WaveTemplateViewModel>();
                var query = db.View_WaveTemplate.AsQueryable();

                if (!string.IsNullOrEmpty(model.process_Index))
                {
                    query = query.Where(c => c.Process_Index == Guid.Parse(model.process_Index));
                }

                if (!string.IsNullOrEmpty(model.wave_Index))
                {
                    query = query.Where(c => c.Wave_Index == Guid.Parse(model.wave_Index));
                }

                if (!string.IsNullOrEmpty(model.rule_Index))
                {
                    query = query.Where(c => c.Rule_Index == Guid.Parse(model.rule_Index));
                }

                var resulte = query.ToList();

                foreach (var item in resulte)
                {
                    var i = new WaveTemplateViewModel();

                    i.wave_Index = item.Wave_Index.ToString();
                    i.wave_Id = item.Wave_Id;
                    i.wave_Name = item.Wave_Name;
                    i.waveRule_Id = item.WaveRule_Id;
                    i.waveRule_Seq = item.WaveRule_Seq;
                    i.waveRule_Index = item.WaveRule_Index.ToString();
                    i.process_Index = item.Process_Index.ToString();
                    i.process_Id = item.Process_Id;
                    i.process_Name = item.Process_Name;
                    i.rule_Index = item.Rule_Index.ToString();
                    i.rule_Id = item.Rule_Id;
                    i.rule_Name = item.Rule_Name;
                    i.rule_Seq = item.Rule_Seq;
                    i.ruleConditionField_Index = item.RuleConditionField_Index.ToString();
                    i.ruleConditionField_Name = item.RuleConditionField_Name;
                    i.ruleConditionOperation_Index = item.RuleConditionOperation_Index.ToString();
                    i.ruleConditionOperationType = item.RuleConditionOperationType;
                    i.ruleConditionOperation = item.RuleConditionOperation;
                    i.ruleCondition_Index = item.RuleCondition_Index.ToString();
                    i.ruleCondition_Param = item.RuleCondition_Param;
                    i.ruleCondition_Seq = item.RuleCondition_Seq;
                    i.isSearch = item.IsSearch;
                    i.isSort = item.IsSort;
                    i.isSource = item.IsSource;
                    i.isDestination = item.IsDestination;
                    i.rowIndex = item.RowIndex;

                    items.Add(i);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

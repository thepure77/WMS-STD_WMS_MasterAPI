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
    public class RuleConditionFieldService
    {
        private MasterDataDbContext db;

        public RuleConditionFieldService()
        {
            db = new MasterDataDbContext();
        }

        public RuleConditionFieldService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterRuleConditionField
        public actionResultRuleConditionFieldViewModel filter(SearchRuleConditionFieldViewModel data)
        {
            try
            {
                var query = db.View_RuleConditionField.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.RuleConditionField_Name.Contains(data.key)
                      || c.Process_Id.Contains(data.key)
                      || c.Process_Name.Contains(data.key));
                }

                var Item = new List<View_RuleConditionField>();
                var TotalRow = new List<View_RuleConditionField>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.RuleConditionField_Name).ToList();

                var result = new List<SearchRuleConditionFieldViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchRuleConditionFieldViewModel();

                    resultItem.ruleConditionField_Index = item.RuleConditionField_Index;
                    resultItem.ruleConditionField_Name = item.RuleConditionField_Name;
                    resultItem.process_Index = item.Process_Index;
                    resultItem.process_Id = item.Process_Id;
                    resultItem.process_Name = item.Process_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultRuleConditionFieldViewModel = new actionResultRuleConditionFieldViewModel();
                actionResultRuleConditionFieldViewModel.itemsRuleConditionField = result.ToList();
                actionResultRuleConditionFieldViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultRuleConditionFieldViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(RuleConditionFieldViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var RuleConditionFieldOld = db.sy_RuleConditionField.Find(data.ruleConditionField_Index);

                if (RuleConditionFieldOld == null)
                {

                    sy_RuleConditionField Model = new sy_RuleConditionField();


                    Model.RuleConditionField_Index = Guid.NewGuid();
                    Model.RuleConditionField_Name = data.ruleConditionField_Name;
                    Model.Process_Index = data.process_Index;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsSort = Convert.ToInt32(data.isSort);
                    Model.IsSearch = Convert.ToInt32(data.isSearch);
                    Model.IsDestination = Convert.ToInt32(data.isDestination);
                    Model.IsSource = Convert.ToInt32(data.isSource);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.sy_RuleConditionField.Add(Model);
                }
                else
                {
                    RuleConditionFieldOld.RuleConditionField_Name = data.ruleConditionField_Name;
                    RuleConditionFieldOld.Process_Index = data.process_Index;
                    RuleConditionFieldOld.IsActive = Convert.ToInt32(data.isActive);
                    RuleConditionFieldOld.IsSort = Convert.ToInt32(data.isSort);
                    RuleConditionFieldOld.IsSearch = Convert.ToInt32(data.isSearch);
                    RuleConditionFieldOld.IsDestination = Convert.ToInt32(data.isDestination);
                    RuleConditionFieldOld.IsSource = Convert.ToInt32(data.isSource);
                    RuleConditionFieldOld.Update_By = data.create_By;
                    RuleConditionFieldOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveRuleConditionField", msglog);
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
        public RuleConditionFieldViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_RuleConditionField.Where(c => c.RuleConditionField_Index == id).FirstOrDefault();

                var result = new RuleConditionFieldViewModel();

                result.ruleConditionField_Index = queryResult.RuleConditionField_Index;
                result.ruleConditionField_Name = queryResult.RuleConditionField_Name;
                result.process_Index = queryResult.Process_Index;
                result.process_Id = queryResult.Process_Id;
                result.process_Name = queryResult.Process_Name;
                result.key = queryResult.Process_Id + " - " + queryResult.Process_Name;
                result.isSort = queryResult.IsSort;
                result.isSearch = queryResult.IsSearch;
                result.isDestination = queryResult.IsDestination;
                result.isSource = queryResult.IsSource;
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
        public Boolean getDelete(RuleConditionFieldViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var RuleConditionField = db.sy_RuleConditionField.Find(data.ruleConditionField_Index);

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
                        olog.logging("DeleteRuleConditionField", msglog);
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

        //public List<RuleConditionFieldViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.sy_RuleConditionField.FromSql("sp_GetRuleConditionField").ToList();

        //            var result = new List<RuleConditionFieldViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new RuleConditionFieldViewModel();

        //                resultItem.RuleConditionFieldIndex = item.RuleConditionField_Index;
        //                resultItem.ProcessIndex = item.Process_Index;
        //                resultItem.RuleConditionFieldName = item.RuleConditionField_Name;

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

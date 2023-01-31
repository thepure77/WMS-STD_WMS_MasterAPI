using Comone.Utils;
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
    public class RuleConditionOperationService
    {
        #region FindRuleConditionOperation
        public RuleConditionOperationViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_RuleConditionOperation.Where(c => c.RuleConditionOperation_Index == id).FirstOrDefault();

                var result = new RuleConditionOperationViewModel();


                result.ruleConditionOperation_Index = queryResult.RuleConditionOperation_Index;
                result.ruleConditionOperationType = queryResult.RuleConditionOperationType;
                result.ruleConditionOperation = queryResult.RuleConditionOperation;
                result.ruleConditionField_Index = queryResult.RuleConditionField_Index;
                result.ruleConditionField_Name = queryResult.RuleConditionField_Name;
                result.isActive = queryResult.IsActive;
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FilterRuleConditionOperation
        //Filter
        private MasterDataDbContext db;

        public RuleConditionOperationService()
        {
            db = new MasterDataDbContext();
        }

        public RuleConditionOperationService(MasterDataDbContext db)
        {
            this.db = db;
        }


        public actionResultRuleConditionOperationViewModel filter(SearchRuleConditionOperationViewModel data)
        {
            try
            {
                var query = db.View_RuleConditionOperation.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.RuleConditionOperationType.Contains(data.key)
                                        || c.RuleConditionOperation.Contains(data.key)
                                        || c.RuleConditionField_Name.Contains(data.key));


                }

                var Item = new List<View_RuleConditionOperation>();
                var TotalRow = new List<View_RuleConditionOperation>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.RuleConditionOperation_Index).ToList();

                var result = new List<SearchRuleConditionOperationViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchRuleConditionOperationViewModel();

                    resultItem.ruleConditionOperation_Index = item.RuleConditionOperation_Index;
                    resultItem.ruleConditionOperationType = item.RuleConditionOperationType;
                    resultItem.ruleConditionOperation = item.RuleConditionOperation;
                    resultItem.ruleConditionField_Index = item.RuleConditionField_Index;
                    resultItem.ruleConditionField_Name = item.RuleConditionField_Name;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultRuleConditionOperationViewModel = new actionResultRuleConditionOperationViewModel();
                actionResultRuleConditionOperationViewModel.itemsRuleConditionOperation = result.ToList();
                actionResultRuleConditionOperationViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultRuleConditionOperationViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDeleteRuleConditionOperation

        public Boolean getDelete(RuleConditionOperationViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var ruleConditionOperation = db.sy_RuleConditionOperation.Find(data.ruleConditionOperation_Index);

                if (ruleConditionOperation != null)
                {
                    ruleConditionOperation.IsActive = 0;
                    ruleConditionOperation.IsDelete = 1;


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
                        olog.logging("DeleteRuleConditionOperation", msglog);
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

        #region GetAutoNumberRuleConditionOperation

        public string genAutonumber(String Sys_Key)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            String result = "";
            try
            {
                sy_AutoNumber model = new sy_AutoNumber();

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                var query = db.sy_AutoNumber.Where(c => c.Sys_Key == Sys_Key).FirstOrDefault();
                if (query != null)
                {
                    var item = db.sy_AutoNumber.Find(query.Sys_Key);
                    item.Sys_Value = query.Sys_Value + 1;
                    item.Update_Date = DateTime.Now;
                    result = item.Sys_Value.ToString();

                }
                else
                {

                    model.Sys_Key = Sys_Key;
                    model.Sys_Text = "";
                    model.Sys_Value = 1;
                    model.IsActive = 1;
                    model.IsDelete = 0;
                    model.IsSystem = 1;
                    model.Status_Id = 0;
                    model.Create_By = "System";
                    model.Create_Date = DateTime.Now;
                    result = model.Sys_Value.ToString();
                    db.sy_AutoNumber.Add(model);
                }

                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SaveRuleConditionOperation", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region SaveChanges 
        public String SaveChanges(RuleConditionOperationViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var RuleConditionOperationOld = db.sy_RuleConditionOperation.Find(data.ruleConditionOperation_Index);

                if (RuleConditionOperationOld == null)
                {





                    sy_RuleConditionOperation Model = new sy_RuleConditionOperation();

                    Model.RuleConditionOperation_Index = Guid.NewGuid();
                    Model.RuleConditionOperationType = data.ruleConditionOperationType;
                    Model.RuleConditionOperation = data.ruleConditionOperation;
                    Model.RuleConditionField_Index = data.ruleConditionField_Index.sParse<Guid>();
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 1;
                    Model.Status_Id = 1;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.sy_RuleConditionOperation.Add(Model);
                }
                else
                {
                    RuleConditionOperationOld.RuleConditionOperation_Index = data.ruleConditionOperation_Index.sParse<Guid>();
                    RuleConditionOperationOld.RuleConditionOperationType = data.ruleConditionOperationType;
                    RuleConditionOperationOld.RuleConditionOperation = data.ruleConditionOperation;
                    RuleConditionOperationOld.RuleConditionField_Index = data.ruleConditionField_Index.sParse<Guid>();
                    RuleConditionOperationOld.IsActive = Convert.ToInt32(data.isActive);
                    RuleConditionOperationOld.Update_By = data.update_By;
                    RuleConditionOperationOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveRuleConditionOperation", msglog);
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

        #region SearchRuleConditionOperationFilter
        public List<ItemListViewModel> autoRuleConditionOperationFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_RuleConditionOperation.Where(c => c.RuleConditionOperationType.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleConditionOperationType,
                        key = s.RuleConditionOperationType
                    }).Distinct();

                    var query2 = db.View_RuleConditionOperation.Where(c => c.RuleConditionOperation.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleConditionOperation,
                        key = s.RuleConditionOperation
                    }).Distinct();

                    var query3 = db.View_RuleConditionOperation.Where(c => c.RuleConditionField_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.RuleConditionField_Name,
                        key = s.RuleConditionField_Name
                    }).Distinct();
                    var query = query1.Union(query2).Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

        #region FilterRuleConditionFieldPopup
        //Filter
        public actionResultRuleConditionOperationViewModel filterpopup(SearchRuleConditionOperationViewModel data)
        {
            try
            {
                var query = db.View_RuleConditionOperation.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c =>  c.RuleConditionField_Name.Contains(data.key));


                }

                var Item = new List<View_RuleConditionOperation>();
                var TotalRow = new List<View_RuleConditionOperation>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.RuleConditionField_Index).ToList();

                var result = new List<SearchRuleConditionOperationViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchRuleConditionOperationViewModel();
                    resultItem.ruleConditionField_Index = item.RuleConditionField_Index;
                    resultItem.ruleConditionField_Name = item.RuleConditionField_Name;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultRuleConditionOperationViewModel = new actionResultRuleConditionOperationViewModel();
                actionResultRuleConditionOperationViewModel.itemsRuleConditionOperation = result.ToList();
                actionResultRuleConditionOperationViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultRuleConditionOperationViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region filterRuleConditionFieldPopup
        public List<RuleConditionOperationViewModel> filterruleconditionoperationpopup(RuleConditionOperationViewModel data)
        {
            try
            {
                var query = db.View_RuleConditionOperation.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!string.IsNullOrEmpty(data.ruleConditionField_Name))
                {
                    query = query.Where(c => c.RuleConditionField_Name.Contains(data.ruleConditionField_Name));

                }
                else if (!string.IsNullOrEmpty(data.ruleConditionOperation))
                {
                    query = query.Where(c => c.RuleConditionOperation.Contains(data.ruleConditionOperation));

                }
                else if (!string.IsNullOrEmpty(data.ruleConditionOperationType))
                {
                    query = query.Where(c => c.RuleConditionOperationType.Contains(data.ruleConditionOperationType));

                }

                var result = new List<RuleConditionOperationViewModel>();

                foreach (var item in query)
                {
                    var resultItem = new RuleConditionOperationViewModel();

                    resultItem.ruleConditionOperation_Index = item.RuleConditionOperation_Index;
                    resultItem.ruleConditionOperation = item.RuleConditionOperation;
                    resultItem.ruleConditionOperationType = item.RuleConditionOperationType;
                    resultItem.ruleConditionField_Index = item.RuleConditionField_Index;
                    resultItem.ruleConditionField_Name = item.RuleConditionField_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //#region autoShipToSearch
        //public List<ItemListViewModel> autoSearchShipTo(ItemListViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var query = context.MS_ShipTo.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

        //            if (data.key == "-")
        //            {

        //            }

        //            else if (!string.IsNullOrEmpty(data.key))
        //            {
        //                query = query.Where(c => c.ShipTo_Id.Contains(data.key)
        //                                        || c.ShipTo_Name.Contains(data.key));
        //            }

        //            var items = new List<ItemListViewModel>();
        //            var result = query.Select(c => new { c.ShipTo_Name, c.ShipTo_Index, c.ShipTo_Id }).Distinct().Take(10).ToList();
        //            foreach (var item in result)
        //            {
        //                var resultItem = new ItemListViewModel
        //                {
        //                    //index = new Guid(item.User_Name),
        //                    index = item.ShipTo_Index,
        //                    id = item.ShipTo_Id,
        //                    name = item.ShipTo_Id + " - " + item.ShipTo_Name,
        //                    key = item.ShipTo_Id + " - " + item.ShipTo_Name,
        //                };

        //                items.Add(resultItem);
        //            }
        //            return items;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion
    }
}

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
    public class RuleConditionFieldPopupService
    {
        private MasterDataDbContext db;

        public RuleConditionFieldPopupService()
        {
            db = new MasterDataDbContext();
        }

        public RuleConditionFieldPopupService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterRuleConditionField
        public List<RuleConditionFieldPopupViewModel> filter(RuleConditionFieldPopupViewModel data)
        {
            try
            {
                var query = db.sy_RuleConditionField.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

            if (!string.IsNullOrEmpty(data.ruleConditionField_Name))
                {
                    query = query.Where(c => c.RuleConditionField_Name.Contains(data.ruleConditionField_Name));

                }

                var result = new List<RuleConditionFieldPopupViewModel>();

                foreach (var item in query)
                {
                    var resultItem = new RuleConditionFieldPopupViewModel();

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

        #region filterRuleConditionFieldPopup
        public List<RuleConditionFieldPopupViewModel> filterruleconditionfield(RuleConditionFieldPopupViewModel data)
        {
            try
            {
                var query = db.View_RuleConditionField.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!string.IsNullOrEmpty(data.ruleConditionField_Name))
                {
                    query = query.Where(c => c.RuleConditionField_Name.Contains(data.ruleConditionField_Name));

                }
                else if (!string.IsNullOrEmpty(data.process_Name))
                {
                    query = query.Where(c => c.Process_Name.Contains(data.process_Name));

                }

                var result = new List<RuleConditionFieldPopupViewModel>();

                foreach (var item in query)
                {
                    var resultItem = new RuleConditionFieldPopupViewModel();

                    resultItem.ruleConditionField_Index = item.RuleConditionField_Index;
                    resultItem.ruleConditionField_Name = item.RuleConditionField_Name;
                    resultItem.process_Index = item.Process_Index;
                    resultItem.process_Name = item.Process_Name;
                    resultItem.isSearch = item.IsSearch;
                    resultItem.isSort = item.IsSort;
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

        //public List<RuleConditionFieldPopupViewModel> search(RuleConditionFieldPopupViewModel data)
        //{
        //    try
        //    {
        //        string pwhere = "";
        //        string pwhereLike = "";
        //        var result = new List<RuleConditionFieldPopupViewModel>();
        //        var queryResult = db.sy_RuleConditionField.Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
        //        if (data.ruleConditionField_Name != "" && data.ruleConditionField_Name != null)
        //        {
        //            pwhere += " And ruleConditionField_Name like N'%" + data.ruleConditionField_Name + "%'";
        //        }
        //        else
        //        {
        //            pwhere += " ";
        //        }
        //        if (data.ruleConditionField_Name != "" && data.ruleConditionField_Name != null)
        //        {
        //            pwhere += " And isActive = '" + 1 + "'";
        //            pwhere += " And isDelete = '" + 0 + "'";

        //            var query = db.sy_RuleConditionField.ToList();
        //            foreach (var item in query)
        //            {
        //                var resultItem = new RuleConditionFieldPopupViewModel();
        //                resultItem.ruleConditionField_Index = item.RuleConditionField_Index;
        //                resultItem.ruleConditionField_Name = item.RuleConditionField_Name;
        //                resultItem.isActive = item.IsActive;
        //                resultItem.isDelete = item.IsDelete;
        //                resultItem.isSystem = item.IsSystem;
        //                resultItem.status_Id = item.Status_Id;
        //                resultItem.create_Date = item.Create_Date.GetValueOrDefault();
        //                resultItem.create_By = item.Create_By;
        //                resultItem.update_Date = item.Update_Date.GetValueOrDefault();
        //                resultItem.update_By = item.Update_By;
        //                resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.cancel_By = item.Cancel_By;
        //                result.Add(resultItem);
        //            }

        //        }

        //        //if (data.process_Id == "" && data.process_Name == "")
        //        //{
        //        //    result = this.filter();
        //        //}

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}

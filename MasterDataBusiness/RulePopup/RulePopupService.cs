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
    public class RulePopupService
    {
        private MasterDataDbContext db;

        public RulePopupService()
        {
            db = new MasterDataDbContext();
        }

        public RulePopupService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterRulePopup
        public List<RulePopupViewModel> filter(RulePopupViewModel data)
        {
            try
            {
                var query = db.MS_Rule.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                if (!string.IsNullOrEmpty(data.rule_Id))
                {
                    query = query.Where(c => c.Rule_Id.Contains(data.rule_Id));

                }
                 else if (!string.IsNullOrEmpty(data.rule_Name))
                {
                    query = query.Where(c => c.Rule_Name.Contains(data.rule_Name));

                }
                var result = new List<RulePopupViewModel>();

                foreach (var item in query)
                {
                    var resultItem = new RulePopupViewModel();

                    resultItem.rule_Id = item.Rule_Id;
                    resultItem.rule_Index = item.Rule_Index;
                    resultItem.rule_Name = item.Rule_Name;
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

        //public List<RuleConditionFieldV2ViewModel> search(RuleConditionFieldV2ViewModel data)
        //{
        //    try
        //    {
        //        string pwhere = "";
        //        string pwhereLike = "";
        //        var result = new List<RuleConditionFieldV2ViewModel>();
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
        //                var resultItem = new RuleConditionFieldV2ViewModel();
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

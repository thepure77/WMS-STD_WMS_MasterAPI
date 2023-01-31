using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{
    public class RuleputawayConditionFieldPopupService
    {
        private MasterDataDbContext db;

        public RuleputawayConditionFieldPopupService()
        {
            db = new MasterDataDbContext();
        }

        public RuleputawayConditionFieldPopupService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterRuleputawayConditionFieldPopup
        public List<RuleputawayConditionFieldViewModel> filter(RuleputawayConditionFieldViewModel data)
        {
            try
            {
                var query = db.MS_RuleputawayConditionField.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                
                if (!string.IsNullOrEmpty(data.ruleputawayConditionField_Id))
                {
                    query = query.Where(c => c.RuleputawayConditionField_Id.Contains(data.ruleputawayConditionField_Id));

                }
                else if (!string.IsNullOrEmpty(data.ruleputawayConditionField_Name))
                {
                    query = query.Where(c => c.RuleputawayConditionField_Name.Contains(data.ruleputawayConditionField_Name));

                }

                var result = new List<RuleputawayConditionFieldViewModel>();

                foreach (var item in query)
                {
                    var resultItem = new RuleputawayConditionFieldViewModel();

                    resultItem.ruleputawayConditionField_Index = item.RuleputawayConditionField_Index;
                    resultItem.ruleputawayConditionField_Id = item.RuleputawayConditionField_Id;
                    resultItem.ruleputawayConditionField_Name = item.RuleputawayConditionField_Name;
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

      
    }
}

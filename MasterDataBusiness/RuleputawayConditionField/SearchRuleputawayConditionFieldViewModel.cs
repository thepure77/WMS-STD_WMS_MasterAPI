using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchRuleputawayConditionFieldViewModel : Pagination
    {
        public Guid ruleputawayConditionField_Index { get; set; }

        public string ruleputawayConditionField_Id { get; set; }

        public string ruleputawayConditionField_Name { get; set; }

        public string ruleputawayConditionField_Description { get; set; }

        public int? isActive { get; set; }
        public int? isDelete { get; set; }

        public string key { get; set; }
    }
    public class actionResultRuleputawayConditionFieldViewModel
    {
        public IList<SearchRuleputawayConditionFieldViewModel> itemsRuleputawayConditionField { get; set; }
        public Pagination pagination { get; set; }
    }
}

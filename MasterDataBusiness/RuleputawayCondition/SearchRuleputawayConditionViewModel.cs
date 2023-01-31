using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchRuleputawayConditionViewModel : Pagination
    {
        public Guid ruleputawayCondition_Index { get; set; }
        public string ruleputawayCondition_Id { get; set; }
        public string ruleputawayCondition_Name { get; set; }
        public string ruleputawayConditionOperator { get; set; }
        public string ruleputawayCondition_Param { get; set; }
        public Guid? ruleputawayConditionField_Index { get; set; }
        public string ruleputawayConditionField_Id { get; set; }
        public string ruleputawayConditionField_Name { get; set; }
        public Guid? zoneputaway_Index { get; set; }
        public string zoneputaway_Id { get; set; }
        public string zoneputaway_Name { get; set; }
        public int? isActive { get; set; }
        public int? isDelete { get; set; }

        public string key { get; set; }
        public string key2 { get; set; }
    }
    public class actionResultRuleputawayConditionViewModel
    {
        public IList<SearchRuleputawayConditionViewModel> itemsRuleputawayCondition { get; set; }
        public Pagination pagination { get; set; }
    }
}

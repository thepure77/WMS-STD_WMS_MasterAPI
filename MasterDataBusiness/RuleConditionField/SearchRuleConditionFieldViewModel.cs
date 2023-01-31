using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchRuleConditionFieldViewModel : Pagination
    {
        public Guid ruleConditionField_Index { get; set; }

        public string ruleConditionField_Name { get; set; }

        public Guid? process_Index { get; set; }

        public string process_Id { get; set; }

        public string process_Name { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? isSearch { get; set; }

        public int? isSort { get; set; }

        public int? isDestination { get; set; }

        public int? isSource { get; set; }

        public int? status_Id { get; set; }

        public string create_By { get; set; }

        public DateTime? create_Date { get; set; }

        public string update_By { get; set; }

        public DateTime? update_Date { get; set; }

        public string cancel_By { get; set; }

        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }
    }
    public class actionResultRuleConditionFieldViewModel
    {
        public IList<SearchRuleConditionFieldViewModel> itemsRuleConditionField { get; set; }
        public Pagination pagination { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class RuleputawayItemViewModel
    {
        public Guid ruleputawaySuggest_Index { get; set; }

        public Guid? ruleputaway_Index { get; set; }

        public Guid ruleputawayCondition_Index { get; set; }

        public string ruleputawayCondition_Id{ get; set; }

        public string ruleputawayCondition_Name { get; set; }

        public int? ruleputawaySuggest_Seq { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

        public string create_By { get; set; }

        public DateTime? create_Date { get; set; }

        public string update_By { get; set; }

        public DateTime? update_Date { get; set; }

        public string cancel_By { get; set; }

        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }

    }
}

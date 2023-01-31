using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class RuleConditionViewModel
    {
        public Guid ruleCondition_Index { get; set; }

        public Guid rule_Index { get; set; }

        public string rule_Id { get; set; }

        public string rule_Name { get; set; }

        public Guid ruleConditionField_Index { get; set; }

        public string ruleConditionField_Name { get; set; }

        public Guid ruleConditionOperation_Index { get; set; }

        public string ruleConditionOperationType { get; set; }

        public string ruleConditionOperation { get; set; }

        [StringLength(200)]
        public string ruleCondition_Param { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

        [StringLength(200)]
        public string create_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime create_Date { get; set; }

        [StringLength(200)]
        public string update_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? update_Date { get; set; }

        [StringLength(200)]
        public string cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? cancel_Date { get; set; }
    }
}

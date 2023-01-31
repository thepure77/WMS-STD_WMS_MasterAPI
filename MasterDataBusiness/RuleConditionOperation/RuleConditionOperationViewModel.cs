using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class RuleConditionOperationViewModel
    {
        public Guid? ruleConditionOperation_Index { get; set; }
        public string ruleConditionOperationType { get; set; }
        public string ruleConditionOperation { get; set; }

        public Guid? ruleConditionField_Index { get; set; }
        public string ruleConditionField_Name { get; set; }


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
    }
}

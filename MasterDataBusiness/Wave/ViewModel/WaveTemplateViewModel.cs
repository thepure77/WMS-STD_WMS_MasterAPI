using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{
    public  class WaveTemplateViewModel
    {
        public string wave_Index { get; set; }
        public string wave_Id { get; set; }

        public string wave_Name { get; set; }

        public string waveRule_Id { get; set; }

        public int? waveRule_Seq { get; set; }

        public string waveRule_Index { get; set; }


        public string process_Index { get; set; }

        public string process_Id { get; set; }

        public string process_Name { get; set; }


        public string rule_Index { get; set; }

        public string rule_Id { get; set; }

        public string rule_Name { get; set; }

        public int? rule_Seq { get; set; }


        public string ruleConditionField_Index { get; set; }

        public string ruleConditionField_Name { get; set; }


        public string ruleConditionOperation_Index { get; set; }

        public string ruleConditionOperationType { get; set; }

        public string ruleConditionOperation { get; set; }

        public string ruleCondition_Index { get; set; }

        public string ruleCondition_Param { get; set; }

        public int? ruleCondition_Seq { get; set; }

        public int? isSearch { get; set; }

        public int? isSort { get; set; }

        public int? isSource { get; set; }

        public int? isDestination { get; set; }

        public long? rowIndex { get; set; }
    }

    public class WaveTemplateFilterViewModel
    {
        public string waveRule_Index { get; set; }
        public string waveRule_Id { get; set; }
        public int? waveRule_Seq { get; set; }
        public string wave_Index { get; set; }
        public string process_Index { get; set; }
        public string rule_Index { get; set; }
    }
}

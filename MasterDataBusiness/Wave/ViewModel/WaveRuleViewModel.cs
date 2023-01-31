using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{
    public class WaveRuleViewModel
    {
        public string waveRule_Index { get; set; }

        public string waveRule_Id { get; set; }

        public int? waveRule_Seq { get; set; }

        public string wave_Index { get; set; }

        public string wave_Id { get; set; }

        public string wave_Name { get; set; }

        public string rule_Index { get; set; }

        public string rule_Id { get; set; }

        public string rule_Name { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

        public string create_By { get; set; }

        public string create_Date { get; set; }

        public string update_By { get; set; }

        public string update_Date { get; set; }

        public string cancel_By { get; set; }

        public string cancel_Date { get; set; }
        public string process_Index { get; set; }
    }

    public class WaveRuleFilterViewModel
    {
        public string waveRule_Index { get; set; }
        public string waveRule_Id { get; set; }
        public int? waveRule_Seq { get; set; }
        public string wave_Index { get; set; }
        public string process_Index { get; set; }
    }

}

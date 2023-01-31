using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchRuleputawayViewModel : Pagination
    {
        public Guid ruleputaway_Index { get; set; }

        public string ruleputaway_Id { get; set; }

        public string ruleputaway_Name { get; set; }

        public int? ruleputaway_Seq { get; set; }

        public int? isActive { get; set; }
        public int? isDelete { get; set; }

        public string key { get; set; }

    }
    public class actionResultRuleputawayViewModel
    {
        public IList<SearchRuleputawayViewModel> itemsRuleputaway { get; set; }
        public Pagination pagination { get; set; }
    }
}

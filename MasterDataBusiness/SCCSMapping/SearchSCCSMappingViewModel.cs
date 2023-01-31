using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{
    public  class SearchSCCSMappingViewModel : Pagination
    {
        public string sCCS_Mapping_Index { get; set; }
        public string sCCS_Mapping_Id { get; set; }
        public string plant { get; set; }
        public string costCenter_Id { get; set; }
        public string costCenter_Description { get; set; }
        public string profitCenter { get; set; }
        public string profitCenter_Description { get; set; }
        public string customerGroup { get; set; }
        public string shipto_Id { get; set; }
        public string remark { get; set; }
        public string aCC_CAT { get; set; }
        public string itemCat { get; set; }
        public string gL_ACC { get; set; }
        public string iO_Code { get; set; }
        public DateTime? create_Date { get; set; }
        public int? version_Id { get; set; }
        public string shortText_PO_item { get; set; }
        public string shortText_Service_line { get; set; }
        public string key { get; set; }
        public int numBerOf { get; set; }
        public string createdate_date { get; set; }
        public string createdate_date_to { get; set; }
    }
    public class actionResultSCCSMappingViewModel
    {
        public IList<SearchSCCSMappingViewModel> itemsSCCSMapping { get; set; }
        public Pagination pagination { get; set; }
    }
}

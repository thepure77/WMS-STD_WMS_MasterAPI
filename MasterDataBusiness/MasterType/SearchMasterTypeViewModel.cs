using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class SearchMasterTypeViewModel : Pagination
    {
        public Guid masterType_Index { get; set; }

        public string masterType_Id { get; set; }

        public string masterType_Name { get; set; }

        public string masterType_SecondName { get; set; }

        public string ref_No1 { get; set; }

        public string ref_No2 { get; set; }

        public string ref_No3 { get; set; }

        public string ref_No4 { get; set; }

        public string ref_No5 { get; set; }

        public string remark { get; set; }

        public string udf_1 { get; set; }

        public string udf_2 { get; set; }

        public string udf_3 { get; set; }

        public string udf_4 { get; set; }

        public string udf_5 { get; set; }

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
    public class actionResultMasterTypeViewModel
    {
        public IList<SearchMasterTypeViewModel> itemsMasterType { get; set; }
        public Pagination pagination { get; set; }
    }
}

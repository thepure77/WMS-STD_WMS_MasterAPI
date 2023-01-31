using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchProductOwnerViewModel : Pagination
    {
        public Guid productOwner_Index { get; set; }
        public string productOwner_Id { get; set; }

        public Guid? product_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }

        public Guid? owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }

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

        public string key { get; set; }

        public string value2 { get; set; }

        public int rowNum { get; set; }

        public string create_date { get; set; }

        public string create_date_to { get; set; }

        public string changeSet { get; set; }
    }
    public class actionResultProductOwnerViewModel
    {
        public IList<SearchProductOwnerViewModel> itemsProductOwner { get; set; }
        public Pagination pagination { get; set; }
    }

    public class SearchProductOwnerInClauseViewModel : Pagination
    {
        public List<SearchProductOwnerInClauseModel> data { get; set; }
    }

    public class SearchProductOwnerInClauseModel
    {
        public Guid Product_Index { get; set; }

        public Guid Owner_Index { get; set; }
    }
}

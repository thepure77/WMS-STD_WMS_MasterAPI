using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchShippingTermsViewModel : Pagination
    {
        public Guid shippingTerms_Index { get; set; }

        public string shippingTerms_Id { get; set; }

        public string shippingTerms_Name { get; set; }

        public string shippingTerms_SecondName { get; set; }

        public string shippingTerms_ThirdName { get; set; }

        public string ref_No1 { get; set; }

        public string ref_No2 { get; set; }

        public string ref_No3 { get; set; }

        public string ref_No4 { get; set; }

        public string ref_No5 { get; set; }

        public string remark { get; set; }

        public string uDF_1 { get; set; }

        public string uDF_2 { get; set; }

        public string uDF_3 { get; set; }

        public string uDF_4 { get; set; }

        public string uDF_5 { get; set; }

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
    public class actionResultShippingTermsViewModel
    {
        public IList<SearchShippingTermsViewModel> itemsShippingTerms { get; set; }
        public Pagination pagination { get; set; }
    }
}

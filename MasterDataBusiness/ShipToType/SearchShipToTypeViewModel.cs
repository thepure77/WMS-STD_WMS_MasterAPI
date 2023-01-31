using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchShipToTypeViewModel : Pagination
    {
        public Guid shipToType_Index { get; set; }

        [StringLength(50)]
        public string shipToType_Id { get; set; }

        [StringLength(200)]
        public string shipToType_Name { get; set; }

        [StringLength(200)]
        public string shipToType_SecondName { get; set; }

        [StringLength(200)]
        public string ref_No1 { get; set; }

        [StringLength(200)]
        public string ref_No2 { get; set; }

        [StringLength(200)]
        public string ref_No3 { get; set; }

        [StringLength(200)]
        public string ref_No4 { get; set; }

        [StringLength(200)]
        public string ref_No5 { get; set; }

        [StringLength(200)]
        public string remark { get; set; }

        [StringLength(200)]
        public string udf_1 { get; set; }

        [StringLength(200)]
        public string udf_2 { get; set; }

        [StringLength(200)]
        public string udf_3 { get; set; }

        [StringLength(200)]
        public string udf_4 { get; set; }

        [StringLength(200)]
        public string udf_5 { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

        [StringLength(200)]
        public string create_By { get; set; }

        public DateTime? create_Date { get; set; }

        [StringLength(200)]
        public string update_By { get; set; }

        public DateTime? update_Date { get; set; }

        [StringLength(200)]
        public string cancel_By { get; set; }

        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }
    }
    public class actionResultShipToTypeViewModel
    {
        public IList<SearchShipToTypeViewModel> itemsShipToType { get; set; }
        public Pagination pagination { get; set; }
    }
}

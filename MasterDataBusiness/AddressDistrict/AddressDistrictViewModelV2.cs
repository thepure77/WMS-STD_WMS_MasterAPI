using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class AddressDistrictViewModelV2 : Pagination
    {
        public Guid district_Index { get; set; }

        [StringLength(50)]
        public string district_Id { get; set; }

        [StringLength(200)]
        public string district_Name { get; set; }
        
        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

        [StringLength(200)]
        public string create_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? create_Date { get; set; }

        [StringLength(200)]
        public string update_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? update_Date { get; set; }

        [StringLength(200)]
        public string cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? cancel_Date { get; set; }

        public string district_SecondName { get; set; }

        public Guid province_Index { get; set; }
        public string province_Id { get; set; }
        public string province_Name { get; set; }
        public Guid country_Index { get; set; }
        public string country_Id { get; set; }
        public string country_Name { get; set; }

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
    }
    public class actionResultDistrictViewModelV2
    {
        public IList<AddressDistrictViewModelV2> itemsDistrict { get; set; }
        public Pagination pagination { get; set; }
    }
}

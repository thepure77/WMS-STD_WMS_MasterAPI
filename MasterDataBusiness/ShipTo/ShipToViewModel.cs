using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class ShipToViewModel : Pagination
    {
        public Guid shipTo_Index { get; set; }
        
        [StringLength(50)]
        public string shipTo_Id { get; set; }
        
        [StringLength(200)]
        public string shipTo_Name { get; set; }

        [StringLength(200)]
        public string shipTo_SecondName { get; set; }

        [StringLength(200)]
        public string shipTo_Address { get; set; }

        public Guid? shipToType_Index { get; set; }

        [StringLength(50)]
        public string shipToType_Id { get; set; }

        [StringLength(200)]
        public string shipToType_Name { get; set; }

        public Guid? subDistrict_Index { get; set; }

        [StringLength(50)]
        public string subDistrict_Id { get; set; }

        [StringLength(200)]
        public string subDistrict_Name { get; set; }

        public Guid? district_Index { get; set; }

        [StringLength(50)]
        public string district_Id { get; set; }

        [StringLength(200)]
        public string district_Name { get; set; }

        public Guid? province_Index { get; set; }

        [StringLength(50)]
        public string province_Id { get; set; }

        [StringLength(200)]
        public string province_Name { get; set; }

        public Guid? country_Index { get; set; }

        [StringLength(50)]
        public string country_Id { get; set; }

        [StringLength(200)]
        public string country_Name { get; set; }

        public Guid? postcode_Index { get; set; }

        [StringLength(50)]
        public string postcode_Id { get; set; }

        [StringLength(200)]
        public string postcode_Name { get; set; }

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
        public int count { get; set; }

        [StringLength(200)]
        public string shipTo_TaxID { get; set; }

        [StringLength(200)]
        public string shipTo_Email { get; set; }

        [StringLength(200)]
        public string shipTo_Fax { get; set; }

        [StringLength(200)]
        public string shipTo_Tel { get; set; }

        [StringLength(200)]
        public string shipTo_Mobile { get; set; }

        [StringLength(200)]
        public string shipTo_Barcode { get; set; }

        [StringLength(200)]
        public string contact_Person { get; set; }

        [StringLength(200)]
        public string contact_Person2 { get; set; }

        [StringLength(200)]
        public string contact_Person3 { get; set; }

        [StringLength(200)]
        public string contact_Tel { get; set; }

        [StringLength(200)]
        public string contact_Tel2 { get; set; }

        [StringLength(200)]
        public string contact_Tel3 { get; set; }

        [StringLength(200)]
        public string contact_Email { get; set; }

        [StringLength(200)]
        public string contact_Email2 { get; set; }

        [StringLength(200)]
        public string contact_Email3 { get; set; }

        [StringLength(200)]
        public string create_By { get; set; }

        public DateTime? create_Date { get; set; }

        [StringLength(200)]
        public string update_By { get; set; }

        public DateTime? update_Date { get; set; }

        [StringLength(200)]
        public string cancel_By { get; set; }

        [StringLength(200)]
        public string Route_Index { get; set; }
        [StringLength(200)]
        public string Route_Id { get; set; }
        [StringLength(200)]
        public string Route_Name { get; set; }
        [StringLength(200)]
        public string SubRoute_Index { get; set; }
        [StringLength(200)]
        public string SubRoute_Id { get; set; }
        [StringLength(200)]
        public string SubRoute_Name { get; set; }

        public DateTime? cancel_Date { get; set; }

        public Guid? businessUnit_Index { get; set; }

        public string businessUnit_Id { get; set; }

        public string businessUnit_Name { get; set; }


    }
}

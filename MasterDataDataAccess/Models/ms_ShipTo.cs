using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MasterDataDataAccess.Models
{

    public partial class MS_ShipTo
    {

        [Key]
        public Guid ShipTo_Index { get; set; }

        [StringLength(50)]
        public string ShipTo_Id { get; set; }

        [StringLength(200)]
        public string ShipTo_Name { get; set; }

        [StringLength(200)]
        public string ShipTo_SecondName { get; set; }

        [StringLength(200)]
        public string ShipTo_Address { get; set; }

        public Guid ShipToType_Index { get; set; }

        [StringLength(50)]
        public string ShipToType_Id { get; set; }

        [StringLength(200)]
        public string ShipToType_Name { get; set; }

        [StringLength(200)]
        public string ShipTo_Mobile { get; set;}

        [StringLength(200)]
        public string ShipTo_TaxID { get; set; }

        [StringLength(200)]
        public string ShipTo_Email { get; set; }

        [StringLength(200)]
        public string ShipTo_Fax { get; set; }

        [StringLength(200)]
        public string ShipTo_Tel { get; set; }

        [StringLength(200)]
        public string ShipTo_Barcode { get; set; }

        [StringLength(200)]
        public string Contact_Person { get; set; }

        [StringLength(200)]
        public string Contact_Person2 { get; set; }

        [StringLength(200)]
        public string Contact_Person3 { get; set; }

        [StringLength(200)]
        public string Contact_Tel { get; set; }

        [StringLength(200)]
        public string Contact_Tel2 { get; set; }

        [StringLength(200)]
        public string Contact_Tel3 { get; set; }

        [StringLength(200)]
        public string Contact_Email { get; set; }

        [StringLength(200)]
        public string Contact_Email2 { get; set; }

        [StringLength(200)]
        public string Contact_Email3 { get; set; }

        public Guid? SubDistrict_Index { get; set; }

        [StringLength(50)]
        public string SubDistrict_Id { get; set; }

        [StringLength(200)]
        public string SubDistrict_Name { get; set; }

        public Guid? District_Index { get; set; }

        [StringLength(50)]
        public string District_Id { get; set; }

        [StringLength(200)]
        public string District_Name { get; set; }

        public Guid? Province_Index { get; set; }

        [StringLength(50)]
        public string Province_Id { get; set; }

        [StringLength(200)]
        public string Province_Name { get; set; }

        public Guid? Country_Index { get; set; }

        [StringLength(50)]
        public string Country_Id { get; set; }

        [StringLength(200)]
        public string Country_Name { get; set; }

        public Guid? Postcode_Index { get; set; }

        [StringLength(50)]
        public string Postcode_Id { get; set; }

        [StringLength(200)]
        public string Postcode_Name { get; set; }

        [StringLength(200)]
        public string Ref_No1 { get; set; }

        [StringLength(200)]
        public string Ref_No2 { get; set; }

        [StringLength(200)]
        public string Ref_No3 { get; set; }

        [StringLength(200)]
        public string Ref_No4 { get; set; }

        [StringLength(200)]
        public string Ref_No5 { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        [StringLength(200)]
        public string UDF_1 { get; set; }

        [StringLength(200)]
        public string UDF_2 { get; set; }

        [StringLength(200)]
        public string UDF_3 { get; set; }

        [StringLength(200)]
        public string UDF_4 { get; set; }

        [StringLength(200)]
        public string UDF_5 { get; set; }

        public Guid? BusinessUnit_Index { get; set; }

        public string BusinessUnit_Id { get; set; }

        public string BusinessUnit_Name { get; set; }


        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }

        [StringLength(200)]
        public string Create_By { get; set; }

  
        public DateTime? Create_Date { get; set; }

        [StringLength(200)]
        public string Update_By { get; set; }

        
        public DateTime? Update_Date { get; set; }

        [StringLength(200)]
        public string Cancel_By { get; set; }

        
        public DateTime? Cancel_Date { get; set; }

     
    }
}

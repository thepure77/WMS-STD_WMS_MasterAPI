using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MasterDataDataAccess.Models
{

    public partial class ms_Forwarder
    {
        [Key]
        [Column(Order = 0)]
        public Guid Forwarder_Index { get; set; }

       
        [Column(Order = 1)]
        [StringLength(50)]
        public string Forwarder_Id { get; set; }

       
        [Column(Order = 2)]
        [StringLength(200)]
        public string Forwarder_Name { get; set; }

        [StringLength(200)]
        public string Forwarder_SecondName { get; set; }

        [StringLength(200)]
        public string Forwarder_ThirdName { get; set; }

        [StringLength(200)]
        public string Forwarder_Address { get; set; }

       
        [Column(Order = 3)]
        public Guid ForwarderType_Index { get; set; }

       
        [Column(Order = 4)]
        public Guid District_Index { get; set; }

        [StringLength(50)]
        public string District_Id { get; set; }

        [StringLength(200)]
        public string District_Name { get; set; }

       
        [Column(Order = 5)]
        public Guid SubDistrict_Index { get; set; }

        [StringLength(50)]
        public string SubDistrict_Id { get; set; }

        [StringLength(200)]
        public string SubDistrict_Name { get; set; }

       
        [Column(Order = 6)]
        public Guid Province_Index { get; set; }

        [StringLength(50)]
        public string Province_Id { get; set; }

        [StringLength(200)]
        public string Province_Name { get; set; }

       
        [Column(Order = 7)]
        public Guid Country_Index { get; set; }

        [StringLength(50)]
        public string Country_Id { get; set; }

        [StringLength(200)]
        public string Country_Name { get; set; }

       
        [Column(Order = 8)]
        public Guid Postcode_Index { get; set; }

        [StringLength(50)]
        public string Postcode_Id { get; set; }

        [StringLength(200)]
        public string Postcode_Name { get; set; }

        [StringLength(200)]
        public string Forwarder_TaxID { get; set; }

        [StringLength(200)]
        public string Forwarder_Email { get; set; }

        [StringLength(200)]
        public string Forwarder_Fax { get; set; }

        [StringLength(200)]
        public string Forwarder_Tel { get; set; }

        [StringLength(200)]
        public string Forwarder_Mobile { get; set; }

        [StringLength(200)]
        public string Forwarder_Barcode { get; set; }

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

       
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsActive { get; set; }

        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsDelete { get; set; }

        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsSystem { get; set; }

        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status_Id { get; set; }

        [Column(Order = 13)]
        [StringLength(200)]
        public string Create_By { get; set; }

        [Column(Order = 14, TypeName = "smalldatetime")]
        public DateTime Create_Date { get; set; }

        [StringLength(200)]
        public string Update_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Update_Date { get; set; }

        [StringLength(200)]
        public string Cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Cancel_Date { get; set; }
    }
}

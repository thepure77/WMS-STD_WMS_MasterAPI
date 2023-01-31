using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{


    public partial class MS_AddressPostcode
    {
        [Key]
        public Guid Postcode_Index { get; set; }

        [StringLength(50)]
        public string Postcode_Id { get; set; }

        [StringLength(200)]
        public string Postcode_Name { get; set; }

        public Guid SubDistrict_Index { get; set; }

        public Guid District_Index { get; set; }

        public Guid Province_Index { get; set; }

        public Guid Country_Index { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }

        [StringLength(200)]
        public string Create_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Create_Date { get; set; }

        [StringLength(200)]
        public string Update_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Update_Date { get; set; }

        [StringLength(200)]
        public string Cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Cancel_Date { get; set; }


        public string Province_Id { get; set; }

        public string Province_Name { get; set; }

        public string Country_Id { get; set; }

        public string Country_Name { get; set; }

        public string SubDistrict_Id { get; set; }

        public string SubDistrict_Name { get; set; }

        public string District_Id { get; set; }

        public string District_Name { get; set; }

        public string Postcode_SecondName { get; set; }

        public string Ref_No1 { get; set; }

        public string Ref_No2 { get; set; }

        public string Ref_No3 { get; set; }

        public string Ref_No4 { get; set; }

        public string Ref_No5 { get; set; }

        public string Remark { get; set; }

        public string UDF_1 { get; set; }

        public string UDF_2 { get; set; }

        public string UDF_3 { get; set; }

        public string UDF_4 { get; set; }

        public string UDF_5 { get; set; }


    }
}

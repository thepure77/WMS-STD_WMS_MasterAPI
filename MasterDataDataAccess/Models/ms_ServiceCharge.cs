using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MasterDataDataAccess.Models
{

    public partial class ms_ServiceCharge
    {
        [Key]
        public Guid ServiceCharge_Index { get; set; }

        [StringLength(50)]
        public string ServiceCharge_Id { get; set; }

        [StringLength(200)]
        public string ServiceCharge_Name { get; set; }
        [StringLength(200)]
        public string ServiceCharge_SecondName { get; set; }

        public int? IsTransaction { get; set; }

        public int? IsSkuUse { get; set; }
        public Guid? DEFAULT_Process_Index { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatRate { get; set; }
        public int? VatType { get; set; }
        [StringLength(50)]
        public string VatCode { get; set; }
        public int? VatGroup { get; set; }
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


    }
}

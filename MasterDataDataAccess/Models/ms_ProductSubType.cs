using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{


    public partial class MS_ProductSubType
    {
        [Key]
        public Guid ProductSubType_Index { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductSubType_Id { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductSubType_Name { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductSubType_SecondName { get; set; }

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

        [Required]
        public Guid ProductType_Index { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductType_Id { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductType_Name { get; set; }

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

        public virtual MS_ProductType MS_ProductType { get; set; }
    }
}
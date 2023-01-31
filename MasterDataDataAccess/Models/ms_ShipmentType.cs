using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MasterDataDataAccess.Models
{

    public partial class ms_ShipmentType
    {
        [Key]
        [Column(Order = 0)]
        public Guid ShipmentType_Index { get; set; }
               
        [Column(Order = 1)]
        [StringLength(50)]
        public string ShipmentType_Id { get; set; }
               
        [Column(Order = 2)]
        [StringLength(200)]
        public string ShipmentType_Name { get; set; }

        [StringLength(200)]
        public string ShipmentType_SecondName { get; set; }

        [StringLength(200)]
        public string ShipmentType_ThirdName { get; set; }
               
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

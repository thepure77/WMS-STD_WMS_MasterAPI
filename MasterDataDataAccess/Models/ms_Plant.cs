using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MasterDataDataAccess.Models
{
    public partial class ms_Plant
    {
    [Key]
        public Guid Plant_Index { get; set; }
        public string Plant_Id { get; set; }
        public string Plant_Name { get; set; }
        public Guid PlantType_Index { get; set; }
        public string Ref_No1 { get; set; }
        public string Ref_No2 { get; set; }
        public string Ref_No3 { get; set; }
        public string Ref_No4 { get; set; }
        public string Ref_No5 { get; set; }
        public string UDF_1 { get; set; }
        public string UDF_2 { get; set; }
        public string UDF_3 { get; set; }
        public string UDF_4 { get; set; }
        public string UDF_5 { get; set; }
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }
        public int? IsSystem { get; set; }
        public int? Status_Id { get; set; }
        public string Create_By { get; set; }
        public DateTime? Create_Date { get; set; }
        public string Update_By { get; set; }
        public DateTime? Update_Date { get; set; }
        public string Cancel_By { get; set; }
        public DateTime? Cancel_Date { get; set; }
    }
}

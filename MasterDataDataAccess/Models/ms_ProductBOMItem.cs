using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class ms_ProductBOMItem
    {
        [Key]
        public Guid? ProductBOMItem_Index { get; set; }

        public Guid? ProductBOM_Index { get; set; }

        public string ProductBOM_No { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_SecondName { get; set; }
        public string Product_ThirdName { get; set; }
        public decimal? Qty { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal? Ratio { get; set; }

        public Guid? ProductConversion_Index { get; set; }
        public string ProductConversion_Id { get; set; }
        public string ProductConversion_Name { get; set; }
        public Guid? ItemStatus_Index { get; set; }
        public string ItemStatus_Id { get; set; }
        public string ItemStatus_Name { get; set; }
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
        public string Create_By { get; set; }
        public DateTime? Create_Date { get; set; }
        public string Update_By { get; set; }
        public DateTime? Update_Date { get; set; }
        public string Cancel_By { get; set; }
        public DateTime? Cancel_Date { get; set; }
        public Guid? ProductConversion_Weight_Index { get; set; }
        public Guid? ProductConversion_Volume_Index { get; set; }
    }
}

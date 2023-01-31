using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class View_ProductDetailV2
    {
        [Key]
        public Guid Product_Index { get; set; }
              
        public string Product_Id { get; set; }

        public string Product_Name { get; set; }

        public string Product_SecondName { get; set; }

        public string Product_ThirdName { get; set; }

        public string ProductImage_Path { get; set; }
        
        public Guid ProductConversion_Index { get; set; }
        
        public string ProductConversion_Id { get; set; }

        public string ProductConversion_Name { get; set; }
                       
        public Guid ProductType_Index { get; set; }
        
        public string ProductType_Id { get; set; }

        public string ProductType_Name { get; set; }

        public Guid ProductSubType_Index { get; set; }

        public string ProductSubType_Id { get; set; }
        
        public string ProductSubType_Name { get; set; }

        public Guid? ProductCategory_Index { get; set; }

        public string ProductCategory_Id { get; set; }

        public string ProductCategory_Name { get; set; }

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
        public string DocumentFile_Url { get; set; }

        public string SALE_ProductConversion_Name { get; set; }

        public string IN_ProductConversion_Name { get; set; }

        public int? IsExpDate { get; set; }

        public int? IsLot { get; set; }

        public int? ProductItemLife_Y { get; set; }

        public int? ProductItemLife_M { get; set; }

        public int? ProductItemLife_D { get; set; }
        public int? ProductShelfLife_D { get; set; }

        public int? IsMfgDate { get; set; }
        public int? IsSerial { get; set; }

        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }
        public decimal? Qty_Per_Tag { get; set; }
        public string Ti { get; set; }
        public string Hi { get; set; }
        public Guid? BusinessUnit_Index { get; set; }
        public string BusinessUnit_Id { get; set; }
        public string BusinessUnit_Name { get; set; }
        public Guid? FireClass_Index { get; set; }
        public string FireClass_Id { get; set; }
        public string FireClass_Name { get; set; }
        public Guid? MasterType_Index { get; set; }
        public string MasterType_Id { get; set; }
        public string MasterType_Name { get; set; }
        public Guid? MaterialClass_Index { get; set; }
        public string MaterialClass_Id { get; set; }
        public string MaterialClass_Name { get; set; }
        public Guid? MovingCondition_Index { get; set; }
        public string MovingCondition_Id { get; set; }
        public string MovingCondition_Name { get; set; }
        public Guid? ProductHierarchy5_Index { get; set; }
        public string ProductHierarchy5_Id { get; set; }
        public string ProductHierarchy5_Name { get; set; }
        public Guid? TempCondition_Index { get; set; }
        public string TempCondition_Id { get; set; }
        public string TempCondition_Name { get; set; }

        //new column
        public Guid? Type_Production_Index { get; set; }
        public string Type_Production_Id { get; set; }
        public string Type_Production_Name { get; set; }


        public int? IsSAP { get; set; }
        public int? ShelfLeft_alert { get; set; }
        public int? IsPending { get; set; }
    }
}

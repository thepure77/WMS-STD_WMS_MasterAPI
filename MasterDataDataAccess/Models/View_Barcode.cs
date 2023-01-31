using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_Barcode
    {
        [Key]

        public long Row_Index { get; set; }
        public Guid Product_Index { get; set; }

        public string Product_Id { get; set; }

        public string Product_Name { get; set; }

        public string Product_SecondName { get; set; }

        public string Product_ThirdName { get; set; }

        public string Ref_No2 { get; set; }
        public string Ref_No3 { get; set; }

        public decimal? ProductConversion_Width { get; set; }

        public decimal? ProductConversion_Length { get; set; }

        public decimal? ProductConversion_Height { get; set; }

        public string ProductConversionBarcode { get; set; }

        public Guid Volume_Index { get; set; }

        public string Volume_Id { get; set; }

        public string Volume_Name { get; set; }

        public decimal? Volume_Ratio { get; set; }

        public int? IsLot { get; set; }
        public int? IsExpDate { get; set; }
        public int? IsMfgDate { get; set; }
        public int? IsSerial { get; set; }
        public int? ProductItemLife_D { get; set; }
        public int? ProductItemLife_M { get; set; }
        public int? ProductItemLife_Y { get; set; }
        public decimal? ProductConversion_Ratio { get; set; }
        public Guid? ProductConversion_Index { get; set; }
        public string ProductConversion_Id { get; set; }
        public string ProductConversion_Name { get; set; }

        public string TIHI { get; set; }
        public decimal? qty_Per_Tag { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class View_CheckDimensionAllPrdouct
    {
        [Key]
        public Guid ProductConversion_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string BusinessUnit_Name { get; set; }
        public string TempCondition_Name { get; set; }
        public int? ShelfLeft_alert { get; set; }
        public string BU_UNIT { get; set; }
        public string SALE_UNIT { get; set; }
        public string IN_UNIT { get; set; }
        public string UNIT { get; set; }
        public decimal? Ratio { get; set; }
        public decimal? Weight { get; set; }
        public decimal? GrsWeight { get; set; }
        public decimal? W { get; set; }
        public decimal? L { get; set; }
        public decimal? H { get; set; }
        public string TI { get; set; }
        public string HI { get; set; }
        public string IsPiecePcik { get; set; }
        public string Ref_No1 { get; set; }
        public string Ref_No2 { get; set; }
        public string Create_By { get; set; }
        public DateTime? Create_Date { get; set; }
        public string Update_By { get; set; }
        public DateTime? Update_Date { get; set; }


    }
}

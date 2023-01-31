using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class ProductExportViewModel
    {
        public Guid productConversion_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string businessUnit_Name { get; set; }
        public string tempCondition_Name { get; set; }
        public string shelfLeft_alert { get; set; }
        public string bu_Unit { get; set; }
        public string sale_Unit { get; set; }
        public string in_Unit { get; set; }
        public string unit { get; set; }
        public decimal? ratio { get; set; }
        public decimal? weight { get; set; }
        public decimal? grsWeight { get; set; }
        public decimal? w { get; set; }
        public decimal? l { get; set; }
        public decimal? h { get; set; }
        public string ti { get; set; }
        public string hi { get; set; }
        public string isPiecePick { get; set; }
        public string ref_No1 { get; set; }
        public string ref_No2 { get; set; }
        public string key { get; set; }
        public string create_By { get; set; }
        public string create_Date { get; set; }
        public string update_By { get; set; }
        public string update_Date { get; set; }
    }

    public class actionResultExportViewModel
    {
        public IList<ProductExportViewModel> itemsProduct { get; set; }
    }
}

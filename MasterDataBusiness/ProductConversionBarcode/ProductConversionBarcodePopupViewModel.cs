using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class ProductConversionBarcodePopupViewModel : Pagination
    {

        public Guid productConversionBarcode_Index { get; set; }
        public string productConversionBarcode_Id { get; set; }
        public string productConversionBarcode { get; set; }

        public Guid? product_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }

        public Guid? productConversion_Index { get; set; }
        public string productConversion_Id { get; set; }
        public string productConversion_Name { get; set; }
        public decimal? productConversion_Ratio { get; set; }

        public Guid? owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }

        public string ref_No1 { get; set; }

        public string ref_No2 { get; set; }

        public string ref_No3 { get; set; }

        public string ref_No4 { get; set; }

        public string ref_No5 { get; set; }

        public string remark { get; set; }

        public string udf_1 { get; set; }

        public string udf_2 { get; set; }

        public string udf_3 { get; set; }

        public string udf_4 { get; set; }

        public string udf_5 { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

        public string create_By { get; set; }

        public DateTime? create_Date { get; set; }

        public string update_By { get; set; }

        public DateTime? update_Date { get; set; }

        public string cancel_By { get; set; }

        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }

        public string key2 { get; set; }

        public string key3 { get; set; }

        public List<ProductConversionBarcodeViewModel> listProductConversionBarcodeViewModel { get; set; }

        public bool? selected { get; set; }

        public class actionResultProductConversionBarcodePopupViewModel
        {
            public IList<ProductConversionBarcodePopupViewModel> itemsProductConversionBarcodePopup { get; set; }
            public Pagination pagination { get; set; }
        }
    }
}

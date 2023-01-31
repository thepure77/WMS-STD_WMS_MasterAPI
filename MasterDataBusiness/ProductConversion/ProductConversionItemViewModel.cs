using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MasterDataAPI.Controllers;

namespace MasterDataBusiness.ViewModels
{


    public  class ProductConversionItemViewModel : Pagination
    {
        public Guid productConversion_Index { get; set; }
        public string productConversion_Id { get; set; }
        public string productConversion_Name { get; set; }
        public string  productConversion_SecondName { get; set; }
        public decimal? productConversion_Ratio { get; set; }
        public decimal? productConversion_Weight { get; set; }
        public decimal? productConversion_Width { get; set; }
        public decimal? productConversion_Length { get; set; }
        public decimal? productConversion_Height { get; set; }
        public decimal? productConversion_VolumeRatio { get; set; }
        public decimal? productConversion_Volume { get; set; }

        public Guid? product_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

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

        public string create_By { get; set; }

        public DateTime? create_Date { get; set; }

        public string update_By { get; set; }

        public DateTime? update_Date { get; set; }

        public string cancel_By { get; set; }

        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }


        //[StringLength(200)]
        //public string ProductName { get; set; }

        //public Guid ProductIndex { get; set; }

        //public string ProductConversionId { get; set; }

        //[StringLength(200)]
        //public string ProductConversionName { get; set; }

        //public Guid ProductConversionIndex { get; set; }

        //public decimal? ProductConversionRatio { get; set; }

        //public decimal? ProductConversionWeight { get; set; }

        //public decimal? ProductConversionWidth { get; set; }

        //public decimal? ProductConversionLength { get; set; }

        //public decimal? ProductConversionHeight { get; set; }

        //public decimal? ProductConversionVolumeRatio { get; set; }

        //public decimal? ProductConversionVolume { get; set; }

        //public int? IsLot { get; set; }

        //public int? IsExpDate { get; set; }

        //public int? IsPack { get; set; }

        //public int? IsSerial { get; set; }

        //public int IsActive { get; set; }

        //public int IsDelete { get; set; }

        //public int IsSystem { get; set; }

        //public int StatusId { get; set; }


        //[StringLength(200)]
        //public string CreateBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime CreateDate { get; set; }

        //[StringLength(200)]
        //public string UpdateBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? UpdateDate { get; set; }

        //[StringLength(200)]
        //public string CancelBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? CancelDate { get; set; }
    }

    //public class actionResultProductConversionViewModel
    //{
    //    public IList<ProductConversionViewModel> items { get; set; }
    //    public Pagination pagination { get; set; }
    //}
}

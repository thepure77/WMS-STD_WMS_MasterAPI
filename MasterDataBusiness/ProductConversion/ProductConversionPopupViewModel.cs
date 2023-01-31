using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ProductConversion
{
    public class ProductConversionPopupViewModel
    {
        public Guid OwnerIndex { get; set; }

        public string ProductName { get; set; }

        public Guid ProductIndex { get; set; }

        public string ProductConversionId { get; set; }

       
        public string ProductConversionName { get; set; }

        public Guid ProductConversionIndex { get; set; }

        public decimal? ProductConversionRatio { get; set; }

        public decimal? ProductConversionWeight { get; set; }

        public decimal? ProductConversionWidth { get; set; }

        public decimal? ProductConversionLength { get; set; }

        public decimal? ProductConversionHeight { get; set; }

        public decimal? ProductConversionVolumeRatio { get; set; }

        public decimal? ProductConversionVolume { get; set; }

        public int? IsLot { get; set; }

        public int? IsExpDate { get; set; }

        public int? IsPack { get; set; }

        public int? IsSerial { get; set; }

        public int IsActive { get; set; }

        public int IsDelete { get; set; }

        public int IsSystem { get; set; }

        public int StatusId { get; set; }


        
        public string CreateBy { get; set; }

       
        public DateTime CreateDate { get; set; }

        
        public string UpdateBy { get; set; }

     
        public DateTime? UpdateDate { get; set; }


        public string CancelBy { get; set; }


        public DateTime? CancelDate { get; set; }
    }
}

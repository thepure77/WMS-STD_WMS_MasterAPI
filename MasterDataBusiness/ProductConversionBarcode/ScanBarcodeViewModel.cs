using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class ScanBarcodeViewModel
    {
        [StringLength(200)]
        public string ProductConversionBarcodeId { get; set; }

        [StringLength(200)]
        public string ProductConversionBarcode { get; set; }

        [StringLength(200)]
        public string ProductConversionName { get; set; }

        [StringLength(200)]
        public string OwnerName { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string ProductId{ get; set; }

        public Guid ProductIndex { get; set; }

        public Guid ProductConversionIndex { get; set; }

        public Guid ProductConversionBarcodeIndex { get; set; }

        public Guid OwnerIndex { get; set; }

        public int IsActive { get; set; }

        public int IsDelete { get; set; }

        public int IsSystem { get; set; }

        public int StatusId { get; set; }

        public decimal? ProductConversionRatio { get; set; }

        [StringLength(200)]
        public string CreateBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [StringLength(200)]
        public string UpdateBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        [StringLength(200)]
        public string CancelBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CancelDate { get; set; }

    }
}

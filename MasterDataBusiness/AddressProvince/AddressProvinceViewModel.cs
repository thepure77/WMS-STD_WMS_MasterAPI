using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MasterDataBusiness.ViewModels
{
    public class AddressProvinceViewModel : Pagination
    {
        public Guid ProvinceIndex { get; set; }

        [StringLength(50)]
        public string ProvinceId { get; set; }

        [StringLength(200)]
        public string ProvinceName { get; set; }

        [StringLength(200)]
        public string ProvinceNameEN { get; set; }

        public Guid CountryIndex { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? StatusId { get; set; }

        public int count { get; set; }

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
    public class actionResultProvinceViewModel
    {
        public IList<AddressProvinceViewModel> itemsProvince { get; set; }
        public Pagination pagination { get; set; }
    }
}

using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class AddressDistrictViewModel : Pagination
    {
        public Guid DistrictIndex { get; set; }

        
        [StringLength(50)]
        public string DistrictId { get; set; }

        
        [StringLength(200)]
        public string DistrictName { get; set; }

        [StringLength(200)]
        public string DistrictNameEN { get; set; }

        public Guid ProvinceIndex { get; set; }

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
    public class actionResultDistrictViewModel
    {
        public IList<AddressDistrictViewModel> itemsDistrict { get; set; }
        public Pagination pagination { get; set; }
    }
}

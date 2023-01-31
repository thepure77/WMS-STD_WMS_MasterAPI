using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MasterDataBusiness.ViewModels
{


    public  class AddressSubDistrictViewModel : Pagination
    {
        public Guid SubDistrictIndex { get; set; }

        
        [StringLength(50)]
        public string SubDistrictId { get; set; }

        
        [StringLength(200)]
        public string SubDistrictName { get; set; }

        public Guid DistrictIndex { get; set; }

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

    public class actionResultSubDistrictViewModel
    {
        [DataMember]
        public IList<AddressSubDistrictViewModel> itemsSubDistrict { get; set; }
        [DataMember]
        public Pagination pagination { get; set; }
    }
}

using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class AddressCountryViewModel : Pagination
    {
        public Guid CountryIndex { get; set; }

        
        [StringLength(50)]
        public string CountryId { get; set; }

        
        [StringLength(200)]
        public string CountryName { get; set; }

        [StringLength(200)]
        public string CountryNameEN { get; set; }

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
    public class actionResultCountryViewModel
    {
        public IList<AddressCountryViewModel> itemsCountry { get; set; }
        public Pagination pagination { get; set; }
    }
}

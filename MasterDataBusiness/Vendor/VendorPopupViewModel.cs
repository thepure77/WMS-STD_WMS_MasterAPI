using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class VendorPopupViewModel : Pagination
    {


        public Guid vendor_Index { get; set; }
        public string vendor_Id { get; set; }
        public string vendor_Name { get; set; }
        public string vendor_Address { get; set; }

        public Guid? vendorType_Index { get; set; }
        public string vendorType_Id { get; set; }
        public string vendorType_Name { get; set; }

        public Guid? country_Index { get; set; }
        public string country_Id { get; set; }
        public string country_Name { get; set; }

        public Guid? district_Index { get; set; }
        public string district_Id { get; set; }
        public string district_Name { get; set; }

        public Guid? subDistrict_Index { get; set; }
        public string subDistrict_Id { get; set; }
        public string subDistrict_Name { get; set; }

        public Guid? province_Index { get; set; }
        public string province_Id { get; set; }
        public string province_Name { get; set; }

        public Guid? postcode_Index { get; set; }
        public string postcode_Id { get; set; }
        public string postcode_Name { get; set; }

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

        public int count { get; set; }

        public List<VendorViewModel> listVendorViewModel { get; set; }

        public bool? selected { get; set; }
    }
    public class actionResultVendorPopupViewModel
    {
        public IList<VendorPopupViewModel> itemsVendorPopup { get; set; }
        public Pagination pagination { get; set; }
    }
}

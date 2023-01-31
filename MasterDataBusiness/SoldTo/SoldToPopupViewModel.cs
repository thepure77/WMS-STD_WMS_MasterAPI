using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SoldToPopupViewModel : Pagination
    {


        public Guid soldTo_Index { get; set; }
        public string soldTo_Id { get; set; }
        public string soldTo_Name { get; set; }
        public string soldTo_Address { get; set; }



        public Guid? soldToType_Index { get; set; }
        public string soldToType_Name { get; set; }



        public Guid? subDistrict_Index { get; set; }
        public string subDistrict_Name { get; set; }

        public Guid? district_Index { get; set; }
        public string district_Name { get; set; }



        public Guid? province_Index { get; set; }
        public string province_Name { get; set; }


        public Guid? country_Index { get; set; }
        public string country_Name { get; set; }

        public Guid? postcode_Index { get; set; }
        public string postcode_Id { get; set; }
        public string postcode_Name { get; set; }

        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }


        public string create_By { get; set; }


        public DateTime? create_Date { get; set; }


        public string update_By { get; set; }


        public DateTime? update_Date { get; set; }


        public string cancel_By { get; set; }


        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }

        public List<SoldToViewModel> listSoldToViewModel { get; set; }

        public bool? selected { get; set; }

        public int? checkSelected { get; set; }

    }
    public class actionResultSoldToPopupViewModel
    {
        public IList<SoldToPopupViewModel> itemsSoldToPopup { get; set; }
        public Pagination pagination { get; set; }
    }
}

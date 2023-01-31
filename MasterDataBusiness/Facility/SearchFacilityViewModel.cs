using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchFacilityViewModel : Pagination
    {
        public Guid facility_Index { get; set; }

        public string facility_Id { get; set; }

        public string facility_Name { get; set; }

        public Guid? facilityType_Index { get; set; }

        public string facilityType_Id { get; set; }

        public string facilityType_Name { get; set; }

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
    }
    public class actionResultFacilityViewModel
    {
        public IList<SearchFacilityViewModel> itemsFacility { get; set; }
        public Pagination pagination { get; set; }
    }
}

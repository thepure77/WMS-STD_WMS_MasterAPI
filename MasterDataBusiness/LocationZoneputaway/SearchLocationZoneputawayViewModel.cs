using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchLocationZoneputawayViewModel : Pagination
    {
        public Guid locationZoneputaway_Index { get; set; }
        public string locationZoneputaway_Id { get; set; }

        public Guid? zoneputaway_Index { get; set; }
        public string zoneputaway_Id { get; set; }
        public string zoneputaway_Name { get; set; }

        public Guid? location_Index { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }

        public int? isActive { get; set; }
        public int? isDelete { get; set; }
        public string key { get; set; }
        public string create_date_to { get; set; }
        public string create_date { get; set; }
    }
    public class actionResultLocationZoneputawayViewModel
    {
        public IList<SearchLocationZoneputawayViewModel> itemsLocationZoneputaway { get; set; }
        public Pagination pagination { get; set; }
    }
}

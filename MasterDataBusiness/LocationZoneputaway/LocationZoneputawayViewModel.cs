using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class LocationZoneputawayViewModel
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

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

        public string create_By { get; set; }

        public DateTime? create_Date { get; set; }

        public string update_By { get; set; }

        public DateTime? update_Date { get; set; }

        public string cancel_By { get; set; }

        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }
        public string key2 { get; set; }

    }
}

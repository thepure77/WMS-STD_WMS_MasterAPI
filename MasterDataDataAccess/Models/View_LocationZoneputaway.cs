using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_LocationZoneputaway
    {
        [Key]
        public Guid LocationZoneputaway_Index { get; set; }
        public string LocationZoneputaway_Id { get; set; }

        public Guid? Zoneputaway_Index { get; set; }
        public string Zoneputaway_Id { get; set; }
        public string Zoneputaway_Name { get; set; }

        public Guid? Location_Index { get; set; }
        public string Location_Id { get; set; }
        public string Location_Name { get; set; }
                
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }
        public string Create_By { get; set; }
        public DateTime? Create_Date { get; set; }
        public string Update_By { get; set; }
        public DateTime? Update_Date { get; set; }
        public string Cancel_By { get; set; }
        public DateTime? Cancel_Date { get; set; }

    }
}

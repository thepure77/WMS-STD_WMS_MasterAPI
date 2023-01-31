using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.RollCage
{
    public   class  RollCageViewModel
    {
    
        public Guid rollCage_Index { get; set; }


        public string rollCage_Id { get; set; }


        public string rollCage_Name { get; set; }

  
        public string rollCage_SecondName { get; set; }

        public int? rollCage_Status { get; set; }

        public Guid rollCageType_Index { get; set; }

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

        public string value1 { get; set; }


    }
}

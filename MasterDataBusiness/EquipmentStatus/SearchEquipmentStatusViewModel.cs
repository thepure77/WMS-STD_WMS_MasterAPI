using Business.Commons;
using System;

namespace MasterDataBusiness.ViewModels
{


    public class SearchEquipmentStatusViewModel : Result
    {
    
        public Guid equipment_Index { get; set; }
        
        public string equipment_Id { get; set; }
        
        public string equipment_Name { get; set; }

        public string equipment_status { get; set; }
        
        public string equipmentType_Name { get; set; }
        
        public string equipmentSubType_Name { get; set; }

        public Guid equipmentType_Index { get; set; }

        public Guid equipmentSubType_Index { get; set; }

        public string isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }
        
        public string create_By { get; set; }
        
        public DateTime create_Date { get; set; }
        
        public string update_By { get; set; }
        
        public DateTime? update_Date { get; set; }
        
        public string cancel_By { get; set; }
        
        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }
        

       
    }
}

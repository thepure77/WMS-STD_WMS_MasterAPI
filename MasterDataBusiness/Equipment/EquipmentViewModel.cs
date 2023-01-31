using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class EquipmentViewModel
    {

        public Guid equipment_Index { get; set; }
        
        [StringLength(50)]
        public string equipment_Id { get; set; }
        
        [StringLength(200)]
        public string equipment_Name { get; set; }

        [StringLength(200)]
        public string equipmentType_Name { get; set; }

        [StringLength(200)]
        public string equipmentSubType_Name { get; set; }

        public Guid equipmentType_Index { get; set; }

        public Guid equipmentSubType_Index { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

        
        [StringLength(200)]
        public string create_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime create_Date { get; set; }

        [StringLength(200)]
        public string update_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? update_Date { get; set; }

        [StringLength(200)]
        public string cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? cancel_Date { get; set; }
    }
}

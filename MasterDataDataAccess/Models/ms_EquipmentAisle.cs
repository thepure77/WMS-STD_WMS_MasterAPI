using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{
    public partial class ms_EquipmentAisle
    {
        [Key]
        public Guid EquipmentAisle_Index { get; set; }
        
        public string EquipmentAisle_Id { get; set; }

        public Guid Equipment_Index { get; set; }
        
        public string Equipment_Id { get; set; }
        
        public string Equipment_Name { get; set; }

        public Guid EquipmentType_Index { get; set; }

        public Guid EquipmentSubType_Index { get; set; }

        public Guid LocationAisle_Index { get; set; }
        
        public string LocationAisle_Id { get; set; }
        
        public string LocationAisle_Name { get; set; }

        public int IsActive { get; set; }

        public int IsDelete { get; set; }

        public int IsSystem { get; set; }

        public int Status_Id { get; set; }
        
        public string Create_By { get; set; }
        
        public DateTime Create_Date { get; set; }
        
        public string Update_By { get; set; }
        
        public DateTime? Update_Date { get; set; }
        
        public string Cancel_By { get; set; }
        
        public DateTime? Cancel_Date { get; set; }
    }
}

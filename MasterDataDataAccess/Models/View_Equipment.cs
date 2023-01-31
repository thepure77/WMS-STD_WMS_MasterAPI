using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_Equipment
    {
        [Key]

        public Guid Equipment_Index { get; set; }
        public string Equipment_Id { get; set; }
        public string Equipment_Name { get; set; }
        public int? Equipment_status { get; set; }

        public Guid EquipmentSubType_Index { get; set; }
        public string EquipmentSubType_Id { get; set; }
        public string EquipmentSubType_Name { get; set; }
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }

        public Guid EquipmentType_Index { get; set; }
        public string EquipmentType_Id { get; set; }
        public string EquipmentType_Name { get; set; }

        public string Create_By { get; set; }
        public DateTime? Create_Date { get; set; }
        public string Update_By { get; set; }
        public DateTime? Update_Date { get; set; }
    }
}

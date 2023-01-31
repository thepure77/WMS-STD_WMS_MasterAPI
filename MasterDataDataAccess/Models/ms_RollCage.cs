using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class ms_RollCage
    {
        [Key]
        public Guid RollCage_Index { get; set; }

        [StringLength(50)]
        public string RollCage_Id { get; set; }

        [StringLength(200)]
        public string RollCage_Name { get; set; }

        [StringLength(200)]
        public string RollCage_SecondName { get; set; }

        public int? RollCage_Status { get; set; }

        public Guid RollCageType_Index { get; set; }

        public Guid? Location_Index { get; set; }

        [StringLength(50)]
        public string Location_Id { get; set; }

        [StringLength(200)]
        public string Location_Name { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }

        [StringLength(200)]
        public string Create_By { get; set; }

 
        public DateTime? Create_Date { get; set; }

        [StringLength(200)]
        public string Update_By { get; set; }


        public DateTime? Update_Date { get; set; }

        [StringLength(200)]
        public string Cancel_By { get; set; }

   
        public DateTime? Cancel_Date { get; set; }


    }
}

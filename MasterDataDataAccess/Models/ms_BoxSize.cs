using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class ms_BoxSize
    {
        [Key]
        public Guid BoxSize_Index { get; set; }

        [StringLength(50)]
        public string BoxSize_Id { get; set; }

        [StringLength(200)]
        public string BoxSize_Name { get; set; }

        [StringLength(200)]
        public string BoxSize_SecondName { get; set; }

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class ms_SubRoute
    {
        [Key] 
        public Guid SubRoute_Index { get; set; }

        [Required]
        [StringLength(50)]
        public string SubRoute_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SubRoute_Name { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }

        [StringLength(200)]
        public string Create_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Create_Date { get; set; }

        [StringLength(200)]
        public string Update_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Update_Date { get; set; }

        [StringLength(200)]
        public string Cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Cancel_Date { get; set; }

        public int? SLA_Day { get; set; }
        public Guid? Route_Index { get; set; }
        public string Route_Id { get; set; }
        public string Route_Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class ms_Storage_Loc
    {
        [Key]
        public Guid StorageLoc_Index { get; set; }

        public string StorageLoc_Id { get; set; }

        public string StorageLoc_Name { get; set; }

        public Guid? Warehouse_Index { get; set; }

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{


    public partial class sy_UserLog
    {
        [Key]
        public Guid UserLog_Index { get; set; }
        public Guid User_Index { get; set; }
        public string User_Id { get; set; }
        public string User_Name { get; set; }
        public Guid? User_Key { get; set; }
        public int? Status_Id { get; set; }
        public string Create_By { get; set; }
        public DateTime? Create_Date { get; set; }
        public string Update_By { get; set; }
        public DateTime? Update_Date { get; set; }

     }
}

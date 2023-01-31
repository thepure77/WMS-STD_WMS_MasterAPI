using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_User
    {
        [Key]
        public Guid User_Index { get; set; }

        public string User_Id { get; set; }

        public string User_Name { get; set; }
        public string User_Password { get; set; }


        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public Guid UserGroup_Index { get; set; }

        public string UserGroup_Id { get; set; }
        public string UserGroup_Name { get; set; }

        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Position_Name { get; set; }
        public string Position_Code { get; set; }
        public string Station_Code { get; set; }
        public string Station_Name { get; set; }
        public string Branch_Code { get; set; }
        public string Branch_Name { get; set; }
        public string Status_Emp { get; set; }
        public string Emp_Code { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }

    }
}

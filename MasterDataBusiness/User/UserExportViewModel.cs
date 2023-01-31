using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class UserExportViewModel
    {
        public Guid user_Index { get; set; }

        public Guid userGroup_Index { get; set; }
        public string userGroup_Id { get; set; }
        public string userGroup_Name { get; set; }


        public string user_Id { get; set; }


        public string user_Name { get; set; }

        public string fullname { get; set; }
        
        public string user_Password { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }


        public string create_By { get; set; }


        public string create_Date { get; set; }


        public string update_By { get; set; }


        public string update_Date { get; set; }


        public string cancel_By { get; set; }


        public string cancel_Date { get; set; }

        public string activeStatus { get; set; }

        public string first_Name { get; set; }
        public string last_Name { get; set; }
        public string position_Name { get; set; }
        public string position_Code { get; set; }
        public string station_Code { get; set; }
        public string station_Name { get; set; }
        public string branch_Code { get; set; }
        public string branch_Name { get; set; }
        public string status_Emp { get; set; }
        public string emp_Code { get; set; }
        public bool checkupdate { get; set; }
        public int numBerOf { get; set; }
        public string key { get; set; }

    }

    public class UserActionResultExportViewModel
    {
        public IList<UserExportViewModel> itemsUser { get; set; }
    }
}

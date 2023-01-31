using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class UserGroupMenuExportViewModel
    {
        public Guid userGroupMenu_Index { get; set; }

        public Guid? userGroup_Index { get; set; }

        public Guid? menu_Index { get; set; }

        [StringLength(50)]
        public string userGroupMenu_Id { get; set; }

        [StringLength(200)]
        public string menu_Name { get; set; }

        [StringLength(200)]
        public string userGroup_Name { get; set; }

        public string userGroup_Id { get; set; }

        public string menu_Id { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }


        [StringLength(200)]
        public string create_By { get; set; }

        public string create_Date { get; set; }

        [StringLength(200)]
        public string update_By { get; set; }

        
        public string update_Date { get; set; }

        [StringLength(200)]
        public string cancel_By { get; set; }

        
        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }

        public int numBerOf { get; set; }

        public string createdate_date { get; set; }
        public string createdate_date_to { get; set; }
    }

    public class UserGroupMenuActionResultExportViewModel
    {
        public IList<UserGroupMenuExportViewModel> itemsUserGroupMenu { get; set; }
    }
}

using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class ConfigUserGroupMenuExportViewModel : Pagination
    {
        public Guid userGroupMenu_Index { get; set; }

        public string userGroupMenu_Id { get; set; }

        public string userGroupMenu_Name { get; set; }

        public Guid? userGroup_Index { get; set; }
        public bool isActive { get; set; }


        public Guid? menu_Index { get; set; }

        public Guid? sub_Menu_Index { get; set; }

        public Guid? menuType_Index { get; set; }

        public string menu_Id { get; set; }
        public string menuControl_Name { get; set; }
        public string menu_Name { get; set; }
        public string menu_SecondName { get; set; }
        public string menu_ThirdName { get; set; }
        
        public string create_By { get; set; }
        public string create_Date { get; set; }
        public string update_By { get; set; }
        public string update_Date { get; set; }
        public string cancel_By { get; set; }
        public string cancel_Date { get; set; }


        public string key { get; set; }

        public int numBerOf { get; set; }
    }

    public class ConfigUserGroupMenuActionResultExportViewModel 
    {
        public IList<ConfigUserGroupMenuExportViewModel> itemsConfigUserGroupMenu { get; set; }
        public IList<ConfigUserGroupMenuViewModel> items { get; set; }
        public string username { get; set; }
        public Pagination pagination { get; set; }
    }

    /*public class actionResultConfigUserGroupMenuViewModel
    {
        public IList<ConfigUserGroupMenuViewModel> items { get; set; }
        public string username { get; set; }
        public Pagination pagination { get; set; }
    }*/
}

using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public class SearchConfigUserGroupMenuViewModel : Pagination
    {
        public Guid? userGroup_Index { get; set; }

        public string userGroup_Id { get; set; }

        public string userGroup_Name { get; set; }

    }

    public class actionResultConfigUserGroupMenuViewModel
    {
        public IList<ConfigUserGroupMenuViewModel> items { get; set; }
        public string username { get; set; }
        public Pagination pagination { get; set; }
    }

    public class ConfigUserGroupMenuViewModel
    {
        public Guid userGroupMenu_Index { get; set; }

        public string userGroupMenu_Id { get; set; }

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

    }

    public class ConfigUserGroupViewModel
    {
        public Guid userGroup_Index { get; set; }

        public string userGroup_Id { get; set; }
        public string userGroup_Name { get; set; }
        public int? isActive { get; set; }
        public int? isDelete { get; set; }

    }
}

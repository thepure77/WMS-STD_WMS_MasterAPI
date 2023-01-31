using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{
    public  class SearchUserGroupMenuViewModel : Pagination
    {
        public Guid userGroupMenu_Index { get; set; }

        public Guid? userGroup_Index { get; set; }

        public string userGroup_Id { get; set; }

        public string menu_Id { get; set; }

        public Guid? menu_Index { get; set; }

        [StringLength(50)]
        public string userGroupMenu_Id { get; set; }

        [StringLength(200)]
        public string menu_Name { get; set; }

        [StringLength(200)]
        public string userGroup_Name { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }


        [StringLength(200)]
        public string create_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime create_Date { get; set; }

        [StringLength(200)]
        public string update_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? update_Date { get; set; }

        [StringLength(200)]
        public string cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }
        public string createdate_date { get; set; }
        public string createdate_date_to { get; set; }
    }
    public class actionResultUserGroupMenuViewModel
    {
        public IList<SearchUserGroupMenuViewModel> itemsUserGroupMenu { get; set; }
        public Pagination pagination { get; set; }
    }
}

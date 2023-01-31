using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchTaskGroupUserViewModel : Pagination
    {

        public Guid taskGroupUser_Index { get; set; }

        [StringLength(50)]
        public string taskGroupUser_Id { get; set; }

        public Guid taskGroup_Index { get; set; }

        public Guid user_Index { get; set; }

        [StringLength(200)]
        public string user_Name { get; set; }

        [StringLength(200)]
        public string taskGroup_Name { get; set; }

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



    }
    public class actionResultTaskGroupUserViewModel
    {
        public IList<SearchTaskGroupUserViewModel> itemsTaskGroupUser { get; set; }
        public Pagination pagination { get; set; }
    }
}

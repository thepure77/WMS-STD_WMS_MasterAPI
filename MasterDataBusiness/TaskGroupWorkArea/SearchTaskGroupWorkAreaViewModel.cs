using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchTaskGroupWorkAreaViewModel : Pagination
    {


        public Guid taskGroupWorkArea_Index { get; set; }

        public Guid taskGroup_Index { get; set; }

        public Guid workArea_Index { get; set; }

        [StringLength(50)]
        public string taskGroupWorkArea_Id { get; set; }

        [StringLength(200)]
        public string taskGroup_Name { get; set; }

        [StringLength(200)]
        public string workArea_Name { get; set; }

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
    public class actionResultTaskGroupWorkAreaViewModel
    {
        public IList<SearchTaskGroupWorkAreaViewModel> itemsTaskGroupWorkArea { get; set; }
        public Pagination pagination { get; set; }
    }
}

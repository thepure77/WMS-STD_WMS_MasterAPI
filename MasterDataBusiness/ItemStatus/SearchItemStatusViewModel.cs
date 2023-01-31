using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchItemStatusViewModel : Pagination
    {


        public Guid itemStatus_Index { get; set; }

        [StringLength(50)]
        public string itemStatus_Id { get; set; }

        [StringLength(200)]
        public string itemStatus_Name { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }


        [StringLength(200)]
        public string create_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public string create_Date { get; set; }

        [StringLength(200)]
        public string update_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public string update_Date { get; set; }

        [StringLength(200)]
        public string cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }

        public int numBerOf { get; set; }

        public string createdateitemstatus_date { get; set; }
        public string createdateitemstatus_date_to { get; set; }
    }
    public class actionResultItemStatusViewModel
    {
        public IList<SearchItemStatusViewModel> itemsItemStatus { get; set; }
        public Pagination pagination { get; set; }
    }
}

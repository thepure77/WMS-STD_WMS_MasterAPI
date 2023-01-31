using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchRoomViewModel : Pagination
    {
        public Guid room_Index { get; set; }

        public Guid? warehouse_Index { get; set; }

        public string warehouse_Name { get; set; }

        public string room_Id { get; set; }

        public string room_Name { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }


        public string create_By { get; set; }


        public DateTime? create_Date { get; set; }


        public string update_By { get; set; }


        public DateTime? update_Date { get; set; }


        public string cancel_By { get; set; }


        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }
        public string create_date_to { get; set; }
        public string create_date { get; set; }
    }
    public class actionResultRoomViewModel
    {
        public IList<SearchRoomViewModel> itemsRoom { get; set; }
        public Pagination pagination { get; set; }
    }
}

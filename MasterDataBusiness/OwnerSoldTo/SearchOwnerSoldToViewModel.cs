using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchOwnerSoldToViewModel : Pagination
    {
        public Guid ownerSoldTo_Index { get; set; }
        public string ownerSoldTo_Id { get; set; }

        public Guid? owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }

        public Guid? soldTo_Index { get; set; }
        public string soldTo_Id { get; set; }
        public string soldTo_Name { get; set; }

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
    }
    public class actionResultOwnerSoldToViewModel
    {
        public IList<SearchOwnerSoldToViewModel> itemsOwnerSoldTo { get; set; }
        public Pagination pagination { get; set; }
    }
}

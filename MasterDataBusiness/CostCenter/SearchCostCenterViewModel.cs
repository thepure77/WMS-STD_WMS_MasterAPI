using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchCostCenterViewModel : Pagination
    {


        public Guid costCenter_Index { get; set; }


        public string costCenter_Id { get; set; }


        public string costCenter_Name { get; set; }


        public string costCenter_Description { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }


        public string create_By { get; set; }


        public DateTime? create_Date { get; set; }


        public string update_By { get; set; }


        public DateTime? update_Date { get; set; }


        public string cancel_By { get; set; }


        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }


    }
    public class actionResultCostCenterViewModel
    {
        public IList<SearchCostCenterViewModel> itemsCostCenter { get; set; }
        public Pagination pagination { get; set; }
    }
}

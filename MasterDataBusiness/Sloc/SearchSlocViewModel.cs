using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{
    public class SearchSlocViewModel : Pagination
    {
        public Guid storageLoc_Index { get; set; }

        public string storageLoc_Id { get; set; }

        public string storageLoc_Name { get; set; }

        public string create_By { get; set; }

        public string create_Date { get; set; }

        public string cancel_By { get; set; }

        public string cancel_Date { get; set; }

        public string update_By { get; set; }

        public string update_Date { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }



        public string activeStatus { get; set; }

        public string key { get; set; }
    }
    public class actionResultSlocServiceViewModel
    {
        public IList<SearchSlocViewModel> itemsSloc { get; set; }
        public Pagination pagination { get; set; }
    }

    /*public class SearchSlocInClauseViewModel : Pagination
    {
        public List<Guid> List_ShipTo_Index { get; set; }

        public List<string> List_ShipTo_Id { get; set; }

        public List<string> List_ShipTo_Name { get; set; }

        public List<string> List_ShipToType_Id { get; set; }

        public List<string> List_ShipToType_Name { get; set; }

        public Guid? Process_Index { get; set; }
    }*/
}

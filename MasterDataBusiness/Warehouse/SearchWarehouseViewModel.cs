using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchWarehouseViewModel : Pagination
    {
        public Guid warehouse_Index { get; set; }

        public string warehouse_Id { get; set; }

        public string warehouse_Name { get; set; }

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
    public class actionResultWarehouseViewModel
    {
        public IList<SearchWarehouseViewModel> itemsWarehouse { get; set; }
        public Pagination pagination { get; set; }
    }

    public class SearchWareHouseInClauseViewModel : Pagination
    {
        public List<Guid> List_WareHouse_Index { get; set; }

        public List<string> List_WareHouse_Id { get; set; }
    }
}

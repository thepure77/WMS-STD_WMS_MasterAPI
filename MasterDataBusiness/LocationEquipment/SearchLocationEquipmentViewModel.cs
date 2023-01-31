using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchLocationEquipmentViewModel : Pagination
    {


        public Guid? locationEquipment_Index { get; set; }
        public string locationEquipment_Id { get; set; }
     
        public Guid? location_Index { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }




        public Guid? equipment_Index { get; set; }
        public string equipment_Name { get; set; }
        public string equipment_Id { get; set; }


        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }

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
    public class actionResultLocationEquipmentViewModel
    {
        public IList<SearchLocationEquipmentViewModel> itemsLocationEquipment { get; set; }
        public Pagination pagination { get; set; }
    }
}

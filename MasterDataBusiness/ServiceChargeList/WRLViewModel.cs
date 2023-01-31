using MasterDataAPI.Controllers;
using MasterDataDataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class WRLViewModel : Pagination
    {

        public WRLViewModel()
        {
            listServiceWRL = new List<WRLViewModel>();
        }

        public Guid? warehouse_Index { get; set; }
        public string warehouse_Id { get; set; }
        public string warehouse_Name { get; set; }

        public Guid? room_Index { get; set; }
        public string room_Id { get; set; }
        public string room_Name { get; set; }

        public Guid? location_Index { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }

        public Guid? locationAisle_Index { get; set; }
        public Guid? locationType_Index { get; set; }

        public List<WRLViewModel> listServiceWRL { get; set; }

}

    public class actionResultWRL
    {
        public IList<WRLViewModel> items { get; set; }
       // public IList<View_WRL> total { get; set; }

        public Pagination pagination { get; set; }
    }
}

using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{
    public class SlocViewModel
    {
        public Guid storageLoc_Index { get; set; }

        public string storageLoc_Id { get; set; }

        public string storageLoc_Name { get; set; } 

        public Guid? warehouse_Index { get; set; }

        public string create_By { get; set; }

        public string create_Date { get; set; }

        public string cancel_By { get; set; }

        public string cancel_Date { get; set; }

        public string update_By { get; set; }

        public string update_Date { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

    }
}

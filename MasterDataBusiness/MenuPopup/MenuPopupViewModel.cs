using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class MenuPopupViewModel
    {

        public Guid menu_Index { get; set; }

        public Guid menuType_Index { get; set; }

        public string menu_Id { get; set; }
        
        public string menuControl_Name { get; set; }

        public string menu_Name { get; set; }

        public string menu_SecondName { get; set; }

        public string menu_ThirdName { get; set; }

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
}

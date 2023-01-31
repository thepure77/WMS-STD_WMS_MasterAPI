using Business.Commons;
using System;
using System.Collections.Generic;

namespace MasterDataBusiness.ViewModels
{


    public  class EquipmentStatusViewModel : Result
    {

        public EquipmentStatusViewModel()
        {
            Crane_enable = new List<EquipmentStatusViewModel>();

            Crane_disable = new List<EquipmentStatusViewModel>();

        }

        
        public Guid? Equipment_Index { get; set; }

        public string Equipment_Id { get; set; }

        public string Equipment_Name { get; set; }

        public Guid? EquipmentType_Index { get; set; }

        public Guid? EquipmentSubType_Index { get; set; }

        public Guid? LocationAisle_Index { get; set; }

        public string LocationAisle_Id { get; set; }

        public string LocationAisle_Name { get; set; }

        public string Update_By { get; set; }

        public bool isUser { get; set; }

        public DateTime? Update_Date { get; set; }

        public List<EquipmentStatusViewModel> Crane_enable { get; set; }
        public List<EquipmentStatusViewModel> Crane_disable { get; set; }

    }
}

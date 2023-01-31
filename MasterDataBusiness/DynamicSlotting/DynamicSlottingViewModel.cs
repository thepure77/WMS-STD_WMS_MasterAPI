using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class DynamicSlottingViewModel
    {
        public Guid? DynamicSlotting_Index { get; set; }

        public string DynamicSlotting_Id { get; set; }

        public string DynamicSlotting_Remark { get; set; }

        public string Crane_Name { get; set; }

        public Guid? Product_Index { get; set; }

        public string Product_Id { get; set; }

        public string Product_Name { get; set; }

        public Guid? Zoneputaway_Index { get; set; }

        public string Zoneputaway_Id { get; set; }

        public string Zoneputaway_Name { get; set; }

        public TimeSpan? Trigger_Time { get; set; }

        public TimeSpan? Trigger_Time_End { get; set; }

        public DateTime? Trigger_Date { get; set; }

        public DateTime? Trigger_Date_End { get; set; }

        public bool IsMonday { get; set; }

        public bool IsTuesday { get; set; }

        public bool IsWednesday { get; set; }

        public bool IsThursday { get; set; }

        public bool IsFriday { get; set; }

        public bool IsSaturday { get; set; }

        public bool IsSunday { get; set; }

        public int? Plan_By_Product { get; set; }

        public int? Plan_By_Location { get; set; }

        public int? Plan_By_Status { get; set; }

        public bool IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }


        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }

        public DateTime? Last_Trigger_Date { get; set; }
    }
}

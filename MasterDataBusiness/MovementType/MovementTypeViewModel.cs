using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.CostCenter
{
    public class MovementTypeViewModel
    {

        public Guid movementType_Index { get; set; }

        public string movementType_Id { get; set; }

        public string movementType_Name { get; set; }

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
    }
}

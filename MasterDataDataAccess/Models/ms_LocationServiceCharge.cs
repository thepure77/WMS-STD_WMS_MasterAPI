using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class ms_LocationServiceCharge
    {
        [Key]
        public Guid LocationServiceCharge_Index { get; set; }


        public Guid ServiceCharge_Index { get; set; }


        public string ServiceCharge_Id { get; set; }


        public string ServiceCharge_Name { get; set; }


        public Guid Owner_Index { get; set; }


        public string Owner_Id { get; set; }


        public string Owner_Name { get; set; }

        public Guid Warehouse_Index { get; set; }


        public string Warehouse_Id { get; set; }


        public string Warehouse_Name { get; set; }

        public Guid Room_Index { get; set; }


        public string Room_Id { get; set; }


        public string Room_Name { get; set; }

        public Guid LocationType_Index { get; set; }


        public string LocationType_Id { get; set; }


        public string LocationType_Name { get; set; }


        public Guid Location_Index { get; set; }


        public string Location_Id { get; set; }


        public string Location_Name { get; set; }


        public string Ref_No1 { get; set; }


        public string Ref_No2 { get; set; }


        public string Ref_No3 { get; set; }


        public string Ref_No4 { get; set; }


        public string Ref_No5 { get; set; }


        public string Remark { get; set; }


        public string UDF_1 { get; set; }


        public string UDF_2 { get; set; }


        public string UDF_3 { get; set; }


        public string UDF_4 { get; set; }


        public string UDF_5 { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_ProductSubType
    {
        [Key]
        public Guid ProductSubType_Index { get; set; }
        public string ProductSubType_Id { get; set; }
        public string ProductSubType_Name { get; set; }
        public string ProductSubType_SecondName { get; set; }

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
        public Guid ProductType_Index { get; set; }
        public string ProductType_Id { get; set; }
        public string ProductType_Name { get; set; }
             
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }

    }
}

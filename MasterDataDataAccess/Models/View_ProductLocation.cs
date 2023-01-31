using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{


    public partial class View_ProductLocation
    {
        [Key]
        public Guid ProductLocation_Index { get; set; }
        public string ProductLocation_Id { get; set; }

        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }

        public Guid? Location_Index { get; set; }
        public string Location_Id { get; set; }
        public string Location_Name { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }


        public decimal Qty { get; set; }

        public decimal? Replenish_Qty { get; set; }
    }
}

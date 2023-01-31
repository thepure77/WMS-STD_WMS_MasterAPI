using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_ServiceChargeFix
    {
        [Key]
        public long? RowIndex { get; set; }

        public Guid? ServiceCharge_Index { get; set; }

        public string ServiceCharge_Id { get; set; }
        public string ServiceCharge_Name { get; set; }
        public Guid? Owner_Index { get; set; }

        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
        public decimal? rate { get; set; }

    }
}

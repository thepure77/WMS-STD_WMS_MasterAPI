using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_ServiceCharge
    {
        [Key]
        public Guid ServiceCharge_Index { get; set; }

        public Guid DEFAULT_Process_Index { get; set; }
            
        public Guid? Owner_Index { get; set; }

        public string ServiceCharge_Id { get; set; }

        public string ServiceCharge_Name { get; set; }

        public decimal? Minimumrate { get; set; }

        public decimal? Rate { get; set; }

         public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

       


    }
}

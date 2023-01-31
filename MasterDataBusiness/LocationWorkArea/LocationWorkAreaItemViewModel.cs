using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public class LocationWorkAreaItemViewModel
    {
        [StringLength(200)]
        public string location_Name { get; set; }

        [StringLength(200)]
        public string workArea_Name { get; set; }

        [StringLength(50)]
        public string locationWorkArea_Id { get; set; }

        public Guid? locationWorkArea_Index { get; set; }

        public Guid? location_Index { get; set; }

        public Guid? workArea_Index { get; set; }

        public int? isActive { get; set; }

        public int? isDelete{ get; set; }

        public int? status_Id { get; set; }

        public int? isSystem { get; set; }

        [StringLength(200)]
        public string create_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? create_Date { get; set; }

        [StringLength(200)]
        public string update_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? update_Date { get; set; }

        [StringLength(200)]
        public string cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? cancel_Date { get; set; }



    }
}

using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public class SoldToShipToViewModel : Pagination
    {
        public Guid soldToShipTo_Index { get; set; }

        [StringLength(50)]
        public string soldToShipTo_Id { get; set; }

        public Guid? soldTo_Index { get; set; }

        public Guid? shipTo_Index { get; set; }

        [StringLength(200)]
        public string shipTo_Name { get; set; }

        [StringLength(200)]
        public string soldTo_Name { get; set; }



        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

        public int count { get; set; }


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

        public List<SoldToShipToViewModel> listSoldToShipToViewModel { get; set; }
    }
}

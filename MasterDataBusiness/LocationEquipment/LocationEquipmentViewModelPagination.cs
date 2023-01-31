using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataBusiness.LocationEquipment
{
    public class LocationEquipmentViewModelPagination : Pagination
    {
        public Guid LocationEquipmentIndex { get; set; }

        public Guid? LocationIndex { get; set; }

        public Guid? EquipmentIndex { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? StatusId { get; set; }

        [StringLength(200)]
        public string LocationEquipmentId { get; set; }

        [StringLength(200)]
        public string LocationName { get; set; }

        [StringLength(200)]
        public string EquipmentName { get; set; }

        [StringLength(200)]
        public string CreateBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [StringLength(200)]
        public string UpdateBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        [StringLength(200)]
        public string CancelBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CancelDate { get; set; }

        public class actionResultViewModel
        {
            public IList<LocationEquipmentViewModelPagination> items { get; set; }
            public Pagination pagination { get; set; }
        }
    }
}

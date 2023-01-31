using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataBusiness.ZoneLocation
{
  
        public class ZoneLocationViewModelPagination : Pagination
        {
            [StringLength(200)]
            public string ZoneName { get; set; }

            [StringLength(200)]
            public string LocationName { get; set; }

            [StringLength(200)]
            public string ZoneLocationId { get; set; }

            public Guid ZoneLocationIndex { get; set; }

            public Guid? ZoneIndex { get; set; }

            public Guid? LocationIndex { get; set; }

            public int? IsActive { get; set; }

            public int? IsDelete { get; set; }

            public int? IsSystem { get; set; }

            public int? StatusId { get; set; }


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
                public IList<ZoneLocationViewModelPagination> items { get; set; }
                public Pagination pagination { get; set; }
            }
        }
    
}

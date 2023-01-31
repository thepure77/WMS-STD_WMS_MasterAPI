using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class MenuViewModel
    {

        public Guid MenuIndex { get; set; }
        public Guid? MenuTypeIndex { get; set; }
        
        [StringLength(50)]
        public string MenuId { get; set; }

        [StringLength(200)]
        public string MenuControlName { get; set; }
        
        [StringLength(200)]
        public string MenuName { get; set; }

        [StringLength(200)]
        public string MenuSecondName { get; set; }

        [StringLength(200)]
        public string ProductThirdName { get; set; }
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class OwnerSoldToViewModel
    {
        public Guid ownerSoldTo_Index { get; set; }
        public string ownerSoldTo_Id { get; set; }

        public Guid? owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }

        public Guid? soldTo_Index { get; set; }
        public string soldTo_Id { get; set; }
        public string soldTo_Name { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }
        
        public string create_By { get; set; }
        
        public DateTime? create_Date { get; set; }
        
        public string update_By { get; set; }
        
        public DateTime? update_Date { get; set; }
        
        public string cancel_By { get; set; }
        
        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }

        public string key2 { get; set; }

        public List<OwnerSoldToItemViewModel> listOwnerSoldToItemViewModel { get; set; }

        //[StringLength(200)]
        //public string OwnerName { get; set; }

        //[StringLength(200)]
        //public string SoldToName { get; set; }

        //public Guid OwnerSoldToIndex { get; set; }

        //[StringLength(50)]
        //public string OwnerSoldToId { get; set; }

        //public Guid? OwnerIndex { get; set; }

        //public Guid? SoldToIndex { get; set; }

        //public int? IsActive { get; set; }

        //public int? IsDelete { get; set; }

        //public int? IsSystem { get; set; }

        //public int? StatusId { get; set; }


        //[StringLength(200)]
        //public string CreateBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime CreateDate { get; set; }

        //[StringLength(200)]
        //public string UpdateBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? UpdateDate { get; set; }

        //[StringLength(200)]
        //public string CancelBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? CancelDate { get; set; }
    }
}

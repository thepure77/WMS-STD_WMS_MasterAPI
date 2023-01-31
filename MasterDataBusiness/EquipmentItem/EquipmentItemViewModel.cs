using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class EquipmentItemViewModel
    {
        public Guid? EquipmentItemIndex { get; set; }

        [StringLength(50)]
        public string EquipmentItemId { get; set; }

        [StringLength(200)]
        public string EquipmentItemName { get; set; }

        public Guid TagOutPickIndex { get; set; }

        public Guid EquipmentIndex { get; set; }

        [StringLength(50)]
        public string EquipmentId { get; set; }

        [StringLength(200)]
        public string EquipmentName { get; set; }

        [StringLength(200)]
        public string TagOutPickNo { get; set; }

        [StringLength(200)]
        public string TagOutNo { get; set; }
        
        public int? TagOutPickStatus { get; set; }

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

        [StringLength(200)]
        public string EquipmentItemDesc { get; set; }
    }
}

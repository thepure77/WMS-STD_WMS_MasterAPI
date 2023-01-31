using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class CartAssignViewModel
    {
        public Guid RefDocumentIndex { get; set; }

        [StringLength(50)]
        public string TagOutPickNo { get; set; }

        [StringLength(50)]
        public string TagOutNo { get; set; }

        public Guid? EquipmentIndex { get; set; }

        [StringLength(50)]
        public string EquipmentId { get; set; }

        [StringLength(200)]
        public string EquipmentName { get; set; }

        public Guid? EquipmentItemIndex { get; set; }

        [StringLength(50)]
        public string EquipmentItemId { get; set; }

        [StringLength(200)]
        public string EquipmentItemName { get; set; }

        [StringLength(200)]
        public string UpdateBy { get; set; }
    }
}

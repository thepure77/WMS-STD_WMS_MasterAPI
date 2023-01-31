using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.DockDoor
{
    public class DockDoorViewModel
    {
      
        public Guid DockDoorIndex { get; set; }
        
        public string DockDoorId { get; set; }

        
        public string DockDoorName { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? StatusId { get; set; }

        
        public string CreateBy { get; set; }

        
        public DateTime? CreateDate { get; set; }

        
        public string UpdateBy { get; set; }

        
        public DateTime? UpdateDate { get; set; }

        
        public string CancelBy { get; set; }

        
        public DateTime? CancelDate { get; set; }
    }
}

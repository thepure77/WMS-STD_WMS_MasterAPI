using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SoldToMenuViewModel
    {

        public Guid UserLogIndex { get; set; }

        public Guid UserIndex { get; set; }

        public Guid UserGroupIndex { get; set; }

        public Guid UserKey { get; set; }

        [StringLength(200)]
        public string UserGroupName { get; set; }

        [StringLength(50)]
        public string UserId { get; set; }
  
        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(200)]
        public string UserPassword { get; set; }


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

    public class soldtoMenuViewModel
    {
        public string menuName { get; set; }
        public int? isActive { get; set; }

        public int seq { get; set; }
    }

    public class actionResultSoldToMenuViewModel
    {
        public string userName { get; set; }
        public string userGroupName { get; set; }
        public List<userMenuViewModel> userMenuViewModel { get; set; }
    }
}

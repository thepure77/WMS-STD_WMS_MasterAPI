using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.T3PL
{
    public class T3PLViewModel
    {
        public Guid C3PLIndex { get; set; }

        public string C3PLId { get; set; }

        public string C3PLName { get; set; }

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

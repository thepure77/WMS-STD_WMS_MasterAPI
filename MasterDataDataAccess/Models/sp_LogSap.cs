using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MasterDataDataAccess.Models
{
    public partial class sp_LogSap
    {
    [Key]
        public long RowIndex { get; set; }
        public string WMS_ID { get; set; }
        public string DOC_LINK { get; set; }
        //public int? IsActive { get; set; }
        //public int? IsDelete { get; set; }
        public string Json { get; set; }
        public string MESSAGE { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string WMS_ID_STATUS { get; set; }
        public string Type { get; set; }
    }
}

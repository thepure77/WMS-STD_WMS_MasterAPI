using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_AutoSearchDocument
    {
        [Key]
        public string DOC_LINK { get; set; }

        public string WMS_ID { get; set; }



    }
}

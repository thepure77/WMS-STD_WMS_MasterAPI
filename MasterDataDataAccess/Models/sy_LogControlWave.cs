using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sy_LogControlWave
    {
        [Key]
        public Guid ControlWave_Index { get; set; }

        public Guid? GoodsIssue_Index { get; set; }

        public string GoodsIssue_No { get; set; }

        public string Action { get; set; }

        public string Action_By { get; set; }

        public DateTime? Action_Date { get; set; }

    }
}

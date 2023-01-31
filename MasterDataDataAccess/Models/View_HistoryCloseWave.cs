using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_HistoryCloseWave
    {
        [Key]
        public Guid GoodsIssue_Index { get; set; }

        public string GoodsIssue_No { get; set; }

        public Int32? WaveComplete_Status { get; set; }

        public DateTime? WaveComplete_Date { get; set; }

        public string Wave_Remark { get; set; }

        public Int32? WCS_status { get; set; }

        public Int32? Document_Status { get; set; }

    }
}

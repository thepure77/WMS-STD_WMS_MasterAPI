using Business.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.HistoryCloseWave
{
    public class HistoryCloseWaveViewModel : Result
    {
        public HistoryCloseWaveViewModel()
        {
            models = new List<HistoryCloseWaveViewModel>();
        }
        public Guid? goodsIssue_Index { get; set; }

        public string goodsIssue_No { get; set; }

        public Int32? waveComplete_Status { get; set; }

        public string waveComplete_Date { get; set; }

        public string waveComplete_Date_Start { get; set; }

        public string waveComplete_Date_End { get; set; }

        public string wave_Remark { get; set; }

        public Int32? wCS_status { get; set; }

        public Int32? document_Status { get; set; }

        public List<HistoryCloseWaveViewModel> models { get; set; }

    }
}

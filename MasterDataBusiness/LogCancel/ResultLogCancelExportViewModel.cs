using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class LogCancelExportViewModel
    {

        public string key { get; set; }
        public string date { get; set; }
        public string DOC_LINK_Due_Date { get; set; }
        public string DOC_LINK_Due_Date_To { get; set; }
        
        public long rownum { get; set; }

        public long rowIndex { get; set; }
        public string wms_ID { get; set; }
        public string doc_LINK { get; set; }
        public string json { get; set; }
        public string createDate { get; set; }
        public string wms_ID_STATUS { get; set; }
        public string type { get; set; }
        public string mat_Doc { get; set; }
        public string mESSAGE { get; set; }

        //date ใช้ตัวนี้เข้าเงื่อนไข
        public string create_Date { get; set; }
        public string create_Date_To { get; set; }

        public LogCancelExportViewModel()
        {
            status = new List<statusExportViewModelCancel>();
            status_ = new List<statusExportViewModelCancel>();

        }
        public List<statusExportViewModelCancel> status { get; set; }
        public List<statusExportViewModelCancel> status_ { get; set; }
    }

    public class statusExportViewModelCancel
    {
        public string value { get; set; }
        public string display { get; set; }
        public int seq { get; set; }
    }
    public class actionResultLogExportViewModelCancel
    {
        public IList<LogCancelExportViewModel> itemsLogCancel { get; set; }
    }

}

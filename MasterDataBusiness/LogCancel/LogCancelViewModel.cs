using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{

    public class LogCancelViewModel : Pagination
    {

        public LogCancelViewModel()
        {
            status = new List<statusViewModelCancel>();
            status_SAP = new List<statusViewModelCancel>();
        }


        public string key { get; set; }
        public string date { get; set; }
        public string DOC_LINK_Due_Date { get; set; }
        public string DOC_LINK_Due_Date_To { get; set; }

        public long rowIndex { get; set; }
        public string wms_ID { get; set; }
        public string doc_LINK { get; set; }
        public string json { get; set; }
        public string mESSAGE { get; set; }
        public string createDate { get; set; }
        public string wms_ID_STATUS { get; set; }
        public string type { get; set; }
        public string Mat_Doc { get; set; }
        public string MESSAGE { get; set; }

        //date ใช้ตัวนี้เข้าเงื่อนไข
        public string create_Date { get; set; }
        public string create_Date_To { get; set; }

        public List<statusViewModelCancel> status { get; set; }
        public List<statusViewModelCancel> status_SAP { get; set; }

    }
    public class statusViewModelCancel
    {
        public string value { get; set; }
        public string display { get; set; }
        public int seq { get; set; }
    }
    public class actionResultLogCancelViewModel
    {
        public IList<LogCancelViewModel> itemsLog { get; set; }
        public Pagination pagination { get; set; }
    }
}

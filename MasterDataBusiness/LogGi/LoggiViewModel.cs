using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{

    public class LogGiViewModel : Pagination
    {

        public LogGiViewModel()
        {
            statusGi = new List<statusGiViewModel>();
            status_SAPGi = new List<statusGiViewModel>();
        }


        public string key { get; set; }
        public string date { get; set; }
        public string PlanGoodsIssue_Due_Date { get; set; }
        public string PlanGoodsIssue_Due_Date_To { get; set; }


        public long rowIndexgi { get; set; }
        public string wms_IDgi { get; set; }
        public string doc_LINKgi { get; set; }
        //public int? IsActive { get; set; }
        //public int? IsDelete { get; set; }
        public string jsongi { get; set; }
        public string mESSAGEgi { get; set; }
        public string createdDategi { get; set; }
        public string wms_ID_STATUSgi { get; set; }
        public string typegi { get; set; }
        public string Mat_Docgi { get; set; }
        public string MESSAGEgi { get; set; }

        public List<statusGiViewModel> statusGi { get; set; }
        public List<statusGiViewModel> status_SAPGi { get; set; }

    }
    public class statusGiViewModel
    {
        public string value { get; set; }
        public string display { get; set; }
        public int seq { get; set; }
    }
    public class actionResultLogGiViewModel
    {
        public IList<LogGiViewModel> itemsLog { get; set; }
        public Pagination pagination { get; set; }
    }
}

using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{

    public class LogTfViewModel : Pagination
    {

        public LogTfViewModel()
        {
            statusTf = new List<statusTfViewModel>();
            status_SAPTf = new List<statusTfViewModel>();
        }


        public string key { get; set; }
        public string date { get; set; }
        public string Goodstransfer_Due_Date { get; set; }
        public string Goodstransfer_Due_Date_To { get; set; }


        public long rowIndextf { get; set; }
        public string wms_IDtf { get; set; }
        public string doc_LINKtf { get; set; }
        //public int? IsActive { get; set; }
        //public int? IsDelete { get; set; }
        public string jsontf { get; set; }
        public string mESSAGEtf { get; set; }
        public string createdDatetf { get; set; }
        public string wms_ID_STATUStf { get; set; }
        public string typetf { get; set; }
        public string Mat_Doctf { get; set; }
        public string MESSAGEtf { get; set; }

        public List<statusTfViewModel> statusTf { get; set; }
        public List<statusTfViewModel> status_SAPTf { get; set; }

    }
    public class statusTfViewModel
    {
        public string value { get; set; }
        public string display { get; set; }
        public int seq { get; set; }
    }
    public class actionResultLogTfViewModel
    {
        public IList<LogTfViewModel> itemsLog { get; set; }
        public Pagination pagination { get; set; }
    }
}

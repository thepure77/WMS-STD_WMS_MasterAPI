using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class LogTfExportViewModel
    {

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
        public string createdDatetf { get; set; }
        public string wms_ID_STATUStf { get; set; }
        public string typetf { get; set; }
        public string mat_Doctf { get; set; }
        public string mESSAGEtf { get; set; }

        public LogTfExportViewModel()
        {
            statustf = new List<statusExporttfViewModel>();
            status_tf = new List<statusExporttfViewModel>();

        }
        public List<statusExporttfViewModel> statustf { get; set; }
        public List<statusExporttfViewModel> status_tf { get; set; }
    }

    public class statusExporttfViewModel
    {
        public string value { get; set; }
        public string display { get; set; }
        public int seq { get; set; }
    }
    public class actionResultLogtfExportViewModel
    {
        public IList<LogTfExportViewModel> itemsLogtf { get; set; }
    }

}

using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    
    public class SearchLogTfViewModel : Pagination
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
        public string messagEtf { get; set; }
        //เงื่อนไขเเยกห้อง
        public string room_Name { get; set; }
        public SearchLogTfViewModel()
        {
            statustf = new List<searchstatustfViewModel>();
            status_tf = new List<searchstatustfViewModel>();

        }
        public List<searchstatustfViewModel> statustf { get; set; }
        public List<searchstatustfViewModel> status_tf { get; set; }

    }
    public class searchstatustfViewModel
    {
        public string value { get; set; }
        public string display { get; set; }
        public int seq { get; set; }
    }
    public class actionResultLogtfViewModel
    {
        public IList<SearchLogTfViewModel> itemsLogtf { get; set; }
        public Pagination pagination { get; set; }
    }
}

using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    
    public class SearchLogGrViewModel : Pagination
    {
        public string purchaseOrder_No { get; set; }
        public string planGoodsReceive_No { get; set; }
        public string goodsReceive_No { get; set; }
        public string date { get; set; }
        public string PlanGoodsReceive_Due_Date { get; set; }
        public string PlanGoodsReceive_Due_Date_To { get; set; }
        public string order_remark { get; set; }

        public long rowIndexgr { get; set; }
        public string wms_IDgr { get; set; }
        public string doc_LINKgr { get; set; }
        //public int? IsActive { get; set; }
        //public int? IsDelete { get; set; }
        public string jsongr { get; set; }
        public string createdDategr { get; set; }
        public string wms_ID_STATUSgr { get; set; }
        public string typegr { get; set; }
        public string mat_Docgr { get; set; }
        public string mESSAGEgr { get; set; }
        //เงื่อนไขเเยกห้อง
        public string room_Name { get; set; }
        public SearchLogGrViewModel()
        {
            statusgr = new List<searchstatustgrViewModel>();
            status_Gr = new List<searchstatustgrViewModel>();

        }


        public List<searchstatustgrViewModel> statusgr { get; set; }
        public List<searchstatustgrViewModel> status_Gr { get; set; }

    }


    public class searchstatustgrViewModel
    {
        public string value { get; set; }
        public string display { get; set; }
        public int seq { get; set; }
    }
    public class actionResultLogGrViewModel
    {
        public IList<SearchLogGrViewModel> itemsLoggr { get; set; }
        public Pagination pagination { get; set; }
    }
    


    //public class searchplantinclauseviewmodel : pagination
    //{

    //    public list<guid> list_shipto_index { get; set; }

    //    public list<string> list_shipto_id { get; set; }

    //    public list<string> list_shipto_name { get; set; }

    //    public guid? process_index { get; set; }
    //}
}

using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    
    public class SearchPlantViewModel : Pagination
    {
        public Guid plant_Index { get; set; }

        public string plant_Id { get; set; }

        public string plant_Name { get; set; }

        public Guid plantType_Index { get; set; }

        public string ref_No1 { get; set; }

        public string ref_No2 { get; set; }

        public string ref_No3 { get; set; }

        public string ref_No4 { get; set; }

        public string ref_No5 { get; set; }

        public string udf_1 { get; set; }

        public string udf_2 { get; set; }

        public string udf_3 { get; set; }

        public string udf_4 { get; set; }

        public string udf_5 { get; set; }

        public string key { get; set; }

        public string create_By { get; set; }
        public string create_Date { get; set; }
        public string update_By { get; set; }
        public string update_Date { get; set; }
        public string cancel_By { get; set; }
        public string cancel_Date { get; set; }
        public int? isActive { get; set; }
        public int? isDelete{ get; set; }
        public string activeStatus { get; set; }

    }
    public class actionResultPlantViewModel
    {
        public IList<SearchPlantViewModel> itemsPlant { get; set; }
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

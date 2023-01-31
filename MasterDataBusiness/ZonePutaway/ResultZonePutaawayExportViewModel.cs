using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ZonePutaway 
{
    public class ResultZonePutawayExportViewModel : Pagination
    {
        public string key { get; set; }
        public int numBerOf { get; set; }
        public string zonePutaway_Id { get; set; }
        public string zonePutaway_Name { get; set; }
        public string activeStatus { get; set; }
        public string create_By { get; set; }
        public string create_Date { get; set; }
        public string update_By { get; set; }
        public string update_Date { get; set; }
        public string cancel_By { get; set; }
        public string cancel_Date { get; set; }
        public string create_date_to { get; set; }
        public string create_date { get; set; }
    }
    public class ResultZonePutawayViewModel
    {
        public IList<ResultZonePutawayExportViewModel> itemsZonePutaway { get; set; }
        public Pagination pagination { get; set; }
    }
}

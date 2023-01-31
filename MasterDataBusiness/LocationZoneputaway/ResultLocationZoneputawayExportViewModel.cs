using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.LocationZoneputaway
{
    public class ResultLocationZoneputawayExportViewModel : Pagination
    {
        public string key { get; set; }
        public int numBerOf { get; set; }
        public string locationZoneputaway_Id { get; set; }
        public string zoneputaway_Name { get; set; }
        public string location_Name { get; set; }
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
    public class ResultLocationZoneputawayViewModel
    {
        public IList<ResultLocationZoneputawayExportViewModel> itemsLocationZoneputway { get; set; }
        public Pagination pagination { get; set; }
    }
}

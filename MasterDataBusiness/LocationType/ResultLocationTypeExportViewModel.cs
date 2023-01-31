using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.LocationType
{
    public class ResultLocationTypeExportViewModel : Pagination
    {
        public string key { get; set; }
        public int numBerOf { get; set; }
        public string locationType_Id { get; set; }
        public string locationType_Name { get; set; }
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
    public class ResultLocationTypeViewModel
    {
        public IList<ResultLocationTypeExportViewModel> itemsLocationType { get; set; }
        public Pagination pagination { get; set; }
    }
}

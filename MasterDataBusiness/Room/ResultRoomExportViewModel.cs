using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using MasterDataAPI.Controllers;


namespace MasterDataBusiness.Room
{
    public class ResultRoomExportViewModel : Pagination
    {
        public string key { get; set; }
        public int numBerOf { get; set; }
        public string warehouse_Name { get; set; }
        public string room_Id { get; set; }
        public string room_Name { get; set; }
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
    public class ResultRoomViewModel
    {
        public IList<ResultRoomExportViewModel> itemsRoom { get; set; }
        public Pagination pagination { get; set; }

    }
}

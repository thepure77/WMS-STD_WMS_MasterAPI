using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class CostCenterExportViewModel
    {
        public Guid costCenter_Index { get; set; }

        public string costCenter_Id { get; set; }

        public string costCenter_Name { get; set; }

        public string costCenter_Description { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public string create_By { get; set; }

        public string create_Date { get; set; }

        public string update_By { get; set; }

        public string update_Date { get; set; }

        public string cancel_By { get; set; }

        public string cancel_Date { get; set; }

        public string key { get; set; }
        public string activeStatus { get; set; }

        public int numBerOf { get; set; }
    }
    public class ResultCostCenterViewModel
    {
        public IList<CostCenterExportViewModel> itemsCostCenter { get; set; }

    }
}

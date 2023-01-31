﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class BusinessUnitExportViewModel
    {
        public Guid businessUnit_Index { get; set; }

        public string businessUnit_Id { get; set; }

        public string businessUnit_Name { get; set; }

        public string businessUnit_SecondName { get; set; }

        public string ref_No1 { get; set; }

        public string ref_No2 { get; set; }

        public string ref_No3 { get; set; }

        public string ref_No4 { get; set; }

        public string ref_No5 { get; set; }

        public string remark { get; set; }

        public string udf_1 { get; set; }

        public string udf_2 { get; set; }

        public string udf_3 { get; set; }

        public string udf_4 { get; set; }

        public string udf_5 { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

        public string create_By { get; set; }

        public string create_Date { get; set; }

        public string update_By { get; set; }

        public string update_Date { get; set; }

        public string cancel_By { get; set; }

        public string cancel_Date { get; set; }

        public string key { get; set; }

        public int numBerOf { get; set; }
        public string createdate_date { get; set; }
        public string createdate_date_to { get; set; }
    }

    public class BusinessUnitActionResultExportViewModel
    {
        public IList<BusinessUnitExportViewModel> itemsBusinessUnit { get; set; }
    }
}

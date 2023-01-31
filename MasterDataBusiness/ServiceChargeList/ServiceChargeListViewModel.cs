using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class ServiceChargeListViewModel
    {
        public Guid serviceChargeList_Index { get; set; }

        public Guid? serviceCharge_Index { get; set; }

        public string serviceCharge_Id { get; set; }

        public string serviceCharge_Name { get; set; }

        public string serviceCharge_SecondName { get; set; }

        public Guid? dEFAULT_Process_Index { get; set; }

        public Guid? owner_Index { get; set; }

        public string owner_Id { get; set; }

        public string owner_Name { get; set; }

        public string ref_No1 { get; set; }

        public string ref_No2 { get; set; }

        public string ref_No3 { get; set; }

        public string ref_No4 { get; set; }

        public string ref_No5 { get; set; }

        public string remark { get; set; }

        public string uDF_1 { get; set; }

        public string uDF_2 { get; set; }

        public string uDF_3 { get; set; }

        public string uDF_4 { get; set; }

        public string uDF_5 { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? atatus_Id { get; set; }

        public string create_By { get; set; }

        public DateTime? create_Date { get; set; }

        public string update_By { get; set; }

        public DateTime? update_Date { get; set; }

        public string cancel_By { get; set; }

        public DateTime? cancel_Date { get; set; }

        public List<ServiceChargeViewModel> listServiceCharge { get; set; }

        

    }
}

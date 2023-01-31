using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public class storageSaveViewModel
    {
        public storageSaveViewModel()
        {
            listServiceWRL = new List<locationServiceChargeViewModel>();

            listStorage = new List<storageChargeModel>();

        }
        public Guid? owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }

        public Guid? serviceCharge_Index { get; set; }

        public string serviceCharge_Id { get; set; }

        public string serviceCharge_Name { get; set; }

        public string userName { get; set; }

        public List<storageChargeModel> listStorage { get; set; }
        public List<locationServiceChargeViewModel> listServiceWRL { get; set; }
    }

    public class actionResultstorage
    {
        public string msg { get; set; }

        public List<storageChargeModel> listStorage { get; set; }
        public List<locationServiceChargeViewModel> listServiceWRL { get; set; }

    }
}

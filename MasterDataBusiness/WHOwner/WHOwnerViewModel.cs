using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class WHOwnerViewModel
    {
        public Guid whOwner_Index { get; set; }

        public string whOwner_Id { get; set; }


        public string whOwner_Name { get; set; }


        public string whOwner_Address { get; set; }

        public Guid? whOwnerType_Index { get; set; }
        public string whOwnerType_Name { get; set; }

        public Guid? country_Index { get; set; }
        public string country_Name { get; set; }

        public Guid? district_Index { get; set; }
        public string district_Name { get; set; }

        public Guid? subDistrict_Index { get; set; }
        public string subDistrict_Name { get; set; }

        public Guid? province_Index { get; set; }
        public string province_Name { get; set; }

        public Guid? postcode_Index { get; set; }
        public string postcode_Id { get; set; }
        public string postcode_Name { get; set; }

        public string owner_TaxID { get; set; }
        public string owner_Email { get; set; }
        public string owner_Fax { get; set; }
        public string owner_Tel { get; set; }
        public string owner_Mobile { get; set; }
        public string owner_Barcode { get; set; }
        public string contact_Person { get; set; }
        public string contact_Tel { get; set; }
        public string contact_Email { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }
        
        public string create_By { get; set; }
        
        public DateTime? create_Date { get; set; }
        
        public string update_By { get; set; }
        
        public DateTime? update_Date { get; set; }
        
        public string cancel_By { get; set; }
        
        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }
    }
}

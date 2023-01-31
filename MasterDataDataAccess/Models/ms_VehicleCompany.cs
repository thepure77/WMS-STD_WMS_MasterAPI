using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class ms_VehicleCompany
    {
        [Key]
        public Guid VehicleCompany_Index { get; set; }

        public string VehicleCompany_Id { get; set; }

        public string VehicleCompany_Name { get; set; }

        public string VehicleCompany_Address { get; set; }

        public Guid VehicleCompanyType_Index { get; set; }

        public Guid Country_Index { get; set; }

        public string Country_Id { get; set; }

        public string Country_Name { get; set; }

        public Guid Province_Index { get; set; }

        public string Province_Id { get; set; }

        public string Province_Name { get; set; }

        public Guid District_Index { get; set; }

        public string District_Id { get; set; }

        public string District_Name { get; set; }

        public Guid SubDistrict_Index { get; set; }

        public string SubDistrict_Id { get; set; }

        public string SubDistrict_Name { get; set; }

        public string Postcode { get; set; }

        public string VehicleCompany_TaxID { get; set; }

        public string VehicleCompany_Email { get; set; }

        public string VehicleCompany_Fax { get; set; }

        public string VehicleCompany_Tel { get; set; }

        public string VehicleCompany_Mobile { get; set; }

        public string VehicleCompany_Barcode { get; set; }

        public string Contact_Person { get; set; }

        public string Contact_Person2 { get; set; }

        public string Contact_Person3 { get; set; }

        public string Contact_Tel { get; set; }

        public string Contact_Tel2 { get; set; }

        public string Contact_Tel3 { get; set; }

        public string Contact_Email { get; set; }

        public string Contact_Email2 { get; set; }

        public string Contact_Email3 { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }
    }
}

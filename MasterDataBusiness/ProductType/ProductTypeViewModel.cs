using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class ProductTypeViewModel
    {
        public Guid productType_Index { get; set; }

        public string productType_Id { get; set; }

        public string productType_Name { get; set; }

        public string productType_SecondName { get; set; }

        public Guid productCategory_Index { get; set; }

        public string productCategory_Id { get; set; }

        public string productCategory_Name { get; set; }

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

        public DateTime? create_Date { get; set; }

        public string update_By { get; set; }

        public DateTime? update_Date { get; set; }

        public string cancel_By { get; set; }

        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }

        //public Guid ProductTypeIndex { get; set; }

        //[StringLength(50)]
        //public string ProductCategoryName { get; set; }

        //[StringLength(50)]
        //public string ProductTypeId { get; set; }

        //[StringLength(200)]
        //public string ProductTypeName { get; set; }

        //[StringLength(200)]
        //public string ProductCategory { get; set; }

        //public Guid ProductCategoryIndex { get; set; }       

        //public int IsActive { get; set; }

        //public int IsDelete { get; set; }

        //public int IsSystem { get; set; }

        //public int StatusId { get; set; }


        //[StringLength(200)]
        //public string CreateBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime CreateDate { get; set; }

        //[StringLength(200)]
        //public string UpdateBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? UpdateDate { get; set; }

        //[StringLength(200)]
        //public string CancelBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? CancelDate { get; set; }
    }
}

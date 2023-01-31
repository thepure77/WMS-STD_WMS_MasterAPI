using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MasterDataAPI.Controllers;

namespace MasterDataBusiness.ViewModels
{


    public class ProductOwnerViewModel
    {
        public Guid productOwner_Index { get; set; }
        public string productOwner_Id { get; set; }

        public Guid? product_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }

        public Guid? owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }

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

        public string key { get; set; }

        public List<ProductOwnerViewModel> listProductOwnerViewModel { get; set; }

        public string key2 { get; set; }
        //    [StringLength(200)]
        //    public string ProductOwnerName { get; set; }

        //    [StringLength(200)]
        //    public string ProductName { get; set; }

        //    public string ProductId { get; set; }

        //    [StringLength(200)]
        //    public string OwnerName { get; set; }

        //    public Guid ProductOwnerIndex { get; set; }

        //    [StringLength(50)]
        //    public string ProductOwnerId { get; set; }

        //    public Guid ProductIndex { get; set; }

        //    public Guid OwnerIndex { get; set; }

        //    public int IsActive { get; set; }

        //    public int IsDelete { get; set; }

        //    public int IsSystem { get; set; }

        //    public int StatusId { get; set; }


        //    [StringLength(200)]
        //    public string CreateBy { get; set; }

        //    [Column(TypeName = "smalldatetime")]
        //    public DateTime CreateDate { get; set; }

        //    [StringLength(200)]
        //    public string UpdateBy { get; set; }

        //    [Column(TypeName = "smalldatetime")]
        //    public DateTime? UpdateDate { get; set; }

        //    [StringLength(200)]
        //    public string CancelBy { get; set; }

        //    [Column(TypeName = "smalldatetime")]
        //    public DateTime? CancelDate { get; set; }

        //    public string Chk { get; set; }
        //}

        //public class actionResultProductOwnerViewModel
        //{
        //    public IList<ProductOwnerViewModel> items { get; set; }
        //    public Pagination pagination { get; set; }
        //}
    }
}

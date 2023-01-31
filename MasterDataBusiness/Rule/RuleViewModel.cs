using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{
    public class RuleViewModel
    {
        public Guid rule_Index { get; set; }

        public string rule_Id { get; set; }

        public string rule_Name { get; set; }

        public Guid process_Index { get; set; }

        public string process_Id { get; set; }

        public string process_Name { get; set; }

        public int? rule_Seq { get; set; }

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

    //public  class RuleViewModel
    //{
    //    public Guid RuleIndex { get; set; }

    //    public Guid Process_ndex { get; set; }

    //    [StringLength(50)]
    //    public string RuleId { get; set; }

    //    [StringLength(200)]
    //    public string RuleName { get; set; }

    //    public int? RuleSeq { get; set; }
    //    public int? IsActive { get; set; }

    //    public int? IsDelete { get; set; }

    //    public int? IsSystem { get; set; }

    //    public int? StatusId { get; set; }


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
    //}
}

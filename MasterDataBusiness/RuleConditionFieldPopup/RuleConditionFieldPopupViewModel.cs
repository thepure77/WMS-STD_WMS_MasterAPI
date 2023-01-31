using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{
    public class RuleConditionFieldPopupViewModel
    {
        public Guid? ruleConditionField_Index { get; set; }

        public string ruleConditionField_Name { get; set; }

        public Guid? process_Index { get; set; }

        public string process_Name { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? isSearch { get; set; }

        public int? isSort { get; set; }

        public int? isDestination { get; set; }

        public int? isSource { get; set; }

        public int? status_Id { get; set; }

        public string create_By { get; set; }

        public DateTime? create_Date { get; set; }

        public string update_By { get; set; }

        public DateTime? update_Date { get; set; }

        public string cancel_By { get; set; }

        public DateTime? cancel_Date { get; set; }


    }

    //public  class RuleConditionFieldViewModel
    //{
    //    public Guid RuleConditionFieldIndex { get; set; }

    //    public Guid ProcessIndex { get; set; }

    //    [StringLength(200)]
    //    public string RuleConditionFieldName { get; set; }

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

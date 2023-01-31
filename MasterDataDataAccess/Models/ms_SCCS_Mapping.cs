using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class ms_SCCS_Mapping
    {
      [Key]
      public Guid SCCS_Mapping_Index        { get; set; }
      public string SCCS_Mapping_Id           { get; set; }
      public string Plant                     { get; set; }
      public string CostCenter_Id             { get; set; }
      public string CostCenter_Description    { get; set; }
      public string ProfitCenter              { get; set; }
      public string ProfitCenter_Description  { get; set; }
      public string CustomerGroup             { get; set; }
      public string Shipto_Id                 { get; set; }
      public string Remark                    { get; set; }
      public string ACC_CAT                   { get; set; }
      public string ItemCat                   { get; set; }
      public string GL_ACC                    { get; set; }
      public string IO_Code                   { get; set; }
      public DateTime? Create_Date               { get; set; }
      public int? Version_Id                   { get; set; }
      public string ShortText_PO_item         { get; set; }
      public string ShortText_Service_line    { get; set; }

    }
}

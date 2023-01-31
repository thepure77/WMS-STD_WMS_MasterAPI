using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{

    public partial class SearchPrepare_ImportsViewModel : Pagination
    {
        [Key]
        public int RowIndex { get; set; }

        public Guid Import_Index { get; set; }

        public DateTime Import_Date { get; set; }

        [Required]
        [StringLength(4000)]
        public string Import_Type { get; set; }

        [StringLength(4000)]
        public string Import_Message { get; set; }

        [StringLength(4000)]
        public string Import_File_Name { get; set; }

        public int Import_Status { get; set; }

        [Required]
        [StringLength(4000)]
        public string Import_By { get; set; }

        [StringLength(4000)]
        public string Import_Case { get; set; }

        public bool? IsHeader { get; set; }

  
    }
    public class actionResultPrepare_ImportsViewModel
    {
        public IList<SearchProductViewModel> itemsPrepare_Imports { get; set; }
        public Pagination pagination { get; set; }
    }
}
